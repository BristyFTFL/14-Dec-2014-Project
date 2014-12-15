using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeSalaryApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string path = @"F:\info.txt";
        double showSalary;
        double sum = 0;
        private void saveButton_Click(object sender, EventArgs e)
        {
            string empInfo = nameTextBox.Text + "," +idTextBox.Text + "," + salaryTextBox.Text;
            
            FileStream dataSaveFileStream = new FileStream(path, FileMode.Append);
            StreamWriter dataSaveStreamWriter = new StreamWriter(dataSaveFileStream);
            dataSaveStreamWriter.Write(empInfo);
            dataSaveStreamWriter.WriteLine();
            //MessageBox.Show("Data are saved successfully");
            //nameTextBox.Clear();
            //idTextBox.Clear();
            //salaryTextBox.Clear();
            dataSaveStreamWriter.Close();
            dataSaveFileStream.Close();
        }
        
        private void showButton_Click(object sender, EventArgs e)
        {
            FileStream dataSaveFileStream = new FileStream(path,FileMode.Open);
            StreamReader dataSaveStreamReader = new StreamReader(dataSaveFileStream);
            showListBox.Items.Clear();
            
            while (!dataSaveStreamReader.EndOfStream)
            {
                string showEmpInfo = dataSaveStreamReader.ReadLine();
                showListBox.Items.Add(showEmpInfo);
                string[] salary = showEmpInfo.Split(',');
                showSalary = Convert.ToDouble(salary[2]);
                sum += showSalary;
            }
            totalAmountTextBox.Text = Convert.ToString(sum);
            dataSaveStreamReader.Close();
            dataSaveFileStream.Close();
        }
    }
}
