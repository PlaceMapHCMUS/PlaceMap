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
using PlaceMap.Resources.layout;
using Android.Graphics;

namespace PlaceMap
{
    [Activity(Label = "LogIn", Theme = "@style/MyTheme")]
    public class LogIn : ActionBarActivity
    {
        SupportToolbar toolbar;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.LogIn);
            toolbar = FindViewById<SupportToolbar>(Resource.Id.toolbarChild);
            toolbar.SetTitle(Resource.String.login);

            SetSupportActionBar(toolbar);

            toolbar.SetNavigationIcon(Resource.Drawable.abc_ic_ab_back_mtrl_am_alpha);
            
            ImageView createAccountButton = FindViewById<ImageView>(Resource.Id.createAccount);
            createAccountButton.Click += delegate
            {
                Intent createAccount = new Intent(this, typeof(CreateAccount));
                StartActivity(createAccount);
            };

        }

      

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Finish();
            return base.OnOptionsItemSelected(item);
        }


    }
}