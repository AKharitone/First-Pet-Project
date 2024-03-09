using System;
using System.Collections.Generic;
using System.Text;
using TemplateAPIModel;
using TemplateShared;

namespace TemplateAPIServices.PublicModels
{
    public class AddressModel
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public int Building { get; set; }
        public int Zip { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public AddressModelStatus ModelStatus {get; set;}
    }
}
