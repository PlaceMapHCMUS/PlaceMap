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
        public async Task<bool> AddItem(ImagePlace item)
        {
            try
            {
                await table.InsertAsync(item);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> UpdateItem(ImagePlace item)
        {
            try
            {
                await table.UpdateAsync(item);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> DeleteItem(ImagePlace item)
        {
            try
            {
                await table.DeleteAsync(item);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<string>> GetListImage(String idPlace)
        {
            try
            {
                var query = from m in table
                            where m.idPlace == idPlace
                            select m.imagePlace;
                return await query.ToListAsync();
            }
            catch
            {
                return null;
            }
        }
    }
}