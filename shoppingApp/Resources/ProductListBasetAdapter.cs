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

namespace shoppingApp.Resources
{
    
        [Activity(Label = "ProductListBasetAdapter")]
        public partial class ProductListBasetAdapter : BaseAdapter<Product>
        {
            IList<Product> productListArrayList;
            private LayoutInflater mInflater;
            private Context activity;
            public ProductListBasetAdapter(Context context,
                                                    IList<Product> results)
            {
                this.activity = context;
                productListArrayList = results;
                mInflater = (LayoutInflater)activity.GetSystemService(Context.LayoutInflaterService);
            }

            public override int Count
            {
                get { return productListArrayList.Count; }
            }

            public override long GetItemId(int position)
            {
                return position;
            }

            public override Product this[int position]
            {
                get { return productListArrayList[position]; }
            }

            public override View GetView(int position, View convertView, ViewGroup parent)
            {
                ImageView btnDelete;
                ProductsViewHolder holder = null;
                if (convertView == null)
                {
                    convertView = mInflater.Inflate(Resource.Layout.list_row_product_list, null);
                    holder = new ProductsViewHolder();

                    holder.txtName = convertView.FindViewById<TextView>(Resource.Id.lr_productName);
                    holder.txtImage= convertView.FindViewById<TextView>(Resource.Id.lr_Image);
                    holder.txtPrice = convertView.FindViewById<TextView>(Resource.Id.lr_price);
                    holder.txtDescription = convertView.FindViewById<TextView>(Resource.Id.lr_description);
                    btnDelete = convertView.FindViewById<ImageView>(Resource.Id.lr_deleteBtn);


                    btnDelete.Click += (object sender, EventArgs e) =>
                    {

                        AlertDialog.Builder builder = new AlertDialog.Builder(activity);
                        AlertDialog confirm = builder.Create();
                        confirm.SetTitle("Confirm Delete");
                        confirm.SetMessage("Are you sure delete?");
                        confirm.SetButton("OK", (s, ev) =>
                        {
                            var poldel = (int)((sender as ImageView).Tag);

                            string id = productListArrayList[poldel].P_Id.ToString();
                            string fname = productListArrayList[poldel].P_Name;

                            productListArrayList.RemoveAt(poldel);

                            DeleteSelectedProduct(id);
                            NotifyDataSetChanged();

                            Toast.MakeText(activity, "Product Deeletd Successfully", ToastLength.Short).Show();
                        });
                        confirm.SetButton2("Cancel", (s, ev) =>
                        {

                        });

                        confirm.Show();
                    };

                    convertView.Tag = holder;
                    btnDelete.Tag = position;
                }
                else
                {
                    btnDelete = convertView.FindViewById<ImageView>(Resource.Id.lr_deleteBtn);
                    holder = convertView.Tag as ProductsViewHolder;
                    btnDelete.Tag = position;
                }

                holder.txtName.Text = productListArrayList[position].P_Name;
                holder.txtImage.Text = productListArrayList[position].Image;
                holder.txtPrice.Text = productListArrayList[position].Price.ToString();
                holder.txtDescription.Text = productListArrayList[position].Details;

                if (position % 2 == 0)
                {
                    convertView.SetBackgroundResource(Resource.Drawable.list_selector);
                }
                else
                {
                    convertView.SetBackgroundResource(Resource.Drawable.list_selector_alternate);
                }

                return convertView;
            }

            public IList<Product> GetAllData()
            {
                return productListArrayList;
            }

            public class ProductsViewHolder : Java.Lang.Object
            {
                public TextView txtName { get; set; }
                public TextView txtImage{ get; set; }
                public TextView txtPrice { get; set; }
                public TextView txtDescription { get; set; }
            }

            private void DeleteSelectedProduct(string productId)
            {
                ProductDbHelper _db = new ProductDbHelper(activity);
                _db.DeleteProduct(productId);
            }

        }
    }