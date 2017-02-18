using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;

namespace PlaceMap
{
    [Activity(Label = "FastSearch", Theme = "@style/MyTheme")]
    public class FastSearch : ActionBarActivity, SeekBar.IOnSeekBarChangeListener
    {
        TextView radius;
        SeekBar seekBar;
        SupportToolbar toolbar;
        GridView gridViewImageButton;
        List<Item> listItemGridView = new List<Item>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.FastSearch);
            toolbar = FindViewById<SupportToolbar>(Resource.Id.toolbarChild);
            toolbar.SetTitle(Resource.String.fastSearch);

            SetSupportActionBar(toolbar);

            toolbar.SetNavigationIcon(Resource.Drawable.abc_ic_ab_back_mtrl_am_alpha);

            //set imageButton
            listItemGridView.Add(new Item() { textView = "School", imageButton = Resource.Drawable.school });
            listItemGridView.Add(new Item() { textView = "Park", imageButton = Resource.Drawable.park });
            listItemGridView.Add(new Item() { textView = "Hopital", imageButton = Resource.Drawable.Hopital });
            listItemGridView.Add(new Item() { textView = "Bank", imageButton = Resource.Drawable.bank });
            listItemGridView.Add(new Item() { textView = "Repairs", imageButton = Resource.Drawable.repairs });
            listItemGridView.Add(new Item() { textView = "Coffee", imageButton = Resource.Drawable.coffee });
            listItemGridView.Add(new Item() { textView = "Shop", imageButton = Resource.Drawable.shop });
            listItemGridView.Add(new Item() { textView = "Gas", imageButton = Resource.Drawable.gas });
            listItemGridView.Add(new Item() { textView = "Add", imageButton = Resource.Drawable.addFastSearch });

       
            gridViewImageButton = FindViewById<GridView>(Resource.Id.gridviewFastSearch);
            GridviewFastSearch adapter = new GridviewFastSearch(this,listItemGridView);
            gridViewImageButton.Adapter = adapter;

            //process seekbar
            seekBar = FindViewById<SeekBar>(Resource.Id.seekBar);
            radius = FindViewById<TextView>(Resource.Id.radius);
            seekBar.SetOnSeekBarChangeListener(this);
        }

        public void OnProgressChanged(SeekBar seekBar, int progress, bool fromUser)
        {
            if (fromUser)
            {
                radius.Text = string.Format("{0} km", seekBar.Progress);
            }
        }

        public void OnStartTrackingTouch(SeekBar seekBar)
        {
            return;
        }

        public void OnStopTrackingTouch(SeekBar seekBar)
        {
            return;
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Finish();
            return base.OnOptionsItemSelected(item);
        }
    }
}