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
    class KeySearchManager
    {
        IMobileServiceTable<KeySearch> table;
        public KeySearchManager()
        {
            table = MainActivity.mClient.GetTable<KeySearch>();
        }
        public async Task<bool> AddItem(KeySearch item)
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
        public async Task<bool> UpdateItem(KeySearch item)
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
        public async Task<bool> DeleteItem(KeySearch item)
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
        public async Task<List<KeySearch>> GetListKey(string idCustomer)
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

    }
}