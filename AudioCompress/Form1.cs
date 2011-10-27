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
using System.Data;
using System.IO;
using Yeti.MMedia;
using Yeti.MMedia.Mp3;
using WaveLib;

namespace AudioCompress
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox textBoxInFile;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox textBoxOutFile;
    private System.Windows.Forms.Button buttonInFile;
    private System.Windows.Forms.Button buttonOutFile;
    private System.Windows.Forms.OpenFileDialog openFileDialog1;
    private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    private System.Windows.Forms.Button buttonConfig;
    private System.Windows.Forms.Button buttonCompress;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.ToolTip toolTip1;
    private System.Windows.Forms.MainMenu mainMenu1;
    private System.Windows.Forms.MenuItem menuItem1;
    private System.Windows.Forms.ProgressBar progressBar;
    private System.ComponentModel.IContainer components;

		public Form1()
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
				if (components != null) 
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
      this.components = new System.ComponentModel.Container();
      this.label1 = new System.Windows.Forms.Label();
      this.textBoxInFile = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.textBoxOutFile = new System.Windows.Forms.TextBox();
      this.buttonInFile = new System.Windows.Forms.Button();
      this.buttonOutFile = new System.Windows.Forms.Button();
      this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
      this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
      this.buttonConfig = new System.Windows.Forms.Button();
      this.buttonCompress = new System.Windows.Forms.Button();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.mainMenu1 = new System.Windows.Forms.MainMenu();
      this.menuItem1 = new System.Windows.Forms.MenuItem();
      this.progressBar = new System.Windows.Forms.ProgressBar();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.Location = new System.Drawing.Point(8, 0);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(100, 16);
      this.label1.TabIndex = 0;
      this.label1.Text = "Wave file:";
      // 
      // textBoxInFile
      // 
      this.textBoxInFile.Location = new System.Drawing.Point(8, 16);
      this.textBoxInFile.Name = "textBoxInFile";
      this.textBoxInFile.ReadOnly = true;
      this.textBoxInFile.Size = new System.Drawing.Size(256, 20);
      this.textBoxInFile.TabIndex = 1;
      this.textBoxInFile.Text = "";
      this.toolTip1.SetToolTip(this.textBoxInFile, "Input file");
      this.textBoxInFile.TextChanged += new System.EventHandler(this.textBoxInFile_TextChanged);
      // 
      // label2
      // 
      this.label2.Location = new System.Drawing.Point(8, 40);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(100, 16);
      this.label2.TabIndex = 2;
      this.label2.Text = "Compressed file:";
      // 
      // textBoxOutFile
      // 
      this.textBoxOutFile.Enabled = false;
      this.textBoxOutFile.Location = new System.Drawing.Point(8, 56);
      this.textBoxOutFile.Name = "textBoxOutFile";
      this.textBoxOutFile.ReadOnly = true;
      this.textBoxOutFile.Size = new System.Drawing.Size(256, 20);
      this.textBoxOutFile.TabIndex = 3;
      this.textBoxOutFile.Text = "";
      this.toolTip1.SetToolTip(this.textBoxOutFile, "Output file");
      // 
      // buttonInFile
      // 
      this.buttonInFile.Location = new System.Drawing.Point(272, 16);
      this.buttonInFile.Name = "buttonInFile";
      this.buttonInFile.TabIndex = 4;
      this.buttonInFile.Text = "Browse...";
      this.toolTip1.SetToolTip(this.buttonInFile, "Select the input wave file");
      this.buttonInFile.Click += new System.EventHandler(this.buttonInFile_Click);
      // 
      // buttonOutFile
      // 
      this.buttonOutFile.Enabled = false;
      this.buttonOutFile.Location = new System.Drawing.Point(272, 56);
      this.buttonOutFile.Name = "buttonOutFile";
      this.buttonOutFile.TabIndex = 5;
      this.buttonOutFile.Text = "Browse...";
      this.toolTip1.SetToolTip(this.buttonOutFile, "Change the output file name or location");
      this.buttonOutFile.Click += new System.EventHandler(this.buttonOutFile_Click);
      // 
      // openFileDialog1
      // 
      this.openFileDialog1.DefaultExt = "wav";
      this.openFileDialog1.Filter = "Wave files (*.wav)|*.wav|All files (*.*)|*.*\"";
      this.openFileDialog1.Title = "Wave file to compress";
      // 
      // saveFileDialog1
      // 
      this.saveFileDialog1.DefaultExt = "mp3";
      this.saveFileDialog1.Filter = "MP3 files (*.mp3)|*.mp3|All files (*.*)|*.*\"";
      this.saveFileDialog1.Title = "Compressed file";
      // 
      // buttonConfig
      // 
      this.buttonConfig.Enabled = false;
      this.buttonConfig.Location = new System.Drawing.Point(184, 112);
      this.buttonConfig.Name = "buttonConfig";
      this.buttonConfig.TabIndex = 6;
      this.buttonConfig.Text = "Config...";
      this.toolTip1.SetToolTip(this.buttonConfig, "Configure the compressor");
      this.buttonConfig.Click += new System.EventHandler(this.buttonConfig_Click);
      // 
      // buttonCompress
      // 
      this.buttonCompress.Enabled = false;
      this.buttonCompress.Location = new System.Drawing.Point(272, 112);
      this.buttonCompress.Name = "buttonCompress";
      this.buttonCompress.TabIndex = 7;
      this.buttonCompress.Text = "Compress";
      this.toolTip1.SetToolTip(this.buttonCompress, "Convert the wave file to the selected format");
      this.buttonCompress.Click += new System.EventHandler(this.buttonCompress_Click);
      // 
      // groupBox1
      // 
      this.groupBox1.Location = new System.Drawing.Point(8, 96);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(336, 8);
      this.groupBox1.TabIndex = 8;
      this.groupBox1.TabStop = false;
      // 
      // mainMenu1
      // 
      this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                              this.menuItem1});
      // 
      // menuItem1
      // 
      this.menuItem1.Index = 0;
      this.menuItem1.Text = "About...";
      this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
      // 
      // progressBar
      // 
      this.progressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.progressBar.Location = new System.Drawing.Point(0, 139);
      this.progressBar.Name = "progressBar";
      this.progressBar.Size = new System.Drawing.Size(352, 16);
      this.progressBar.TabIndex = 9;
      // 
      // Form1
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.ClientSize = new System.Drawing.Size(352, 155);
      this.Controls.Add(this.progressBar);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.buttonCompress);
      this.Controls.Add(this.buttonConfig);
      this.Controls.Add(this.buttonOutFile);
      this.Controls.Add(this.buttonInFile);
      this.Controls.Add(this.textBoxOutFile);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.textBoxInFile);
      this.Controls.Add(this.label1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.Menu = this.mainMenu1;
      this.Name = "Form1";
      this.Text = "Audio Compress";
      this.Closing += new System.ComponentModel.CancelEventHandler(this.Form1_Closing);
      this.Load += new System.EventHandler(this.Form1_Load);
      this.ResumeLayout(false);

    }
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

    private Mp3WriterConfig m_Config = null;

    private void buttonInFile_Click(object sender, System.EventArgs e)
    {
      if ( openFileDialog1.ShowDialog(this) == DialogResult.OK )
      {
        try
        {
          WaveStream s = new WaveStream(openFileDialog1.FileName);
          try
          {
            m_Config = new Mp3WriterConfig(s.Format);
            textBoxInFile.Text = openFileDialog1.FileName;
            textBoxOutFile.Text = System.IO.Path.ChangeExtension(textBoxInFile.Text, ".mp3");
          }
          finally
          {
            s.Close();
          }
        }
        catch
        {
          MessageBox.Show(this, "Invalid wave file or format", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
          textBoxInFile.Text = "";
          textBoxOutFile.Text = "";
        }
      }
    }

    private void textBoxInFile_TextChanged(object sender, System.EventArgs e)
    {
      if ( textBoxInFile.Text != "" )
      {
        buttonCompress.Enabled = buttonConfig.Enabled = buttonOutFile.Enabled = true;
      }
      else
      {
        buttonCompress.Enabled = buttonConfig.Enabled = buttonOutFile.Enabled = false;
      }
    }

    private void buttonOutFile_Click(object sender, System.EventArgs e)
    {
      saveFileDialog1.FileName = textBoxOutFile.Text;
      if (saveFileDialog1.ShowDialog(this) == DialogResult.OK)
      {
        textBoxOutFile.Text = saveFileDialog1.FileName;
      }
    }

    private void buttonConfig_Click(object sender, System.EventArgs e)
    {
      Config cfg = new Config();
      cfg.Mp3Config = m_Config;
      if ( cfg.ShowDialog(this) == DialogResult.OK )
      {
        m_Config = cfg.Mp3Config;
      }
    }
    
    private bool Compressing = false;

    private void RefreshControls()
    {
      if (Compressing)
      {
        buttonInFile.Enabled = false;
        buttonOutFile.Enabled = false;
        buttonConfig.Enabled = false;
        buttonCompress.Enabled = false;
      }
      else
      {
        buttonInFile.Enabled = true;
        buttonOutFile.Enabled = true;
        buttonConfig.Enabled = true;
        buttonCompress.Enabled = true;
      }
    }

    private void buttonCompress_Click(object sender, System.EventArgs e)
    {
      if ( File.Exists(textBoxOutFile.Text) && (MessageBox.Show(this, "Override the existing file?", "File exists", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) )
      {
        return;
      }
      try
      {
        progressBar.Value = 0;
        toolTip1.SetToolTip(progressBar, "");
        this.Text = "Audio Compress";
        Compressing = true;
        try
        {
          RefreshControls();
          WaveStream InStr = new WaveStream(textBoxInFile.Text);
          try
          {
            Mp3Writer writer = new Mp3Writer(new FileStream(textBoxOutFile.Text, FileMode.Create), m_Config);
            try
            {
              byte[] buff = new byte[writer.OptimalBufferSize];
              int read = 0;
              int actual = 0;
              long total = InStr.Length;
              Cursor.Current = Cursors.WaitCursor;
              try
              {
                while ( (read = InStr.Read(buff, 0, buff.Length)) > 0 )
                {
                  Application.DoEvents();
                  writer.Write(buff, 0, read);
                  actual += read;
                  progressBar.Value = (int)(((long)actual*100)/total);
                  toolTip1.SetToolTip(progressBar, string.Format("{0}% compresssed", progressBar.Value));
                  this.Text = string.Format("Audio Compress - {0}% compresssed", progressBar.Value);
                  Application.DoEvents();
                }
                toolTip1.SetToolTip(progressBar, "Done");
                this.Text = "Audio Compress - Done";
              }
              finally
              {
                Cursor.Current = Cursors.Default;
              }
            }
            finally
            {
              writer.Close();
            }
          }
          finally
          {
            InStr.Close();
          }
        }
        finally
        {
          Compressing = false;
          RefreshControls();
        }
      }
      catch(Exception ex)
      {
        MessageBox.Show(this, ex.Message, "An exception has ocurred with the following message", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void Form1_Load(object sender, System.EventArgs e)
    {
      RefreshControls();
    }

    private void menuItem1_Click(object sender, System.EventArgs e)
    {
      About dlg = new About();
      dlg.ShowDialog(this);
    }

    private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
      e.Cancel = Compressing;
    }
	}
}
