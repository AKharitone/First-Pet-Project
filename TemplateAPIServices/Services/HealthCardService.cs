using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TemplateAPIModel;
using TemplateAPIServices.Extensions;
using TemplateAPIServices.Interfaces;
using TemplateAPIServices.PublicModels;
using TemplateShared;

namespace TemplateAPIServices.Services
{
    public class HealthCardService : IHealthCardService
    {
        private readonly TemplateContext _context;
        public HealthCardService(TemplateContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<HealthCardModel>> FindAll()
        {
            try
            {
                List<HealthCardModel> result = new List<HealthCardModel>();

                List<HealthCard> dbHealthCards = await _context.HealthCard
                    .ToListAsync();

                foreach (HealthCard hc in dbHealthCards)
                    result.Add(HealthCardExtensions.MapDbModel(hc));

                return result;
            }
            catch
            {
                return null;
            }
        }

        public async Task<HealthCardModel> DeleteHealthCard(int id)
        {
            var dbHealthCard = await _context.HealthCard.Include(hc=>hc.HealthCardIllnesses).SingleOrDefaultAsync(hc => hc.Id == id);

            if (dbHealthCard != null)
            {
                if (dbHealthCard.HealthCardIllnesses != null)
                {
                    foreach (HealthCardIllness hci in dbHealthCard.HealthCardIllnesses)
                        _context.HealthCardIllnesses.Remove(hci);
                }

                _context.HealthCard.Remove(dbHealthCard);
                await _context.SaveChangesAsync();

                return HealthCardExtensions.MapDbModel(dbHealthCard);
            }
            else 
                return null;
        }

        public async Task<HealthCardModel> CreateNewHealthCard(HealthCardModel cardM)
        {
            if (cardM == null)
                return null;

            var dbHealthCard = new HealthCard
            {
                Insurance = cardM.Insurance,
                HealthCardIllnesses = new List<HealthCardIllness>()
            };

            if(cardM.Illnesses != null)
            {
                foreach(IllnessModel illM in cardM.Illnesses)
                    dbHealthCard.HealthCardIllnesses.Add(new HealthCardIllness
                    {
                        HealthCard = dbHealthCard,
                        HealthCardId = dbHealthCard.Id,
                        Illness = IllnessExtensions.MapViewModel(illM),
                        IllnessId = illM.Id
                    });
            }

            _context.HealthCard.Add(dbHealthCard);
            await _context.SaveChangesAsync();

            return HealthCardExtensions.MapDbModel(dbHealthCard);
        }

        public async Task<HealthCardModel> UpdateHealthCard(HealthCardModel cardM)
        {
            if (cardM == null)
                return null;

            var dbHealthCard = await _context.HealthCard.Include(hc => hc.HealthCardIllnesses).SingleOrDefaultAsync(hc => hc.Id == cardM.Id);

            if (dbHealthCard == null)
                return null;

            dbHealthCard.Insurance = cardM.Insurance;
            
            if(cardM.Illnesses != null)
            {
                dbHealthCard.HealthCardIllnesses = new List<HealthCardIllness>();

                foreach(IllnessModel illM in cardM.Illnesses)
                    dbHealthCard.HealthCardIllnesses.Add(new HealthCardIllness
                    {
                        HealthCard = dbHealthCard,
                        HealthCardId = dbHealthCard.Id,
                        Illness = IllnessExtensions.MapViewModel(illM),
                        IllnessId = illM.Id
                    });
            }
            _context.HealthCard.Update(dbHealthCard);
            await _context.SaveChangesAsync();
            return HealthCardExtensions.MapDbModel(dbHealthCard);
        }
                
    }
}
