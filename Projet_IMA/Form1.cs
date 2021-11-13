using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace Projet_IMA
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			pictureBox1.Image = BitmapScreen.Init(pictureBox1.Width, pictureBox1.Height);
		}

		public bool Checked()			   { return checkBox1.Checked;   }
		public void PictureBoxInvalidate()  { pictureBox1.Invalidate(); }
		public void PictureBoxRefresh()	 { pictureBox1.Refresh();	}

		private void Go()
		{
			BitmapScreen.RefreshScreen(new Color(0, 0, 0));
			BitmapScreen.Show();

			ProjetEleve.SPP = Convert.ToInt32(sppControl.Value);
			ProjetEleve.MAX_DIFFUSE_BOUNCES = Convert.ToInt32(diffuseBouncesControl.Value);
			ProjetEleve.MAX_SPECULAR_BOUNCES = Convert.ToInt32(specularBouncesControl.Value);
			ProjetEleve.USE_PATHTRACING = pathtracingControl.Checked;
			ProjetEleve.Go(sceneControl.SelectedIndex);
		}

		private void Interrupt()
		{
			ProjetEleve.Interrupt();
		}

		private void EventFormShown(object sender, EventArgs e)
		{
			//Go();
		}

		private void EventFormClosing(object sender, FormClosingEventArgs e)
		{
			Interrupt();
		}

		private void stopButton_Click(object sender, EventArgs e)
		{
			Interrupt();
		}

		private void startButton_Click(object sender, EventArgs e)
		{
			Interrupt();
			Go();
		}

		private void pathtracingControl_CheckedChanged(object sender, EventArgs e)
		{
			sppControl.Enabled = pathtracingControl.Checked;
			diffuseBouncesControl.Enabled = pathtracingControl.Checked;
			specularBouncesControl.Enabled = pathtracingControl.Checked;
		}

		private void sceneControl_SelectedIndexChanged(object sender, EventArgs e)
		{
			startButton.Enabled = sceneControl.SelectedIndex != -1;
		}
	}
}
