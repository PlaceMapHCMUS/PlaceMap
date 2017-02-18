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

namespace PlaceMap
{
    class Item
    {
        public int imageButton { get; set; }
        public string textView { get; set; }
    }
    class GridviewFastSearch:BaseAdapter<Item>
    {
       

        // references to our images
        int[] thumbIds = {
                Resource.Drawable.school,
                Resource.Drawable.park,
                Resource.Drawable.Hopital,
                Resource.Drawable.bank,
                Resource.Drawable.repairs,
                Resource.Drawable.coffee,
                Resource.Drawable.shop,
                Resource.Drawable.gas,
                Resource.Drawable.add
           };
        private List<Item> mItems;
        private Context mContext;
        public GridviewFastSearch(Context context, List<Item> items)
        {
            mItems = items;
            mContext = context;
        }

        public override Item this[int position]
        {
            get
            {
                return mItems[position];
            }
        }

        public override int Count
        {
            get
            {
                return mItems.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View block = convertView;
            if (block == null)
            {
                block = LayoutInflater.From(mContext).Inflate(Resource.Layout.GridViewFastSearch, null, false);
            }
            ImageView imageViewMenu = block.FindViewById<ImageView>(Resource.Id.image);

            imageViewMenu.SetImageResource(mItems[position].imageButton);

            TextView txtName = block.FindViewById<TextView>(Resource.Id.text);
            txtName.Text = mItems[position].textView;
            return block;
        }
    }
}