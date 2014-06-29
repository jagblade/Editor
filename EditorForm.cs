using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Editor
{
    public partial class EditorForm : Form
    {
        public EditorForm()
        {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Editor files | *.edt;*.xedt";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Document doc = null;
                    using (Stream file = dialog.OpenFile())
                    {
                        if (dialog.FileName.ToLower().EndsWith(".edt"))
                        {
                            BinaryFormatter formatter = new BinaryFormatter();
                            doc = (Document)formatter.Deserialize(file);
                        }
                        else if (dialog.FileName.ToLower().EndsWith(".xedt"))
                        {
                            XmlSerializer serializer = new XmlSerializer(typeof(Document));
                            doc = (Document)serializer.Deserialize(file);
                        }
                        else
                        {
                            throw new InvalidOperationException("Unexpected file type");
                        }
                    }

                    txtTitle.Text = doc.Title;
                    txtAuthor.Text = doc.Author;
                    txtContent.Text = doc.Content;
                }
                catch (Exception)
                {
                    MessageBox.Show("Cannot load file");
                }
            }
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Missing title");
                return;
            }

            if (String.IsNullOrWhiteSpace(txtAuthor.Text))
            {
                MessageBox.Show("Missing author");
                return;
            }

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Editor files | *.edt;*.xedt";


            if (dialog.ShowDialog() == DialogResult.OK)
            {
                using (Stream file = dialog.OpenFile())
                {
                    Document doc = new Document();
                    doc.Author = txtAuthor.Text.Trim();
                    doc.Title = txtTitle.Text.Trim();
                    doc.Content = txtContent.Text;

                    if (dialog.FileName.ToLower().EndsWith(".edt"))
                    {
                        BinaryFormatter formatter = new BinaryFormatter();
                        formatter.Serialize(file, doc);
                    }
                    else if (dialog.FileName.ToLower().EndsWith(".xedt"))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(Document));
                        serializer.Serialize(file, doc);
                    }
                }
            }
        }

    }
}
