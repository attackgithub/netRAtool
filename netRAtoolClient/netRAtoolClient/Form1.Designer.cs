﻿namespace netRAtoolClient
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
            this.components = new System.ComponentModel.Container();
            this.btnConnectClick = new System.Windows.Forms.Button();
            this.btnRemoteDesktop = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // btnConnectClick
            // 
            this.btnConnectClick.BackColor = System.Drawing.Color.Black;
            this.btnConnectClick.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnConnectClick.Location = new System.Drawing.Point(12, 12);
            this.btnConnectClick.Name = "btnConnectClick";
            this.btnConnectClick.Size = new System.Drawing.Size(75, 37);
            this.btnConnectClick.TabIndex = 2;
            this.btnConnectClick.Text = "Connect";
            this.btnConnectClick.UseVisualStyleBackColor = false;
            this.btnConnectClick.Click += new System.EventHandler(this.btnConnectClick_Click);
            // 
            // btnRemoteDesktop
            // 
            this.btnRemoteDesktop.BackColor = System.Drawing.Color.Black;
            this.btnRemoteDesktop.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnRemoteDesktop.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnRemoteDesktop.Location = new System.Drawing.Point(12, 55);
            this.btnRemoteDesktop.Name = "btnRemoteDesktop";
            this.btnRemoteDesktop.Size = new System.Drawing.Size(75, 37);
            this.btnRemoteDesktop.TabIndex = 3;
            this.btnRemoteDesktop.Text = "Share Desktop";
            this.btnRemoteDesktop.UseVisualStyleBackColor = false;
            this.btnRemoteDesktop.Click += new System.EventHandler(this.btnRemoteDesktop_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(95, 101);
            this.Controls.Add(this.btnRemoteDesktop);
            this.Controls.Add(this.btnConnectClick);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.Text = "System";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnConnectClick;
        private System.Windows.Forms.Button btnRemoteDesktop;
        private System.Windows.Forms.Timer timer1;
    }
}

