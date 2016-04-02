namespace WFormsXMLFormatter
{
    partial class Form4
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.xmlTextBox1 = new WFormsXMLFormatter.Controls.XmlTextBox();
            this.SuspendLayout();
            // 
            // xmlTextBox1
            // 
            this.xmlTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xmlTextBox1.Location = new System.Drawing.Point(0, 0);
            this.xmlTextBox1.Name = "xmlTextBox1";
            this.xmlTextBox1.Size = new System.Drawing.Size(869, 497);
            this.xmlTextBox1.TabIndex = 0;
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 497);
            this.Controls.Add(this.xmlTextBox1);
            this.Name = "Form4";
            this.Text = "Form4";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.XmlTextBox xmlTextBox1;

    }
}