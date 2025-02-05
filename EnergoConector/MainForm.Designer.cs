namespace EnergoConector
{
    partial class Main
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.ComPortChoice = new System.Windows.Forms.ComboBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.InfoLabel = new System.Windows.Forms.Label();
            this.inputMacField = new System.Windows.Forms.TextBox();
            this.inputPassField = new System.Windows.Forms.TextBox();
            this.FindBtn = new System.Windows.Forms.Button();
            this.SendBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.listBoxResults = new System.Windows.Forms.ListBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.указатьФайлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DefaultFilePathBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonBrowse = new System.Windows.Forms.ToolStripMenuItem();
            this.labelFilePath = new System.Windows.Forms.Label();
            this.filePathPointer = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ComPortChoice
            // 
            this.ComPortChoice.FormattingEnabled = true;
            this.ComPortChoice.Location = new System.Drawing.Point(148, 89);
            this.ComPortChoice.Margin = new System.Windows.Forms.Padding(2);
            this.ComPortChoice.Name = "ComPortChoice";
            this.ComPortChoice.Size = new System.Drawing.Size(92, 21);
            this.ComPortChoice.TabIndex = 0;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.richTextBox1.Location = new System.Drawing.Point(0, 215);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(2);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(600, 215);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // InfoLabel
            // 
            this.InfoLabel.AutoSize = true;
            this.InfoLabel.Location = new System.Drawing.Point(146, 126);
            this.InfoLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.InfoLabel.Name = "InfoLabel";
            this.InfoLabel.Size = new System.Drawing.Size(0, 13);
            this.InfoLabel.TabIndex = 2;
            // 
            // inputMacField
            // 
            this.inputMacField.Location = new System.Drawing.Point(256, 91);
            this.inputMacField.Margin = new System.Windows.Forms.Padding(2);
            this.inputMacField.Name = "inputMacField";
            this.inputMacField.Size = new System.Drawing.Size(101, 20);
            this.inputMacField.TabIndex = 3;
            this.inputMacField.TextChanged += new System.EventHandler(this.TextBoxSearch_TextChanged);
            // 
            // inputPassField
            // 
            this.inputPassField.Location = new System.Drawing.Point(383, 89);
            this.inputPassField.Margin = new System.Windows.Forms.Padding(2);
            this.inputPassField.Name = "inputPassField";
            this.inputPassField.Size = new System.Drawing.Size(76, 20);
            this.inputPassField.TabIndex = 4;
            // 
            // FindBtn
            // 
            this.FindBtn.Location = new System.Drawing.Point(383, 126);
            this.FindBtn.Margin = new System.Windows.Forms.Padding(2);
            this.FindBtn.Name = "FindBtn";
            this.FindBtn.Size = new System.Drawing.Size(75, 19);
            this.FindBtn.TabIndex = 5;
            this.FindBtn.Text = "Найти";
            this.FindBtn.UseVisualStyleBackColor = true;
            // 
            // SendBtn
            // 
            this.SendBtn.Location = new System.Drawing.Point(485, 89);
            this.SendBtn.Margin = new System.Windows.Forms.Padding(2);
            this.SendBtn.Name = "SendBtn";
            this.SendBtn.Size = new System.Drawing.Size(75, 19);
            this.SendBtn.TabIndex = 6;
            this.SendBtn.Text = "Изменить";
            this.SendBtn.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(146, 73);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "COM PORT";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(279, 73);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "MAC";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(381, 73);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "ПАРОЛЬ";
            // 
            // listBoxResults
            // 
            this.listBoxResults.FormattingEnabled = true;
            this.listBoxResults.Location = new System.Drawing.Point(256, 107);
            this.listBoxResults.Name = "listBoxResults";
            this.listBoxResults.Size = new System.Drawing.Size(101, 95);
            this.listBoxResults.TabIndex = 10;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.указатьФайлToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(600, 24);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // указатьФайлToolStripMenuItem
            // 
            this.указатьФайлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DefaultFilePathBtn,
            this.buttonBrowse});
            this.указатьФайлToolStripMenuItem.Name = "указатьФайлToolStripMenuItem";
            this.указатьФайлToolStripMenuItem.Size = new System.Drawing.Size(92, 20);
            this.указатьФайлToolStripMenuItem.Text = "Указать файл";
            // 
            // DefaultFilePathBtn
            // 
            this.DefaultFilePathBtn.Name = "DefaultFilePathBtn";
            this.DefaultFilePathBtn.Size = new System.Drawing.Size(189, 22);
            this.DefaultFilePathBtn.Text = "Файл по умолчанию";
            this.DefaultFilePathBtn.Click += new System.EventHandler(this.DefaultFilePathBtn_Click);
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(189, 22);
            this.buttonBrowse.Text = "Открыть файл";
            this.buttonBrowse.Click += new System.EventHandler(this.ButtonBrowse_Click);
            // 
            // labelFilePath
            // 
            this.labelFilePath.AutoSize = true;
            this.labelFilePath.Location = new System.Drawing.Point(110, 28);
            this.labelFilePath.Name = "labelFilePath";
            this.labelFilePath.Size = new System.Drawing.Size(0, 13);
            this.labelFilePath.TabIndex = 12;
            // 
            // filePathPointer
            // 
            this.filePathPointer.AutoSize = true;
            this.filePathPointer.Location = new System.Drawing.Point(12, 28);
            this.filePathPointer.Name = "filePathPointer";
            this.filePathPointer.Size = new System.Drawing.Size(74, 13);
            this.filePathPointer.TabIndex = 13;
            this.filePathPointer.Text = "Путь к файлу";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 430);
            this.Controls.Add(this.filePathPointer);
            this.Controls.Add(this.labelFilePath);
            this.Controls.Add(this.listBoxResults);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SendBtn);
            this.Controls.Add(this.FindBtn);
            this.Controls.Add(this.inputPassField);
            this.Controls.Add(this.inputMacField);
            this.Controls.Add(this.InfoLabel);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.ComPortChoice);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Main";
            this.Text = "Энерго сборщик";
            this.Load += new System.EventHandler(this.DefaultFilePathBtn_Click);
            this.Click += new System.EventHandler(this.MainForm_Click);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox ComPortChoice;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label InfoLabel;
        private System.Windows.Forms.TextBox inputMacField;
        private System.Windows.Forms.TextBox inputPassField;
        private System.Windows.Forms.Button FindBtn;
        private System.Windows.Forms.Button SendBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox listBoxResults;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem указатьФайлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DefaultFilePathBtn;
        private System.Windows.Forms.ToolStripMenuItem buttonBrowse;
        private System.Windows.Forms.Label labelFilePath;
        private System.Windows.Forms.Label filePathPointer;
    }
}

