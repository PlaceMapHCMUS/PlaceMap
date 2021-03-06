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
    [Activity(Label = "GGDirection", Theme = "@style/MyTheme")]
    public class GGDirection : ActionBarActivity
    {
        SupportToolbar toolbar;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.GGDirection);
            toolbar = FindViewById<SupportToolbar>(Resource.Id.toolbarChild);
            toolbar.SetTitle(Resource.String.ggDirection);

            SetSupportActionBar(toolbar);

            toolbar.SetNavigationIcon(Resource.Drawable.abc_ic_ab_back_mtrl_am_alpha);


        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Finish();
            return base.OnOptionsItemSelected(item);
        }
    }
}
