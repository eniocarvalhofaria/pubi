package com.peixeurbano.salesforce.replica.task;

import java.io.*;

public class SalesforceCopyLoader extends Thread {
	Process p;

	public SalesforceCopyLoader(String databaseAddress, String database,
			String filesDirectory, String copyLoaderPath, String redShiftUID,
			String redShiftPWD, String s3Path, String S3AccessKey, String S3SecretKey,
			String redShiftLoaderPath, String javaPath,String target) {
		this.databaseAddress = databaseAddress;
		this.database = database;
		this.directory = filesDirectory;
		this.copyLoaderPath = copyLoaderPath;
		this.redShiftUID = redShiftUID;
		this.redShiftPWD = redShiftPWD;
		this.s3Path = s3Path;
		this.S3AccessKey = S3AccessKey;
		this.S3SecretKey = S3SecretKey;
		this.redShiftLoaderPath = redShiftLoaderPath;
		this.javaPath = javaPath;
		this.target = target;
	}

	String databaseAddress;
	String database;
	String directory;
	String copyLoaderPath;
	String redShiftUID;
	String redShiftPWD;
	String s3Path; 
	String S3AccessKey;
	String S3SecretKey;
	String redShiftLoaderPath;
	String javaPath;
	String target;
	@Override
	public void run() {

			try {
			isAlive = true;
			p = Runtime.getRuntime().exec(
					copyLoaderPath + " --address " + databaseAddress
							+ " --database " + database
							+ " --listenerdirectory \"" + directory + "\\\\\""
						    +	" --redshiftuid " + redShiftUID 
						    + " --redshiftpwd " + redShiftPWD 
						    + " --s3path " + "\"" +  s3Path  + "\""
                            + " --s3accesskey " + S3AccessKey
                            + " --s3secretkey " + S3SecretKey 
                            + " --redshiftloaderpath " + "\"" + redShiftLoaderPath + "\""
                           + " --javapath  " + "\"" + javaPath + "\""
                           + " --target " + target
					);
			
			
			

			BufferedReader input = new BufferedReader(new InputStreamReader(
					p.getInputStream()));

			String line;

			do {

				if (this.isReadSalesforceEnded()) {
					try {
						Thread.sleep(1000);

					} catch (InterruptedException e) {
						e.printStackTrace();
					}

				}
				line = input.readLine();
				isWaitingForNewFile = line.toLowerCase().contains("waiting for a xml");
				if (!isWaitingForNewFile) {
					System.out.println(line);
				} else if (this.isReadSalesforceEnded()) {
					close();
					break;
				}

			} while (line != null);
			isAlive = false;

		} catch (Exception e1) {
			isAlive = false;
			close();
		}
	}

	public boolean isWaitingForNewFile() {
		return isWaitingForNewFile;
	}

	private boolean isWaitingForNewFile;
	boolean isAlive = false;

	public boolean isRunning() {
		return isAlive;
	}

	private boolean readSalesforceEnded;

	public void close() {
		try {
			p.destroy();
			this.interrupt();
		} catch (Exception e) {

		}
		try {
			this.interrupt();
		} catch (Exception e) {

		}

		isAlive = false;
	}

	public boolean waitSuccessfullExecution() {

		while (!this.isReadSalesforceEnded() || this.isRunning()) {
			try {
				Thread.sleep(1000);
			} catch (InterruptedException e) {
				e.printStackTrace();
				return false;
			}

		}
		
		if(p.exitValue() != 0)
		{
			return false;
		}else
		{
			return true;
		}
	

	}

	public boolean isReadSalesforceEnded() {
		return readSalesforceEnded;
	}

	public void setReadSalesforceEnded(boolean readSalesforceEnded) {
		this.readSalesforceEnded = readSalesforceEnded;
	}

}
