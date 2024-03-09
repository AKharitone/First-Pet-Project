using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TemplateAPIModel;
using TemplateAPIServices.Extensions;
using TemplateAPIServices.Interfaces;
using TemplateAPIServices.PublicModels;
using TemplateShared;

namespace TemplateAPIServices.Services
{
    public class IllnessService : IIllnessService
    {
        private readonly TemplateContext _context;

        public IllnessService(TemplateContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


       public async Task<List<IllnessModel>> FindAll()
       {
            try
            {
                List<IllnessModel> result = new List<IllnessModel>();

                List<Illness> dbIll = await _context.Illness.Include(ill => ill.IllnessSymptoms).ThenInclude(illSym => illSym.Symptom)
                                                            .Include(ill => ill.HealthCardIllnesses).ThenInclude(healthCardIll => healthCardIll.HealthCard)
                                                            .ToListAsync();

                foreach (Illness ill in dbIll)
                    result.Add(IllnessExtensions.MapDbModel(ill));

                return result;
            }
            catch
            {
                return null;
            }
       }


        public async Task<IllnessModel> FindIllnessById(int id)
        {
            Illness dbIll = await _context.Illness.Include(ill => ill.IllnessSymptoms).ThenInclude(illSym => illSym.Symptom)
                                                  .Include(ill => ill.HealthCardIllnesses).ThenInclude(healthCardIll => healthCardIll.HealthCard)
                                                  .SingleOrDefaultAsync(ill => ill.Id == id);

            if (dbIll == null)
                return null;
            else
                return IllnessExtensions.MapDbModel(dbIll);
        }


        public async Task<IllnessModel> DeleteIllness(int id)
        {
            Illness dbIll = await _context.Illness.Include(ill => ill.HealthCardIllnesses).ThenInclude(healthCardIll => healthCardIll.HealthCard)
                                                  .Include(ill => ill.IllnessSymptoms).ThenInclude(illSym => illSym.Symptom)
                                                  .SingleOrDefaultAsync(ill => ill.Id == id);

            if (dbIll == null)
                return null;

            if (dbIll.HealthCardIllnesses != null)
                foreach(HealthCardIllness healthCardIll in dbIll.HealthCardIllnesses)
                    _context.Remove(healthCardIll);

            if (dbIll.IllnessSymptoms != null)
                foreach (IllnessSymptom illSym in dbIll.IllnessSymptoms)
                    _context.Remove(illSym);

            _context.Remove(dbIll);
            await _context.SaveChangesAsync();
            return IllnessExtensions.MapDbModel(dbIll);
        }


        public async Task<IllnessModel> CreateNewIllness(IllnessModel illnessM)
        {
            if (illnessM == null)
                return null;

            Illness dbIll = new Illness
            {
                Id = illnessM.Id,
                Description = illnessM.Description,
                HealthCardIllnesses = new List<HealthCardIllness>(),
                IllnessSymptoms = new List<IllnessSymptom>()
            };

            if (illnessM.HealthCardModels != null)
                foreach (HealthCardModel healthCardM in illnessM.HealthCardModels)
                    dbIll.HealthCardIllnesses.Add(new HealthCardIllness
                    {
                        HealthCard = HealthCardExtensions.MapViewModel(healthCardM),
                        HealthCardId = healthCardM.Id,
                        Illness = dbIll,
                        IllnessId = dbIll.Id
                    });

            if (illnessM.SymptomModels != null)
                foreach (SymptomModel symM in illnessM.SymptomModels)
                    dbIll.IllnessSymptoms.Add(new IllnessSymptom
                    {
                        Illness = dbIll,
                        IllnessId = dbIll.Id,
                        Symptom = SymptomExtensions.MapViewModel(symM),
                        SymptomId = symM.Id
                    });

            _context.Illness.Add(dbIll);
            await _context.SaveChangesAsync();
            return IllnessExtensions.MapDbModel(dbIll);
        }


        public async Task<IllnessModel> UpdateIllness(IllnessModel illnessM)
        {
            if (illnessM == null)
                return null;

            Illness dbIll = await _context.Illness.Include(ill => ill.IllnessSymptoms)
                                                  .Include(ill => ill.HealthCardIllnesses)
                                                  .SingleOrDefaultAsync(ill => ill.Id == illnessM.Id);
            if (dbIll == null)
                return null;

            dbIll.Description = illnessM.Description;

            if(illnessM.HealthCardModels != null)
            {
                dbIll.HealthCardIllnesses = new List<HealthCardIllness>();

                foreach (HealthCardModel healthCardM in illnessM.HealthCardModels)
                    dbIll.HealthCardIllnesses.Add(new HealthCardIllness
                    {
                        HealthCard = HealthCardExtensions.MapViewModel(healthCardM),
                        HealthCardId = healthCardM.Id,
                        Illness = dbIll,
                        IllnessId = dbIll.Id
                    });
            }

            if(illnessM.SymptomModels != null)
            {
                dbIll.IllnessSymptoms = new List<IllnessSymptom>();

                foreach (SymptomModel symM in illnessM.SymptomModels)
                    dbIll.IllnessSymptoms.Add(new IllnessSymptom
                    {
                        Illness = dbIll,
                        IllnessId = dbIll.Id,
                        Symptom = SymptomExtensions.MapViewModel(symM),
                        SymptomId = symM.Id
                    });
            }

            _context.Illness.Update(dbIll);
            await _context.SaveChangesAsync();
            return IllnessExtensions.MapDbModel(dbIll);
                
        }
    }
}
