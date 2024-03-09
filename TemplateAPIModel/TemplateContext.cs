using Microsoft.EntityFrameworkCore;

namespace TemplateAPIModel
{
    public class TemplateContext : DbContext
    {
        public TemplateContext()
        {
        }

        public TemplateContext(DbContextOptions<TemplateContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<HealthCard> HealthCard { get; set; }
        public virtual DbSet<Doctor> Doctor { get; set; }
        public virtual DbSet<Illness> Illness { get; set; }
        public virtual DbSet<Symptom> Symptom { get; set; }
        public virtual DbSet<HealthCardIllness> HealthCardIllnesses { get; set; }
        public virtual DbSet<IllnessSymptom> IllnessSymptoms { get; set; }

        
    }
}
