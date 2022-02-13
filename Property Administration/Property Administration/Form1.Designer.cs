
namespace Property_Administration
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.dgvOwners = new System.Windows.Forms.DataGridView();
            this.addOwner = new System.Windows.Forms.Button();
            this.deleteOwnerButton = new System.Windows.Forms.Button();
            this.editButton = new System.Windows.Forms.Button();
            this.viewOwnerButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOwners)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvOwners
            // 
            this.dgvOwners.AllowUserToAddRows = false;
            this.dgvOwners.AllowUserToDeleteRows = false;
            this.dgvOwners.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvOwners.BackgroundColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.dgvOwners.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOwners.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvOwners.GridColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dgvOwners.Location = new System.Drawing.Point(27, 24);
            this.dgvOwners.MultiSelect = false;
            this.dgvOwners.Name = "dgvOwners";
            this.dgvOwners.ReadOnly = true;
            this.dgvOwners.RowHeadersWidth = 51;
            this.dgvOwners.RowTemplate.Height = 30;
            this.dgvOwners.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOwners.Size = new System.Drawing.Size(934, 261);
            this.dgvOwners.TabIndex = 0;
            // 
            // addOwner
            // 
            this.addOwner.BackColor = System.Drawing.Color.SkyBlue;
            this.addOwner.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addOwner.Location = new System.Drawing.Point(27, 325);
            this.addOwner.Name = "addOwner";
            this.addOwner.Size = new System.Drawing.Size(218, 51);
            this.addOwner.TabIndex = 1;
            this.addOwner.Text = "Adăugare proprietar";
            this.addOwner.UseVisualStyleBackColor = false;
            this.addOwner.Click += new System.EventHandler(this.addOwner_Click);
            // 
            // deleteOwnerButton
            // 
            this.deleteOwnerButton.BackColor = System.Drawing.Color.SkyBlue;
            this.deleteOwnerButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deleteOwnerButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.deleteOwnerButton.Location = new System.Drawing.Point(27, 396);
            this.deleteOwnerButton.Name = "deleteOwnerButton";
            this.deleteOwnerButton.Size = new System.Drawing.Size(218, 51);
            this.deleteOwnerButton.TabIndex = 2;
            this.deleteOwnerButton.Text = "Ștergere proprietar";
            this.deleteOwnerButton.UseVisualStyleBackColor = false;
            this.deleteOwnerButton.Click += new System.EventHandler(this.deleteOwnerButton_Click);
            // 
            // editButton
            // 
            this.editButton.BackColor = System.Drawing.Color.SkyBlue;
            this.editButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editButton.Location = new System.Drawing.Point(27, 471);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(218, 51);
            this.editButton.TabIndex = 3;
            this.editButton.Text = "Editare proprietar";
            this.editButton.UseVisualStyleBackColor = false;
            this.editButton.Click += new System.EventHandler(this.editButton_Click);
            // 
            // viewOwnerButton
            // 
            this.viewOwnerButton.BackColor = System.Drawing.Color.SkyBlue;
            this.viewOwnerButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.viewOwnerButton.Location = new System.Drawing.Point(748, 325);
            this.viewOwnerButton.Name = "viewOwnerButton";
            this.viewOwnerButton.Size = new System.Drawing.Size(213, 51);
            this.viewOwnerButton.TabIndex = 4;
            this.viewOwnerButton.Text = "Vizualizare profil";
            this.viewOwnerButton.UseVisualStyleBackColor = false;
            this.viewOwnerButton.Click += new System.EventHandler(this.viewOwnerButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(993, 548);
            this.Controls.Add(this.viewOwnerButton);
            this.Controls.Add(this.editButton);
            this.Controls.Add(this.deleteOwnerButton);
            this.Controls.Add(this.addOwner);
            this.Controls.Add(this.dgvOwners);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOwners)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button addOwner;
        internal System.Windows.Forms.DataGridView dgvOwners;
        private System.Windows.Forms.Button deleteOwnerButton;
        private System.Windows.Forms.Button editButton;
        private System.Windows.Forms.Button viewOwnerButton;
    }
}

