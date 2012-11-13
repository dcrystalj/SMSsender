using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace sms
{
    public partial class Form1 : Form
    {
        public String username = ""; //enter your najdi.si username here
        public String password = ""; //enter your najdi.si password here
        public Form1()
        {
            InitializeComponent();
            webBrowser1.Visible = true;
        }
        int a = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("https://id.najdi.si/login");
            webBrowser1.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(webBrowser1_DocumentCompleted);
          
        }

        void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            string navi = webBrowser1.Url.ToString();
            if (navi == "https://id.najdi.si/login")
            {
                webBrowser1.Document.GetElementById("j_username").InnerText=username;
                webBrowser1.Document.GetElementById("j_password").InnerText=password;
                webBrowser1.Document.GetElementById("submit").InvokeMember("Click");
                a++;
                                
            }
            else if (navi == "http://www.najdi.si/index.jsp")
            {

                webBrowser1.Document.GetElementById("icon_freeSMS").InvokeMember("Click");
            
                string s = webBrowser1.DocumentText;
                string stevilo = fs(s, "sms_so_l_");
                try
                {
                    if (textBox1.Text.Substring(0, 3).Equals("031"))
                        webBrowser1.Document.GetElementById("sms_so_ac_" + stevilo).SetAttribute("selectedIndex", "1");
                    else if (textBox1.Text.Substring(0, 3).Equals("041"))
                        webBrowser1.Document.GetElementById("sms_so_ac_" + stevilo).SetAttribute("selectedIndex", "2");
                    else if (textBox1.Text.Substring(0, 3).Equals("051"))
                        webBrowser1.Document.GetElementById("sms_so_ac_" + stevilo).SetAttribute("selectedIndex", "3");
                    else if (textBox1.Text.Substring(0, 3).Equals("040"))
                        webBrowser1.Document.GetElementById("sms_so_ac_" + stevilo).SetAttribute("selectedIndex", "5");
                    else { }
                    webBrowser1.Document.GetElementById("sms_so_l_" + stevilo).InnerText = textBox1.Text.Substring(3, 6);
                    webBrowser1.Document.GetElementById("sms_message_" + stevilo).InnerText = textBox2.Text;
                    webBrowser1.Document.GetElementById("doitsms").InvokeMember("Click");
                    label3.Text = "Poslano";
                    webBrowser1.Document.InvokeScript("clearInputFields();sendNextMessage();");
                }
                catch { }
            }
            else
            {
                webBrowser1.Navigate("http://www.najdi.si/index.jsp");
            }
            
        }
        public string fs(string text, string s)
        {
            for (int i = 0; i < text.Length - 50; i++)
            {
                if (text.Substring(i, s.Length).Equals(s))
                    return text.Substring(i + s.Length, 13);
            }
            return s;

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

   
    }
}
