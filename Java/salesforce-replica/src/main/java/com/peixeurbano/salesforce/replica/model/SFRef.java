package com.peixeurbano.salesforce.replica.model;

import java.io.PrintStream;

public class SFRef
{
	private SFColumn parent;
	private SFTable target;
	private int index = 0;
	
	public SFRef(SFColumn parent, SFTable target)
	{
		this.parent = parent;
		this.target = target;
	}

	public SFColumn getParent()
	{
		return parent;
	}

	public SFTable getTarget()
	{
		return target;
	}
	
	public String getForeignKeyBaseName()
	{
		SFTable childTable = parent.getParent();
		SFTable parentTable = target;

		return String.format("FK_%s_%s", childTable.getTableName(), parentTable.getTableName());
	}
	
	public int getIndex()
	{
		return index;
	}

	public void setIndex(int index)
	{
		this.index = index;
	}
	
	
	public void writeCreateSQL(PrintStream out) throws Exception
	{
		SFTable childTable = parent.getParent();
		SFTable parentTable = target;
		
		out.printf("ALTER TABLE [%s].[%s] ADD CONSTRAINT %s_%d FOREIGN KEY (%s) REFERENCES [%s].[%s] (%s)\nGO\n\n", 
				childTable.getSchemaName(),
				childTable.getTableName(),
				getForeignKeyBaseName(),
				index,
				parent.getColumnName(),
				parentTable.getSchemaName(),
				parentTable.getTableName(),
				"Id"
				);
	}	
}
