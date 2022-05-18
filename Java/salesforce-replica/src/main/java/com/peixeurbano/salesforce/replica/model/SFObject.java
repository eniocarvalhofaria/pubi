package com.peixeurbano.salesforce.replica.model;

import java.io.PrintStream;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import com.sforce.soap.enterprise.DescribeSObjectResult;
import com.sforce.soap.enterprise.Field;
import com.sforce.soap.enterprise.FieldType;

public class SFObject {
	private String objectName;

	private SFField[] fields;

	private SFTable[] tables;

	private boolean isActivateable;
	private boolean isCreateable;
	private boolean isCustom;
	private boolean isCustomSetting;
	private boolean isDeletable;
	private boolean isDeprecatedAndHidden;
	private boolean isFeedEnabled;
	private boolean isLayoutable;
	private boolean isMergeable;
	private boolean isQueryable;
	private boolean isReplicateable;
	private boolean isSearchable;
	private boolean isTriggerable;
	private boolean isUndeletable;
	private boolean isUpdateable;
	private int maxFieldsPerTable = 15;

	public SFObject(DescribeSObjectResult dsr) {
		objectName = dsr.getName();
		isActivateable = dsr.isActivateable();
		isCreateable = dsr.isCreateable();
		isCustom = dsr.isCustom();
		isCustomSetting = dsr.isCustomSetting();
		isDeletable = dsr.isDeletable();
		isDeprecatedAndHidden = dsr.isDeprecatedAndHidden();
		isFeedEnabled = dsr.isFeedEnabled();
		isLayoutable = dsr.isLayoutable();
		isMergeable = dsr.isMergeable();
		isQueryable = dsr.isQueryable();
		isReplicateable = dsr.isReplicateable();
		isSearchable = dsr.isSearchable();
		isTriggerable = dsr.isTriggerable();
		isUndeletable = dsr.isUndeletable();
		isActivateable = dsr.isUpdateable();

		fillFields(dsr.getFields());

		fillTables(dsr);
	}

	private Map<String, String> mergeSql = new HashMap<String, String>();

	public Map<String, String> getMergeSql(DBType dbType) {
		if (dbType.equals(DBType.RedShift)) {
			return mergeSqlRedshift;
		} else {
			return mergeSql;
		}

	}

	private Map<String, String> mergeSqlRedshift = new HashMap<String, String>();

	private List<SFTable> partitionTable(String schemaName, String tableName,
			SFField[] fields) {
		List<SFTable> partitionedTables = new ArrayList<SFTable>();

		List<SFField> pks = new ArrayList<SFField>();
		List<SFField> updatedTime = new ArrayList<SFField>();

		if (fields.length > maxFieldsPerTable) {

			StringBuilder sbSelect = new StringBuilder();
			StringBuilder sbSelectRedshift = new StringBuilder();
			sbSelect.append(String.format(
					"IF object_id(N'[%s].[%s]') IS NOT NULL \r\n", schemaName
							+ "stage", tableName));
			sbSelect.append("begin\r\n");
			sbSelect.append(String.format("drop table [%s].[%s]\r\n",
					schemaName + "stage", tableName));
			sbSelect.append("end\r\n");

			sbSelectRedshift.append(String.format(
					"drop table if exists salesforce.%sstage%s ;\r\n", schemaName,
					tableName));
			sbSelectRedshift
					.append(String.format("CREATE TABLE salesforce.%sstage%s\n as \r\n",
							schemaName, tableName));

			for (SFField f : fields) {
				if (f.isPK()) {
					pks.add(f);
				}
			}
			for (SFField f : fields) {
				if (f.getName().equals("CreatedDate")
						|| f.getName().equals("LastModifiedDate")) {
					updatedTime.add(f);
				}
			}
			int fieldsAdded = 0;
			int currentTableIndex = 0;
			List<SFField> fieldsCurrentTable = new ArrayList<SFField>();
			List<String> fAdded = new ArrayList<String>();
			String currentTableName = tableName + "_"
					+ String.format("%02d", currentTableIndex);
			String selectpk = "select ";
			String join = "on ";
			String from;
			for (SFField pk : pks) {

				selectpk += ",\t t0." + pk.getName() + "\r\n";
				join += "\r\nand t0." + pk.getName() + " = t1." + pk.getName();
				fieldsCurrentTable.add(pk);
				fAdded.add(pk.getName().replace("__c", "").toUpperCase());
			}
			for (SFField up : updatedTime) {
				fieldsCurrentTable.add(up);
				selectpk += ",\t t0." + up.getName() + "\r\n";
				fAdded.add(up.getName().replace("__c", "").toUpperCase());
			}
			sbSelect.append(selectpk.replace("select ,", "select\r\n"));
			sbSelectRedshift.append(selectpk.replace("select ,", "select\r\n"));
			join = join.replace("on \r\nand", "on");
			from = "from " + schemaName + "stage." + currentTableName + " t"
					+ new Integer(currentTableIndex).toString() + "\r\n";

			for (SFField f : fields) {
				fieldsAdded++;

				if (!f.isPK()
						&& !f.getName().equals("CreatedDate")
						&& !f.getName().equals("LastModifiedDate")
						&& !fAdded.contains(f.getName().replace("__c", "")
								.toUpperCase())) {
					if (fieldsAdded > maxFieldsPerTable - pks.size()) {
						partitionedTables.add(new SFTable(this, schemaName,
								currentTableName, fieldsCurrentTable
										.toArray(new SFField[0])));

						currentTableIndex++;
						currentTableName = tableName + "_"
								+ String.format("%02d", currentTableIndex);

						from += "inner join "
								+ schemaName
								+ "stage."
								+ currentTableName
								+ " t"
								+ new Integer(currentTableIndex).toString()
								+ "\r\n"
								+ join.replace(
										"t1",
										"t"
												+ new Integer(currentTableIndex)
														.toString()) + "\r\n";

						fieldsCurrentTable.clear();

						for (SFField pk : pks) {
							fieldsCurrentTable.add(pk);
						}
						for (SFField up : updatedTime) {
							fieldsCurrentTable.add(up);
						}
						fieldsAdded = 0;
					}
					sbSelect.append(",\t" + " t"
							+ new Integer(currentTableIndex).toString() + "."
							+ f.getName() + "\r\n");
					sbSelectRedshift.append(",\t" + " t"
							+ new Integer(currentTableIndex).toString() + "."
							+ f.getName() + "\r\n");
					fieldsCurrentTable.add(f);
					fAdded.add(f.getName().replace("__c", "").toUpperCase());
				}
			}
			partitionedTables.add(new SFTable(this, schemaName,
					currentTableName, fieldsCurrentTable
							.toArray(new SFField[0])));
			sbSelect.append("into " + schemaName + "stage." + tableName
					+ "\r\n");
			sbSelect.append(from);
			sbSelectRedshift.append(from.replace(schemaName + "stage.", "salesforce." + schemaName + "stage"));
			mergeSql.put(schemaName + "." + tableName, sbSelect.toString()
					.replaceAll("__c", ""));
			
			mergeSqlRedshift.put(schemaName + "." + tableName, sbSelectRedshift.toString()
					.replaceAll("__c", ""));
			

		} else {
			partitionedTables.add(new SFTable(this, schemaName, tableName,
					fields));
		}

		// return partitionedTables.toArray(new SFTable[0]);
		return partitionedTables;

	}

	private void fillTables(DescribeSObjectResult dsr) {
		List<SFTable> internalTables = new ArrayList<SFTable>();

		boolean hasCustomFields = false;
		if (dsr.isCustom() || objectName.endsWith("__History")
				|| objectName.endsWith("__Share")) {

			// internalTables.add(new SFTable(this, "ct", objectName, fields));
			internalTables.addAll(partitionTable("ct", objectName, fields));
		} else {
			List<SFField> customFields = new ArrayList<SFField>();
			List<SFField> staticFields = new ArrayList<SFField>();

			for (SFField field : fields) {
				if (field.isPK() || field.getName().equals("CreatedDate")
						|| field.getName().equals("LastModifiedDate")) {
					staticFields.add(field);
					customFields.add(field);

				} else {
					if (field.isCustom()) {
						customFields.add(field);
						hasCustomFields = true;
					} else {
						staticFields.add(field);
					}

				}
			}

			internalTables.add(new SFTable(this, "sf", objectName, staticFields
					.toArray(new SFField[0])));

			if (hasCustomFields) {
				internalTables.add(new SFTable(this, "cf", objectName,
						customFields.toArray(new SFField[0])));
			}
		}

		tables = internalTables.toArray(new SFTable[0]);
	}

	private void fillFields(Field[] describeFields) {
		List<SFField> internalFields = new ArrayList<SFField>();

		for (Field field : describeFields) {
			FieldType ft = field.getType();

			if (ft.equals(FieldType.base64)) {
				continue;
			}

			internalFields.add(new SFField(this, field));
		}

		fields = internalFields.toArray(new SFField[0]);
	}

	protected void updateReferences(SFSchema schema,
			List<SFRef> internalReferences) {
		for (SFTable t : tables) {
			t.updateReferences(schema, internalReferences);
		}
	}

	public String getObjectName() {
		return objectName;
	}

	public SFField[] getFields() {
		return fields;
	}

	public SFTable getFirstTable() {
		if (tables.length > 0) {
			return tables[0];
		}

		return null;
	}

	public SFTable[] getTables() {
		return tables;
	}

	public void writeCreateSQL(PrintStream out, DBType dbType) throws Exception {
		for (SFTable t : tables) {
			t.writeCreateSQL(out, dbType);
		}
	}

	public boolean isActivateable() {
		return isActivateable;
	}

	public boolean isCreateable() {
		return isCreateable;
	}

	public boolean isCustom() {
		return isCustom;
	}

	public boolean isCustomSetting() {
		return isCustomSetting;
	}

	public boolean isDeletable() {
		return isDeletable;
	}

	public boolean isDeprecatedAndHidden() {
		return isDeprecatedAndHidden;
	}

	public boolean isFeedEnabled() {
		return isFeedEnabled;
	}

	public boolean isLayoutable() {
		return isLayoutable;
	}

	public boolean isMergeable() {
		return isMergeable;
	}

	public boolean isQueryable() {
		return isQueryable;
	}

	public boolean isReplicateable() {
		return isReplicateable;
	}

	public boolean isSearchable() {
		return isSearchable;
	}

	public boolean isTriggerable() {
		return isTriggerable;
	}

	public boolean isUndeletable() {
		return isUndeletable;
	}

	public boolean isUpdateable() {
		return isUpdateable;
	}

	public Calendar getLastUpdate() {
		return lastUpdate;
	}

	public void setLastUpdate(Calendar lastUpdate) {
		this.lastUpdate = lastUpdate;
	}

	private Calendar lastUpdate;
}
