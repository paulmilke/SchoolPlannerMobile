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
            bool dbExists = File.Exists(Constants.DatabasePath);
            if (!dbExists)
            {
                // Create tables if the database does not exist
                await Database.CreateTableAsync<Term>();
                await Database.CreateTableAsync<Class>();
                await Database.CreateTableAsync<Assessment>();
                await PopulateTestData(); 
            }

        }



        public async Task PopulateTestData()
        {
            Term testTerm = new Term("Test Term", new DateTime(2024,1,1), new DateTime(2024,5,1));
            int termID = await SaveTestTermAsync(testTerm);
            Class testClass = new Class(termID, "Test Course", new DateTime(2024, 1, 1), new DateTime(2024, 5, 1), "Plan to Take", "Anika Patel", "555-123-4567", "anika.patel@strimeuniversity.edu");
            int classID = await SaveTestClassAsync(testClass);
            Assessment performanceAssessment = new Assessment(classID, "Test - My Peformance Assessment", "Performance Assessment", new DateTime(2024, 1, 1), new DateTime(2024, 5, 1));
            Assessment objectiveAssessment = new Assessment(classID, "Test - My Objective Assessment", "Objective Assessment", new DateTime(2024, 1, 1), new DateTime(2024, 5, 1));
            await SaveTestAssessmentAsync(performanceAssessment);
            await SaveTestAssessmentAsync(objectiveAssessment); 
        }

        public async Task<int> SaveTestTermAsync(Term term)
        {
            if (term.Id != 0)
            {
                await Database.UpdateAsync(term);
            }

            else
            {
                await Database.InsertAsync(term);

            }
            return term.Id;
        }

        public async Task<int> SaveTestClassAsync(Class newClass)
        {
            if (newClass.Id != 0)
            {
                await Database.UpdateAsync(newClass);

            }
            else
            {
                await Database.InsertAsync(newClass);

            }
            return newClass.Id;
        }

        public async Task<int> SaveTestAssessmentAsync(Assessment newAssessment)
        {
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

        public async Task<List<Term>> GetTermsAsync()
        {
            await Init(); 
            return await Database.Table<Term>().ToListAsync();
        }

        public async Task<Term> GetSingleTermAsync(int termID)
        {
            await Init(); 
            return await Database.Table<Term>().Where(t=>t.Id == termID).FirstOrDefaultAsync();
        }

        public async Task<List<Class>> GetClassesAsync(int termID)
        {
            await Init(); 
            return await Database.Table<Class>().Where(c=>c.TermId == termID).ToListAsync();
        }

        public async Task<List<Assessment>> GetAssessmentsAsync(int classID)
        {
            await Init();
            return await Database.Table<Assessment>().Where(c => c.ClassId == classID).ToListAsync();
        }

        public async Task<Assessment> GetSingleAssessmentAsync(int assessmentID)
        {
            await Init();
            return await Database.Table<Assessment>().Where(c => c.Id == assessmentID).FirstOrDefaultAsync();
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
                await Database.UpdateAsync(term);
                WeakReferenceMessenger.Default.Send(new TermUpdateMessage());
            }

            else
            {
                await Database.InsertAsync(term);

                WeakReferenceMessenger.Default.Send(new TermUpdateMessage());
                
            }
            return term.Id; 
        }

        public async Task<int> SaveClassAsync(Class newClass)
        {
            await Init(); 
            if (newClass.Id != 0)
            {
                await Database.UpdateAsync(newClass);

            }
            else
            {
                await Database.InsertAsync(newClass);

            }
            return newClass.Id; 
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


            //Find Classes associated with term 
            List<Class> classList = await GetClassesAsync(term.Id);
            foreach (var items in classList)
            {
                //For each class find and delete assessments. 
                List<Assessment> assessments = await GetAssessmentsAsync(items.Id);
                foreach(Assessment item in assessments)
                {
                    await DeleteAssessmentAsync(item);
                }
            }
            //After deleting assessments, delete the classes. 
            foreach (var item in classList)
            {
                await DeleteClassAsync(item);
            }
            //Finally delete the term. 
            int ret = await Database.DeleteAsync(term);
            WeakReferenceMessenger.Default.Send(new TermUpdateMessage());
            return ret;
        }

        public async Task<int> DeleteClassAsync(Class currentClass)
        {
            await Init();

            //Find assessments for the class and loop through to delete them.
            List<Assessment> assessments = await GetAssessmentsAsync(currentClass.Id);
            foreach (Assessment item in assessments)
            {
                await DeleteAssessmentAsync(item);
            }
            //Then delete the class. 
            int ret = await Database.DeleteAsync(currentClass);
            return ret; 
        }

        public async Task<int> DeleteAssessmentAsync(Assessment currentAssessment)
        {
            await Init(); 
            int ret = await Database.DeleteAsync(currentAssessment);
            return ret;
        }
    }

}
  