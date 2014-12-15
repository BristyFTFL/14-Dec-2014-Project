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

namespace PersonInfomationApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string filePath = "E:\\personInfo.csv";
        List<string> aPersonList = new List<string>();
        private void saveButton_Click(object sender, EventArgs e)
        {
            string personName = nameTextBox.Text;
            string cellNo = cellNoTextBox.Text;
            string email = emailTextBox.Text;


            if (File.Exists(filePath))
            {
                bool uniqueNumber = UniqueCheker(cellNo);

                if (uniqueNumber == true)
                {
                    bool fileCreate = fileCreateMethods(filePath, personName, cellNo, email);
                    if (fileCreate == true)
                    {
                        MessageBox.Show("Successfully Added");
                    }
                }
                else
                {
                    MessageBox.Show("Registration Number Already added");
                }
            }
            else
            {
                bool fileCreate = fileCreateMethods(filePath, personName, cellNo, email);
                if (fileCreate == true)
                {
                    MessageBox.Show("Successfully Added");
                }
            }
        }

        private bool fileCreateMethods(string filePath, string personName, string cellNo, string email)
        {
            FileStream aFileStream = new FileStream(filePath, FileMode.Append);
            CsvFileWriter aWriter = new CsvFileWriter(aFileStream);

           

            aPersonList.Add(personName);
            aPersonList.Add(cellNo);
            aPersonList.Add(email);

            nameTextBox.Clear();
            cellNoTextBox.Clear();
            emailTextBox.Clear();


            aWriter.WriteRow(aPersonList);
            aFileStream.Close();
            return true;
        }

        private bool UniqueCheker(string cellNo)
        {
            int cellNoInt = Convert.ToInt32(cellNo);
            FileStream aFileStream = new FileStream(filePath, FileMode.Open);
            CsvFileReader aCsvFileReader = new CsvFileReader(aFileStream);
            List<string> aPersonDataRead = new List<string>();
            bool a = true;
            while (aCsvFileReader.ReadRow(aPersonDataRead))
            {
                int cellNoInt2 = Convert.ToInt32(aPersonDataRead[1]);
                if (cellNoInt == cellNoInt2)
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
            List<string> aPersonDataRead = new List<string>();
            showlistView.Items.Clear();
            while (aCsvFileReader.ReadRow(aPersonDataRead))
            {
                string personNameShow = aPersonDataRead[0];
                string cellNoShow = aPersonDataRead[1];
                string emailShow = aPersonDataRead[2];
                showlistView.Items.Add(personNameShow + " " + cellNoShow + " " + emailShow);
            }
            aFileStream.Close();
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            FileStream aFileStream = new FileStream(filePath, FileMode.Open);
            CsvFileReader aCsvFileReader = new CsvFileReader(aFileStream);
            List<string> aPersonDataRead = new List<string>();
            showlistView.Items.Clear();
            searchTextBox.Clear();
            string searchName = searchTextBox.Text.ToLower();
            while (aCsvFileReader.ReadRow(aPersonDataRead))
            {
                if (nameRadioButton.Checked)
                {
                    string name = aPersonDataRead[0].ToLower();
                    if (name.Contains(searchName))
                    {

                        showlistView.Items.Add(name);
                    }
                    else
                    {
                        continue;
                    }
                }

                else if (cellNoRadioButton.Checked)
                {
                    string cell = aPersonDataRead[1].ToLower();
                    if (cell.Contains(searchName))
                    {

                        showlistView.Items.Add(cell);
                    }
                    else
                    {
                        continue;
                    }
                }
                else if (emailRadioButton.Checked)
                {
                    string email = aPersonDataRead[2].ToLower();
                    if (email.Contains(searchName))
                    {

                        showlistView.Items.Add(email);
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            aFileStream.Close();
            
        }
    }
}
