using System;
using System.Collections.Generic;
using System.Text;

namespace TemplateAPIModel
{
    public class IllnessSymptom
    {
        public int Id {get; set;}
        public Illness Illness { get; set; }
        public int IllnessId { get; set; }
        public Symptom Symptom { get; set; }
        public int SymptomId { get; set; }
    }
}
