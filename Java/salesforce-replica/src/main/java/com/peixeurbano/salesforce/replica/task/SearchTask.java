package com.peixeurbano.salesforce.replica.task;

import java.util.List;

import joptsimple.OptionSet;

import com.peixeurbano.salesforce.replica.Task;
import com.peixeurbano.salesforce.replica.model.SFObject;
import com.peixeurbano.salesforce.replica.model.SFSchema;
import com.peixeurbano.salesforce.replica.utils.ObjectCallback;
import com.peixeurbano.salesforce.replica.utils.SalesforceCatalog;
import com.sforce.soap.enterprise.EnterpriseConnection;
import com.sforce.ws.ConnectorConfig;

public class SearchTask implements Task
{
	private EnterpriseConnection connection = null;
	
	private List<String> objectList = null;
	
	@SuppressWarnings("unchecked")
	public SearchTask(OptionSet options)
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
					System.out.println(o.getObjectName());
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
