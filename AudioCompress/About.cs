//
//
//  THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
//  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
//  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR
//  PURPOSE. IT CAN BE DISTRIBUTED FREE OF CHARGE AS LONG AS THIS HEADER 
//  REMAINS UNCHANGED. 
//  SEE  http://www.mp3dev.org/ FOR TECHNICAL AND COPYRIGHT INFORMATION REGARDING 
//  LAME PROJECT.
//
//  Email:  yetiicb@hotmail.com
//
//  Copyright (C) 2002-2003 Idael Cardoso. 
//
//
//  About Thomson and/or Fraunhofer patents:
//  Any use of this product does not convey a license under the relevant 
//  intellectual property of Thomson and/or Fraunhofer Gesellschaft nor imply 
//  any right to use this product in any finished end user or ready-to-use final 
//  product. An independent license for such use is required. 
//  For details, please visit http://www.mp3licensing.com.
//
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Yeti.Lame;

namespace AudioCompress
{
	/// <summary>
	/// Summary description for About.
	/// </summary>
	public class About : System.Windows.Forms.Form
	{
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Label labelDllVer;
    private System.Windows.Forms.Label labelEngVer;
    private System.Windows.Forms.Label labelDate;
    private System.Windows.Forms.LinkLabel linkLabel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public About()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.button1 = new System.Windows.Forms.Button();
      this.labelDllVer = new System.Windows.Forms.Label();
      this.labelEngVer = new System.Windows.Forms.Label();
      this.labelDate = new System.Windows.Forms.Label();
      this.linkLabel = new System.Windows.Forms.LinkLabel();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.linkLabel);
      this.groupBox1.Controls.Add(this.labelDate);
      this.groupBox1.Controls.Add(this.labelEngVer);
      this.groupBox1.Controls.Add(this.labelDllVer);
      this.groupBox1.Controls.Add(this.label4);
      this.groupBox1.Controls.Add(this.label3);
      this.groupBox1.Controls.Add(this.label2);
      this.groupBox1.Controls.Add(this.label1);
      this.groupBox1.Location = new System.Drawing.Point(8, 8);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(328, 128);
      this.groupBox1.TabIndex = 0;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Lame_enc DLL version info:";
      // 
      // label1
      // 
      this.label1.Location = new System.Drawing.Point(4, 24);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(96, 16);
      this.label1.TabIndex = 0;
      this.label1.Text = "DLL version:";
      // 
      // label2
      // 
      this.label2.Location = new System.Drawing.Point(4, 48);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(136, 24);
      this.label2.TabIndex = 1;
      this.label2.Text = "Encoding engine version:";
      // 
      // label3
      // 
      this.label3.Location = new System.Drawing.Point(4, 72);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(96, 16);
      this.label3.TabIndex = 2;
      this.label3.Text = "Release date:";
      // 
      // label4
      // 
      this.label4.Location = new System.Drawing.Point(4, 96);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(128, 23);
      this.label4.TabIndex = 3;
      this.label4.Text = "Lame_enc\'s homepage:";
      // 
      // button1
      // 
      this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.button1.Location = new System.Drawing.Point(259, 144);
      this.button1.Name = "button1";
      this.button1.TabIndex = 1;
      this.button1.Text = "Ok";
      // 
      // labelDllVer
      // 
      this.labelDllVer.Location = new System.Drawing.Point(72, 24);
      this.labelDllVer.Name = "labelDllVer";
      this.labelDllVer.Size = new System.Drawing.Size(100, 16);
      this.labelDllVer.TabIndex = 4;
      this.labelDllVer.Text = " ";
      // 
      // labelEngVer
      // 
      this.labelEngVer.Location = new System.Drawing.Point(128, 48);
      this.labelEngVer.Name = "labelEngVer";
      this.labelEngVer.Size = new System.Drawing.Size(100, 16);
      this.labelEngVer.TabIndex = 5;
      this.labelEngVer.Text = " ";
      // 
      // labelDate
      // 
      this.labelDate.AutoSize = true;
      this.labelDate.Location = new System.Drawing.Point(80, 72);
      this.labelDate.Name = "labelDate";
      this.labelDate.Size = new System.Drawing.Size(7, 16);
      this.labelDate.TabIndex = 6;
      this.labelDate.Text = " ";
      // 
      // linkLabel
      // 
      this.linkLabel.AutoSize = true;
      this.linkLabel.LinkArea = new System.Windows.Forms.LinkArea(0, 0);
      this.linkLabel.Location = new System.Drawing.Point(128, 96);
      this.linkLabel.Name = "linkLabel";
      this.linkLabel.Size = new System.Drawing.Size(130, 16);
      this.linkLabel.TabIndex = 7;
      this.linkLabel.Text = "                                          ";
      this.linkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelUrl_LinkClicked);
      // 
      // About
      // 
      this.AcceptButton = this.button1;
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.CancelButton = this.button1;
      this.ClientSize = new System.Drawing.Size(344, 175);
      this.Controls.Add(this.button1);
      this.Controls.Add(this.groupBox1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "About";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "About Audio Compress";
      this.Load += new System.EventHandler(this.About_Load);
      this.groupBox1.ResumeLayout(false);
      this.ResumeLayout(false);

    }
		#endregion

    private void About_Load(object sender, System.EventArgs e)
    {
      BE_VERSION ver = new BE_VERSION();
      Lame_encDll.beVersion(ver);
      labelDllVer.Text = string.Format("{0}.{1}", ver.byDLLMajorVersion, ver.byDLLMinorVersion);
      labelEngVer.Text = string.Format("{0}.{1}", ver.byMajorVersion, ver.byMinorVersion);
      DateTime date = new DateTime(ver.wYear, ver.byMonth, ver.byDay);
      labelDate.Text = date.ToShortDateString();
      linkLabel.Text = ver.zHomepage;
      linkLabel.LinkArea = new LinkArea(0, linkLabel.Text.Length);
      linkLabel.Links[0].LinkData = linkLabel.Text;
    }

    private void linkLabelUrl_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
    {
      string target = e.Link.LinkData as string;
      e.Link.Visited = true;
      if(target != null)
      {
        System.Diagnostics.Process.Start(target);
      }
    }
	}
}
