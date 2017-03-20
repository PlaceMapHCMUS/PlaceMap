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

        //add account
        public async Task<bool> AddItem(Customer item)
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

        //login return id
        public async Task<string> GetIdLogin(string email, string pass)
        {
            try
            {
                var query = from m in table
                            where m.email == email && m.pass==pass
                            select m.id;
                List<string> listId = await query.ToListAsync();
                return listId[0] ;
            }
            catch
            {
                return null;
            }
        }

        
        public async Task<bool> UpdateItem(Customer item)
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
        public async Task<bool> DeleteItem(Customer item)
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

        public async Task<string> GetName(string id)
        {
            try
            {
                var query = from m in table
                            where m.id == id
                            select m.account;
                var list= await query.ToListAsync();
                return list[0] ;
            }
            catch
            {
                return null;
            }
        }





        public async  Task<List<string>> GetListAccount()
        {
            try
            {
                var query = from m in table
                            select m.account;
                return await query.ToListAsync();
            }
            catch
            {
                return null;
            }

        }

        public async Task<List<string>> GetListPass()
        {
            try
            {
                var query = from m in table
                            select m.pass;
                return await query.ToListAsync();
            }
            catch
            {
                return null;
            }
        }
    }
}