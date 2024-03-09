using System;
using System.Collections.Generic;
using System.Text;

namespace TemplateAPIModel
{
    public class Symptom
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public List<IllnessSymptom> IllnessSymptoms { get; set; }
    }
}
