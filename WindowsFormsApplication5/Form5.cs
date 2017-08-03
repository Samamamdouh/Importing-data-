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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            if (File.Exists("Data.xml"))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load("Data.xml");
                XmlNodeList table = doc.GetElementsByTagName("table");
                for (int i = 0; i < table.Count; i++)
                {
                    comboBox1.Items.Add(table[i].Attributes["name"].InnerText);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();
            XmlDocument doc = new XmlDocument();
            doc.Load("Data.xml");
            string tablename = comboBox1.SelectedItem.ToString();
            XmlNodeList row = doc.GetElementsByTagName(tablename);
            XmlNodeList child = row[1].ChildNodes;
            for (int i = 0; i < child.Count; i++)
            {
                string name = child[i].Name;
                if (dataGridView1.ColumnCount < child.Count)
                    dataGridView1.Columns.Add(name, name);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("Data.xml");
            string tablename = comboBox1.SelectedItem.ToString();
            XmlNodeList row = doc.GetElementsByTagName(tablename);
            int c = row.Count, k = 0;
            for (int j = 0; j < dataGridView1.Rows.Count - 1; j++)
            {
                XmlNodeList child = row[c - 1].ChildNodes;
                XmlElement element = doc.CreateElement(tablename);
                for (k = 0; k < child.Count; k++)
                {
                    XmlElement node = doc.CreateElement(child[k].Name.ToString());
                    node.InnerText = dataGridView1.Rows[j].Cells[k].Value.ToString();
                    element.AppendChild(node);
                }

                // XmlElement root = (XmlElement)doc.SelectSingleNode(doc.);
                XmlElement root = doc.DocumentElement;
                root.InsertAfter(element, row[c - 1]);
                doc.Save("Data.xml");
            }
            dataGridView1.Rows.Clear();
            MessageBox.Show("Successfully Added !!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f = new Form1();
            f.Show();
        }
    }
}
