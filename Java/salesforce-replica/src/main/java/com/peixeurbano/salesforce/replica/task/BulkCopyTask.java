package com.peixeurbano.salesforce.replica.task;

import java.io.File;
import java.io.FileOutputStream;
import java.io.OutputStream;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.List;
import java.util.Map.Entry;

import joptsimple.OptionSet;
import com.peixeurbano.salesforce.replica.Task;
import com.peixeurbano.salesforce.replica.model.SFObject;
import com.peixeurbano.salesforce.replica.model.SFSchema;
import com.peixeurbano.salesforce.replica.utils.SalesforceCatalog;
import com.sforce.soap.enterprise.EnterpriseConnection;
import com.sforce.ws.ConnectorConfig;

public class BulkCopyTask implements Task {

	static final String BUCKET_NAME = "pu-salesforce";

	private List<String> objectList = null;
	private static BulkCopyTask currentInstance;
	private String base = null;
	private String sqlServerAddress = null;

	@SuppressWarnings("unchecked")
	public BulkCopyTask(OptionSet options, boolean isIncrementalCopy)
			throws ParseException {
		currentInstance = this;
		this.setIncrementalCopy(isIncrementalCopy);
		if (options.has("o")) {
			objectList = (List<String>) options.valuesOf("o");
		}
		if (options.has("d")) {
			this.lastGlobalUpdateReference = Calendar.getInstance();
			String s = ((List<String>) options.valuesOf("d")).get(0);
			Date date = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss").parse(s);
			this.lastGlobalUpdateReference.setTime(date);

		}
		if (options.has("e")) {
			sqlServerAddress = options.valuesOf("e").toString()
					.replace("[", "").replace("]", "");
		}
		if (options.has("b")) {
			base = options.valuesOf("b").toString().replace("[", "")
					.replace("]", "");
		}
		if (options.has("s")) {
			sessions = Integer.parseInt(options.valuesOf("s").toString()
					.replace("[", "").replace("]", ""));
		}
		if (options.has("c")) {
			copyLoaderPath = options.valuesOf("c").toString().replace("[", "")
					.replace("]", "");
		} else {
			// copyLoaderPath = "SalesForceCopyLoader.exe";
		}
		if (options.has("t")) {
			directory = options.valuesOf("t").toString().replace("[", "")
					.replace("]", "");
		} else {
			directory = "files";
		}
		if (options.has("j")) {
			javaPath = options.valuesOf("j").toString().replace("[", "")
					.replace("]", "");
		} else {
			javaPath = "C:\\Program Files\\Java\\jre7\\bin\\java.exe";
		}
		if (options.has("u")) {
			redShiftUID = options.valuesOf("u").toString().replace("[", "")
					.replace("]", "");
		} else {
			System.out
					.println("Não foi especificado um usuário Redshift através do parâmetro -u");
		}
		if (options.has("p")) {
			redShiftPWD = options.valuesOf("p").toString().replace("[", "")
					.replace("]", "");
		} else {
			System.out
					.println("Não foi especificado uma senha do usuário Redshift através do parâmetro -p");
		}
		if (options.has("x")) {
			s3Path = options.valuesOf("x").toString().replace("[", "")
					.replace("]", "");
		} else {
			System.out
					.println("Não foi especificado o caminho do aplicativo s3.exe através do parâmetro -s");
		}
		if (options.has("k")) {
			S3AccessKey = options.valuesOf("k").toString().replace("[", "")
					.replace("]", "");
		} else {
			System.out
					.println("Não foi especificado access key S3 através do parâmetro -a");
		}
		if (options.has("y")) {
			S3SecretKey = options.valuesOf("y").toString().replace("[", "")
					.replace("]", "");
		} else {
			System.out
					.println("Não foi especificado secret key S3 através do parâmetro -y");
		}
		if (options.has("r")) {
			redShiftLoaderPath = options.valuesOf("r").toString()
					.replace("[", "").replace("]", "");
		} else {
			System.out
					.println("Não foi especificado o caminho do Redshift loader através do parâmetro -r");
		}
		if (options.has("g")) {
			target = options.valuesOf("g").toString()
					.replace("[", "").replace("]", "");
		} 
	}

	String redShiftUID;
	String redShiftPWD;
	String s3Path;
	String S3AccessKey;
	String S3SecretKey;
	String redShiftLoaderPath;
	String javaPath;
	String target;
	private String directory;
	private String copyLoaderPath;
	private int sessions = 5;

	private boolean isIncrementalCopy;

	BulkConnectionFactory factory;

	AmazonS3ClientFactory s3f;

	private File createAndCleanDir(String path) {
		File dir = new File(path);
		dir.mkdir();
		for (File file : dir.listFiles()) {
			file.delete();
		}
		return dir;
	}

	public void execute(ConnectorConfig config) throws Exception {
		EnterpriseConnection connection = new EnterpriseConnection(config);
		SalesforceCopyLoader sfcl = null;
		if (copyLoaderPath != null) {
			sfcl = new SalesforceCopyLoader(sqlServerAddress, base, directory,
					copyLoaderPath, redShiftUID, redShiftPWD, s3Path,
					S3AccessKey, S3SecretKey, redShiftLoaderPath, javaPath,target);
		}

		List<BulkCopyObject> bctl = new ArrayList<BulkCopyObject>();
		try {
			final Calendar c = connection.getServerTimestamp().getTimestamp();

			s3f = new AmazonS3ClientFactory();

			s3f.setMaxQtyClients(sessions);
			factory = new BulkConnectionFactory(config);

			factory.setMaxQtyConnections(sessions);
			System.out.println(new SimpleDateFormat("yyyy-MM-dd HH:mm:ss")
					.format(new Date()) + " Retrieving salesforce schema ..");

			SFSchema schema = SalesforceCatalog.retrieveSchema(connection,
					objectList);

			System.out.println(new SimpleDateFormat("yyyy-MM-dd HH:mm:ss")
					.format(new Date()) + " Salesforce schema retrieved.");
			createAndCleanDir(directory + "\\xml");
			createAndCleanDir(directory + "\\ddl");
			createAndCleanDir(directory + "\\ddlRedShift");
			createAndCleanDir(directory + "\\soql");
			createAndCleanDir(directory + "\\merge");
			createAndCleanDir(directory + "\\mergeRedShift");
			createAndCleanDir(directory + "\\loaded");
			
			if (sfcl != null) {
				sfcl.start();
				System.out.println(new SimpleDateFormat("yyyy-MM-dd HH:mm:ss")
						.format(new Date()) + " Copy loader started.");
			} else {
				System.out.println(new SimpleDateFormat("yyyy-MM-dd HH:mm:ss")
						.format(new Date()) + " Copy loader will not start.");
			}

			List<BulkCopyObject> tl = new ArrayList<BulkCopyObject>();
			for (SFObject o : schema.getObjects()) {

				BulkCopyObject bct = new BulkCopyObject(s3f, o, c, factory,
						directory);
				bctl.add(bct);
				tl.add(bct);
				bct.run();

				try {
					Thread.sleep(4000);
				} catch (InterruptedException e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
				}
			}

			for (BulkCopyObject th : tl) {

				while (th.isAlive()) {
					try {

						Thread.sleep(1000);
					} catch (InterruptedException e) {
						// TODO Auto-generated catch block
						e.printStackTrace();
					}

				}
				if (!th.wasSuccessfull) {
					// System.out.println(new
					// SimpleDateFormat("yyyy-MM-dd HH:mm:ss").format(new
					// Date()) + " Error of " + th.table.getSchemaName() + "." +
					// th.table.getTableName() + " Load bla.");
				}
			}

		} catch (Exception e) {
			throw e;
		} finally {
		//	connection.logout();

			System.out.println(new SimpleDateFormat("yyyy-MM-dd HH:mm:ss")
					.format(new Date()) + " End of Sales force Read.");
			File fEnd = new File(directory, "xml\\End.xml");

			OutputStream outEnd = new FileOutputStream(fEnd);

			outEnd.write("End".getBytes());

			outEnd.close();

			if (sfcl != null) {
				sfcl.setReadSalesforceEnded(true);

				boolean sucess = sfcl.waitSuccessfullExecution();

				try {
					sfcl.interrupt();
				} catch (Exception e) {

				}

				if (!sucess) {

					System.out.println(new SimpleDateFormat(
							"yyyy-MM-dd HH:mm:ss").format(new Date())
							+ " Error on Sales force copy load.");
					System.exit(1);
				} else {
					for (BulkCopyObject bct : bctl) {
						bct.storeFile();
					}

					System.out.println(new SimpleDateFormat(
							"yyyy-MM-dd HH:mm:ss").format(new Date())
							+ " End of Sales force copy load.");
				}
			} else {
				for (BulkCopyObject bct : bctl) {
					bct.storeFile();
				}
				System.out.println(new SimpleDateFormat("yyyy-MM-dd HH:mm:ss")
						.format(new Date()) + " Start the load manually.");

			}
		}
	}

	public boolean isIncrementalCopy() {
		return isIncrementalCopy;
	}

	public void setIncrementalCopy(boolean isIncrementalCopy) {
		this.isIncrementalCopy = isIncrementalCopy;
	}

	public Calendar getLastGlobalUpdateReference() {
		return lastGlobalUpdateReference;
	}

	public void setLastGlobalUpdateReference(Calendar lastGlobalUpdateReference) {
		this.lastGlobalUpdateReference = lastGlobalUpdateReference;
	}

	public static BulkCopyTask getCurrentInstance() {
		return currentInstance;
	}

	private Calendar lastGlobalUpdateReference;
}
