using System;
using System.Collections.Generic;
using System.Text;

namespace TemplateAPIServices.PublicModels
{
    public class HealthCardModel
    {
        public int Id { get; set; }
        public string Insurance { get; set; }
        public List<IllnessModel> Illnesses { get; set; }
    }
}
