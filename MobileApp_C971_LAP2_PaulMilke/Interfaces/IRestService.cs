using MobileApp_C971_LAP2_PaulMilke.Models; 

namespace MobileApp_C971_LAP2_PaulMilke.Interfaces
{
    public interface IRestService
    {
        Task<List<Term>> RefreshTermsAsync();
        Task<bool> SaveNewTermAsync(Term newTerm);
        Task<bool> UpdateExistingTermAsync(Term updatedTerm);
        Task<bool> DeleteTermAsync(int termId);
        Task<Term> GetSingleTermAsync(int termId);
        Task<List<Class>> GetClassesAsync(int termId);
        Task<Class> GetSingleClassAsync(int classId);
        Task<bool> SaveNewClassAsync(Class newClass);
        Task<bool> UpdateCurrentClassAsync(Class currentClass);
        Task<bool> DeleteClassAsync(int classId);
    }
}
