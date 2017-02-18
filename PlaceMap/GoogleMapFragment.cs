
using Android.Widget;
using Android.OS;
using Android.Gms.Maps;
using Android.Locations;
using Android.Gms.Maps.Model;
using Android.Content;
using Android.Support.V4.App;
using Android.Graphics.Drawables;
using Android.Views.Animations;
using System;
using Android.Views;
using Android.Graphics;
using Android.Text;

namespace PlaceMap
{
    class GoogleMapFragment : Fragment, IOnMapReadyCallback, Android.Gms.Maps.GoogleMap.IInfoWindowAdapter
    {
        private GoogleMap mMap;
        LocationManager locationManager;
        private bool isRunButtonMain = false;
        LayoutInflater _inflater;
  
        public GoogleMapFragment(LocationManager _locationManager)
        {
            locationManager = _locationManager;
        }
        public override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            _inflater = inflater;
            View view = inflater.Inflate(Resource.Layout.MapFragment, container, false);

            ImageButton buttonMain = view.FindViewById<ImageButton>(Resource.Id.buttonMain);

            ImageButton imageSearch = view.FindViewById<ImageButton>(Resource.Id.buttonSearch);
            ImageButton imageFastSearch = view.FindViewById<ImageButton>(Resource.Id.buttonFastSearch);
            ImageButton imageDirection = view.FindViewById<ImageButton>(Resource.Id.buttonDirection);

            //hide button
            imageSearch.Visibility = ViewStates.Invisible;
            imageFastSearch.Visibility = ViewStates.Invisible;
            imageDirection.Visibility = ViewStates.Invisible;

            //button main click
            buttonMain.Click += delegate
            {
                if (isRunButtonMain == false)
                {
                    buttonMain.SetImageResource(Resource.Animation.ButtonMainOpen);

                    AnimationDrawable animation = (AnimationDrawable)buttonMain.Drawable;
                    animation.Start();

                    imageSearch.Visibility = ViewStates.Visible;
                    var animationSearchOpen = AnimationUtils.LoadAnimation(view.Context, Resource.Animation.ButtonSearchRolateOpen);
                    imageSearch.StartAnimation(animationSearchOpen);

                    imageFastSearch.Visibility = ViewStates.Visible;
                    var animationFastSearchOpen = AnimationUtils.LoadAnimation(view.Context, Resource.Animation.ButtonFastSearchRolateOpen);
                    imageFastSearch.StartAnimation(animationFastSearchOpen);

                    imageDirection.Visibility = ViewStates.Visible;
                    var animationDirectionOpen = AnimationUtils.LoadAnimation(view.Context, Resource.Animation.ButtonDirectionRolateOpen);
                    imageDirection.StartAnimation(animationDirectionOpen);

                    isRunButtonMain = true;
                }
                else
                {
                    buttonMain.SetImageResource(Resource.Animation.ButtonMainClose);

                    AnimationDrawable animation = (AnimationDrawable)buttonMain.Drawable;
                    animation.Start();

                    var animationSearchClose = AnimationUtils.LoadAnimation(view.Context, Resource.Animation.ButtonSearchRolateClose);
                    imageSearch.StartAnimation(animationSearchClose);
                    imageSearch.Visibility = ViewStates.Invisible;

                    var animationFastSearchClose = AnimationUtils.LoadAnimation(view.Context, Resource.Animation.ButtonFastSearchRolateClose);
                    imageFastSearch.StartAnimation(animationFastSearchClose);
                    imageFastSearch.Visibility = ViewStates.Invisible;

                    var animationDirectionClose = AnimationUtils.LoadAnimation(view.Context, Resource.Animation.ButtonDirectionRolateClose);
                    imageDirection.StartAnimation(animationDirectionClose);
                    imageDirection.Visibility = ViewStates.Invisible;

                    isRunButtonMain = false;
                }
            };
            ImageButton search = view.FindViewById<ImageButton>(Resource.Id.buttonSearch);
            search.Click += delegate
            {
                Intent start = new Intent(view.Context, typeof(Search));
                StartActivity(start);
            };

            ImageButton fastSearch = view.FindViewById<ImageButton>(Resource.Id.buttonFastSearch);
            fastSearch.Click += delegate
            {
                Intent start = new Intent(view.Context, typeof(FastSearch));
                StartActivity(start);
            };

            ImageButton ggDirection = view.FindViewById<ImageButton>(Resource.Id.buttonDirection);
            ggDirection.Click += delegate
            {
                Intent start = new Intent(view.Context, typeof(GGDirection));
                StartActivity(start);
            };
            

            return view;
        
        }
    
        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            SupportMapFragment mapFragment = (SupportMapFragment)ChildFragmentManager.FindFragmentById(Resource.Id.mapnew);
            mapFragment.GetMapAsync(this);
        }
        public void OnMapReady(GoogleMap googleMap)
        {
            mMap = googleMap;
            mMap.MyLocationEnabled = true;
            MyLocation();
            if (mMap != null)
            {
                mMap.SetInfoWindowAdapter(this);
            }
         
        }
 
        private void MyLocation()
        {

            //LocationManager locationManager = GetSystemService(Context.LocationService) as LocationManager;
            Criteria criteria = new Criteria();

            Location lastLocation = locationManager.GetLastKnownLocation(locationManager.GetBestProvider(criteria, false));
            if (lastLocation != null)
            {
                LatLng latLng = new LatLng(lastLocation.Latitude, lastLocation.Longitude);
                mMap.AnimateCamera(CameraUpdateFactory.NewLatLngZoom(latLng, 10));

                CameraPosition cameraPosition = new CameraPosition.Builder()
                .Target(latLng)      // Sets the center of the map to location user
                .Zoom(15)                   // Sets the zoom
                .Bearing(90)                // Sets the orientation of the camera to east
                .Tilt(40)                   // Sets the tilt of the camera to 30 degrees
                .Build();                   // Creates a CameraPosition from the builder
                mMap.AnimateCamera(CameraUpdateFactory.NewCameraPosition(cameraPosition));
                //Thêm MarketOption cho Map:
                MarkerOptions option = new MarkerOptions();
                option.SetTitle("Chỗ Tui đang ngồi đó");
                option.SetSnippet("Gần làng SOS");
                option.SetPosition(latLng);
                Marker currentMarker = mMap.AddMarker(option);
                currentMarker.ShowInfoWindow();
            }
        }

        public View GetInfoContents(Marker marker)
        {
          
            return null;
        }
        
        public View GetInfoWindow(Marker marker)
        {
           View view = _inflater.Inflate(Resource.Layout.ChoosePlace, null);
            
            return view;
        }
           
        public void OnInfoWindowClick(Marker marker)
        {
            Console.WriteLine("okkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkk");
            
        }
    }
}