using TemplateAPIServices.PublicModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TemplateAPIServices.Interfaces
{
    public interface ISymptomService
    {
        Task<List<SymptomModel>> FindAll();
        Task<SymptomModel> FindSymptomById(int id);
        Task<SymptomModel> DeleteSymptom(int id);
        Task<SymptomModel> CreateNewSymptom(SymptomModel symptomM);
        Task<SymptomModel> UpdateSymptom(SymptomModel symptomM);
    }
}
