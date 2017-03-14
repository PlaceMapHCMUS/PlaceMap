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
    class CustomerManager
    {
        IMobileServiceTable<Customer> table;
        public CustomerManager()
        {
            table = MainActivity.mClient.GetTable<Customer>();
        }
        public async Task AddItem(Customer item)
        {
            await table.InsertAsync(item);
        }
        public async Task UpdateItem(Customer item)
        {
            await table.UpdateAsync(item);
        }
        public async Task DeleteItem(Customer item)
        {
            await table.DeleteAsync(item);
        }
        public async  Task<List<string>> GetListAccount()
        {
            var query = from m in table
                        select m.account;
            return await query.ToListAsync();
        }

        public async Task<List<string>> GetListPass()
        {
            var query = from m in table
                        select m.pass;
            return await query.ToListAsync();
        }
    }
}