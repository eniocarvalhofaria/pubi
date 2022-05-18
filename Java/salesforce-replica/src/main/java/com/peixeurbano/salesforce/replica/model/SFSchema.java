package com.peixeurbano.salesforce.replica.model;

import java.io.PrintStream;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;

import com.peixeurbano.salesforce.replica.utils.ObjectCallback;
import com.sforce.soap.enterprise.DescribeSObjectResult;

public class SFSchema {
	private SFObject[] objects;

	public SFObject[] getObjects() {
		return objects;
	}
	
	public SFTable[] getTables()
	{
		List<SFTable> tl = new ArrayList<SFTable>();
		for(SFObject o : objects)
		{
			for(SFTable t: o.getTables())
			{
				tl.add(t);
			}
		}
		SFTable[] tables = new SFTable[tl.size()] ;
		tl.toArray(tables);
		return tables;
	}

	private SFRef[] references;

	public SFSchema(List<DescribeSObjectResult> describes) {
		// keep call order
		System.out.println(new SimpleDateFormat("yyyy-MM-dd HH:mm:ss")
				.format(new Date()) + " filling objects...");
		fillObjects(describes);
		System.out.println(new SimpleDateFormat("yyyy-MM-dd HH:mm:ss")
				.format(new Date()) + " updating references...");
		updateReferences();
		System.out.println(new SimpleDateFormat("yyyy-MM-dd HH:mm:ss")
				.format(new Date()) + " updated references.");
	}

	private void fillObjects(List<DescribeSObjectResult> describes) {
		List<SFObject> internalObjects = new ArrayList<SFObject>();

		for (DescribeSObjectResult dsr : describes) {
			if (dsr.isQueryable()) {
				internalObjects.add(new SFObject(dsr));
			}
		}

		this.objects = internalObjects.toArray(new SFObject[0]);
	}

	private void updateReferences() {
		List<SFRef> internalReferences = new ArrayList<SFRef>();

		for (SFObject o : objects) {
			System.out.println(new SimpleDateFormat("yyyy-MM-dd HH:mm:ss")
					.format(new Date())
					+ " updating "
					+ o.getObjectName()
					+ " references...");
			o.updateReferences(this, internalReferences);
		}

		this.references = internalReferences.toArray(new SFRef[0]);
	}

	public SFRef[] getReferences() {
		return references;
	}

	public SFObject lookForObject(String objectName) {
		for (SFObject o : objects) {
			if (objectName.equals(o.getObjectName())) {
				return o;
			}
		}

		return null;
	}

	public void writeCreateSQL(PrintStream out,DBType dbType) throws Exception {
		for (SFObject o : objects) {
			o.writeCreateSQL(out, dbType);
		}

		for (SFRef r : references) {
			r.writeCreateSQL(out);
		}
	}

	public void fetchObjects(ObjectCallback oc) {
		for (SFObject o : objects) {

			oc.objectCallback(o);

		}
	}
}
