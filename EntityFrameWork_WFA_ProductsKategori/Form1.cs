using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EntityFrameWork_WFA_ProductsKategori.Models;

namespace EntityFrameWork_WFA_ProductsKategori
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        void TedarikciDoldur()
        {
            var list = db.Suppliers.Select(x => new
            {
                x.SupplierID,
                x.CompanyName
            }).ToList();

            comboBox1.DisplayMember = "CompanyName";
            comboBox1.ValueMember = "SupplierID";
            comboBox1.DataSource = list;
        }

        void KategoriDoldur()
        {
            var list = db.Categories.Select(x => new
            {

                x.CategoryID,
                x.CategoryName
            }).ToList();
            comboBox2.DisplayMember = "CategoryName";
            comboBox2.ValueMember = "CategoryID";
            comboBox2.DataSource = list;

        }
        void Doldur()
        {
            var plist = db.Products.Select(x => new
            {
                x.ProductID,
                x.Category.CategoryName,
                x.ProductName,
                x.Supplier.CompanyName,
                x.Supplier.ContactName,
                x.QuantityPerUnit,
                x.UnitPrice,
                x.UnitsInStock
            }).Where(x => x.ProductName.Contains(txtAra.Text) || x.CategoryName.Contains(txtAra.Text)).ToList();

            dataGridView1.DataSource = plist;
        }

        NorthwindEntities db = new NorthwindEntities();

        private void Form1_Load(object sender, EventArgs e)
        {
            Doldur();
            KategoriDoldur();
            TedarikciDoldur();
        }

        private void txtAra_TextChanged(object sender, EventArgs e)
        {
            Doldur();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            Product p = new Product();

            p.ProductName = txtUrunAdi.Text;
            p.UnitPrice = Convert.ToDecimal(txtFiyat.Text);

            p.UnitsInStock = Convert.ToInt16(txtStok.Text);
            //sdajsdasd
            p.QuantityPerUnit = txtMiktar.Text;

            p.SupplierID = Convert.ToInt32(comboBox1.SelectedValue);

            p.CategoryID= Convert.ToInt32(comboBox2.SelectedValue);
            
            db.Products.Add(p);
            db.SaveChanges();



            Doldur();
        }
    }
}
