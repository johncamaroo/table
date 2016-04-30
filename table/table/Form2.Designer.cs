namespace table
{
    partial class Form2
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
            this.components = new System.ComponentModel.Container();
            this.tabledbDataSet = new table.tabledbDataSet();
            this.tabledbDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.tabledbDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabledbDataSetBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // tabledbDataSet
            // 
            this.tabledbDataSet.DataSetName = "tabledbDataSet";
            this.tabledbDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tabledbDataSetBindingSource
            // 
            this.tabledbDataSetBindingSource.DataSource = this.tabledbDataSet;
            this.tabledbDataSetBindingSource.Position = 0;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(262, 244);
            this.Name = "Form2";
            this.Text = "Form2";
            ((System.ComponentModel.ISupportInitialize)(this.tabledbDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabledbDataSetBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.BindingSource tabledbDataSetBindingSource;
        private tabledbDataSet tabledbDataSet;
    }
}