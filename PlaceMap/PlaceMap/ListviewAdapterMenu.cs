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
    class ItemMenu
    {
        public string textViewMenu { get; set; }
        public int imageViewMenu { get; set; }
    }
    class ListviewAdapterMenu : BaseAdapter<ItemMenu>
    {
        private List<ItemMenu> mItems;
        private Context mContext;
        public ListviewAdapterMenu(Context context, List<ItemMenu> items)
        {
            mItems = items;
            mContext = context;
        }


        public override int Count
        {
            get
            {
                return mItems.Count;
            }
        }

        public override ItemMenu this[int position]
        {
            get
            {
                return mItems[position];
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;
            if (row == null)
            {
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.ListviewMenu, null, false);
            }
            ImageView imageViewMenu = row.FindViewById<ImageView>(Resource.Id.imageViewMenu);
            
            imageViewMenu.SetImageResource(mItems[position].imageViewMenu);

            TextView txtName = row.FindViewById<TextView>(Resource.Id.textViewMenu);
            txtName.Text = mItems[position].textViewMenu;
            return row;
        }
        
    }
}