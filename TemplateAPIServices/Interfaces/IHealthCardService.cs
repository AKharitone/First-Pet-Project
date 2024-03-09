using TemplateAPIServices.PublicModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TemplateAPIServices.Interfaces
{
    public interface IHealthCardService
    {
        Task<List<HealthCardModel>> FindAll();
        Task<HealthCardModel> DeleteHealthCard(int id);
        Task<HealthCardModel> CreateNewHealthCard(HealthCardModel cardM);
        Task<HealthCardModel> UpdateHealthCard(HealthCardModel cardM);
    }
}
