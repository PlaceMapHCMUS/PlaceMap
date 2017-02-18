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
        }
        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (resultCode == Result.Ok)
            {
                Stream stream = ContentResolver.OpenInputStream(data.Data);
                arrayImage[index].SetImageBitmap(BitmapFactory.DecodeStream(stream));
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