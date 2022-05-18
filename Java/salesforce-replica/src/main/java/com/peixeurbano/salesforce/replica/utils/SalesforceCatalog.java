package com.peixeurbano.salesforce.replica.utils;

import java.io.BufferedReader;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.List;

import com.peixeurbano.salesforce.replica.model.SFSchema;
import com.sforce.soap.enterprise.DescribeGlobalResult;
import com.sforce.soap.enterprise.DescribeGlobalSObjectResult;
import com.sforce.soap.enterprise.DescribeSObjectResult;
import com.sforce.soap.enterprise.EnterpriseConnection;
import com.sforce.ws.ConnectionException;

public class SalesforceCatalog {
	public static SFSchema retrieveSchema(EnterpriseConnection connection)
			throws ConnectionException {
		return retrieveSchema(connection, null);
	}

	public static SFSchema retrieveSchema(EnterpriseConnection connection,
			List<String> objectList) throws ConnectionException {
		DescribeGlobalResult dgr = connection.describeGlobal();

		DescribeGlobalSObjectResult[] objects = dgr.getSobjects();

		List<DescribeSObjectResult> describes = new ArrayList<DescribeSObjectResult>();
		List<String> SalesForceExclusionList = new ArrayList<String>();

		try {
			FileReader fr = new FileReader("SalesforceObjectExclusionList.txt");
			BufferedReader input = new BufferedReader(fr);

			String line;

			line = input.readLine();

			while (line != null) {
				SalesForceExclusionList.add(line.toUpperCase());
				line = input.readLine();
				}

		} catch (Exception e) {

		}

		System.out.println(new SimpleDateFormat("yyyy-MM-dd HH:mm:ss")
				.format(new Date())
				+ " "
				+ objects.length
				+ " objects returned.");
		for (DescribeGlobalSObjectResult r : objects) {
			if (objectList != null) {
				for (String objectName : objectList) {
			//		System.out.println(r.getName());
					String resultName = r.getName().toUpperCase();
					// termina com
					if (objectName.indexOf("%") == 0) {
						if (resultName.indexOf(objectName.replace("%", "")
								.toUpperCase()) == resultName.length()
								- objectName.replace("%", "").length()) {
							describes.add(connection.describeSObject(r
									.getName()));
							break;
						}
					} /* comeca com */
					else if (objectName.indexOf("%") == objectName.length() - 1) {

						if (resultName.indexOf(objectName.replace("%", "").toUpperCase()) == 0) {
							describes.add(connection.describeSObject(r
									.getName()));
							break;
						}

					} else {
						if (resultName.equals(objectName.replace("%", "")
								.toUpperCase())) {
							describes.add(connection.describeSObject(r
									.getName()));
							break;
						}
					}

				}
			} else {

				if (!SalesForceExclusionList
						.contains(r.getName().toUpperCase())) {
					describes.add(connection.describeSObject(r.getName()));
					System.out.println(new SimpleDateFormat(
							"yyyy-MM-dd HH:mm:ss").format(new Date())
							+ " "
							+ r.getName());

				}
			}
		}

		return new SFSchema(describes);
	}

}
