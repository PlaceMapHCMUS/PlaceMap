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
using Android.Gms.Common.Apis;
using Android.Gms.Common;
using static Android.Gms.Common.Apis.GoogleApiClient;
using Android.Gms.Plus;
using Android.Gms.Plus.Model.People;
using Xamarin.Facebook;
using Android.Content.PM;
using Java.Security;
using Xamarin.Facebook.Login;
using Java.Lang;
using Xamarin.Facebook.Login.Widget;
using Xamarin.Facebook.Share.Model;
using Xamarin.Facebook.Share.Widget;
using System.Net;
namespace PlaceMap
{
    [Activity(Label = "LogIn", Theme = "@style/MyTheme")]
    public class LogIn : ActionBarActivity, IConnectionCallbacks, IOnConnectionFailedListener, IFacebookCallback, GraphRequest.IGraphJSONObjectCallback
    {
        SupportToolbar toolbar;
        private bool mIntentInProgress;
        private GoogleApiClient mGoogleApiClient;
        public SignInButton mSignOn;
        private ConnectionResult mConnectionResult;

        ImageButton signFace;
        ImageButton signFB;
        private bool mSignInClicked;
        private bool mInfoPopulated;

        //facebook
        private ICallbackManager mCallBackManager;
        private MyProfileTracker mProfileTracker;
        private Button mBtnGetEmail;
        private ShareButton mBtnShared;
        private EditText account;
        private EditText pass;
        private Button login;
        CustomerManager manager = new CustomerManager();
       // List<string> listAccount = new List<string>();
       // List<string> listPass = new List<string>();
        protected override  void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            FacebookSdk.SdkInitialize(this.ApplicationContext);
            mProfileTracker = new MyProfileTracker();
            mProfileTracker.mOnProfileChanged += MProfileTracker_mOnProfileChanged;
            mProfileTracker.StartTracking();
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

            //siggnin gg
            mGoogleApiClient = new GoogleApiClient.Builder(this)
                .AddConnectionCallbacks(this)
                .AddOnConnectionFailedListener(this)
                .AddApi(PlusClass.API)
                .AddScope(PlusClass.ScopePlusProfile)
                .AddScope(PlusClass.ScopePlusLogin).Build();


            signFace = FindViewById<ImageButton>(Resource.Id.signGoogle);



            signFace.Click += delegate
            {
                //Fire sign in
                if (!mGoogleApiClient.IsConnecting)
                {
                    mSignInClicked = true;
                    ResolveSignInError();
                }

            };

            //facebook

            //FacebookSdk.SdkInitialize(this.ApplicationContext);
            //PackageInfo info = this.PackageManager.GetPackageInfo("PlaceMap.PlaceMap", PackageInfoFlags.Signatures);
            //foreach (Android.Content.PM.Signature signature in info.Signatures)
            //{
            //    MessageDigest md = MessageDigest.GetInstance("SHA");
            //    md.Update(signature.ToByteArray());
            //    string keyhash = Convert.ToBase64String(md.Digest());
            //    Console.WriteLine("KeyHash:", keyhash);
            //}

            //facebook
            signFB = FindViewById<ImageButton>(Resource.Id.signFacebook);
            mCallBackManager = CallbackManagerFactory.Create();
            LoginManager.Instance.RegisterCallback(mCallBackManager, this);
            signFB.Click += delegate
            {
                if (AccessToken.CurrentAccessToken != null)
                {
                    //The user is logged in through Facebook
                    LoginManager.Instance.LogOut();
                    return;
                }

                else
                {
                    //The user is not logged in
                    LoginManager.Instance.LogInWithReadPermissions(this, new List<string> { "public_profile", "user_friends", "email" });

                }
            };

            // LoginButton button = FindViewById<LoginButton>(Resource.Id.login_button);
            // mBtnShared = FindViewById<ShareButton>(Resource.Id.btnShare);
            // mBtnGetEmail = FindViewById<Button>(Resource.Id.btnGetEmail);
            //button.SetReadPermissions(new List<string> { "public_profile", "user_friends", "email" });

            //mCallBackManager = CallbackManagerFactory.Create();

            //  button.RegisterCallback(mCallBackManager, this);


            //mBtnGetEmail.Click += (o, e) =>
            //{
            //    GraphRequest request = GraphRequest.NewMeRequest(AccessToken.CurrentAccessToken, this);

            //    Bundle parameters = new Bundle();
            //    parameters.PutString("fields", "id,name,age_range,email");
            //    request.Parameters = parameters;
            //    request.ExecuteAsync();
            //};

            // ShareLinkContent content = new ShareLinkContent.Builder().Build();
            //  mBtnShared.ShareContent = content;

            //sign in by app
            account = FindViewById<EditText>(Resource.Id.username);
            pass = FindViewById<EditText>(Resource.Id.password);
            login = FindViewById<Button>(Resource.Id.buttonSignin);

           

            login.Click += async delegate
            {
                List<string> listAccount = await manager.GetListAccount();
                List<string> listPass = await manager.GetListPass();

                if (listAccount.Contains(account.Text) && listPass.Contains(pass.Text))
                {
                    Toast.MakeText(this, "Login successful", ToastLength.Short).Show();
                }
                else
                {
                    Toast.MakeText(this, "Username or password is wrong", ToastLength.Short).Show();
                }
               
            };
        }


        

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Finish();
            return base.OnOptionsItemSelected(item);
        }
        private void ResolveSignInError()
        {
            if (mGoogleApiClient.IsConnected)
            {
                //No need to resolve errors, already connected
                return;
            }

            if (mConnectionResult.HasResolution)
            {
                try
                {
                    mIntentInProgress = true;
                    StartIntentSenderForResult(mConnectionResult.Resolution.IntentSender, 0, null, 0, 0, 0);
                }

                catch (Android.Content.IntentSender.SendIntentException e)
                {
                    //The intent was cancelled before it was sent. Return to the default
                    //state and attempt to connect to get an updated ConnectionResult
                    mIntentInProgress = false;
                    mGoogleApiClient.Connect();
                }
            }
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            mCallBackManager.OnActivityResult(requestCode, (int)resultCode, data);
            if (requestCode == 0)
            {
                if (resultCode != Result.Ok)
                {
                    mSignInClicked = false;
                }

                mIntentInProgress = false;

                if (!mGoogleApiClient.IsConnecting)
                {
                    mGoogleApiClient.Connect();
                }
            }
        }
        protected override void OnStart()
        {
            base.OnStart();
            mGoogleApiClient.Connect();
        }

        protected override void OnStop()
        {
            base.OnStop();
            if (mGoogleApiClient.IsConnected)
            {
                mGoogleApiClient.Disconnect();
            }
        }

        public async void OnConnected(Bundle connectionHint)
        {
            //Successful log in hooray!!
            mSignInClicked = false;

            if (mInfoPopulated)
            {
                //No need to populate info again
                return;
            }

            if (PlusClass.PeopleApi.GetCurrentPerson(mGoogleApiClient) != null)
            {
                IPerson plusUser = PlusClass.PeopleApi.GetCurrentPerson(mGoogleApiClient);

                if (plusUser.HasDisplayName)
                {
                    string t;
                    t = plusUser.Id;
                    List<string> listAccount = await manager.GetListAccount();
                    if (listAccount.Contains(t))
                    {
                        return;
                    }
                    Customer item = new Customer();
                    item.account = t;
                    item.email = "";
                    item.pass = "";
                    await manager.AddItem(item);
                    Toast.MakeText(this, "Google login successful", ToastLength.Short).Show();
                }
                mInfoPopulated = true;
            }
        }
        //login facebook
        public void OnConnectionSuspended(int cause)
        {
            throw new NotImplementedException();
        }

        public void OnConnectionFailed(ConnectionResult result)
        {
            if (!mIntentInProgress)
            {
                //Store the ConnectionResult so that we can use it later when the user clicks 'sign-in;
                mConnectionResult = result;

                if (mSignInClicked)
                {
                    //The user has already clicked 'sign-in' so we attempt to resolve all
                    //errors until the user is signed in, or the cancel
                    ResolveSignInError();
                }
            }
        }

        public void OnCompleted(Org.Json.JSONObject json, GraphResponse response)
        {
            //string data = json.ToString();
            // FacebookResult result = JsonConvert.DeserializeObject<FacebookResult>(data);
        }

        void client_UploadValuesCompleted(object sender, UploadValuesCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

       async void MProfileTracker_mOnProfileChanged(object sender, OnProfileChangedEventArgs e)
        {
            if (e.mProfile != null)
            {
                try
                {
                    string temp1 = e.mProfile.Id;
                    List<string> listAccount = await manager.GetListAccount();
                    if (listAccount.Contains(temp1))
                    {
                        return;
                    }
                    Customer item = new Customer();
                    item.account = temp1;
                    item.email = "";
                    item.pass = "";
                    await manager.AddItem(item);
                    Toast.MakeText(this, "Facebook login successful", ToastLength.Short).Show();

                }

                catch (Java.Lang.Exception ex)
                {
                    //Handle error
                }
            }

            else
            {

            }
        }

        public void OnCancel()
        {
            //throw new NotImplementedException();
        }

        public void OnError(FacebookException error)
        {
            //throw new NotImplementedException();
        }

        public void OnSuccess(Java.Lang.Object result)
        {
            LoginResult loginResult = result as LoginResult;
            Console.WriteLine(AccessToken.CurrentAccessToken.UserId);
        }



        protected override void OnDestroy()
        {
            mProfileTracker.StopTracking();
            base.OnDestroy();
        }

    }
    public class MyProfileTracker : ProfileTracker
    {
        public event EventHandler<OnProfileChangedEventArgs> mOnProfileChanged;

        protected override void OnCurrentProfileChanged(Profile oldProfile, Profile newProfile)
        {
            if (mOnProfileChanged != null)
            {
                mOnProfileChanged.Invoke(this, new OnProfileChangedEventArgs(newProfile));
            }
        }
    }

    public class OnProfileChangedEventArgs : EventArgs
    {
        public Profile mProfile;

        public OnProfileChangedEventArgs(Profile profile) { mProfile = profile; }
    }
}