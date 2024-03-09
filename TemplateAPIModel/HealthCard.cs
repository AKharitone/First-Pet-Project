using System;
using System.Collections.Generic;
using System.Text;

namespace TemplateAPIModel
{
    public class HealthCard
    {
        public int Id { get; set; }
        public string Insurance { get; set; }
        public List<HealthCardIllness> HealthCardIllnesses { get; set; }
    }
}

