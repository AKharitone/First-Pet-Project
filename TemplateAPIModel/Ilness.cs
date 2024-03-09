using System;
using System.Collections.Generic;
using System.Text;

namespace TemplateAPIModel
{
    public class Illness
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public List<HealthCardIllness> HealthCardIllnesses { get; set; }
        public List<IllnessSymptom> IllnessSymptoms { get; set; }
    }
}
