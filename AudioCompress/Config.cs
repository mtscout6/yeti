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
using Yeti.MMedia.Mp3;

namespace AudioCompress
{
	/// <summary>
	/// Summary description for Config.
	/// </summary>
	public class Config : System.Windows.Forms.Form
	{
    private Yeti.MMedia.Mp3.EditMp3Writer editMp3Writer1;
    private System.Windows.Forms.Button buttonOk;
    private System.Windows.Forms.Button buttonApply;
    private System.Windows.Forms.Button buttonCancel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Config()
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
      System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Config));
      this.editMp3Writer1 = new Yeti.MMedia.Mp3.EditMp3Writer();
      this.buttonOk = new System.Windows.Forms.Button();
      this.buttonApply = new System.Windows.Forms.Button();
      this.buttonCancel = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // editMp3Writer1
      // 
      this.editMp3Writer1.Config = ((Yeti.MMedia.AudioWriterConfig)(resources.GetObject("editMp3Writer1.Config")));
      this.editMp3Writer1.Location = new System.Drawing.Point(0, 0);
      this.editMp3Writer1.Name = "editMp3Writer1";
      this.editMp3Writer1.Size = new System.Drawing.Size(336, 280);
      this.editMp3Writer1.TabIndex = 0;
      this.editMp3Writer1.ConfigChange += new System.EventHandler(this.editMp3Writer1_ConfigChange);
      // 
      // buttonOk
      // 
      this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.buttonOk.Location = new System.Drawing.Point(344, 8);
      this.buttonOk.Name = "buttonOk";
      this.buttonOk.TabIndex = 1;
      this.buttonOk.Text = "Ok";
      // 
      // buttonApply
      // 
      this.buttonApply.Enabled = false;
      this.buttonApply.Location = new System.Drawing.Point(344, 40);
      this.buttonApply.Name = "buttonApply";
      this.buttonApply.TabIndex = 2;
      this.buttonApply.Text = "Apply";
      this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
      // 
      // buttonCancel
      // 
      this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.buttonCancel.Enabled = false;
      this.buttonCancel.Location = new System.Drawing.Point(344, 72);
      this.buttonCancel.Name = "buttonCancel";
      this.buttonCancel.TabIndex = 3;
      this.buttonCancel.Text = "Cancel";
      // 
      // Config
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.ClientSize = new System.Drawing.Size(432, 285);
      this.Controls.Add(this.buttonCancel);
      this.Controls.Add(this.buttonApply);
      this.Controls.Add(this.buttonOk);
      this.Controls.Add(this.editMp3Writer1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "Config";
      this.ShowInTaskbar = false;
      this.Text = "Output file options";
      this.ResumeLayout(false);

    }
		#endregion

    public Mp3WriterConfig Mp3Config
    {
      get
      {
        return (Mp3WriterConfig)editMp3Writer1.Config;
      }
      set
      {
        editMp3Writer1.Config = value;
      }
    }

    private void editMp3Writer1_ConfigChange(object sender, System.EventArgs e)
    {
      buttonApply.Enabled = true;
    }

    private void buttonApply_Click(object sender, System.EventArgs e)
    {
      editMp3Writer1.DoApply();
      buttonApply.Enabled = false;
    }
	}
}
