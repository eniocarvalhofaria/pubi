package com.peixeurbano.salesforce.replica.model;

import java.io.PrintStream;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import com.sforce.soap.enterprise.Field;
import com.sforce.soap.enterprise.FieldType;
import com.sforce.soap.enterprise.PicklistEntry;

public class SFColumn {
	@SuppressWarnings("serial")
	private static Map<FieldType, String> TYPE_SQL_MAP = new HashMap<FieldType, String>() {
		{
			put(FieldType._boolean, "BIT");
			put(FieldType._double, "FLOAT");
			put(FieldType._int, "INT");
			put(FieldType.anyType, "VARCHAR(8000)");
			put(FieldType.combobox, "VARCHAR(%d)");
			put(FieldType.currency, "MONEY");
			put(FieldType.date, "DATE");
			put(FieldType.datetime, "DATETIME");
			put(FieldType.email, "VARCHAR(%d)");
			put(FieldType.encryptedstring, "VARCHAR(%d)");
			put(FieldType.id, "VARCHAR(18)");
			put(FieldType.multipicklist, "VARCHAR(%d)");
			put(FieldType.percent, "FLOAT");
			put(FieldType.phone, "VARCHAR(50)");
			put(FieldType.picklist, "VARCHAR(%d)");
			put(FieldType.reference, "VARCHAR(18)");
			put(FieldType.string, "VARCHAR(%d)");
			put(FieldType.textarea, "TEXT");
			put(FieldType.time, "TIME");
			put(FieldType.url, "VARCHAR(255)");
		}
	};
	private static Map<FieldType, String> TYPE_REDSHIFT_MAP = new HashMap<FieldType, String>() {
		{
			put(FieldType._boolean, "BOOL");
			put(FieldType._double, "FLOAT");
			put(FieldType._int, "INT");
			put(FieldType.anyType, "NVARCHAR(MAX)");
			put(FieldType.combobox, "NVARCHAR(MAX)");
			put(FieldType.currency, "NUMERIC(18,2)");
			put(FieldType.date, "DATE");
			put(FieldType.datetime, "TIMESTAMP");
			put(FieldType.email, "NVARCHAR(%d)");
			put(FieldType.encryptedstring, "NVARCHAR(MAX)");
			put(FieldType.id, "NVARCHAR(18)");
			put(FieldType.multipicklist, "NVARCHAR(MAX)");
			put(FieldType.percent, "FLOAT");
			put(FieldType.phone, "NVARCHAR(50)");
			put(FieldType.picklist, "NVARCHAR(MAX)");
			put(FieldType.reference, "NVARCHAR(18)");
			put(FieldType.string, "NVARCHAR(MAX)");
			put(FieldType.textarea, "NVARCHAR(MAX)");
			put(FieldType.time, "TIMESTAMP");
			put(FieldType.url, "NVARCHAR(255)");
		}
	};
	private String columnName;

	protected Field field;
	protected SFTable table;

	protected SFColumn(SFTable parent, SFField field) {
		this.field = field.field;
		this.table = parent;

		columnName = convertFieldToColumnName(field);
	}

	public static String convertFieldToColumnName(SFField field) {
		return field.field.getName().replaceAll("__c$", "");
	}

	public SFTable getParent() {
		return table;
	}

	public String getColumnName() {
		return columnName;
	}

	protected void updateReferences(SFSchema schema,
			List<SFRef> internalReferences) {
		FieldType ft = field.getType();

		if (ft.equals(FieldType.reference)) {
			for (String refTo : field.getReferenceTo()) {
				SFObject o = schema.lookForObject(refTo);

				if (o == null) {
					continue;
				}

				SFTable t = o.getFirstTable();

				if (t == null) {
					continue;
				}

				SFRef ref = new SFRef(this, t);

				int i = 0;

				for (SFRef r : internalReferences) {
					String referenceName = r.getForeignKeyBaseName();

					if (referenceName.equals(ref.getForeignKeyBaseName())) {
						i++;
					}
				}

				ref.setIndex(i);

				internalReferences.add(ref);
			}
		}
	}

	public void writeCreateSQL(PrintStream out, DBType dbType) throws Exception {
		out.printf(getCreateSQL(dbType));
	}

	private int max(int value1, int value2) {
		if (value1 > value2) {
			return value1;
		} else {
			return value2;
		}
	}

	public String getCreateSQL(DBType dbType) throws Exception {
		StringBuilder sb = new StringBuilder();
		sb.append(String.format("\t[%s]\t\t", columnName));

		String sql = null;
		switch (dbType) {
		case SqlServer:
			if (TYPE_SQL_MAP.containsKey(field.getType())) {
				sql = TYPE_SQL_MAP.get(field.getType());
			} else {
				sql = TYPE_SQL_MAP.get(FieldType.anyType);
			}
			break;
		case RedShift:
			if (TYPE_REDSHIFT_MAP.containsKey(field.getType())) {
				sql = TYPE_REDSHIFT_MAP.get(field.getType());
			} else {
				sql = TYPE_REDSHIFT_MAP.get(FieldType.anyType);
			}
			break;
		}

		if (sql == null) {
			throw new Exception("not implemented yet.");
		}

		sb.append(String.format(sql,
				max(max(field.getLength(), field.getByteLength()), 255)));
		/*
		 * sb.append(field.isNillable() || ft.equals(FieldType._boolean) ?
		 * "\tNULL,\n" : "\tNOT NULL,\n");
		 * 
		 * if (ft.equals(FieldType.picklist) || ft.equals(FieldType.combobox) ||
		 * ft.equals(FieldType.multipicklist)) { PicklistEntry[] picklistValues
		 * = field.getPicklistValues();
		 * 
		 * if (picklistValues.length > 0) {
		 * sb.append(String.format("\t\tCHECK ([%s] IN (", columnName));
		 * 
		 * for (int j = 0; j < picklistValues.length; j++) { if (j > 0) {
		 * sb.append(","); }
		 * 
		 * sb.append(String.format("'%s'", picklistValues[j].getValue())); }
		 * 
		 * sb.append(String.format(")),\n")); } }
		 */
		return sb.toString();
	}

}
