using TemplateAPIServices.PublicModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TemplateAPIServices.Interfaces
{
    public interface IAddressService
    {
        Task<AddressModel> CreateNewAddress(AddressModel address);
        Task<AddressModel> UpdateAddress(AddressModel address);
        Task<AddressModel> DeleteAddress(int id);
        Task<AddressModel> FindAddress(int userId);
    }
}
