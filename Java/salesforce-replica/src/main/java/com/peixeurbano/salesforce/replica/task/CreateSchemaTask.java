package com.peixeurbano.salesforce.replica.task;

import java.util.List;

import joptsimple.OptionSet;

import com.peixeurbano.salesforce.replica.Task;
import com.peixeurbano.salesforce.replica.model.DBType;
import com.peixeurbano.salesforce.replica.model.SFSchema;
import com.peixeurbano.salesforce.replica.utils.SalesforceCatalog;
import com.sforce.soap.enterprise.EnterpriseConnection;
import com.sforce.ws.ConnectorConfig;

public class CreateSchemaTask implements Task
{
	private EnterpriseConnection connection = null;
	
	private List<String> objectList = null;
	
	@SuppressWarnings("unchecked")
	public CreateSchemaTask(OptionSet options)
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
		SFSchema sc = 	SalesforceCatalog.retrieveSchema(connection, objectList);
		sc.writeCreateSQL(System.out,DBType.SqlServer);
		sc.writeCreateSQL(System.out,DBType.RedShift);
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
