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

namespace Notepad
{
    public partial class Form1 : Form
    {
        public string fileName;
        public bool isFileChanged;
        public Form1()
        {
            InitializeComponent();

            Init();
        }

        private void Init()
        {
            fileName = "";
            isFileChanged = false;
            UpdateTitle();
        }
        private void CreateNewDocment(object sender, EventArgs e)
        {
            SaveUnsavedFile();
            richTextBox1.Text = "";
            fileName = "";
            isFileChanged = false;
            UpdateTitle();
        }
        private void OpenFile(object sender, EventArgs e)
        {
            SaveUnsavedFile();
            openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK) 
            {
                try
                {
                    StreamReader streamReader = new StreamReader(openFileDialog1.FileName);
                    richTextBox1.Text = streamReader.ReadToEnd();
                    streamReader.Close();
                    fileName = openFileDialog1.FileName;
                    isFileChanged = false;
                }
                catch
                {
                    MessageBox.Show("Невозможно открыть файл");
                }
            }
        }
        private void SaveFile(string _fileName)
        {
            if (_fileName == "")
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    _fileName = saveFileDialog1.FileName;
                }
            }
            try
            {
                StreamWriter streamWriter = new StreamWriter(_fileName + ".txt");
                streamWriter.Write(richTextBox1.Text);
                streamWriter.Close();
                fileName = _fileName;
                isFileChanged = false;
            }
            catch
            {
                MessageBox.Show("Невозможно сохранить файл");
            }
            UpdateTitle();
        }
        private void Save(object sender, EventArgs e)
        {
            SaveFile(fileName);
        }
        private void SaveAs(object sender, EventArgs e)
        {
            SaveFile("");
        }
        private void OnTextChanged(object sender, EventArgs e)
        {
            if (!isFileChanged)
            {
                this.Text = this.Text.Replace('*', ' ');
                isFileChanged = true;
                this.Text = '*' + this.Text;
            }
        }
        private void UpdateTitle()
        {
            if (fileName != "")
            {
                this.Text = fileName + " - Блокнот";
            }
            else
            {
                this.Text = "Безымянный - Блокнот";
            }
            
        }
        private void SaveUnsavedFile()
        {
            if (isFileChanged)
            {
                DialogResult dialogResult = MessageBox.Show("Сохранить изменения в файле?", "Сохранение файла", MessageBoxButtons.YesNo, MessageBoxIcon.Question ,MessageBoxDefaultButton.Button1);
                if (dialogResult == DialogResult.Yes)
                {
                    SaveFile(fileName);
                }
            }
        }

        private void CloseForm(object sender, EventArgs e)
        {
            SaveUnsavedFile();
            Application.Exit();
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = printDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {

            }
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fnt = new FontDialog();
            if (fnt.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.Font = fnt.Font;
            }
        }

        private void colorToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            ColorDialog clr = new ColorDialog();
            if (clr.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.ForeColor = clr.Color;
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Приложение разработано Назиповой Айгуль", "О программе", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectedText != "")
            {
                richTextBox1.Cut();
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionLength > 0)
            {
                richTextBox1.Copy();
            }
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Clipboard.GetDataObject().GetDataPresent(DataFormats.Text) == true)
            {
                richTextBox1.Paste();
            }
        }
    }
}
