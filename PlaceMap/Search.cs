using System.Text;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;
using PlaceMap.Resources.layout;
using System;

namespace PlaceMap
{
    [Activity(Label = "Search",Theme= "@style/MyTheme")]
    public class Search : ActionBarActivity, SeekBar.IOnSeekBarChangeListener
    {
        TextView radius;
        SeekBar seekBar;
        SupportToolbar toolbar;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Search);
            toolbar = FindViewById<SupportToolbar>(Resource.Id.toolbarChild);
            toolbar.SetTitle(Resource.String.search);

            SetSupportActionBar(toolbar);

            toolbar.SetNavigationIcon(Resource.Drawable.abc_ic_ab_back_mtrl_am_alpha);

            //process seekbar
            seekBar = FindViewById<SeekBar>(Resource.Id.seekBar);
            radius = FindViewById<TextView>(Resource.Id.radius);
            seekBar.SetOnSeekBarChangeListener(this);

        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Finish();
            return base.OnOptionsItemSelected(item);
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
    }
}