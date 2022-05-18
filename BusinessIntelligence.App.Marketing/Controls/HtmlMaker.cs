using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BusinessIntelligence.App.Marketing
{
    public partial class HtmlMaker : UserControl
    {
        public HtmlMaker()
        {
            InitializeComponent();


        }
        public string Html
        {
            get { return txtHtml.Text; }
            set
            {
                txtHtml.Text = value;
                if (!string.IsNullOrEmpty(value))
                {
                    try
                    {
                        webBrowser1.DocumentText = value;
                        //      HtmlElement head = webBrowser1.Document.GetElementsByTagName("head")[0];
                        HtmlElement scriptEl = webBrowser1.Document.CreateElement("script");
                        HtmlElement head = webBrowser1.Document.CreateElement("head");
                        var obj = scriptEl.DomElement;
                        var t = obj.GetType().ToString();
                        var a = obj.GetType().Assembly.FullName;
                        var element = scriptEl.DomElement;
                        string alertBlocker = @"window.alert = function () { };";
                        var textAttribute = element.GetType().GetProperty("text");
                        textAttribute.SetValue(element, alertBlocker, null);
                        //element.text = alertBlocker;
                        head.AppendChild(scriptEl);
                        webBrowser1.ScriptErrorsSuppressed = true;
                    }catch(Exception ex)
                    {
                    
                    }
                }
            }
        }
        private ITemplate _Template;

        public ITemplate Template
        {
            get { return _Template; }
            set
            {
                _Template = value;
                if (_Template != null)
                {
                    Html = _Template.OriginalTemplate;
                }
            }
        }
        private void OnHtmlTextChanged(object sender, EventArgs e)
        {
            webBrowser1.DocumentText = txtHtml.Text;

          
            if (_Template != null)
            {
                _Template.OriginalTemplate = txtHtml.Text;
            }
        }
        public bool ReadOnly
        {
            get { return txtHtml.ReadOnly; }
            set { txtHtml.ReadOnly = value; }
        }


    }
}
