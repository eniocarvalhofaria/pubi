using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Windows;
using System.Text;
using System.IO;
using System.Data.SqlClient;

namespace BusinessIntelligence.MIME
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
            /*
			MimeMessage mail = new MimeMessage();
			mail.SetDate();
			mail.Setversion();
			mail.SetFrom("sender@local.com",null);
			mail.SetTo("recipient1@server1.com, Nick Name <recipient2@server1.com>, \"Nick Name\" <recipient3@server3.com>, \"—ÓÃŒ\" <recipient5@server1.com>",null);
			mail.SetCC("recipient4@server4.com",null);
			mail.SetSubject("≤‚ ‘≥Ã–Ú",null);//charset gb2312
			mail.SetFieldValue("X-Priority", "3 (Normal)", null);

			// Initialize header
			mail.SetContentType("multipart/mixed");

			// generate a boundary string automatically
			// if the parameter is NULL
			mail.SetBoundary(null);

			// Add a text body part
			// default Content-Type is "text/plain"
			// default Content-Transfer-Encoding is "7bit"
			MimeBody mBody = mail.CreatePart();
			mBody.SetText("Hi, there");  // set the content of the body part

 

			// Add a file attachment body part
			mBody = mail.CreatePart();
			mBody.SetDescription("enclosed photo",null);
			mBody.SetTransferEncoding("base64");
			// if Content-Type is not specified, it'll be
			// set to "image/jpeg" by ReadFromFile()
			mBody.ReadFromFile(".\\00.jpg"); 

 

			// Generate a simple message
			MimeMessage mail2 = new MimeMessage();
			mail2.SetFrom("abc@abc.com", null);
			mail2.SetTo("abc@abc.com",null);
			mail2.SetSubject("This is an attached message",null);
			mail2.SetText("Content of attached message.\r\n");


			// Attach the message
			mBody = mail.CreatePart();
			mBody.SetDescription("enclosed message",null);
			mBody.SetTransferEncoding("7bit");
			// if Content-Type is not specified, it'll be
			// set to "message/rfc822" by SetMessage()
			mBody.SetMessage(mail2); 


			// Add an embeded multipart
			mBody = mail.CreatePart();
			mBody.SetContentType("multipart/alternative");
			mBody.SetBoundary("embeded_multipart_boundary");
			MimeBody mBodyChild = mBody.CreatePart();
			mBodyChild.SetText("Content of Part 1\r\n");
			mBodyChild = mBody.CreatePart();
			mBodyChild.SetText("Content of Part 2\r\n");


			//store content to a string buffer
			StringBuilder sb = new StringBuilder();
			mail.StoreBody(sb);

			StreamWriter sw = new StreamWriter(".\\aaa.txt");
			sw.Write(sb.ToString());
			sw.Close();
			*/

			StreamReader sr = new StreamReader(@"C:\Users\enio.faria\Desktop\teste.eml");
			string message = sr.ReadToEnd();
			MimeMessage aMimeMessage = new MimeMessage();
			aMimeMessage.LoadBody(message);
            var bodylist = aMimeMessage.GetBodyPartList();
			for(int i=0;i<bodylist.Count;i++)
			{
				MimeBody ab = (MimeBody) bodylist[i];
				
				if(ab.IsAttachment())
				{
					ab.WriteToFile(ab.GetName());
				}else if(ab.IsText())
				{
					string m = ab.GetText();
					System.Windows.Forms.MessageBox.Show(m);
				}
			}

			Application.Run(new Form1());
		}
	}
}
