using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Database;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace shoppingApp.Resources
{
    [Activity(Label = "AddEditProductActivity")]
    public class AddEditProductActivity : Activity
    {
        EditText txtId, txtName, txtImage, txtPrice, txtDescription;
        Button btnSave;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.AddEditProduct);

            txtId = FindViewById<EditText>(Resource.Id.addEdit_Id);
            txtName = FindViewById<EditText>(Resource.Id.addEdit_ProductName);
            txtImage = FindViewById<EditText>(Resource.Id.addEdit_ImageUrl);
            txtPrice = FindViewById<EditText>(Resource.Id.addEdit_Price);
            txtDescription = FindViewById<EditText>(Resource.Id.addEdit_Description);
            btnSave = FindViewById<Button>(Resource.Id.addEdit_btnSave);

            btnSave.Click += buttonSave_Click;

            string editId = Intent.GetStringExtra("ProductId") ?? string.Empty;

            if (editId.Trim().Length > 0)
            {
                txtId.Text = editId;
                LoadDataForEdit(editId);
            }
        }

        private void LoadDataForEdit(string productId)
        {
            ProductDbHelper db = new ProductDbHelper(this);
            ICursor cData = db.getProductById(int.Parse(productId));
            if (cData.MoveToFirst())
            {
                txtName.Text = cData.GetString(cData.GetColumnIndex("ProductName"));
                txtImage.Text = cData.GetString(cData.GetColumnIndex("Image"));
                txtPrice.Text = cData.GetString(cData.GetColumnIndex("Price"));
                txtDescription.Text = cData.GetString(cData.GetColumnIndex("Details"));
            }
        }

        void buttonSave_Click(object sender, EventArgs e)
        {
            ProductDbHelper db = new ProductDbHelper(this);
            if (txtName.Text.Trim().Length < 1)
            {
                Toast.MakeText(this, "Enter Full Name.", ToastLength.Short).Show();
                return;
            }

            if (txtImage.Text.Trim().Length < 1)
            {
                Toast.MakeText(this, "Enter Image Url.", ToastLength.Short).Show();
                return;
            }

        /*    if (txtEmail.Text.Trim().Length > 0)
            {
                string EmailPattern = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
                if (!Regex.IsMatch(txtEmail.Text, EmailPattern, RegexOptions.IgnoreCase))
                {
                    Toast.MakeText(this, "Invalid email address.", ToastLength.Short).Show();
                    return;
                }
            }*/

            Product ab = new Product();

           if (txtId.Text.Trim().Length > 0)
            {
                ab.P_Id = int.Parse(txtId.Text);
            }
            ab.P_Name = txtName.Text;
            ab.Image = txtImage.Text;
            ab.Price = int.Parse(txtId.Text);
            ab.Details = txtDescription.Text;

            try
            {

                if (txtId.Text.Trim().Length > 0)
                {
                    db.UpdateProduct(ab);
                    Toast.MakeText(this, "Product Updated Successfully.", ToastLength.Short).Show();
                }
                else
                {
                    db.AddNewProduct(ab);
                    Toast.MakeText(this, "New Product Added Successfully.", ToastLength.Short).Show();
                }

                Finish();

                //Go to main activity after save/edit
                var mainActivity = new Intent(this, typeof(MainActivity));
                StartActivity(mainActivity);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}