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
    class HistoryManager
    {
        IMobileServiceTable<History> table;
        public HistoryManager()
        {
            table = MainActivity.mClient.GetTable<History>();
        }
        public async Task<bool> AddItem(History item)
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
        public async Task<bool> UpdateItem(History item)
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
        public async Task<bool> DeleteItem(History item)
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


        public async Task<List<string>> GetListHistory(string idCustomer)
        {
            try
            {
                var query = from m in table
                            where m.idCustomer == idCustomer
                            select m.nameHistory;
                return await query.ToListAsync();
            }
            catch
            {
                return null;
            }
        }
    }
}