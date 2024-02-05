using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using CoursWork_Etap1.Constants;
using CoursWork_Etap1.Services;
using CoursWork_Etap1.Shapes;
using CoursWork_Etap1.Utilities.Interfaces;

namespace CoursWork_Etap1
{
    public partial class ExportForm : Form
    {
        public ExportForm()
        {
            _serializeShapeService = new SerializeShapeService();
            InitializeComponent();
        }

        private readonly List<Shape> _shapes;

        private readonly ISerializeShapeService _serializeShapeService;

        public ExportForm(List<Shape> shapes)
        {
            this._shapes = shapes;
            _serializeShapeService = new SerializeShapeService();
            InitializeComponent();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (checkBoxTxt.Checked)
            {
                var saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Text files (*.txt)|*.txt";
                saveFileDialog.FileName = FileLocation.FileLocationTxt;
                saveFileDialog.InitialDirectory = FileLocation.FilesFolderPath;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    _serializeShapeService.SerializeToTxtFile(_shapes, saveFileDialog.FileName);
                    DialogResult = DialogResult.OK;
                }
            }
            else if (checkBoxJson.Checked)
            {
                var saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "JSON files (*.json)|*.json";
                saveFileDialog.FileName = FileLocation.FileLocationJson;
                saveFileDialog.InitialDirectory = FileLocation.FilesFolderPath;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    _serializeShapeService.SerializeToJsonFile(_shapes, saveFileDialog.FileName);
                    DialogResult = DialogResult.OK;
                }
            }
            else if (checkBoxXml.Checked)
            {
                var saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "XML files (*.xml)|*.xml";
                saveFileDialog.FileName = FileLocation.FileLocationXml ;
                saveFileDialog.InitialDirectory = FileLocation.FilesFolderPath;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    _serializeShapeService.SerializeToXmlFile(_shapes, saveFileDialog.FileName);
                    DialogResult = DialogResult.OK;
                }

            }
            else if (checkBoxJpeg.Checked)
            {
                var saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "JPEG files (*.jpeg)|*.jpeg";
                saveFileDialog.FileName = FileLocation.FileLocationJpeg;
                saveFileDialog.InitialDirectory = FileLocation.FilesFolderPath;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    GraphicsForm graphicsForm = new GraphicsForm();
                    PictureBox pictureBox = graphicsForm.GetPictureBox();
                    Bitmap bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
                    pictureBox.DrawToBitmap(bitmap, pictureBox.ClientRectangle);

                    //getting a graphics object to draw onto the bitmap
                    using (Graphics graphics = Graphics.FromImage(bitmap))
                    {
                        foreach (var shape in _shapes)
                        {
                            Pen pen = new Pen(shape.ColorBorder, 0.4f);
                            SolidBrush brush = new SolidBrush(shape.FillColor);

                            //drawing the shape onto the bitmap
                            shape.DrawShape(graphics, pen, brush);

                            pen.Dispose();
                            brush.Dispose();
                        }
                    }
                    //saving the bitmap as a JPEG file
                    bitmap.Save(saveFileDialog.FileName, ImageFormat.Jpeg);
                    DialogResult = DialogResult.OK;
                }
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void checkBoxTxt_Click(object sender, EventArgs e)
        {
            checkBoxJson.Checked = false;
            checkBoxXml.Checked = false;
        }

        private void checkBoxJson_Click(object sender, EventArgs e)
        {
            checkBoxTxt.Checked = false;
            checkBoxXml.Checked = false;
        }

        private void checkBoxXml_Click(object sender, EventArgs e)
        {
            checkBoxJson.Checked = false;
            checkBoxTxt.Checked = false;
        }

        private static void CreateMessageBox(string message, string caption, MessageBoxButtons messageBoxButtons,
            MessageBoxIcon messageBoxIcon) =>
            MessageBox.Show(message, caption, messageBoxButtons, messageBoxIcon);

    }
}
