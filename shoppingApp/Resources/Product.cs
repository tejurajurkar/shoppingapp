using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace shoppingApp.Resources
{
 
        public class Product
        {

            //[PrimaryKey, AutoIncrement, Column("_Id")]
            public int P_Id { get; set; }
            public string P_Name { get; set; }
            public string Image { get; set; }
            public int Price { get; set; }
            public string Details { get; set; }

            public static explicit operator Product(Java.Lang.Object v)
            {
                throw new NotImplementedException();
            }

        }
    
}
