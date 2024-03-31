using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Salon.ModelData;
using Salon.UserElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Salon
{
    public partial class Form1 : Form
    {
        private enum SwipeType
        {
            Left, Right
        }
        private Model1 model = new Model1();
        private List<Product> products = new List<Product>();
        private int SvipeID;

        private void Loadproducts()
        {
            products.Clear();
            SvipeID = 0;
            products = model.Product.ToList();
        }
        private void Sort()
        {
            if (checkBox1.Checked == false)
            {
                if (comboBoxSort.SelectedIndex == 0)
                    products = products.OrderBy(x => x.ID).ToList();
                else if (comboBoxSort.SelectedIndex == 1)
                    products = products.OrderBy(x => x.Title).ToList();
                else if (comboBoxSort.SelectedIndex == 2)
                    products = products.OrderBy(x => x.Cost).ToList();
            }
            else
            {
                if (comboBoxSort.SelectedIndex == 0)
                    products = products.OrderByDescending(x => x.ID).ToList();
                else if (comboBoxSort.SelectedIndex == 1)
                    products = products.OrderByDescending(x => x.Title).ToList();
                else if (comboBoxSort.SelectedIndex == 2)
                    products = products.OrderByDescending(x => x.Cost).ToList();
            }
            CreateTile();
        }

        private void SetTextlabel()
        {
            if (products.Count != 0)
            {
                labelCount.Text = products.Count >= 6 ?
                    $"с {SvipeID + 1} по {SvipeID + 6} из {products.Count} Товаров" :
                    $"с 1 по {products.Count} Товаров";
            }
            else
                labelCount.Text = $"с 0 из {products.Count} Товаров";
        }
        private void CreateTile()
        {
            FLPTile.Controls.Clear();
            SetTextlabel();
            for(int i = 0; i < 6; i++)
            {
                if(products.Count > i)
                {
                    int count = i + SvipeID;
                    UserTitle title = new UserTitle(products[count]);
                    FLPTile.Controls.Add(title);
                }
            }
        }
        private void Search()
        {
            products.Clear();
            SvipeID = 0;
            products = model.Product.Where(
                x => x.Title.Contains(textBoxSearch.Text) ).ToList();
            labelNothing.Visible = products.Count == 0 ? true : false;
            CreateTile();
        }
        public Form1()
        {
            InitializeComponent();
            Loadproducts();
            CreateTile();
        }
        private void Svipe(SwipeType svipeType)
        {
            if(svipeType == SwipeType.Left && SvipeID != 0)
            {
                SvipeID--;
                CreateTile();
            }
            if (svipeType == SwipeType.Right && SvipeID + 5 < products.Count - 1)
            {
                SvipeID++;
                CreateTile();
            }
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            Search();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonLeftx2_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < 6; i++)
                Svipe(SwipeType.Left);
        }

        private void buttonLeft_Click(object sender, EventArgs e)
        {
            Svipe(SwipeType.Left);
        }

        private void buttonRightx2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 6; i++)
                Svipe(SwipeType.Right);
        }

        private void buttonRight_Click(object sender, EventArgs e)
        {
            Svipe(SwipeType.Right);
        }

        private void comboBoxSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            Sort();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Sort();
        }
    }
}
