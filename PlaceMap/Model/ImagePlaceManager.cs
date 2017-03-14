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
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace PlaceMap
{
    class ImagePlaceManager
    {

        IMobileServiceTable<ImagePlace> table;
        public ImagePlaceManager()
        {
            table = MainActivity.mClient.GetTable<ImagePlace>();
        }
        public async Task AddItem(ImagePlace item)
        {
            await table.InsertAsync(item);
        }
        public async Task UpdateItem(ImagePlace item)
        {
            await table.UpdateAsync(item);
        }
        public async Task DeleteItem(ImagePlace item)
        {
            await table.DeleteAsync(item);
        }
    }
}