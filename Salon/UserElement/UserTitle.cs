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

namespace Salon.UserElement
{
    public partial class UserTitle : UserControl
    {
        private Product _product;
        private Model1 model = new Model1();
        public UserTitle(Product product)
        {
            InitializeComponent();
            Fill(product);
        }
        public void Fill(Product product)
        {
            _product = product;
            labelName.Text = _product.Title;
            labelCost.Text = $"Стоимость: {_product.Cost} руб.";
            try
            {
                pictureBox1.Image = Image.FromFile(_product.MainImagePath);
            }
            catch
            {
                pictureBox1.Image = Salon.Properties.Resources.beauty_logo;
            }
            BackColor = _product.IsActive ? Color.White : Color.LightGray;
        }
        private void Delete()
        {
            DialogResult result = MessageBox.Show(
                $"Вы действительно хотите удалить товар с ID {_product.ID}",
                "Сообщение", MessageBoxButtons.YesNo);
            if(result == DialogResult.Yes)
            {
                try
                {
                    model.Product.Remove(
                        model.Product.First(x => x.ID == _product.ID));
                    model.SaveChanges();
                    this.Dispose();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        public void Clicking(MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                MainForm f = new MainForm(_product);
                f.ShowDialog();
            }
            else if(e.Button == MouseButtons.Right)
            {
                Delete();
            }
        }

        private void labelName_Click(object sender, EventArgs e)
        {

        }

        private void Controls_Click(object sender, MouseEventArgs e)
        {
            Clicking(e);
        }
    }
}
