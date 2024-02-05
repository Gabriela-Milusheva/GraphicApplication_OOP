using System.ComponentModel;

namespace CoursWork_Etap1
{
    partial class ExportForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.checkBoxTxt = new System.Windows.Forms.CheckBox();
            this.checkBoxXml = new System.Windows.Forms.CheckBox();
            this.checkBoxJson = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.checkBoxJpeg = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // checkBoxTxt
            // 
            this.checkBoxTxt.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBoxTxt.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxTxt.Location = new System.Drawing.Point(11, 38);
            this.checkBoxTxt.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBoxTxt.Name = "checkBoxTxt";
            this.checkBoxTxt.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBoxTxt.Size = new System.Drawing.Size(64, 50);
            this.checkBoxTxt.TabIndex = 2;
            this.checkBoxTxt.Text = "TXT";
            this.checkBoxTxt.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.checkBoxTxt.UseVisualStyleBackColor = true;
            this.checkBoxTxt.Click += new System.EventHandler(this.checkBoxTxt_Click);
            // 
            // checkBoxXml
            // 
            this.checkBoxXml.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBoxXml.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxXml.Location = new System.Drawing.Point(188, 38);
            this.checkBoxXml.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBoxXml.Name = "checkBoxXml";
            this.checkBoxXml.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBoxXml.Size = new System.Drawing.Size(71, 50);
            this.checkBoxXml.TabIndex = 3;
            this.checkBoxXml.Text = "XML";
            this.checkBoxXml.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.checkBoxXml.UseVisualStyleBackColor = true;
            this.checkBoxXml.Click += new System.EventHandler(this.checkBoxXml_Click);
            // 
            // checkBoxJson
            // 
            this.checkBoxJson.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBoxJson.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxJson.Location = new System.Drawing.Point(98, 38);
            this.checkBoxJson.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBoxJson.Name = "checkBoxJson";
            this.checkBoxJson.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBoxJson.Size = new System.Drawing.Size(79, 50);
            this.checkBoxJson.TabIndex = 4;
            this.checkBoxJson.Text = "JSON";
            this.checkBoxJson.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.checkBoxJson.UseVisualStyleBackColor = true;
            this.checkBoxJson.Click += new System.EventHandler(this.checkBoxJson_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 7);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(224, 28);
            this.label1.TabIndex = 5;
            this.label1.Text = "Select the file type\r\n";
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Font = new System.Drawing.Font("Comic Sans MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSave.Location = new System.Drawing.Point(188, 116);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(106, 40);
            this.buttonSave.TabIndex = 6;
            this.buttonSave.Text = "OK";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Font = new System.Drawing.Font("Comic Sans MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCancel.Location = new System.Drawing.Point(43, 116);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(106, 40);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // checkBoxJpeg
            // 
            this.checkBoxJpeg.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBoxJpeg.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxJpeg.Location = new System.Drawing.Point(263, 38);
            this.checkBoxJpeg.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxJpeg.Name = "checkBoxJpeg";
            this.checkBoxJpeg.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBoxJpeg.Size = new System.Drawing.Size(71, 50);
            this.checkBoxJpeg.TabIndex = 8;
            this.checkBoxJpeg.Text = "JPEG";
            this.checkBoxJpeg.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.checkBoxJpeg.UseVisualStyleBackColor = true;
            // 
            // ExportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 167);
            this.Controls.Add(this.checkBoxJpeg);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBoxJson);
            this.Controls.Add(this.checkBoxXml);
            this.Controls.Add(this.checkBoxTxt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Export";
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Button buttonCancel;

        private System.Windows.Forms.Button buttonSave;

        private System.Windows.Forms.Label label1;

        private System.Windows.Forms.CheckBox checkBoxJson;
        private System.Windows.Forms.CheckBox checkBoxXml;
        private System.Windows.Forms.CheckBox checkBoxTxt;


        #endregion

        private System.Windows.Forms.CheckBox checkBoxJpeg;
    }
}