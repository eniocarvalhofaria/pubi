package com.peixeurbano.salesforce.replica.model;

import java.io.ByteArrayInputStream;
import java.io.InputStream;
import java.io.PrintStream;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.List;

public class SFTable {
	private SFObject parent;

	private String schemaName;
	private String tableName;
	private SFColumn[] columns;

	protected SFTable(SFObject parent, String schemaName, String tableName,
			SFField[] fields) {
		this.parent = parent;

		this.schemaName = schemaName;
		this.tableName = tableName.replace("__c", "");

		List<SFColumn> internalColumns = new ArrayList<SFColumn>();

		List<String> columnNames = new ArrayList<String>();

		for (SFField field : fields) {
			String measuredColumnName = SFColumn
					.convertFieldToColumnName(field);

			if (columnNames.contains(measuredColumnName.toUpperCase())) {
				continue;
			}

			columnNames.add(measuredColumnName.toUpperCase());

			internalColumns.add(new SFColumn(this, field));
		}

		this.columns = internalColumns.toArray(new SFColumn[0]);
	}

	protected void updateReferences(SFSchema schema,
			List<SFRef> internalReferences) {
		for (SFColumn c : columns) {
			c.updateReferences(schema, internalReferences);
		}
	}

	public String getSchemaName() {
		return schemaName;
	}

	public String getTableName() {
		return tableName;
	}

	public SFColumn[] getColumns() {
		return columns;
	}

	public SFObject getParent() {
		return parent;
	}

	private boolean hasColumn(String columnName) {
		for (SFColumn c : getColumns()) {
			if (c.getColumnName().equals(columnName)) {
				return true;
			}

		}
		return false;
	}

	public void writeCreateSQL(PrintStream out, DBType dbType) throws Exception {
		out.printf(getCreateSQL(dbType));
	}

	public String getCreateSQL(DBType dbType) throws Exception {
		StringBuilder sb = new StringBuilder();
		switch (dbType) {
		case SqlServer:
			sb.append(String.format(
					"IF object_id(N'[%s].[%s]') IS NOT NULL \r\n", schemaName,
					tableName));
			sb.append("begin\r\n");
			sb.append(String.format("drop table [%s].[%s]\r\n", schemaName,	tableName));
			sb.append("end\r\n");
			sb.append("go\r\n");
			sb.append(String.format("CREATE TABLE [%s].[%s]\r\n(\r\n", schemaName,	tableName));
			break;
		case RedShift:
			sb.append(String.format("drop table if exists salesforce.%s%s ;\r\n",
					schemaName, tableName));
			sb.append(String.format("CREATE TABLE salesforce.%s%s\n(\r\n", schemaName,
					tableName));

		}
		

		boolean isFirst = true;
		for (SFColumn c : columns) {
			try{
			if (!this.getSchemaName().equals("cf")
					|| (!c.getColumnName().equals("CreatedDate") && !c
							.getColumnName().equals("LastModifiedDate"))) {
				if (!isFirst) {
					sb.append(",");
				}
				sb.append(c.getCreateSQL(dbType) + "\r\n");
				isFirst = false;
			}
			}catch(Exception e)
			{
				System.out.println(c.getColumnName());
			}
		}
		switch (dbType) {
		case SqlServer:
			sb.append(String.format(
					"\tCONSTRAINT PK_%s PRIMARY KEY (Id)\n)\nGO\n\n", tableName));
			return sb.toString();
	
		case RedShift:
			sb.append(
					",\tPRIMARY KEY (Id)\n)\n");
			return sb.toString().replace("[","\"").replace("]","\"");
	default:
		return sb.toString();
		}
		
		
	}

	public String getSOQL(boolean isIncrementalCopy) {
		StringBuffer sb = new StringBuffer();

		sb.append("SELECT ");

		boolean first = true;

		for (SFColumn c : columns) {
			if (!this.getSchemaName().equals("cf")
					|| (!c.getColumnName().equals("CreatedDate") && !c
							.getColumnName().equals("LastModifiedDate"))) {

				if (!first) {
					sb.append(", ");
				}

				sb.append(c.field.getName());
				first = false;
			}
		}

		sb.append(" FROM ");
		sb.append(parent.getObjectName());
		if (isIncrementalCopy && this.getParent().getLastUpdate() != null) {

			String lu = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss").format(this
					.getParent().getLastUpdate().getTime())
					+ "+00:00";
			lu = lu.replace(" ", "T");

			boolean hasCriterial = false;
			if (this.hasColumn("CreatedDate")) {
				sb.append(" where CreatedDate  >= " + lu);
				hasCriterial = true;
			}

			if (this.hasColumn("LastModifiedDate")) {
				if (hasCriterial) {
					sb.append(" or ");
				} else {
					sb.append(" where ");
				}
				sb.append(" LastModifiedDate >= " + lu);

			}
		}
		// System.out.println(sb.toString());
		// return sb.toString() + " where Name > '00112000' ";
		return sb.toString();
	}

	public InputStream getSOQLInputStream(boolean isIncrementalCopy) {
		if (!isIncrementalCopy) {
			return new ByteArrayInputStream(getSOQL(isIncrementalCopy)
					.getBytes());
		} else {
			return new ByteArrayInputStream(getSOQL(isIncrementalCopy)
					.getBytes());
		}
	}

}
