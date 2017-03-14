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
using Android.Graphics;
using System.IO;
using Java.Util;
using Android.Util;

namespace PlaceMap
{
    [Activity(Label = "PostLocation",Theme ="@style/MyTheme")]
    public class PostLocation : ActionBarActivity
    {
        List<ImageButton> arrayImage = new List<ImageButton>();
        SupportToolbar toolbar;
        ImageButton imageFirst;
        ImageButton result;
        bool isImageFinal = false;
        LinearLayout linearLayoutImage;
        int index;
        AutoCompleteTextView keyAuto;

        //bitmap array
        List<String> listStringBitmap = new List<String>();

        //azure 
        PlaceManager placeManager = new PlaceManager();
        ImagePlaceManager imageManager = new ImagePlaceManager();

        Button post;
        EditText key, name, phone, website, descrip;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.PostLocation);
            toolbar = FindViewById<SupportToolbar>(Resource.Id.toolbarChildPost);
            toolbar.SetTitle(Resource.String.login);

            SetSupportActionBar(toolbar);

            toolbar.SetNavigationIcon(Resource.Drawable.abc_ic_ab_back_mtrl_am_alpha);

            linearLayoutImage = FindViewById<LinearLayout>(Resource.Id.image);

            imageFirst = FindViewById<ImageButton>(Resource.Id.upImage);
            arrayImage.Add(imageFirst);

            imageFirst.Click += delegate
            {
                Intent intent = new Intent();
                intent.SetType("image/*");
                intent.SetAction(Intent.ActionGetContent);
                index = 0;
                if(arrayImage.Count==1)
                    isImageFinal = true;
                else
                    isImageFinal = false;
                this.StartActivityForResult(Intent.CreateChooser(intent, "Select a Photo"), 0);

            };

            //autocomplete key
            keyAuto = FindViewById<AutoCompleteTextView>(Resource.Id.key);
            var keys = new string[]
            {
                "Coffee","Restaurant","Store","Hopital","Internet","Repairs","Food","Gas station","School"
            };
            ArrayAdapter adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, keys);
            keyAuto.Adapter = adapter;


            //post data on azure
            GetDataFromScreen();
        }
        public void GetDataFromScreen()
        {
            key = FindViewById<EditText>(Resource.Id.key);
            name = FindViewById<EditText>(Resource.Id.name);
            phone = FindViewById<EditText>(Resource.Id.phone);
            website = FindViewById<EditText>(Resource.Id.website);
            descrip = FindViewById<EditText>(Resource.Id.describe);
            post = FindViewById<Button>(Resource.Id.buttonPost);

            post.Click += async delegate
            {
                Place item = new Place();
                item.keyPlace = key.Text;
                item.name = name.Text;
                item.phone = phone.Text;
                item.website = website.Text;
                item.discrip = descrip.Text;

                await placeManager.AddItem(item);

                for (int i = 0; i < listStringBitmap.Count; i++)
                {
                    ImagePlace itemImage = new ImagePlace();
                    itemImage.idPlace = item.id;
                    itemImage.imagePlace = listStringBitmap[i];
                    await imageManager.AddItem(itemImage);
                }

            };

        }
        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (resultCode == Result.Ok)
            {
                Stream stream = ContentResolver.OpenInputStream(data.Data);
                Bitmap bitmap = BitmapFactory.DecodeStream(stream);

                //resize bitmap
                Bitmap bitmapResized = Bitmap.CreateScaledBitmap(bitmap, 100, 100, true);
                arrayImage[index].SetImageBitmap(bitmapResized);

                //convert image to string
                int numByteBitmap = bitmapResized.ByteCount;
                var memoryStream = new MemoryStream();
                bitmapResized.Compress(Bitmap.CompressFormat.Png, 100, memoryStream);
                var array = memoryStream.ToArray();
                listStringBitmap.Add( Base64.EncodeToString(array, Base64.Default));

                if (isImageFinal)
                {
                    ImageButton imageButton = new ImageButton(this);
                    imageButton.SetImageResource(Resource.Drawable.add);
                    LinearLayout.LayoutParams lp = new LinearLayout.LayoutParams(150,150);
                    lp.LeftMargin = 10;
                    imageButton.LayoutParameters = lp;
                    imageButton.SetScaleType(ImageButton.ScaleType.FitCenter);
                    linearLayoutImage.AddView(imageButton);
                    arrayImage.Add(imageButton);
                    imageButton.Click += delegate
                        {
                            for(int i = 0; i < arrayImage.Count; i++)
                            {
                                if (imageButton == arrayImage[i])
                                    index = i;
                            }
                            Intent intent = new Intent();
                            intent.SetType("image/*");
                            intent.SetAction(Intent.ActionGetContent);
                            
                            if (index == arrayImage.Count - 1)
                                isImageFinal = true;
                            else
                                isImageFinal = false;
                            this.StartActivityForResult(Intent.CreateChooser(intent, "Select a Photo"), 0);

                        };
                }
            }

        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Finish();
            return base.OnOptionsItemSelected(item);
        }
    }
}