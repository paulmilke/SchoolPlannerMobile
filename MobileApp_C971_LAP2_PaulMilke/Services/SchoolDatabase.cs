using CommunityToolkit.Mvvm.Messaging;
using MobileApp_C971_LAP2_PaulMilke.Models;
using SQLite;
using MobileApp_C971_LAP2_PaulMilke.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp_C971_LAP2_PaulMilke.Services
{
    public class SchoolDatabase
    {
        SQLiteAsyncConnection Database; 

        public SchoolDatabase()
        {
        }

        async Task Init()
        {
            if (Database is not null)
                return;

            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            var result = await Database.CreateTableAsync<Term>(); 
        }

        public async Task<List<Term>> GetTermsAsync()
        {
            await Init(); 
            return await Database.Table<Term>().ToListAsync();
        }

        public async Task<int> SaveTermAsync(Term term)
        {
            await Init();
            if (term.Id != 0)
            {
                int ret = await Database.UpdateAsync(term);
                WeakReferenceMessenger.Default.Send(new TermUpdateMessage());
                return ret;
            }

            else
            {
                int ret = await Database.InsertAsync(term);
                WeakReferenceMessenger.Default.Send(new TermUpdateMessage());
                return ret;
            }

        }

        public async Task<int> DeleteTermAsync(Term term)
        {
            await Init();
            int ret = await Database.DeleteAsync(term);
            WeakReferenceMessenger.Default.Send(new TermUpdateMessage());
            return ret;
        }
    }
}
  