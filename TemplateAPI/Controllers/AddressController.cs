using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TemplateAPIServices.Interfaces;
using TemplateAPIServices.PublicModels;

namespace TemplateAPI.Controllers
{
    [Route("address")]
    public class AddressController : Controller
    {
        protected readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewAddress([FromBody] AddressModel data)
        {
            AddressModel address = await _addressService.CreateNewAddress(data);

            if (address == null)
                return NotFound();

            return Ok();
        }

        //[Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateAddress([FromBody] AddressModel data)
        {
            AddressModel address = await _addressService.UpdateAddress(data);

            if (address == null)
                return NotFound();
            else
                return Ok();
        }

        //[Authorize]
        [HttpGet("{id}")]
        public async Task<AddressModel> FindAddress(int id)
        {
            if (id <= 0)
                return null;

            var address = await _addressService.FindAddress(id);

            if (address != null)
                return address;
            else
                return null;
        }

        //[Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            if (id <= 0)
                return null;

            var address = await _addressService.DeleteAddress(id);

            if (address != null)
                return Ok();
            else
                return NotFound(id);
        }
    }
}
