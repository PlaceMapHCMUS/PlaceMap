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

using Android.Support.V7.App;
using Microsoft.WindowsAzure.MobileServices;
using Android.Widget;
using System.Threading.Tasks;

namespace PlaceMap
{
    [Activity(Label = "CreateAccount", Theme = "@style/MyTheme")]
    public class CreateAccount : ActionBarActivity
    {
        SupportToolbar toolbar;
        Button signUp;
        EditText create, email, pass, rePass;

        CustomerManager manager = new CustomerManager();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.CreateAccount);
            toolbar = FindViewById<SupportToolbar>(Resource.Id.toolbarChild);
            toolbar.SetTitle(Resource.String.register);

            SetSupportActionBar(toolbar);

            toolbar.SetNavigationIcon(Resource.Drawable.abc_ic_ab_back_mtrl_am_alpha);

            //sign up
            signUp = FindViewById<Button>(Resource.Id.buttonSignin);
            create = FindViewById<EditText>(Resource.Id.createAccount);
            email = FindViewById<EditText>(Resource.Id.email);
            pass = FindViewById<EditText>(Resource.Id.password);
            rePass = FindViewById<EditText>(Resource.Id.rePassword);
            signUp.Click += async delegate
            {
                List<string> listAccount = await manager.GetListAccount();

                if (listAccount.Contains(create.Text))
                {
                    Toast.MakeText(this, "Account already exists ", ToastLength.Short).Show();
                    return;
                }

               

                if (create.Text == "" || email.Text == "" || pass.Text == "" || rePass.Text == "")
                {
                    Toast.MakeText(this, "Please enter full", ToastLength.Short).Show();

                    return;
                }
               
                if (pass.Text.CompareTo(rePass.Text)!=0)
                {
                    Toast.MakeText(this, "Re-password is wrong", ToastLength.Short).Show();
                   
                    return;
                }

                if (pass.Text.Length<6)
                {
                    Toast.MakeText(this, "Pass must contain at least 6 characters", ToastLength.Short).Show();
                    return;
                }

                Customer item = new Customer();
                item.account = create.Text;
                item.email = email.Text;
                item.pass = pass.Text;

                 await  manager.AddItem(item);
                Toast.MakeText(this, "Your account was created", ToastLength.Short).Show();

                // await MainActivity.mClient.GetTable<Customer>().InsertAsync(item);
                //   IMobileServiceTable<ImagePlace> table = MainActivity.mClient.GetTable<ImagePlace>();
                // IMobileServiceTable<Place> place = MainActivity.mClient.GetTable<Place>();

                //var query = from m in table
                //            where m.id == "7a706c90-df20-417e-b3f6-009c3d4fab65"
                //            select m;
                //List<ImagePlace> list = await query.ToListAsync();
                //int t = 5;

                //var query1 = from m in place
                //             where m.id == list[0].idPlace
                //             select m;
                //List<Place> li = await query1.ToListAsync();
                //int f = 5;

                //    var query = from m in table join t in place on m equals "3" 
            };
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Finish();
            return base.OnOptionsItemSelected(item);
        }
    }
}