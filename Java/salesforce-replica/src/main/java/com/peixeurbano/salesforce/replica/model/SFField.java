package com.peixeurbano.salesforce.replica.model;

import com.sforce.soap.enterprise.Field;

public class SFField
{
	private SFObject parent;
	
	protected Field field;
	
	public SFField(SFObject parent, Field field)
	{
		this.parent = parent;
		this.field = field;
	}
	
	public boolean isPK()
	{
		return field.isIdLookup() && !field.isCustom();
	}
	
	public boolean isCustom()
	{
		return field.isCustom();
	}

	public SFObject getParent()
	{
		return parent;
	}
	
	public String getName()
	{
		return field.getName();
	}
}
