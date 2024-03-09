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
    public class SymptomService : ISymptomService
    {
        private readonly TemplateContext _context;

        public SymptomService(TemplateContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<SymptomModel>> FindAll()
        {
            List<Symptom> dbSym = await _context.Symptom.Include(sym => sym.IllnessSymptoms)
                                                        .ThenInclude(illSym => illSym.Illness)
                                                        .ToListAsync();

            List<SymptomModel> result = new List<SymptomModel>();

            if (dbSym != null)
                foreach (Symptom sym in dbSym)
                    result.Add(SymptomExtensions.MapDbModel(sym));

            return result;
        }
        public async Task<SymptomModel> FindSymptomById(int id)
        {
            Symptom dbSym = await _context.Symptom.Include(sym => sym.IllnessSymptoms)
                                                  .ThenInclude(illSym => illSym.Illness)
                                                  .SingleOrDefaultAsync(sym => sym.Id == id);

            if (dbSym == null)
                return null;
            else
                return SymptomExtensions.MapDbModel(dbSym);
        }

        public async Task<SymptomModel> DeleteSymptom(int id)
        {
            Symptom dbSym = await _context.Symptom.Include(sym => sym.IllnessSymptoms).SingleOrDefaultAsync(sym => sym.Id == id);

            if (dbSym == null)
                return null;

            if (dbSym.IllnessSymptoms != null)
                foreach (IllnessSymptom illSym in dbSym.IllnessSymptoms)
                    _context.Remove(illSym);

            _context.Remove(dbSym);
            await _context.SaveChangesAsync();
            return SymptomExtensions.MapDbModel(dbSym);
        }

        public async Task<SymptomModel> CreateNewSymptom(SymptomModel symptomM)
        {
            if (symptomM == null)
                return null;

            Symptom dbSym = new Symptom
            {
                Id = symptomM.Id,
                Description = symptomM.Description,
                IllnessSymptoms = new List<IllnessSymptom>()
            };

            if (symptomM.IllnessesModels != null)
                foreach (IllnessModel illM in symptomM.IllnessesModels)
                    dbSym.IllnessSymptoms.Add(new IllnessSymptom
                    {
                        Illness = IllnessExtensions.MapViewModel(illM),
                        IllnessId = illM.Id,
                        Symptom = dbSym,
                        SymptomId = dbSym.Id
                    });

            await _context.Symptom.AddAsync(dbSym);
            await _context.SaveChangesAsync();
            return SymptomExtensions.MapDbModel(dbSym);
        }

        public async Task<SymptomModel> UpdateSymptom(SymptomModel symptomM)
        {
            if (symptomM == null)
                return null;

            Symptom dbSym = await _context.Symptom.Include(sym => sym.IllnessSymptoms)
                                                  .ThenInclude(illSym => illSym.Illness)
                                                  .SingleOrDefaultAsync(sym => sym.Id == symptomM.Id);

            if (dbSym == null)
                return null;
            else
            {
                dbSym.Description = symptomM.Description;
                dbSym.IllnessSymptoms = new List<IllnessSymptom>();

                if (symptomM.IllnessesModels != null)
                    foreach (IllnessModel illM in symptomM.IllnessesModels)
                        dbSym.IllnessSymptoms.Add(new IllnessSymptom
                        {
                            Illness = IllnessExtensions.MapViewModel(illM),
                            IllnessId = illM.Id,
                            Symptom = dbSym,
                            SymptomId = dbSym.Id
                        });
                _context.Symptom.Update(dbSym);
                await _context.SaveChangesAsync();
                return SymptomExtensions.MapDbModel(dbSym);
            }
        }
    }
}
