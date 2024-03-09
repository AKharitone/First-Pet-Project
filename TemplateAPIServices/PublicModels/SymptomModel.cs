using System;
using System.Collections.Generic;
using System.Text;

namespace TemplateAPIServices.PublicModels
{
    public class SymptomModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public List<IllnessModel> IllnessesModels { get; set; }
    }
}
