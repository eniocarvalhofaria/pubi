package com.peixeurbano.salesforce.replica;

import com.sforce.ws.ConnectorConfig;

public interface Task
{
	public void execute(ConnectorConfig config) throws Exception;
}
