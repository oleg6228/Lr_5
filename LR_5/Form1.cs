using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_5
{
    public partial class Form1 : Form
    {
        private SortedSet<string> data = new SortedSet<string>();

        public Form1()
        {
            InitializeComponent();
            listView1.View = View.Details;
            listView1.Columns.Add("id", -2, HorizontalAlignment.Left);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;

                    string[] lines = File.ReadAllLines(filePath);
                    data = new SortedSet<string>(lines, new CustomStringComparer());

                    listView1.Items.Clear();
                    foreach (string item in data)
                    {
                        ListViewItem listItem = new ListViewItem(item);
                        listView1.Items.Add(listItem);
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string searchString = textBox1.Text;
            int comparisons = 0;

            listView1.Items.Clear();

            foreach (string item in data)
            {
                if (item.Contains(searchString))
                {
                    ListViewItem listItem = new ListViewItem(item);
                    listView1.Items.Add(listItem);
                }
                comparisons++;
            }

            label1.Text = $"кількість операцій при пошуку: {comparisons}";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;

                    List<string> filteredData = new List<string>();
                    foreach (string item in data)
                    {
                        if (item.Contains(textBox1.Text))
                        {
                            filteredData.Add(item);
                        }
                    }

                    filteredData.Sort();
                    File.WriteAllLines(filePath, filteredData);
                }
            }
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
    class CustomStringComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            bool xIsNumeric = int.TryParse(x, out int xNumeric);
            bool yIsNumeric = int.TryParse(y, out int yNumeric);

            if (xIsNumeric && yIsNumeric)
            {
                return xNumeric.CompareTo(yNumeric);
            }
            else if (xIsNumeric && !yIsNumeric)
            {
                return -1;
            }
            else if (!xIsNumeric && yIsNumeric)
            {
                return 1;
            }
            else
            {
                return x.CompareTo(y);
            }
        }
    }
}