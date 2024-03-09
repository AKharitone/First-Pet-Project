using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TemplateAPIModel
{
    public class Address
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public int Building { get; set; }
        public int Zip { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
