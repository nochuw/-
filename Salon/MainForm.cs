﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Salon.ModelData;

namespace Salon
{
    public partial class MainForm : Form
    {
        private Product _product;
        public MainForm(Product product)
        {
            InitializeComponent();
            _product = product;
            LoadData();
        }
        private void LoadData()
        {
            textBoxName.Text = _product.Title;
            textBoxCost.Text = _product.Cost.ToString();
            textBoxManufacture.Text = _product.Manufacturer.Name;
            textBoxDescription.Text = _product.Description;
            checkBoxActive.Checked = _product.IsActive;
            try
            {
                pictureBox1.Image = Image.FromFile(_product.MainImagePath);
            }
            catch
            {
                pictureBox1.Image = Salon.Properties.Resources.beauty_logo;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
