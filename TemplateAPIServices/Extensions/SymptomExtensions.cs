using System.Collections.Generic;
using TemplateAPIModel;
using TemplateAPIServices.PublicModels;
using TemplateShared;

namespace TemplateAPIServices.Extensions
{
    public static class SymptomExtensions
    {
        public static SymptomModel MapDbModel(Symptom dbSym)
        {
            if (dbSym == null)
                return null;

            SymptomModel symM = new SymptomModel
            {
                Id = dbSym.Id,
                Description = dbSym.Description,
                IllnessesModels = new List<IllnessModel>()
            };

            if (dbSym.IllnessSymptoms != null)
                foreach (IllnessSymptom illSym in dbSym.IllnessSymptoms)
                    symM.IllnessesModels.Add(IllnessExtensions.MapDbModel(illSym.Illness));

            return symM;
            
        }

        public static Symptom MapViewModel(SymptomModel symModel)
        {
            if (symModel == null)
                return null;

            Symptom dbSym = new Symptom
            {
                Id = symModel.Id,
                Description = symModel.Description,
                IllnessSymptoms = new List<IllnessSymptom>()
            };

            if (symModel.IllnessesModels != null)
                foreach (IllnessModel illM in symModel.IllnessesModels)
                    dbSym.IllnessSymptoms.Add(new IllnessSymptom
                    {
                        Illness = IllnessExtensions.MapViewModel(illM),
                        IllnessId = illM.Id,
                        Symptom = dbSym,
                        SymptomId = dbSym.Id
                    });

            return dbSym;
        }
    }
}
