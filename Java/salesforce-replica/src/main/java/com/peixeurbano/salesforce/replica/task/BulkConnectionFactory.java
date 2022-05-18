package com.peixeurbano.salesforce.replica.task;

import com.sforce.async.AsyncApiException;
import com.sforce.async.BulkConnection;
import com.sforce.ws.ConnectorConfig;

import java.text.SimpleDateFormat;
import java.util.*;

public class BulkConnectionFactory {

	public BulkConnectionFactory(ConnectorConfig config) {
		setConfig(config);

	}

	private BulkConnection getNewConnection() throws AsyncApiException {
		String soapEndpoint = getConfig().getServiceEndpoint();
		ConnectorConfig cc = new ConnectorConfig();
		cc.setSessionId(getConfig().getSessionId());
		cc.setRestEndpoint(soapEndpoint.substring(0,soapEndpoint.indexOf("Soap/")) + "async/27.0");
		cc.setCompression(true);
		cc.setTraceMessage(false);
	cc.setConnectionTimeout(0);
		return new BulkConnection(cc);
	}

	private int totalConnections;
	private List<BulkConnection> releasedConnections = new ArrayList<BulkConnection>();

	private Hashtable<Thread, BulkConnection> threadObjectProvidedList = new Hashtable<Thread, BulkConnection>();
    private boolean gettingConnection = false;

	public BulkConnection getBulkConnection()

			throws AsyncApiException, InterruptedException {
        if (gettingConnection)
        {
           Thread.sleep(1000);
            return getBulkConnection();
        }
		
        gettingConnection = true;
//		System.out.println(new SimpleDateFormat("yyyy-MM-dd HH:mm:ss").format(new Date()) + " BulkConnection=> Openeds: " + totalConnections + ", Busy: " + (totalConnections - this.releasedConnections.size()) +".");
		/*
		if (releasedConnections.size() > 0) {
			BulkConnection bc = this.releasedConnections.remove(0);
			this.threadObjectProvidedList.put(Thread.currentThread(), bc);
			System.out.println(new SimpleDateFormat("yyyy-MM-dd HH:mm:ss").format(new Date()) + " BulkConnection provided to thread " + Thread.currentThread().getId() + ".");
			System.out.println(new SimpleDateFormat("yyyy-MM-dd HH:mm:ss").format(new Date()) + " BulkConnection=> Openeds: " + totalConnections + ", Busy: " + (totalConnections - this.releasedConnections.size()) +".");
			gettingConnection = false;
			return bc;
		}
		else  */
		if (getMaxQtyConnections() < 1 || totalConnections < getMaxQtyConnections()) {
	
			System.out.println(new SimpleDateFormat("yyyy-MM-dd HH:mm:ss").format(new Date()) + " BulkConnection provided to thread " + Thread.currentThread().getId() + ".");
			BulkConnection bc = getNewConnection();
			totalConnections++;
			this.threadObjectProvidedList.put(Thread.currentThread(), bc);
			gettingConnection = false;
			return bc;
		} else {
			/*
			Enumeration<Thread> e = threadObjectProvidedList.keys();
			boolean releasedAuto = false;
			while(e.hasMoreElements())
			{
				Thread t = e.nextElement();		
				if(!t.isAlive())
				{
					this.releaseConnection(t,threadObjectProvidedList.remove(t));
					releasedAuto = true;
				}
			}
			
			if(releasedAuto)
			{
				return getBulkConnection();
			}
*/
			Thread.sleep(1000);
			return getBulkConnection();
			}
		}
	
	
	private void releaseConnection(Thread t, BulkConnection connection) {
		this.threadObjectProvidedList.remove(t.getId());
		if(!releasedConnections.contains(connection))
		{
		releasedConnections.add(connection);
		totalConnections--;
		System.out.println(new SimpleDateFormat("yyyy-MM-dd HH:mm:ss").format(new Date())
				+ " Thread " + t.getId() + " released BulkConnection.");
		}else
		{
			System.out.println(new SimpleDateFormat("yyyy-MM-dd HH:mm:ss").format(new Date())
					+ " Thread " + t.getId() + " tried to release a BulkConnection released.");
		}
	}
	public void releaseConnection(BulkConnection connection) {
		
		releaseConnection(Thread.currentThread(), connection);
		
	}

	private int maxQtyConnections = 0;
	private ConnectorConfig config;

	public ConnectorConfig getConfig() {
		return config;
	}

	public void setConfig(ConnectorConfig config) {
		this.config = config;
	}

	public int getMaxQtyConnections() {
		return maxQtyConnections;
	}

	public void setMaxQtyConnections(int maxQtyConnections) {
		this.maxQtyConnections = maxQtyConnections;
	}

	public int getTotalConnections() {
		return totalConnections;
	}

	public void setTotalConnections(int totalConnections) {
		this.totalConnections = totalConnections;
	}
}
