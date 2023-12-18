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
            var resultClass = await Database.CreateTableAsync<Class>();
            var resultAssessment=await Database.CreateTableAsync<Assessment>();
        }

        public async Task<List<Term>> GetTermsAsync()
        {
            await Init(); 
            return await Database.Table<Term>().ToListAsync();
        }

        public async Task<List<Class>> GetClassesAsync(int termID)
        {
            await Init(); 
            return await Database.Table<Class>().Where(c=>c.TermId == termID).ToListAsync();
        }

        public async Task<Class> GetSingleClass(int classID)
        {
            await Init(); 
            return await Database.Table<Class>().Where(c=>c.Id == classID).FirstOrDefaultAsync();
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
                for (int i = 0; i < 6; i++)
                {
                    int d = i + 1;
                    Class c = new Class(term.Id, $"Class {d}");
                    await SaveClassAsync(c);
                }

                WeakReferenceMessenger.Default.Send(new TermUpdateMessage());
                return ret;
            }

        }

        public async Task<int> SaveClassAsync(Class newClass)
        {
            await Init(); 
            if (newClass.Id != 0)
            {
                int ret = await Database.UpdateAsync(newClass);
                return ret; 
            }
            else
            {
                int ret = await Database.InsertAsync(newClass);
                return ret; 
            }
        }

        public async Task<int> SaveAssessmentAsync(Assessment newAssessment)
        {
            await Init();
            if (newAssessment.Id != 0)
            {
                int ret = await Database.UpdateAsync(newAssessment);
                return ret;
            }
            else
            {
                int ret = await Database.InsertAsync(newAssessment);
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

        public async Task<int> DeleteClassAsync(Class currentClass)
        {
            await Init();
            int ret = await Database.DeleteAsync(currentClass);
            return ret; 
        }
    }

}
  