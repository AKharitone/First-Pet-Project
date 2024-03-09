using TemplateAPIServices.PublicModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TemplateAPIServices.Interfaces
{
    public interface IDoctorService
    {
        Task<List<DoctorModel>> FindAll();
        Task<DoctorModel> FindDoctorBySurname(string surname);
        Task<DoctorModel> DeleteDoctor(int id);
        Task<DoctorModel> CreateNewDoctor(DoctorModel doctor);
        Task<DoctorModel> UpdateDoctor(DoctorModel doctor);
        Task<DoctorModel> AssignPacient(DoctorModel docM, UserModel userM);
        Task<DoctorModel> UnassignPacient(DoctorModel docM, UserModel userM);
    }
}
