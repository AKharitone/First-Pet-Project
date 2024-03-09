using System;
using System.Collections.Generic;
using System.Text;

namespace TemplateAPIModel
{
    public class HealthCardIllness
    {
        public int Id { get; set; }
        public HealthCard HealthCard { get; set; }
        public int HealthCardId { get; set; }
        public Illness Illness { get; set; }
        public int IllnessId { get; set; }
    }
}
