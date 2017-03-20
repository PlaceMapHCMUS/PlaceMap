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
    class CommentManager
    {
        IMobileServiceTable<Comment> table;
        public CommentManager()
        {
            table = MainActivity.mClient.GetTable<Comment>();
        }
        public async Task<bool> AddItem(Comment item)
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
        public async Task<bool> UpdateItem(Comment item)
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
        public async Task<bool> DeleteItem(Comment item)
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

        public async Task<List<Comment>> GetListComment(string idPlace)
        {
            try
            {
                var query = from m in table
                            where m.idPlace == idPlace
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