using TemplateAPIServices.PublicModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TemplateAPIServices.Interfaces
{
    public interface IIllnessService
    {
        Task<List<IllnessModel>> FindAll();
        Task<IllnessModel> FindIllnessById(int id);
        Task<IllnessModel> DeleteIllness(int id);
        Task<IllnessModel> CreateNewIllness(IllnessModel illnessM);
        Task<IllnessModel> UpdateIllness(IllnessModel illnessM);
    }
}
