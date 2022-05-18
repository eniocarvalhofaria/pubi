package com.peixeurbano.salesforce.replica.task;

import java.io.Console;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.OutputStream;
import java.text.SimpleDateFormat;
import java.util.Date;

import joptsimple.OptionParser;
import joptsimple.OptionSet;

import com.peixeurbano.salesforce.replica.Task;
import com.sforce.soap.enterprise.EnterpriseConnection;
import com.sforce.soap.enterprise.fault.LoginFault;
import com.sforce.ws.ConnectionException;
import com.sforce.ws.ConnectorConfig;
import com.sforce.ws.tools.wsdlc;

public class SalesforceReplica {
	private static final String SOAP_AUTH_END_POINT = "https://login.salesforce.com/services/Soap/c/27.0/";
	private static final String REST_AUTH_END_POINT = "https://login.salesforce.com/services/async/c/27.0/";
	/*
	private static String sfUser = null;
	private static String sfPassword = null;
	private static String sfToken = null;

	public static void getNewCredentials() {
		try {
			File f = new File("sfUser");

			OutputStream outFile = new FileOutputStream(f);
			Console cnsl = null;

			// creates a console object
			cnsl = System.console();

			// if console is not null
			if (cnsl != null) {
				sfUser = cnsl.readLine("Salesforce UserId: ");
				sfPassword = cnsl.readLine("Salesforce Password: ");
				sfToken = cnsl.readLine("Salesforce Token: ");
			}
			outFile.write(String.format("{0}\t{1}{2}", sfUser, sfPassword,
					sfToken).getBytes());
			outFile.close();
		} catch (Exception ex) {
			// if any error occurs
			ex.printStackTrace();
		}
	}

	public void getCredentials() {

	}
*/
	public static String getSFUserName() {
		return System.getenv().get("SFUSER");

	}

	public static String getSFPassword() {
	return	System.getenv().get("SFPWD");
	}

	public static String getSFToken() {
		return System.getenv().get("SFTOKEN");
	}

	public static void doCompile(OptionSet options) throws Exception {
		if (!options.has("f")) {
			System.err.printf("file to compile not defined");
			System.exit(-2);
		}

		wsdlc.main(new String[] { options.valueOf("f").toString(),
				"peixeurbano-enterprise.jar" });
	}

	private static void doTaskExecute(Task task) {
		ConnectorConfig config = new ConnectorConfig();

		config.setUsername(getSFUserName());
		config.setPassword(getSFPassword() + getSFToken());
		config.setAuthEndpoint(SOAP_AUTH_END_POINT);
		config.setRestEndpoint(REST_AUTH_END_POINT);
			try {
			task.execute(config);
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
/*
		try {
			EnterpriseConnection connection = new EnterpriseConnection(config);
			connection.logout();
			try {
				task.execute(config);
			} catch (Exception e) {
				e.printStackTrace();
			}
		} catch (LoginFault e1) {

			System.out.println(new SimpleDateFormat("yyyy-MM-dd HH:mm:ss")
					.format(new Date()) + " Invalid Salesfoce login.");
	/*		getNewCredentials();
			doTaskExecute(task); 
		} catch (ConnectionException e2) {
			System.out.println(new SimpleDateFormat("yyyy-MM-dd HH:mm:ss")
					.format(new Date()) + " Salesfoce connection error.");
		}
*/
	}

	public static void main(String[] args) {
		try {
			OptionParser parser = new OptionParser("a:o:d:s:e:b:f:t:c:u:r:y:p:x:k:j:g::");
			OptionSet options = parser.parse(args);

			if (!options.has("a")) {
				System.err.printf("action not defined");
				System.exit(-2);
			}

			String whatAction = options.valueOf("a").toString();

			if ("compile".equalsIgnoreCase(whatAction)) {
				doCompile(options);
			} else if ("bulk-copy".equalsIgnoreCase(whatAction)) {
				doTaskExecute(new BulkCopyTask(options, false));
			} else if ("incremental-bulk-copy".equalsIgnoreCase(whatAction)) {
				doTaskExecute(new BulkCopyTask(options, true));
			} else if ("create-schema".equalsIgnoreCase(whatAction)) {
				doTaskExecute(new CreateSchemaTask(options));
			} else if ("describe".equalsIgnoreCase(whatAction)) {
				doTaskExecute(new DescribeTask(options));
			} else if ("search".equalsIgnoreCase(whatAction)) {
				doTaskExecute(new SearchTask(options));
			} else {
				System.out.println("action not defined!");
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
	}
}
