package com.peixeurbano.salesforce.replica.task;

import com.amazonaws.services.s3.AmazonS3Client;
import java.text.SimpleDateFormat;
import java.util.*;

public class AmazonS3ClientFactory {

	private int totalClients;
	private List<AmazonS3Client> releasedClients = new ArrayList<AmazonS3Client>();

	private Hashtable<Thread, AmazonS3Client> threadObjectProvidedList = new Hashtable<Thread, AmazonS3Client>();

	public AmazonS3Client getAmazonS3Client() throws InterruptedException {
		System.out.println(new SimpleDateFormat("yyyy-MM-dd HH:mm:ss").format(new Date()) + " AmazonS3Client=> Openeds: " + totalClients + ", Busy: " + (totalClients - this.releasedClients.size()) +".");

		return getAmazonS3Client(0);
	}
	private AmazonS3Client getAmazonS3Client(int attemptIndex) throws 
			InterruptedException {
		if (releasedClients.size() > 0) {
			AmazonS3Client bc = this.releasedClients.remove(0);
					System.out.println(new SimpleDateFormat("yyyy-MM-dd HH:mm:ss").format(new Date()) + " AmazonS3Client provided to thread "  + Thread.currentThread().getId() + "." );
			return bc;
		} else if (getMaxQtyClients() < 1
				|| totalClients < getMaxQtyClients()) {
			totalClients++;
			System.out.println(new SimpleDateFormat("yyyy-MM-dd HH:mm:ss").format(new Date()) + " AmazonS3Client provided to thread "  + Thread.currentThread().getId() + "." );

			return new AmazonS3Client();
		} else {
			Enumeration<Thread> e = threadObjectProvidedList.keys();
			boolean releasedAuto = false;
			while(e.hasMoreElements())
			{
				Thread t = e.nextElement();
				if(!t.isAlive())
				{
					this.releaseClient(t,threadObjectProvidedList.remove(t));
					releasedAuto = true;
				}
			}
			if(releasedAuto)
			{
				return getAmazonS3Client(attemptIndex + 1);
			}else
			{
			
				if(attemptIndex % 10 == 0 || attemptIndex == 0)
				{
					System.out.println(new SimpleDateFormat("yyyy-MM-dd HH:mm:ss").format(new Date()) + " Thread " + Thread.currentThread().getId() + " waiting for AmazonS3Client release ...");
				
				}
				Thread.sleep(1000);
				return getAmazonS3Client(attemptIndex+ 1);
			}
		}
	}
	private void releaseClient(Thread t, AmazonS3Client Client) {
		this.threadObjectProvidedList.remove(t.getId());
		releasedClients.add(Client);
		System.out.println(new SimpleDateFormat("yyyy-MM-dd HH:mm:ss").format(new Date())
				+ " Thread " + t.getId() + " released AmazonS3Client.");
	}
	public void releaseClient(AmazonS3Client Client) {
		
		releaseClient(Thread.currentThread(), Client);
		
	}
	


	private int maxQtyClients = 0;

	public int getMaxQtyClients() {
		return maxQtyClients;
	}

	public void setMaxQtyClients(int maxQtyClients) {
		this.maxQtyClients = maxQtyClients;
	}

	public int getTotalClients() {
		return totalClients;
	}

	public void setTotalClients(int totalClients) {
		this.totalClients = totalClients;
	}
}
