using Microsoft.EntityFrameworkCore;
using TemplateAPIModel;
using TemplateAPIServices.Interfaces;
using TemplateAPIServices.PublicModels;
using TemplateShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TemplateAPIServices.Extensions;

namespace TemplateAPIServices.Services
{
    public class AddressService : IAddressService
    {
        private readonly TemplateContext _context;

        public AddressService(TemplateContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


        public async Task<AddressModel> CreateNewAddress(AddressModel address)
        {

            AddressExtensions.ValidateAddress(address);

            if (address.ModelStatus == AddressModelStatus.OK)
            {
                var dbAddress = new Address
                {
                    Street = address.Street,
                    Building = address.Building,
                    Zip = address.Zip,
                    City = address.City,
                    Country = address.Country
                };

                _context.Address.Add(dbAddress);
                await _context.SaveChangesAsync();

                return AddressExtensions.MapDbModel(dbAddress);
            }
            else
                return address;
        }

        public async Task<AddressModel> UpdateAddress (AddressModel address)
        {
            AddressExtensions.ValidateAddress(address);

            if (address.ModelStatus == AddressModelStatus.OK)
            {
                var dbAddrress = await _context.Address.FirstOrDefaultAsync(u => u.Id == address.Id);
                if (dbAddrress != null)
                {
                    dbAddrress.Street = address.Street;
                    dbAddrress.Building = address.Building;
                    dbAddrress.Zip = address.Zip;
                    dbAddrress.City = address.City;
                    dbAddrress.Country = address.Country;

                    _context.Update(dbAddrress);
                    await _context.SaveChangesAsync();

                    return AddressExtensions.MapDbModel(dbAddrress);
                }
                else
                    return null;
            }
            else
                return address;
        }

        public async Task<AddressModel> FindAddress(int id)
        {
            var dbAddress = await _context.Address.Where(aI => aI.Id == id).FirstOrDefaultAsync();
            return dbAddress != null ? AddressExtensions.MapDbModel(dbAddress) : null;
        }

        public async Task<AddressModel> DeleteAddress(int id)
        {
            var dbAddress = await _context.Address.FirstOrDefaultAsync(aI => aI.Id == id);

            if (dbAddress != null)
            {
                _context.Remove(dbAddress);
                await _context.SaveChangesAsync();
                return AddressExtensions.MapDbModel(dbAddress);
            }
            else
                return null;
        }
    }
}
