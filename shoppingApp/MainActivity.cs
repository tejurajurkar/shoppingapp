using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;
using Android.Content;
using Android.Views;


using System.Collections.Generic;
using shoppingApp.Resources;

namespace shoppingApp
{
   
    [Activity(Label = "Product List", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        Button btnAdd, btnSearch;
        EditText txtSearch;
        ListView lv;
        IList<Product> listItsms = null;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            btnAdd = FindViewById<Button>(Resource.Id.ProductList_btnAdd);
            btnSearch = FindViewById<Button>(Resource.Id.ProductList_btnSearch);
            txtSearch = FindViewById<EditText>(Resource.Id.ProductList_txtSearch);
            lv = FindViewById<ListView>(Resource.Id.ProductList_listView);

            btnAdd.Click += delegate
            {
                var activityAddEdit = new Intent(this, typeof(AddEditProductActivity));
                StartActivity(activityAddEdit);

            };

            btnSearch.Click += delegate
            {
                LoadProductInList();
            };

            LoadProductInList();

        }

        private void LoadProductInList()
        {
            ProductDbHelper dbVals = new ProductDbHelper(this);
            if (txtSearch.Text.Trim().Length < 1)
            {
                listItsms = dbVals.GetAllProducts();
            }
            else
            {

                listItsms = dbVals.GetProductsBySearchName(txtSearch.Text.Trim());
            }


            lv.Adapter = new ProductListBasetAdapter(this, listItsms);

            lv.ItemLongClick += lv_ItemLongClick;
        }

        private void lv_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            Product o = listItsms[e.Position];

            //  Toast.MakeText(this, o.Id.ToString(), ToastLength.Long).Show();

            var activityAddEdit = new Intent(this, typeof(AddEditProductActivity));
            activityAddEdit.PutExtra("ProductId", o.P_Id.ToString());
            activityAddEdit.PutExtra("ProductName", o.P_Name);
            StartActivity(activityAddEdit);
        }

    }
}