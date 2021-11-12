namespace Projet_IMA
{
	partial class Form1
	{
		/// <summary>
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Nettoyage des ressources utilisées.
		/// </summary>
		/// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Code généré par le Concepteur Windows Form

		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
			this.startButton = new System.Windows.Forms.Button();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.diffuseBouncesControl = new System.Windows.Forms.NumericUpDown();
			this.specularBouncesControl = new System.Windows.Forms.NumericUpDown();
			this.sppControl = new System.Windows.Forms.NumericUpDown();
			this.sppLabel = new System.Windows.Forms.Label();
			this.diffuseBouncesLabel = new System.Windows.Forms.Label();
			this.specularBouncesLabel = new System.Windows.Forms.Label();
			this.stopButton = new System.Windows.Forms.Button();
			this.sceneLabel = new System.Windows.Forms.Label();
			this.sceneControl = new System.Windows.Forms.ComboBox();
			this.pathtracingControl = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.diffuseBouncesControl)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.specularBouncesControl)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.sppControl)).BeginInit();
			this.SuspendLayout();
			// 
			// startButton
			// 
			this.startButton.Location = new System.Drawing.Point(23, 17);
			this.startButton.Name = "startButton";
			this.startButton.Size = new System.Drawing.Size(179, 35);
			this.startButton.TabIndex = 0;
			this.startButton.Text = "Start";
			this.startButton.UseVisualStyleBackColor = true;
			this.startButton.Click += new System.EventHandler(this.startButton_Click);
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pictureBox1.Location = new System.Drawing.Point(23, 103);
			this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(560, 390);
			this.pictureBox1.TabIndex = 1;
			this.pictureBox1.TabStop = false;
			// 
			// checkBox1
			// 
			this.checkBox1.AutoSize = true;
			this.checkBox1.Checked = true;
			this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox1.Enabled = false;
			this.checkBox1.Location = new System.Drawing.Point(208, 28);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(15, 14);
			this.checkBox1.TabIndex = 4;
			this.checkBox1.UseVisualStyleBackColor = true;
			// 
			// diffuseBouncesControl
			// 
			this.diffuseBouncesControl.Location = new System.Drawing.Point(635, 43);
			this.diffuseBouncesControl.Name = "diffuseBouncesControl";
			this.diffuseBouncesControl.Size = new System.Drawing.Size(88, 20);
			this.diffuseBouncesControl.TabIndex = 5;
			this.diffuseBouncesControl.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
			// 
			// specularBouncesControl
			// 
			this.specularBouncesControl.Location = new System.Drawing.Point(635, 69);
			this.specularBouncesControl.Name = "specularBouncesControl";
			this.specularBouncesControl.Size = new System.Drawing.Size(88, 20);
			this.specularBouncesControl.TabIndex = 6;
			this.specularBouncesControl.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
			// 
			// sppControl
			// 
			this.sppControl.Location = new System.Drawing.Point(635, 17);
			this.sppControl.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
			this.sppControl.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.sppControl.Name = "sppControl";
			this.sppControl.Size = new System.Drawing.Size(88, 20);
			this.sppControl.TabIndex = 7;
			this.sppControl.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
			// 
			// sppLabel
			// 
			this.sppLabel.AutoSize = true;
			this.sppLabel.Location = new System.Drawing.Point(601, 19);
			this.sppLabel.Name = "sppLabel";
			this.sppLabel.Size = new System.Drawing.Size(28, 13);
			this.sppLabel.TabIndex = 8;
			this.sppLabel.Text = "SPP";
			// 
			// diffuseBouncesLabel
			// 
			this.diffuseBouncesLabel.AutoSize = true;
			this.diffuseBouncesLabel.Location = new System.Drawing.Point(503, 45);
			this.diffuseBouncesLabel.Name = "diffuseBouncesLabel";
			this.diffuseBouncesLabel.Size = new System.Drawing.Size(126, 13);
			this.diffuseBouncesLabel.TabIndex = 9;
			this.diffuseBouncesLabel.Text = "Diffuse bounces per path";
			// 
			// specularBouncesLabel
			// 
			this.specularBouncesLabel.AutoSize = true;
			this.specularBouncesLabel.Location = new System.Drawing.Point(494, 71);
			this.specularBouncesLabel.Name = "specularBouncesLabel";
			this.specularBouncesLabel.Size = new System.Drawing.Size(135, 13);
			this.specularBouncesLabel.TabIndex = 10;
			this.specularBouncesLabel.Text = "Specular bounces per path";
			// 
			// stopButton
			// 
			this.stopButton.Location = new System.Drawing.Point(23, 60);
			this.stopButton.Name = "stopButton";
			this.stopButton.Size = new System.Drawing.Size(179, 35);
			this.stopButton.TabIndex = 11;
			this.stopButton.Text = "Stop";
			this.stopButton.UseVisualStyleBackColor = true;
			this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
			// 
			// sceneLabel
			// 
			this.sceneLabel.AutoSize = true;
			this.sceneLabel.Location = new System.Drawing.Point(859, 24);
			this.sceneLabel.Name = "sceneLabel";
			this.sceneLabel.Size = new System.Drawing.Size(38, 13);
			this.sceneLabel.TabIndex = 13;
			this.sceneLabel.Text = "Scène";
			// 
			// sceneControl
			// 
			this.sceneControl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.sceneControl.FormattingEnabled = true;
			this.sceneControl.Items.AddRange(new object[] {
            "Space",
            "Cornell",
            "Deer"});
			this.sceneControl.Location = new System.Drawing.Point(862, 45);
			this.sceneControl.Name = "sceneControl";
			this.sceneControl.Size = new System.Drawing.Size(167, 21);
			this.sceneControl.TabIndex = 12;
			// 
			// pathtracingControl
			// 
			this.pathtracingControl.AutoSize = true;
			this.pathtracingControl.Checked = true;
			this.pathtracingControl.CheckState = System.Windows.Forms.CheckState.Checked;
			this.pathtracingControl.Location = new System.Drawing.Point(348, 44);
			this.pathtracingControl.Name = "pathtracingControl";
			this.pathtracingControl.Size = new System.Drawing.Size(104, 17);
			this.pathtracingControl.TabIndex = 14;
			this.pathtracingControl.Text = "Use path tracing";
			this.pathtracingControl.UseVisualStyleBackColor = true;
			this.pathtracingControl.CheckedChanged += new System.EventHandler(this.pathtracingControl_CheckedChanged);
			// 
			// Form1
			// 
			this.AcceptButton = this.startButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.ClientSize = new System.Drawing.Size(1162, 899);
			this.Controls.Add(this.pathtracingControl);
			this.Controls.Add(this.sceneLabel);
			this.Controls.Add(this.sceneControl);
			this.Controls.Add(this.stopButton);
			this.Controls.Add(this.specularBouncesLabel);
			this.Controls.Add(this.diffuseBouncesLabel);
			this.Controls.Add(this.sppLabel);
			this.Controls.Add(this.sppControl);
			this.Controls.Add(this.specularBouncesControl);
			this.Controls.Add(this.diffuseBouncesControl);
			this.Controls.Add(this.checkBox1);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.startButton);
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "EA Pierre - LIM Serhane";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EventFormClosing);
			this.Shown += new System.EventHandler(this.EventFormShown);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.diffuseBouncesControl)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.specularBouncesControl)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.sppControl)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button startButton;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.NumericUpDown diffuseBouncesControl;
		private System.Windows.Forms.NumericUpDown specularBouncesControl;
		private System.Windows.Forms.NumericUpDown sppControl;
		private System.Windows.Forms.Label sppLabel;
		private System.Windows.Forms.Label diffuseBouncesLabel;
		private System.Windows.Forms.Label specularBouncesLabel;
		private System.Windows.Forms.Button stopButton;
		private System.Windows.Forms.Label sceneLabel;
		private System.Windows.Forms.ComboBox sceneControl;
		private System.Windows.Forms.CheckBox pathtracingControl;
	}
}

