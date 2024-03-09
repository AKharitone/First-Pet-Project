using System.Collections.Generic;
using TemplateAPIModel;
using TemplateAPIServices.PublicModels;
using TemplateShared;

namespace TemplateAPIServices.Extensions
{
    public static class HealthCardExtensions
    {
        public static HealthCardModel MapDbModel(HealthCard dbCard)
        {
            if (dbCard == null)
                return null;

            HealthCardModel hcModel = new HealthCardModel
            {
                Id = dbCard.Id,
                Insurance = dbCard.Insurance,
                Illnesses = new List<IllnessModel>()
            };

            if (dbCard.HealthCardIllnesses != null)
            {
                foreach (HealthCardIllness ill in dbCard.HealthCardIllnesses)
                {
                    IllnessModel illM = new IllnessModel
                    {
                        Id = ill.IllnessId,
                        Description = ill.Illness.Description,
                        HealthCardModels = new List<HealthCardModel>(),
                        SymptomModels = new List<SymptomModel>()
                    };
                    hcModel.Illnesses.Add(illM);
                }
            }

            return hcModel;
        }

        public static HealthCard MapViewModel(HealthCardModel healthCardM)
        {
            if (healthCardM == null)
                return null;

            HealthCard dbHealthCard = new HealthCard
            {
                Id = healthCardM.Id,
                Insurance = healthCardM.Insurance,
                HealthCardIllnesses = new List<HealthCardIllness>()
            };

            if (healthCardM.Illnesses != null)
            {
                foreach (IllnessModel illM in healthCardM.Illnesses)
                {
                    HealthCardIllness healthCIlls = new HealthCardIllness
                    {
                        HealthCard = dbHealthCard,
                        HealthCardId = dbHealthCard.Id,
                        IllnessId = illM.Id
                    };

                    healthCIlls.Illness = new Illness
                    {
                        Id = illM.Id,
                        Description = illM.Description,
                        HealthCardIllnesses = new List<HealthCardIllness>(),
                        IllnessSymptoms = new List<IllnessSymptom>()
                    };

                    dbHealthCard.HealthCardIllnesses.Add(healthCIlls);
                }
            }           
            return dbHealthCard;
        }
    }
}
