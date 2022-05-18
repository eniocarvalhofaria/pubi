package com.peixeurbano.salesforce.replica.task;

import java.util.List;

import joptsimple.OptionSet;

import com.peixeurbano.salesforce.replica.Task;
import com.peixeurbano.salesforce.replica.model.SFObject;
import com.peixeurbano.salesforce.replica.model.SFSchema;
import com.peixeurbano.salesforce.replica.model.SFTable;
import com.peixeurbano.salesforce.replica.utils.ObjectCallback;
import com.peixeurbano.salesforce.replica.utils.SalesforceCatalog;
import com.sforce.soap.enterprise.EnterpriseConnection;
import com.sforce.ws.ConnectorConfig;

public class DescribeTask implements Task
{
	private EnterpriseConnection connection = null;
	
	private List<String> objectList = null;
	
	@SuppressWarnings("unchecked")
	public DescribeTask(OptionSet options)
	{
		if (options.has("o"))
		{
			objectList = (List<String>) options.valuesOf("o");
		}
	}

	public void execute(ConnectorConfig config) throws Exception
	{
		connection = new EnterpriseConnection(config);
		
		try
		{
			SFSchema schema = SalesforceCatalog.retrieveSchema(connection, objectList);
			
			schema.fetchObjects(new ObjectCallback() 
			{
				@Override
				public void objectCallback(SFObject o)
				{
					System.out.println("--------------------------------------------------------");
					System.out.println("Object Name: " + o.getObjectName());
					System.out.println("--------------------------------------------------------");
					System.out.println("isActivateable: " + o.isActivateable());
					System.out.println("isCreateable: " + o.isCreateable());
					System.out.println("isCustom: " + o.isCustom());
					System.out.println("isCustomSetting: " + o.isCustomSetting());
					System.out.println("isDeletable: " + o.isDeletable());
					System.out.println("isDeprecatedAndHidden: " + o.isDeprecatedAndHidden());
					System.out.println("isFeedEnabled: " + o.isFeedEnabled());
					System.out.println("isLayoutable: " + o.isLayoutable());
					System.out.println("isMergeable: " + o.isMergeable());
					System.out.println("isQueryable: " + o.isQueryable());
					System.out.println("isReplicateable: " + o.isReplicateable());
					System.out.println("isSearchable: " + o.isSearchable());
					System.out.println("isTriggerable: " + o.isTriggerable());
					System.out.println("isUndeletable: " + o.isUndeletable());
					System.out.println("isUpdateable: " + o.isUpdateable());
					System.out.println("--------------------------------------------------------");
					System.out.println("Tables: ");

					for (SFTable t : o.getTables())
					{
						System.out.println("\t" + t.getSchemaName() + "." + t.getTableName());
					}

					System.out.println("--------------------------------------------------------");
				}
			});
		}
		catch (Exception e)
		{
			throw e;
		}
		finally
		{
			connection.logout();
		}
	}

}
