using System;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using System.Xml.Linq;
using System.Drawing.Text;

namespace SP3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Binary Files (*.bin)|*.bin|All Files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (FileStream fs = new FileStream(openFileDialog.FileName, FileMode.Open))
                        {

                            using (BinaryReader br = new BinaryReader(fs, Encoding.Default))
                            {
                                int intValue = br.ReadInt32();
                                double doubleValue = br.ReadDouble();
                                char charValue = br.ReadChar();
                                string stringValue = br.ReadString();
                                bool boolValue = br.ReadBoolean();

                                textBox1.AppendText($"Int value: {intValue}\n ");
                                textBox1.AppendText($"Double value: {doubleValue}\n ");
                                textBox1.AppendText($"Char value: {charValue}\n ");
                                textBox1.AppendText($"String value: {stringValue}\n ");
                                textBox1.AppendText($"Boolean value: {boolValue}\n ");
                                fs.Close();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при чтении файла: " + ex.Message);

                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Binary Files (*.bin)|*.bin|All Files (*.*)|*.*";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.Create))
                        {
                            using (BinaryWriter bw = new BinaryWriter(fs, GetEncoding()))
                            {
                                string fileContent = textBox1.Text;
                                bw.Write(fileContent);
                            }
                        }
                        MessageBox.Show("Файл успешно сохранен.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при сохранении файла: " + ex.Message);
                    }
                }
            }
        }

        private Encoding GetEncoding()
        {
            if (radioButton1.Checked)
            {
                return Encoding.UTF8;
            }
            else if (radioButton2.Checked)
            {
                return Encoding.Unicode;
            }
            else if (radioButton3.Checked)
            {
                return Encoding.ASCII;
            }
            else
            {
                return Encoding.Default;
            }
        }
    }
}