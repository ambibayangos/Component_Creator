namespace Altium_Component_Creator
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            databaseDatagridView = new DataGridView();
            categoryComboxBox = new ComboBox();
            categoryLabel = new Label();
            addNewItemButton = new Button();
            deleteItemButton = new Button();
            ((System.ComponentModel.ISupportInitialize)databaseDatagridView).BeginInit();
            SuspendLayout();
            // 
            // databaseDatagridView
            // 
            databaseDatagridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            databaseDatagridView.Location = new Point(12, 56);
            databaseDatagridView.Name = "databaseDatagridView";
            databaseDatagridView.Size = new Size(776, 346);
            databaseDatagridView.TabIndex = 0;
            // 
            // categoryComboxBox
            // 
            categoryComboxBox.FormattingEnabled = true;
            categoryComboxBox.Location = new Point(73, 21);
            categoryComboxBox.Name = "categoryComboxBox";
            categoryComboxBox.Size = new Size(258, 23);
            categoryComboxBox.TabIndex = 1;
            // 
            // categoryLabel
            // 
            categoryLabel.AutoSize = true;
            categoryLabel.Location = new Point(12, 24);
            categoryLabel.Name = "categoryLabel";
            categoryLabel.Size = new Size(55, 15);
            categoryLabel.TabIndex = 2;
            categoryLabel.Text = "Category";
            // 
            // addNewItemButton
            // 
            addNewItemButton.Location = new Point(632, 415);
            addNewItemButton.Name = "addNewItemButton";
            addNewItemButton.Size = new Size(75, 23);
            addNewItemButton.TabIndex = 3;
            addNewItemButton.Text = "Add";
            addNewItemButton.UseVisualStyleBackColor = true;
            addNewItemButton.Click += addNewItemButton_Click;
            // 
            // deleteItemButton
            // 
            deleteItemButton.Location = new Point(713, 415);
            deleteItemButton.Name = "deleteItemButton";
            deleteItemButton.Size = new Size(75, 23);
            deleteItemButton.TabIndex = 4;
            deleteItemButton.Text = "Delete";
            deleteItemButton.UseVisualStyleBackColor = true;
            deleteItemButton.Click += deleteItemButton_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(deleteItemButton);
            Controls.Add(addNewItemButton);
            Controls.Add(categoryLabel);
            Controls.Add(categoryComboxBox);
            Controls.Add(databaseDatagridView);
            Name = "MainForm";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)databaseDatagridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView databaseDatagridView;
        private ComboBox categoryComboxBox;
        private Label categoryLabel;
        private Button addNewItemButton;
        private Button deleteItemButton;
    }
}
