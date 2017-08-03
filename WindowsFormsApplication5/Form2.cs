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
using System.IO;
namespace WindowsFormsApplication5
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
           // panel2.Visible = false;
            XmlDocument doc = new XmlDocument();
            doc.Load("Data.xml");
            XmlNodeList table = doc.GetElementsByTagName("table");
            for (int i = 0; i < table.Count; i++)
            {
                comboBox1.Items.Add(table[i].Attributes["name"].InnerText);
            }
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            XmlDocument doc = new XmlDocument();
            doc.Load("Data.xml");
            int c = 0;
            string table_name = comboBox1.SelectedItem.ToString();
            string col_name = textBox2.Text;

            XmlNodeList list = doc.GetElementsByTagName(table_name);

            if (radioButton1.Checked )//Not NULL>>>>.....................
            {
                int t = 0;
                 c = 0; 
                for (int j = 0; j < list.Count; j++)
                {        
                    XmlNodeList child = list[j].ChildNodes;
                    List<string> row = new List<string>();
                    for (int k = 0; k < child.Count; k++)
                    {
                        if (child[k].Name == col_name)
                        {
                            XmlAttribute cons = doc.CreateAttribute("constraint");
                            cons.Value = "Not NULL";
                            child[k].Attributes.Append(cons);
                            if ( child[k].InnerText == "")
                            {

                                for (int T = 0; T < child.Count; T++)
                                {
                                    string name = child[T].Name;
                                    string value = child[T].InnerText;
                                    if (dataGridView1.ColumnCount < child.Count)
                                    {
                                        dataGridView1.Columns.Add(name, name);
                                    }
                                    row.Add(value);
                                }
                                c++;
                                dataGridView1.Rows.Add(row.ToArray());
                                if (radioButton1.Checked && c == 1)
                                    MessageBox.Show("please, Enter value at the empty cell");
                     
                            }
                            
                        }
                    }
                    t = j;
                }
                if (c == 0 && t == list.Count-1)
                    MessageBox.Show("no columns need to edite");
                
            }
            if (radioButton2.Checked)//Default>>>>>.............
            {
                panel2.Visible = true;
                for (int j = 0; j < list.Count; j++)
                {
                    XmlNodeList child = list[j].ChildNodes;
                    List<string> row = new List<string>();
                    for (int k = 0; k < child.Count; k++)
                    {
                        if (child[k].Name == col_name)
                        {
                            XmlAttribute cons = doc.CreateAttribute("constraint");
                            cons.Value = "Default";
                            child[k].Attributes.Append(cons);
                        }
                    }
                }
                
            }
            if (radioButton3.Checked)//checked>>>................
            {
                dataGridView1.Rows.Clear();
                
                int value = int.Parse(textBox1.Text);
                char op = char.Parse(comboBox2.SelectedItem.ToString());
                c = 0; int t = 0;
                for (int i = 0; i < list.Count; i++)
                {
                    XmlNodeList child = list[i].ChildNodes;
                    List<string> row = new List<string>();
                   
                    for (int j = 0; j < child.Count; j++)
                    {
                        
                        if (child[j].Name == col_name)
                        {
                            
                                XmlAttribute cons = doc.CreateAttribute("constraint");
                                cons.Value = "Checked";
                                child[j].Attributes.Append(cons);
                            
                            
                                if (op == '>' && (child[j].InnerText == "" ||  int.Parse(child[j].InnerText) <= value))
                                     {
                                c++;
                                for (int T = 0; T < child.Count; T++)
                                        {
                                    string name = child[T].Name;
                                    string val = child[T].InnerText;
                                    if (dataGridView1.ColumnCount < child.Count)
                                    {
                                        dataGridView1.Columns.Add(name, name);
                                    }
                                    row.Add(val);
                                }
                                dataGridView1.Rows.Add(row.ToArray());
                                
                                if(c==1)
                                MessageBox.Show("Please , change this value(s)");
                            }

                                else if (op == '<' && ( child[j].InnerText == "" ||  int.Parse(child[j].InnerText) >= value))
                            {
                                c++;
                                for (int T = 0; T < child.Count; T++)
                                {
                                    string name = child[T].Name;
                                    string val = child[T].InnerText;
                                    if (dataGridView1.ColumnCount < child.Count)
                                    {
                                        dataGridView1.Columns.Add(name, name);
                                    }
                                    row.Add(val);
                                }
                                dataGridView1.Rows.Add(row.ToArray());
                                if (c == 1)
                                MessageBox.Show("Please , change this value(s)");
                            }
                                else if (op == '=' &&( child[j].InnerText == "" || int.Parse(child[j].InnerText) != value))
                            {
                                c++;
                                for (int T = 0; T < child.Count; T++)
                                {
                                    string name = child[T].Name;
                                    string val = child[T].InnerText;
                                    if (dataGridView1.ColumnCount < child.Count)
                                    {
                                        dataGridView1.Columns.Add(name, name);
                                    }
                                    row.Add(val);
                                }
                                dataGridView1.Rows.Add(row.ToArray());
                                
                                if (c == 1)
                                MessageBox.Show("Please , change this value(s)");
                                   
                            }
                                
                        }
                      
                    }
                     t = i;
                }
                if (c == 0 && t == list.Count-1)
                    MessageBox.Show("no columns need to edite");
            }
           if (radioButton4.Checked) //Unique<----------
            {
                bool check = true; int t = 0;
                Dictionary<string, bool> dict = new Dictionary<string, bool>();
                for (int j = 0; j < list.Count; j++)
                {
                    XmlNodeList child = list[j].ChildNodes;
                    List<string> row = new List<string>();
                    for (int k = 0; k < child.Count; k++)
                    {
                        if (child[k].Name == col_name && child[k].InnerText != "")
                        {
                            XmlAttribute cons = doc.CreateAttribute("constraint");
                            cons.Value = "Unique";
                            child[k].Attributes.Append(cons);
                            if (dict.TryGetValue(child[k].InnerText, out check) == false)
                            {
                                dict[child[k].InnerText] = true;
                            }
                            else
                            {
                                  t++;
                                for (int T = 0; T < child.Count; T++)
                                {
                                    string name = child[T].Name;
                                    string value = child[T].InnerText;
                                    if (dataGridView1.ColumnCount < child.Count)
                                    {
                                        dataGridView1.Columns.Add(name, name);
                                    }
                                    row.Add(value);
                                }
                                dataGridView1.Rows.Add(row.ToArray());
                                panel1.Visible = true;
                            }
                        }
                    }
                }
                if (t > 0)
                    MessageBox.Show("please , change the values of the " + col_name.ToString() + " Column");
                else
                    MessageBox.Show("all Columns are unique");
            }
           panel1.Visible = true;
           doc.Save("Data.xml");
            }

        private void button1_Click(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("Data.xml");

            string table_name = comboBox1.SelectedItem.ToString();
            string col_name = textBox2.Text;
            XmlNodeList list = doc.GetElementsByTagName(table_name);
            if (radioButton1.Checked)  //not null
            {

                int row = 0;
                for (int r = 0; r < list.Count; r++)
                {
                    XmlNodeList child = list[r].ChildNodes;

                    for (int c = 0; c < child.Count; c++)
                    {

                        if (child[c].Name == col_name && child[c].InnerText == "")
                        {
                            child[c].InnerText = dataGridView1.Rows[row].Cells[c].Value.ToString();
                            row++;
                        }
                    }
                }
                MessageBox.Show("The constraint is applied");
                panel1.Visible = false;
                dataGridView1.Columns.Clear();
                dataGridView1.Rows.Clear();
            }
            if (radioButton2.Checked) //default
            {

                string val = textBox3.Text.ToString();
                for (int i = 0; i < list.Count; i++)
                {
                    XmlNodeList child = list[i].ChildNodes;
                    for (int j = 0; j < child.Count; j++)
                    {
                        if (child[j].Name == col_name)
                        {
                            child[j].InnerText = val;
                        }
                    }
                }
                MessageBox.Show("The constraint is applied");
                panel1.Visible = false;

            }
            if (radioButton3.Checked)  //checked
            {
                int row = 0;
                int value = int.Parse(textBox1.Text);
                char op = char.Parse(comboBox2.SelectedItem.ToString());
                int count = 0;
                for (int r = 0; r < list.Count; r++)
                {
                    XmlNodeList child = list[r].ChildNodes;

                    for (int c = 0; c < child.Count; c++)
                    {
                        if (child[c].Name == col_name)
                        {
                            if (op == '>' && (child[c].InnerText == "" || int.Parse(child[c].InnerText) <= value))
                            {
                                child[c].InnerText = dataGridView1.Rows[row].Cells[c].Value.ToString();
                                row++;
                                if (child[c].InnerText == "" || int.Parse(child[c].InnerText) <= value)
                                {
                                    count++;
                                    MessageBox.Show("please,change this value ( " + child[c].InnerText + " ) and click the (Apply Constraint) button again");
                                    break;
                                }
                            }

                            else if (op == '<' && (child[c].InnerText == "" || int.Parse(child[c].InnerText) >= value))
                            {
                                child[c].InnerText = dataGridView1.Rows[row].Cells[c].Value.ToString();
                                row++;
                                if (child[c].InnerText == "" || int.Parse(child[c].InnerText) >= value)
                                {
                                    count++;
                                    MessageBox.Show("please,change this value ( " + child[c].InnerText + " ) and click the (Apply Constraint) button again");
                                    break;
                                }
                            }
                            else if (op == '=' && (child[c].InnerText == "" || int.Parse(child[c].InnerText) != value))
                            {
                                child[c].InnerText = dataGridView1.Rows[row].Cells[c].Value.ToString();
                                row++;
                                if (child[c].InnerText == "" || int.Parse(child[c].InnerText) != value)
                                {
                                    count++;
                                    MessageBox.Show("please,change this value ( " + child[c].InnerText + " ) and click the (Apply Constraint) button again");
                                    break;
                                }
                            }
                        }
                    }
                }
                if (count == 0)
                {
                    MessageBox.Show("The constraint is applied");
                    panel1.Visible = false;
                    dataGridView1.Columns.Clear();
                    dataGridView1.Rows.Clear();
                }
            }
            if (radioButton4.Checked)
            {
                int row = 0;

                bool check = true;
                Dictionary<string, bool> dict = new Dictionary<string, bool>();
                for (int r = 0; r < list.Count; r++)
                {
                    XmlNodeList child = list[r].ChildNodes;
                    for (int c = 0; c < child.Count; c++)
                    {

                        if (child[c].Name == col_name && child[c].InnerText != "")
                        {
                            if (dict.TryGetValue(child[c].InnerText, out check) == false)
                            {
                                dict[child[c].InnerText] = true;
                            }
                            else
                            {

                                child[c].InnerText = dataGridView1.Rows[row].Cells[c].Value.ToString();

                                if (dict.TryGetValue(child[c].InnerText, out check) == true)
                                    MessageBox.Show("this value of the " + col_name + "of the person " + r + " is repeted");
                                else
                                    dict[child[c].InnerText] = true;

                                row++;

                            }
                        }
                    }
                }
            }

            doc.Save("Data.xml");
            MessageBox.Show("The constraint is applied");
            panel1.Visible = false;
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();
        }
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f = new Form1();
            f.Show();
        }
       
    }
}
