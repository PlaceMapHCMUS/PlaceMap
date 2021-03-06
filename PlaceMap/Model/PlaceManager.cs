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


        public async Task<bool> AddItem(Place item)
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
        public async Task<bool> UpdateItem(Place item)
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
        public async Task<bool> DeleteItem(Place item)
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

        public async Task<List<Place>> GetListPlace(String idCustomer)
        {
            try
            {
                var query = from m in table
                            where m.idCustomer == idCustomer
                            select m;
                return await query.ToListAsync();
            }
            catch
            {
                return null;
            }
        }

        public async Task<int> GetReport(string id)
        {
            try
            {
                Place place = await table.LookupAsync(id);
                return place.report;
            }
            catch
            {
                return -1;
            }
        }

        public async Task<List<string>>GetListKey(string key)
        {
            try
            {
                var query = from m in table
                            where m.keyPlace == key
                            select m.id;
                return await query.ToListAsync();
            }
            catch
            {
                return null;
            }
        }
            
    }
}