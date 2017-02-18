using Android.App;
using Android.Widget;
using Android.OS;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using Android.Views;
using System.Collections.Generic;
using Android.Gms.Maps;
using Android.Locations;
using Android.Content;
using Android.Graphics.Drawables;
using System;

namespace PlaceMap
{
    [Activity(Label = "PlaceMap", MainLauncher = true, Icon = "@drawable/icon",Theme ="@style/MyTheme")]
    public class MainActivity : ActionBarActivity//, IOnMapReadyCallback
    {
        private GoogleMap mMap;
        private bool isRunButtonMain = false;
        AnimationDrawable rocketAnimation;

        private SupportToolbar mToolbar;
        private DrawerLayout mDrawerLayout;
        private ListView mLeftDrawer;
        private MyActionBarDrawerToggle mDrawerToggle;

        private List<ItemMenu> mItems;
        private ListView mListView;
        
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            
            SetContentView (Resource.Layout.Main);
            LocationManager locationManager = GetSystemService(Context.LocationService) as LocationManager;

            mToolbar = FindViewById<SupportToolbar>(Resource.Id.toolbar);

            mToolbar = FindViewById<SupportToolbar>(Resource.Id.toolbar);
            mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            mLeftDrawer = FindViewById<ListView>(Resource.Id.left_drawer);
            SetSupportActionBar(mToolbar);
            

            mDrawerToggle = new MyActionBarDrawerToggle(
                this,                           //Host Activity
                mDrawerLayout,                  //DrawerLayout
                Resource.String.openDrawer,     //Opened Message
                Resource.String.closeDrawer     //Closed Message
            );
            mDrawerLayout.SetDrawerListener(mDrawerToggle);
            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetDisplayShowTitleEnabled(true);
            mDrawerToggle.SyncState();

            mListView = FindViewById<ListView>(Resource.Id.left_drawer);
            mItems = new List<ItemMenu>();
            mItems.Add(new ItemMenu() {textViewMenu=GetString(Resource.String.login),imageViewMenu=Resource.Drawable.Login });
            mItems.Add(new ItemMenu() { textViewMenu = GetString(Resource.String.post), imageViewMenu = Resource.Drawable.Post });
            mItems.Add(new ItemMenu() { textViewMenu = GetString(Resource.String.locationManager), imageViewMenu = Resource.Drawable.Manager });
            mItems.Add(new ItemMenu() { textViewMenu = GetString(Resource.String.language), imageViewMenu = Resource.Drawable.Language });
            mItems.Add(new ItemMenu() { textViewMenu = GetString(Resource.String.about), imageViewMenu = Resource.Drawable.Info });
            mItems.Add(new ItemMenu() { textViewMenu = GetString(Resource.String.exit), imageViewMenu = Resource.Drawable.Exit });

            ListviewAdapterMenu adapter = new ListviewAdapterMenu(this, mItems);
            mListView.Adapter = adapter;
            mListView.ItemClick += mListView_ItemClick;

            var newFragment = new GoogleMapFragment(locationManager);
            var ft = SupportFragmentManager.BeginTransaction();
            ft.Add(Resource.Id.fragmentContainer, newFragment,"Fragment1");
            ft.Commit();
            mDrawerLayout.SetDrawerLockMode(DrawerLayout.LockModeLockedClosed);
        }

        private void mListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            switch (e.Position)
            {
                case 0:
                    Intent login = new Intent(this, typeof(LogIn));
                    StartActivity(login);
                    break;
                case 1:
                    Intent post = new Intent(this, typeof(PostLocation));
                    StartActivity(post);
                    break;
            }
            
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            mDrawerToggle.OnOptionsItemSelected(item);
            return base.OnOptionsItemSelected(item);
        }

       
    }
}

