package com.peixeurbano.salesforce.replica.task;

import java.io.BufferedWriter;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.io.OutputStreamWriter;
import java.text.DecimalFormat;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Map.Entry;

import com.amazonaws.services.s3.AmazonS3Client;
import com.amazonaws.util.StringInputStream;
import com.peixeurbano.salesforce.replica.model.DBType;
import com.peixeurbano.salesforce.replica.model.SFObject;
import com.peixeurbano.salesforce.replica.model.SFTable;
import com.sforce.async.BatchInfo;
import com.sforce.async.BatchStateEnum;
import com.sforce.async.BulkConnection;
import com.sforce.async.ConcurrencyMode;
import com.sforce.async.ContentType;
import com.sforce.async.JobInfo;
import com.sforce.async.OperationEnum;
import com.sforce.async.QueryResultList;

import com.amazonaws.services.s3.model.GetObjectRequest;
import com.amazonaws.services.s3.model.ObjectMetadata;
import com.amazonaws.services.s3.model.S3Object;

public class BulkCopyObject extends Thread {
	int errorLimit = 100;
	int countError = 0;

	public BulkCopyObject(AmazonS3ClientFactory s3f, SFObject o, Calendar c,
			BulkConnectionFactory bcf, String directory) {

		this.object = o;
		this.c = c;
		this.s3f = s3f;
		this.isIncrementalCopy = BulkCopyTask.getCurrentInstance()
				.isIncrementalCopy();
		this.task = BulkCopyTask.getCurrentInstance();
		this.bcf = bcf;
		this.directory = directory;

	}

	public boolean wasSuccessfull = false;
	BulkConnectionFactory bcf;
	BulkCopyTask task;
	String BUCKET_NAME = BulkCopyTask.BUCKET_NAME;
	String directory;

	public void storeFile() throws InterruptedException {

		System.out.println(new SimpleDateFormat("yyyy-MM-dd HH:mm:ss")
				.format(new Date())
				+ " Storing "
				+ object.getObjectName()
				+ " last update file.");

		String controlDirectory = directory + "\\control\\";
		File dirControl = new File(controlDirectory);
		dirControl.mkdir();
		File fControl = new File(controlDirectory + this.object.getObjectName()
				+ ".lastupdate");
		if (fControl.exists()) {
			fControl.delete();
		}
		try {
			OutputStream outControl = new FileOutputStream(fControl);
			String millis = Long.toString(c.getTimeInMillis());
			outControl.write(millis.getBytes());
			outControl.close();
		} catch (IOException e) {
			e.printStackTrace();
		}

		System.out.println(new SimpleDateFormat("yyyy-MM-dd HH:mm:ss")
				.format(new Date())
				+ " "
				+ object.getObjectName()
				+ " last update file stored.");
	}

	// private long contentLength;
	InputStream content;

	public Calendar getLastUpdate() {
		String controlDirectory = directory + "\\control\\";
		File fControl = new File(controlDirectory + this.object.getObjectName()
				+ ".lastupdate");
		Calendar c = Calendar.getInstance();
		if (fControl.exists()) {
			byte[] lastupdate = new byte[64];
			InputStream in;
			try {
				in = new FileInputStream(fControl);
				in.read(lastupdate);
				in.close();
			} catch (Exception e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
			System.out.println(new SimpleDateFormat("yyyy-MM-dd HH:mm:ss")
					.format(new Date())
					+ " "
					+ object.getObjectName()
					+ " last update file readed.");

			Long lu = Long.parseLong(new String(lastupdate).trim());

			c.setTimeInMillis(lu);
			System.out.println(" The value founded was: "
					+ new SimpleDateFormat("yyyy-MM-dd HH:mm:ss").format(c
							.getTime()));

		} else {
	
			c.set(2014, 4, 8, 0, 0, 0);
			System.out.println(" The value assumed is: "
					+ new SimpleDateFormat("yyyy-MM-dd HH:mm:ss").format(c
							.getTime()));
		}
		return c;
	}

	/*
	 * private Map<String, Calendar> lastUpdateRead = new HashMap<String,
	 * Calendar>();
	 * 
	 * public Calendar getLastUpdate(AmazonS3ClientFactory s3f, SFTable table)
	 * throws Exception { if (task.getLastGlobalUpdateReference() != null) {
	 * return task.getLastGlobalUpdateReference(); } if
	 * (lastUpdateRead.containsKey(object.getObjectName())) { return
	 * lastUpdateRead.get(object.getObjectName()); } byte[] lastupdate = new
	 * byte[64];
	 * 
	 * AmazonS3Client s3 = s3f.getAmazonS3Client(); System.out.println(new
	 * SimpleDateFormat("yyyy-MM-dd HH:mm:ss") .format(new Date()) + " Reading "
	 * + object.getObjectName() + " last update file.");
	 * 
	 * S3Object o = s3.getObject(new GetObjectRequest("pu-salesforce", "ctrl/" +
	 * object.getObjectName() + ".lastupdate"));
	 * 
	 * InputStream in = o.getObjectContent(); in.read(lastupdate); in.close();
	 * System.out.println(new SimpleDateFormat("yyyy-MM-dd HH:mm:ss")
	 * .format(new Date()) + " " + object.getObjectName() +
	 * " last update file readed."); s3f.releaseClient(s3); Long lu =
	 * Long.parseLong(new String(lastupdate).trim());
	 * 
	 * Calendar c = Calendar.getInstance(); c.setTimeInMillis(lu);
	 * lastUpdateRead.put(object.getObjectName(), c);
	 * 
	 * return c; }
	 */
	interface BatchJobCallback {
		public void onJobComplete(int resultCount) throws Exception;

		public void onJobResult(int resultIndex, InputStream in,
				int totalResults) throws Exception;

		public void onJobResultFinish() throws Exception;
	}
	BulkConnection bc;
	private void createBatchJob(String objectName, final SFTable t,
			final BulkConnectionFactory bcf, BatchJobCallback callback)
			throws Exception {
		bc = bcf.getBulkConnection();
		System.out.println(new SimpleDateFormat("yyyy-MM-dd HH:mm:ss")
		.format(new Date())
		+ " Thread "
		+ Thread.currentThread().getId()
		+ " ("
		+ t.getTableName() + ") initiated.");
		 connectionInUse = true;
		JobInfo job = new JobInfo();
		job.setObject(objectName);
		job.setOperation(OperationEnum.query);
		job.setConcurrencyMode(ConcurrencyMode.Parallel);
		job.setContentType(ContentType.XML);

		job = bc.createJob(job);

		assert job.getId() != null;

		job = bc.getJobStatus(job.getId());

		BatchInfo info = bc.createBatchFromStream(job,
				t.getSOQLInputStream(this.isIncrementalCopy));

		String soqlFileName = String.format("%s.%s.soql", t.getSchemaName(), t.getTableName());

		String soqlDirectory = directory + "\\soql\\";
		File fSoql = new File(soqlDirectory + soqlFileName);

		OutputStream outSoql = new FileOutputStream(fSoql);

		outSoql.write((t.getSOQL(isIncrementalCopy)).getBytes());

		outSoql.close();

		while (true) {
			Thread.sleep(1 * 1000);

			info = bc.getBatchInfo(job.getId(), info.getId());

			if (info.getState() == BatchStateEnum.Completed) {
				System.out.println(new SimpleDateFormat("yyyy-MM-dd HH:mm:ss")
						.format(new Date())
						+ " "
						+ t.getSchemaName()
						+ "."
						+ t.getTableName()
						+ " retrieved to thread "
						+ Thread.currentThread().getId() + ".");
				QueryResultList list = bc.getQueryResultList(job.getId(),
						info.getId());

				String[] queryResults = list.getResult();

				if (queryResults == null) {
					System.out.println("sf job failed: "
							+ info.getStateMessage());
					break;
				}

				callback.onJobComplete(queryResults.length);

				List<InputStream> ins = new ArrayList<InputStream>();
				for (int resultIndex = 0; resultIndex < queryResults.length; resultIndex++) {
					ins.add(bc.getQueryResultStream(job.getId(), info.getId(),
							queryResults[resultIndex]));
				}
				if(connectionInUse)
				{
				bcf.releaseConnection(bc);
				}
				 connectionInUse = false;
				for (int resultIndex = 0; resultIndex < queryResults.length; resultIndex++) {

					callback.onJobResult(resultIndex, ins.get(resultIndex),
							queryResults.length);
					ins.get(resultIndex).close();
				}
				callback.onJobResultFinish();

				break;
			}

			if (info.getState() == BatchStateEnum.Failed) {
				System.out.println("sf job failed: " + info.getStateMessage());
				break;
			}

			// System.out.print(".");
			System.out.flush();
		}

	}

	private String getBytesLength(long qtyBytes) {

		if (qtyBytes < 1024) {
			return qtyBytes + " Bytes";
		} else if (qtyBytes < Math.pow(1024, 2)) {
			return new DecimalFormat("0.##").format(qtyBytes / 1024) + " KB";
		} else if (qtyBytes < Math.pow(1024, 3)) {
			return new DecimalFormat("0.##").format(qtyBytes
					/ Math.pow(1024, 2))
					+ " MB";
		} else if (qtyBytes < Math.pow(1024, 4)) {
			return new DecimalFormat("0.##").format(qtyBytes
					/ Math.pow(1024, 3))
					+ " GB";
		} else {
			return new DecimalFormat("0.##").format(qtyBytes
					/ Math.pow(1024, 4))
					+ " TB";
		}
	}

	public SFObject object;
	private Calendar c;
	private AmazonS3ClientFactory s3f;
	boolean isIncrementalCopy;

	@Override
	public void run() {

		try {
			if (isIncrementalCopy) {
				this.object.setLastUpdate(getLastUpdate());
			}
			for (SFTable table : object.getTables()) {
	//			InputStream in = null;

				countError = 0;
				writeBulkStream(s3f, bcf, table);

				/*
				 * in = new StringInputStream(millis); this.content = in;
				 * this.contentLength = millis.length(); in.close();
				 */
			}
			for (File fg : allFilesGenerated) {
				String newFileName = fg.getName().replace(".tmp", "");
				File nf = new File(newFileName);
				if (nf.exists()) {
					nf.delete();
				}
				fg.renameTo(new File(directory + "\\xml\\" + newFileName));
			}
			for (Entry<String, String> merge : object.getMergeSql(DBType.SqlServer).entrySet()) {
				File fMerge = new File(directory + "\\merge\\"
						+ merge.getKey().replaceAll("__c", "") + ".merge.sql");

				OutputStream outMerge = new FileOutputStream(fMerge);
				outMerge.write(merge.getValue().getBytes());
				outMerge.close();
			}
			
			for (Entry<String, String> merge : object.getMergeSql(DBType.RedShift).entrySet()) {
				File fMerge = new File(directory + "\\mergeRedShift\\"
						+ merge.getKey().replaceAll("__c", "") + ".merge.sql");

				OutputStream outMerge = new FileOutputStream(fMerge);
				outMerge.write(merge.getValue().getBytes());
				outMerge.close();
			}			
			
			System.out.println(new SimpleDateFormat("yyyy-MM-dd HH:mm:ss")
					.format(new Date())
					+ " Thread "
					+ Thread.currentThread().getId()
					+ " ("
					+ object.getObjectName() + ") terminated.");
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
			System.out.println(new SimpleDateFormat("yyyy-MM-dd HH:mm:ss")
					.format(new Date())
					+ " Thread "
					+ Thread.currentThread().getId()
					+ " ("
					+ object.getObjectName() + ") terminated with error.");
		}
	}

	List<File> allFilesGenerated = new ArrayList<File>();

	private void writeBulkStream(final AmazonS3ClientFactory s3f,
			final BulkConnectionFactory bcf, final SFTable t) throws Exception {

		try {
			SFObject o = t.getParent();

			createBatchJob(o.getObjectName(), t, bcf, new BatchJobCallback() {
				@Override
				public void onJobComplete(int resultCount) throws Exception {

				}

				Integer files = -1;

				@Override
				public void onJobResult(int resultIndex, InputStream in,
						int totalResults) throws Exception {

					int len;
					byte[] buffer = new byte[8192];

					String fileName;
					fileName = String.format("%s.%s.xml.tmp",
							t.getSchemaName(), t.getTableName());

					String xmlDirectoryName = directory + "\\xml\\";
					String ddlDirectory = directory + "\\ddl\\";
					String ddlDirectoryRedShift = directory + "\\ddlRedshift\\";
					
					
					File fCreate = new File(ddlDirectory
							+ fileName.replace(".xml.tmp", ".ddl.sql"));
					
					File fCreateRedShift = new File(ddlDirectoryRedShift
							+ fileName.replace(".xml.tmp", ".ddl.sql"));

					OutputStream outCreate = new FileOutputStream(fCreate);
					outCreate.write(t.getCreateSQL(DBType.SqlServer).getBytes());
					outCreate.close();
					
					OutputStream outCreateRS = new FileOutputStream(fCreateRedShift);
					outCreateRS.write(t.getCreateSQL(DBType.RedShift).getBytes());
					outCreateRS.close();

					files++;
					List<File> filesGenerated = new ArrayList<File>();
					File f = new File(xmlDirectoryName	+ fileName.replace(".xml",	"." + String.format("%02d", files)	+ ".xml"));
					if (f.exists()) {
						f.delete();
					}
			//		OutputStream out = new FileOutputStream(f);
					BufferedWriter out = new BufferedWriter(new OutputStreamWriter(new FileOutputStream(f),"UTF8"));
					int i = 0;
					filesGenerated.clear();
					while ((len = in.read(buffer)) != -1) {
						i++;
						// String s = new String(buffer, "UTF-8");
										
						byte[] buf2 = new byte[len];
						
						for(int e = 0;e < len;e++)
						{
							buf2[e] = buffer[e];
						}
						
						String	s = new String(buf2, "UTF-8");

						if ( i > 1500000000 && s.contains("</records>")) {
							files++;
							int stopPoint = s.indexOf("</records>")	+ "</records>".length();
					//		out.write(s.substring(0, stopPoint).getBytes("UTF-8"));
					//		out.write("</queryResult>".getBytes("UTF-8"));
							out.write(s.substring(0, stopPoint));
							out.write("</queryResult>");

							out.close();
							System.out.println(new SimpleDateFormat(
									"yyyy-MM-dd HH:mm:ss").format(new Date())
									+ " File "
									+ f.getName()
									+ " size: "
									+ getBytesLength(f.length())
									+ " (thread "
									+ Thread.currentThread().getId() + ").");

							filesGenerated.add(f);
							f = new File(xmlDirectoryName + fileName.replace(".xml",	"." + String.format("%02d", files)	+ ".xml"));
							if (f.exists()) {
								f.delete();
							}
					
					//		out = new FileOutputStream(f);
							 out = new BufferedWriter(new OutputStreamWriter(new FileOutputStream(f),"UTF8"));
							
					//		out.write("<?xml version=\"1.0\" encoding=\"UTF-8\"?><queryResult xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns=\"http://www.force.com/2009/06/asyncapi/dataload\">".getBytes("UTF-8"));
								out.write("<?xml version=\"1.0\" encoding=\"UTF-8\"?><queryResult xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns=\"http://www.force.com/2009/06/asyncapi/dataload\">");

								/*
							byte[] bytes = s.substring(stopPoint).getBytes(
									"UTF-8");

							for (int g = 0; g < bytes.length; g++) {
								if (bytes[g] == 0x00) {
									break;
								}
								out.write(bytes[g]);
							}
*/
								out.write(s.substring(stopPoint));
							i = 0;

						} else {
					//		out.write(buffer, 0, len);
							out.write(s);
						}
						buffer = null;
						buffer = new byte[8192];
						s = null;
					}

					out.close();
					filesGenerated.add(f);

					System.out.println(new SimpleDateFormat(
							"yyyy-MM-dd HH:mm:ss").format(new Date())
							+ " File "
							+ f.getName()
							+ " size: "
							+ getBytesLength(f.length())
							+ " (thread "
							+ Thread.currentThread().getId() + ").");
					int countF = 0;
					for (File fg : filesGenerated) {
						countF++;

						String newFileName = null;

						if (filesGenerated.size() == 1) {
							newFileName = xmlDirectoryName
									+ fileName.replace(".xml", ".UN.xml");
						} else if (countF == filesGenerated.size()
								&& resultIndex == totalResults - 1) {
							newFileName = xmlDirectoryName
									+ fileName.replace(".xml", ".LA.xml");
						}
						if (newFileName != null) {
							File nf = new File(newFileName);
							if (nf.exists()) {
								nf.delete();
							}

							fg.renameTo(new File(newFileName));
							allFilesGenerated.add(nf);
						} else {
							allFilesGenerated.add(fg);
						}

					}
					wasSuccessfull = true;

				}

				@Override
				public void onJobResultFinish() throws Exception {
					// System.out.print("(" + (System.currentTimeMillis() -
					// startTimestamp) + " ms)");
				}

			});
		} catch (Exception ex) {
			if(connectionInUse)
			{
			bcf.releaseConnection(bc);
			 connectionInUse = false;
			}
			countError++;
			if (countError < errorLimit) {
				System.out.println(new SimpleDateFormat("yyyy-MM-dd HH:mm:ss")
				.format(new Date())
				+ " Trying to read again "
				+ " (object " + object.getObjectName() + ").");
				writeBulkStream(s3f, bcf, t);
			}else
			{
				System.out.println(new SimpleDateFormat("yyyy-MM-dd HH:mm:ss")
				.format(new Date())
				+ " Error limit exceeded "
				+ " (object " + object.getObjectName() + ").");
			}
		}
	}
	boolean connectionInUse = false;
}
