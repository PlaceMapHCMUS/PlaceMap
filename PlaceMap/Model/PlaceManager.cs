using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;

namespace PlaceMap
{
    class PlaceManager
    {
        IMobileServiceTable<Place> table;
        public PlaceManager()
        {
            table = MainActivity.mClient.GetTable<Place>();
        }
        public async Task AddItem(Place item)
        {
            await table.InsertAsync(item);
        }
        public async Task UpdateItem(Place item)
        {
            await table.UpdateAsync(item);
        }
        public async Task DeleteItem(Place item)
        {
            await table.DeleteAsync(item);
        }
    }
}