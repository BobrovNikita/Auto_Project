namespace Andasuk.Views
{
    partial class MainView
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.CarBtn = new System.Windows.Forms.Button();
            this.CatalogBtn = new System.Windows.Forms.Button();
            this.SpareBtn = new System.Windows.Forms.Button();
            this.CreatorBtn = new System.Windows.Forms.Button();
            this.ProductBtn = new System.Windows.Forms.Button();
            this.CarProductBtn = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.panel1.Controls.Add(this.CarProductBtn);
            this.panel1.Controls.Add(this.ProductBtn);
            this.panel1.Controls.Add(this.CreatorBtn);
            this.panel1.Controls.Add(this.SpareBtn);
            this.panel1.Controls.Add(this.CatalogBtn);
            this.panel1.Controls.Add(this.CarBtn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(195, 450);
            this.panel1.TabIndex = 1;
            // 
            // CarBtn
            // 
            this.CarBtn.FlatAppearance.BorderSize = 0;
            this.CarBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CarBtn.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.CarBtn.ForeColor = System.Drawing.SystemColors.Control;
            this.CarBtn.Location = new System.Drawing.Point(3, 122);
            this.CarBtn.Name = "CarBtn";
            this.CarBtn.Size = new System.Drawing.Size(189, 39);
            this.CarBtn.TabIndex = 0;
            this.CarBtn.Text = "Car";
            this.CarBtn.UseVisualStyleBackColor = true;
            // 
            // CatalogBtn
            // 
            this.CatalogBtn.FlatAppearance.BorderSize = 0;
            this.CatalogBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CatalogBtn.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.CatalogBtn.ForeColor = System.Drawing.SystemColors.Control;
            this.CatalogBtn.Location = new System.Drawing.Point(3, 167);
            this.CatalogBtn.Name = "CatalogBtn";
            this.CatalogBtn.Size = new System.Drawing.Size(189, 39);
            this.CatalogBtn.TabIndex = 1;
            this.CatalogBtn.Text = "Catalog";
            this.CatalogBtn.UseVisualStyleBackColor = true;
            // 
            // SpareBtn
            // 
            this.SpareBtn.FlatAppearance.BorderSize = 0;
            this.SpareBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SpareBtn.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.SpareBtn.ForeColor = System.Drawing.SystemColors.Control;
            this.SpareBtn.Location = new System.Drawing.Point(3, 212);
            this.SpareBtn.Name = "SpareBtn";
            this.SpareBtn.Size = new System.Drawing.Size(189, 39);
            this.SpareBtn.TabIndex = 2;
            this.SpareBtn.Text = "Spare";
            this.SpareBtn.UseVisualStyleBackColor = true;
            // 
            // CreatorBtn
            // 
            this.CreatorBtn.FlatAppearance.BorderSize = 0;
            this.CreatorBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CreatorBtn.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.CreatorBtn.ForeColor = System.Drawing.SystemColors.Control;
            this.CreatorBtn.Location = new System.Drawing.Point(3, 257);
            this.CreatorBtn.Name = "CreatorBtn";
            this.CreatorBtn.Size = new System.Drawing.Size(189, 39);
            this.CreatorBtn.TabIndex = 3;
            this.CreatorBtn.Text = "Creator";
            this.CreatorBtn.UseVisualStyleBackColor = true;
            // 
            // ProductBtn
            // 
            this.ProductBtn.FlatAppearance.BorderSize = 0;
            this.ProductBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ProductBtn.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ProductBtn.ForeColor = System.Drawing.SystemColors.Control;
            this.ProductBtn.Location = new System.Drawing.Point(3, 302);
            this.ProductBtn.Name = "ProductBtn";
            this.ProductBtn.Size = new System.Drawing.Size(189, 39);
            this.ProductBtn.TabIndex = 4;
            this.ProductBtn.Text = "Product";
            this.ProductBtn.UseVisualStyleBackColor = true;
            // 
            // CarProductBtn
            // 
            this.CarProductBtn.FlatAppearance.BorderSize = 0;
            this.CarProductBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CarProductBtn.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.CarProductBtn.ForeColor = System.Drawing.SystemColors.Control;
            this.CarProductBtn.Location = new System.Drawing.Point(6, 347);
            this.CarProductBtn.Name = "CarProductBtn";
            this.CarProductBtn.Size = new System.Drawing.Size(189, 39);
            this.CarProductBtn.TabIndex = 5;
            this.CarProductBtn.Text = "Car Product";
            this.CarProductBtn.UseVisualStyleBackColor = true;
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.IsMdiContainer = true;
            this.Name = "MainView";
            this.Text = "MainView";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private Button ProductBtn;
        private Button CreatorBtn;
        private Button SpareBtn;
        private Button CatalogBtn;
        private Button CarBtn;
        private Button CarProductBtn;
    }
}