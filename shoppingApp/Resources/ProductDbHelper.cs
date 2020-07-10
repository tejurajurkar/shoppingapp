using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.Database;
using Android.Database.Sqlite;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace shoppingApp.Resources
{
    public class ProductDbHelper : SQLiteOpenHelper
    {
        private const string APP_DATABASENAME = "Product.db";
        private const int APP_DATABASE_VERSION = 1;


        public ProductDbHelper(Context ctx) :
            base(ctx, APP_DATABASENAME, null, APP_DATABASE_VERSION)
        {

        }

        public override void OnCreate(SQLiteDatabase db)
        {
          /*  db.ExecSQL(@"CREATE TABLE IF NOT EXISTS Product(
                            P_Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            P_Name TEXT NOT NULL,
                            Image  TEXT NOT NULL,
                            Price   INTEGER NOT NULL,
                            Details TEXT)");

            db.ExecSQL("Insert into Product(P_Name,Image,Price ,Details)VALUES('Ram','image1',806,'this is details')");
            db.ExecSQL("Insert into Product (P_Name,Image,Price ,Details)VALUES('Ram','im','506','this is details')");
            db.ExecSQL("Insert into Product(P_Name,Image,Price ,Details)VALUES('Ram','jj','500','this is details')");
            */
            
        }

        public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
        {
            db.ExecSQL("DROP TABLE IF EXISTS Product");
            OnCreate(db);
        }

        //Retrive All Contact Details
        public IList<Product> GetAllProducts()
        {

            SQLiteDatabase db = this.ReadableDatabase;
            ICursor c = db.Query("Product", new string[] { "P_Id", "P_Name", "Image", "Price", "Details" }, null, null, null, null, null);

            var products = new List<Product>();

            while (c.MoveToNext())
            {
                products.Add(new Product
                {
                    P_Id = c.GetInt(0),
                    P_Name = c.GetString(1),
                    Image = c.GetString(2),
                    Price = c.GetInt(3),
                    Details = c.GetString(4)
                });
            }
            c.Close();
       
            return products;

          
            
        }


        //Retrive All Contact Details
        public IList<Product> GetProductsBySearchName(string nameToSearch)
        {

            SQLiteDatabase db = this.ReadableDatabase;

            ICursor c = db.Query("Product", new string[] { "P_Id", "P_Name", "Image"," Price", "Details" }, "upper(P_Name) LIKE ?", new string[] { "%" + nameToSearch.ToUpper() + "%" }, null, null, null, null);

            var products = new List<Product>();

            while (c.MoveToNext())
            {
                products.Add(new Product
                {
                    P_Id = c.GetInt(0),
                    P_Name = c.GetString(1),
                    Image = c.GetString(2),
                    Price = c.GetInt(3),
                    Details = c.GetString(4)
                });
            }

            c.Close();
            db.Close();

            return products;
        }

        //Add New Contact
        public void AddNewProduct(Product productinfo)
        {
            SQLiteDatabase db = this.WritableDatabase;
            ContentValues vals = new ContentValues();
            vals.Put("ProductName", productinfo.P_Name);
            vals.Put("ImageUrl", productinfo.Image);
            vals.Put("Price", productinfo.Price);
            vals.Put("Details", productinfo.Details);
            db.Insert("Product", null, vals);
        }

        //Get contact details by contact Id
        public ICursor getProductById(int id)
        {
            SQLiteDatabase db = this.ReadableDatabase;
            ICursor res = db.RawQuery("select * from Product where P_Id=" + id + "", null);
            return res;
        }

        //Update Existing contact
        public void UpdateProduct(Product contitem)
        {
            if (contitem == null)
            {
                return;
            }

            //Obtain writable database
            SQLiteDatabase db = this.WritableDatabase;

            //Prepare content values
            ContentValues vals = new ContentValues();
            vals.Put("ProductName", contitem.P_Name);
            vals.Put("ImageUrl", contitem.Image);
            vals.Put("Price", contitem.Price);
            vals.Put("Details", contitem.Details);


            ICursor cursor = db.Query("Product",
                    new String[] { "P_Id", "P_Name", "Image", "Price", "Details" }, "P_Id=?", new string[] { contitem.P_Id.ToString() }, null, null, null, null);

            if (cursor != null)
            {
                if (cursor.MoveToFirst())
                {
                    // update the row
                    db.Update("Product", vals, "P_Id=?", new String[] { cursor.GetString(0) });
                }

                cursor.Close();
            }

        }


        //Delete Existing contact
        public void DeleteProduct(string productId)
        {
            if (productId == null)
            {
                return;
            }

            //Obtain writable database
            SQLiteDatabase db = this.WritableDatabase;

            ICursor cursor = db.Query("Product",
                    new String[] { "P_Id", "P_Name", "Image", "Price", "Details" }, "P_Id=?", new string[] { productId }, null, null, null, null);

            if (cursor != null)
            {
                if (cursor.MoveToFirst())
                {
                    // update the row
                    db.Delete("Product", "P_Id=?", new String[] { cursor.GetString(0) });
                }

                cursor.Close();
            }

        }



    }
}