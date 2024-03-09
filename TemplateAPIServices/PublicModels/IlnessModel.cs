using System;
using System.Collections.Generic;
using System.Text;

namespace TemplateAPIServices.PublicModels
{
    public class IllnessModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public List<HealthCardModel> HealthCardModels { get; set; }
        public List<SymptomModel> SymptomModels { get; set; }
    }
}
