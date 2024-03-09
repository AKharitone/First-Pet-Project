using System;
using System.Collections.Generic;
using TemplateAPIModel;
using TemplateAPIServices.PublicModels;
using TemplateShared;

namespace TemplateAPIServices.Extensions
{
    public static class IllnessExtensions
    {
        public static IllnessModel MapDbModel(Illness dbIll)
        {
            if (dbIll == null)
                return null;

            IllnessModel illM = new IllnessModel
            {
                Id = dbIll.Id,
                Description = dbIll.Description,
                HealthCardModels = new List<HealthCardModel>(),
                SymptomModels = new List<SymptomModel>()
            };

            if (dbIll.HealthCardIllnesses != null)
                foreach (HealthCardIllness hCI in dbIll.HealthCardIllnesses)
                    illM.HealthCardModels.Add(new HealthCardModel
                    {
                        Id = hCI.HealthCardId,
                        Insurance = hCI.HealthCard.Insurance,
                        Illnesses = new List<IllnessModel>()
                    });

            if (dbIll.IllnessSymptoms != null)
                foreach (IllnessSymptom iS in dbIll.IllnessSymptoms)
                    illM.SymptomModels.Add(new SymptomModel
                    {
                        Id = iS.SymptomId,
                        Description = iS.Symptom.Description,
                        IllnessesModels = new List<IllnessModel>()
                    });

             return illM;
        }

        public static Illness MapViewModel(IllnessModel illModel)
        {
            if (illModel == null)
                return null;

            Illness dbIll = new Illness
            {
                Id = illModel.Id,
                Description = illModel.Description,
                HealthCardIllnesses = new List<HealthCardIllness>(),
                IllnessSymptoms = new List<IllnessSymptom>()
            };

            if (illModel.HealthCardModels != null)
                foreach (HealthCardModel hcM in illModel.HealthCardModels)
                {
                    HealthCardIllness healthCardIll = new HealthCardIllness
                    {
                        Illness = dbIll,
                        IllnessId = dbIll.Id,
                        HealthCardId = hcM.Id
                    };

                    healthCardIll.HealthCard = new HealthCard
                    {
                        Id = hcM.Id,
                        Insurance = hcM.Insurance,
                        HealthCardIllnesses = new List<HealthCardIllness>()
                    };

                    dbIll.HealthCardIllnesses.Add(healthCardIll);
                }


            if (illModel.SymptomModels != null)
                foreach (SymptomModel symM in illModel.SymptomModels)
                {
                    IllnessSymptom illSym = new IllnessSymptom
                    {
                        Illness = dbIll,
                        IllnessId = dbIll.Id,
                        SymptomId = symM.Id
                    };

                    illSym.Symptom = new Symptom
                    {
                        Id = symM.Id,
                        Description = symM.Description,
                        IllnessSymptoms = new List<IllnessSymptom>()
                    };

                    dbIll.IllnessSymptoms.Add(illSym);
                }

            return dbIll;

        }
    }
}
