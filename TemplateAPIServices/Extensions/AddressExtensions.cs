using TemplateAPIServices.PublicModels;
using TemplateShared;
using TemplateAPIModel;

namespace TemplateAPIServices.Extensions
{
    public static class AddressExtensions
    {
        public static AddressModel MapDbModel(Address dbAddress)
        {
            if (dbAddress == null)
                return null;

            return new AddressModel
            {
                Id = dbAddress.Id,
                Street = dbAddress.Street,
                Building = dbAddress.Building,
                Zip = dbAddress.Zip,
                City = dbAddress.City,
                Country = dbAddress.Country
            };
        }

        public static Address MapViewModel(AddressModel addressModel)
        {
            return new Address
            {
                Id = addressModel.Id,
                Street = addressModel.Street,
                Building = addressModel.Building,
                Zip = addressModel.Zip,
                City = addressModel.City,
                Country = addressModel.Country
            };
        }

        public static void ValidateAddress(AddressModel address)
        {
            if (string.IsNullOrEmpty(address.Street))
                address.ModelStatus |= AddressModelStatus.EmptyStreet;

            if (string.IsNullOrEmpty(address.Building.ToString()))
                address.ModelStatus |= AddressModelStatus.EmptyBuilding;
            else if (!address.Building.ToString().IsValidBuilding())
                address.ModelStatus |= AddressModelStatus.WrongBuildingFormat;

            if (string.IsNullOrEmpty(address.Zip.ToString()))
                address.ModelStatus |= AddressModelStatus.EmptyZip;
            else if (!address.Zip.ToString().IsValidZip())
                address.ModelStatus |= AddressModelStatus.WrongZipFormat;

            if (string.IsNullOrEmpty(address.City))
                address.ModelStatus |= AddressModelStatus.EmptyCity;

            if (string.IsNullOrEmpty(address.Country))
                address.ModelStatus |= AddressModelStatus.EmptyCountry;
        }
    }
}
