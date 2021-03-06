﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace Polygons.Forms.DialogForms
{
    /// <summary>
    /// Dialog box asking the user to specify a name for a polygon
    /// </summary>
    internal partial class ShapeNameChooserPrompt : Form
    {
        private readonly PolygonCreationForm parentForm;
        internal ShapeNameChooserPrompt(PolygonCreationForm parentForm)
        {
            InitializeComponent();
            this.parentForm = parentForm;
            textBox1.Text = parentForm.ShapeName;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            parentForm.ShapeName = textBox1.Text;
            if (parentForm.ShapeName == string.Empty)
            { label1.ForeColor = Color.Red; }
            else
            { Close(); }
            
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label1.ForeColor = default(Color);
        }
    }
}
