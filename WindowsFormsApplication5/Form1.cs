using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.IO;
using System.Data.OleDb;
using System.Collections;

namespace WindowsFormsApplication5
{
    public partial class Form1 : Form
    {
        XmlDocument doc = new XmlDocument();
        private string Excel03ConString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
        private string Excel07ConString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";


        public Form1()
        {
            InitializeComponent();
            //doc.Load("Data.xml");
        }

       

      

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }
       
        private void button6_Click_1(object sender, EventArgs e)
        {
           
            this.Hide();
            Form5 f = new Form5();
            f.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            this.Hide();
            f.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            XmlWriter XTW = XmlWriter.Create("Data.xml");
            FileStream f = new FileStream("data.txt", FileMode.Open);
            StreamReader sr = new StreamReader(f);
            XTW.WriteStartDocument();
            char dlimiter = char.Parse(textBox1.Text);
            string rec;
            int c = 0;
            string[] columns = { };

            string table_name = "";

            while (sr.Peek() != -1)
            {

                rec = sr.ReadLine();
                if (rec == "table")
                {
                    table_name = sr.ReadLine();
                    XTW.WriteStartElement("table");
                    XTW.WriteAttributeString("name", table_name);


                    c = 1;
                    if (c == 1)
                    {
                        c++;

                        string names = sr.ReadLine();
                        columns = names.Split(dlimiter);
                        rec = sr.ReadLine();
                    }
                }
                if (c >= 2)
                {
                    string[] fields = rec.Split(dlimiter);
                    XTW.WriteStartElement(table_name);
                    for (int i = 0; i < fields.Length; i++)
                    {
                        XTW.WriteStartElement(columns[i]);
                        XTW.WriteString(fields[i]);
                        XTW.WriteEndElement(); //col
                    }
                    XTW.WriteEndElement(); //ChildNode
                }
            }
            XTW.WriteEndElement(); //table
            XTW.WriteEndDocument();
            XTW.Close();
            MessageBox.Show("import");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 f3 = new Form3();
            f3.Show();
        }
    }
 }

 