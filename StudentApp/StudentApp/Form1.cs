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
using CSVLib;

namespace StudentApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string filePath = "F:\\studentRecord.csv";
        private void saveButton_Click(object sender, EventArgs e)
        {
            string studentReg = studentRegTextBox.Text;
            string studentName = studentNameTextBox.Text;
            if (File.Exists(filePath))
            {
                bool uniqueReg = UniqueCheker(studentReg);

                if (uniqueReg == true)
                {
                    bool fileCreate = fileCreateMethods(filePath, studentReg, studentName);
                    if (fileCreate == true)
                    {
                        MessageBox.Show("Data Saved Successfully");
                    }
                }
                else
                {
                    MessageBox.Show("This Reg No already taken");
                }
            }
            else
            {
                bool fileCreate = fileCreateMethods(filePath, studentReg, studentName);
                if (fileCreate == true)
                {
                    MessageBox.Show("Data Saved Successfully");
                }
            }
        }
        private bool fileCreateMethods(string filePath, string studentReg, string studentName)
        {
            FileStream aFileStream = new FileStream(filePath, FileMode.Append);
            CsvFileWriter aWriter = new CsvFileWriter(aFileStream);
            List<string> aStudentRecord = new List<string>();
            aStudentRecord.Add(studentReg);
            aStudentRecord.Add(studentName);
            aWriter.WriteRow(aStudentRecord);
            aFileStream.Close();
            return true;
        }

        private bool UniqueCheker(string studentReg)
        {
            FileStream aFileStream = new FileStream(filePath, FileMode.Open);
            CsvFileReader aCsvFileReader = new CsvFileReader(aFileStream);
            List<string> aStudentDataRead = new List<string>();
            bool a = true;
            while (aCsvFileReader.ReadRow(aStudentDataRead))
            {
                string studentRegNo = aStudentDataRead[0];
                if (studentRegNo == studentReg)
                {
                    a = false;
                    break;
                }
                else
                {
                    a = true;
                }
            }
            aFileStream.Close();
            return a;
        }

        private void showButton_Click(object sender, EventArgs e)
        {
            FileStream aFileStream = new FileStream(filePath, FileMode.Open);
            CsvFileReader aCsvFileReader = new CsvFileReader(aFileStream);
            List<string> aStudentDataRead = new List<string>();
            showStudentDataListBox.Items.Clear();
            while (aCsvFileReader.ReadRow(aStudentDataRead))
            {
                string studentRegNo = aStudentDataRead[0];
                string studentName = aStudentDataRead[1];
                showStudentDataListBox.Items.Add(studentRegNo + " " + studentName);
            }
            aFileStream.Close();
        }
    }
}
