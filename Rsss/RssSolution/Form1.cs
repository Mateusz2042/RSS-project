﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using Rsss.DatabaseWriter;
using HtmlAgilityPack;
using System.Xml;

namespace Rsss
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        RssManager reader = new RssManager();
        Collection<Rss.Items> list;
        ListViewItem row;

        private void Form1_Load(object sender, EventArgs e)
        {
            listView1.View = View.Details;
            listView1.Columns.Clear();
            listView1.Columns.Add("Title");
            listView1.Columns.Add("Link");
            listView1.Columns.Add("Date publishing");
            listView1.Items.Clear();
            listView1.Columns[0].Width = 200;
            listView1.Columns[1].Width = 280;
            listView1.Columns[2].Width = 100;
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(listView1.SelectedItems[0].SubItems[1].Text);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (listView1.SelectedItems.Count == 1)
            //{
            //    for (int i = 0; i < list.Count; i++)
            //    {
            //        if (list[i].Title == listView1.SelectedItems[0].Text)
            //        {
            //            textBox2.Text = list[i].Description.Substring(0, 200);
            //        }
            //    }
            //}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DbWriter.Write();
            
        }

        List<string> AllLinks = new List<string>();
        List<string> XmlLinks = new List<string>();

        private void button3_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            AllLinks.Clear();
            XmlLinks.Clear();

            HtmlWeb hw = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc = hw.Load("http://www.rss.lostsite.pl/index.php?rss=32");
            foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]"))
            {
                string hrefValue = link.GetAttributeValue("href", string.Empty);
                AllLinks.Add(hrefValue);
            }

            int size = 0;
            for (int i = 0; i < AllLinks.Count; i++)
            {
                size = AllLinks[i].Length;
                if (AllLinks[i][AllLinks[i].Length - 3] == 'x' && AllLinks[i][AllLinks[i].Length - 2] == 'm' && AllLinks[i][AllLinks[i].Length - 1] == 'l')
                {
                    XmlLinks.Add(AllLinks[i]);
                }
            }
            for (int i = 0; i < XmlLinks.Count; i++)
            {
                comboBox1.Items.Add(XmlLinks[i]);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            try
            {
                reader.Url = comboBox1.Text;
                reader.GetFeed();
                list = reader.RssItems;

                for (int i = 0; i < list.Count; i++)
                {
                    row = new ListViewItem();
                    row.Text = list[i].Title;
                    row.SubItems.Add(list[i].Link);
                    row.SubItems.Add(list[i].Date.ToShortDateString());
                    listView1.Items.Add(row);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
        }
    }
}
