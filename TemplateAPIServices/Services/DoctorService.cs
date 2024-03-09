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
    public class DoctorService : IDoctorService
    {
        private readonly TemplateContext _context;

        public DoctorService(TemplateContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<DoctorModel>> FindAll()
        {
            try
            {
                List<DoctorModel> result = new List<DoctorModel>();

                List<Doctor> dbDoctors = await _context.Doctor.ToListAsync();

                foreach (Doctor doctor in dbDoctors)
                    result.Add(DoctorExtensions.MapDbModel(doctor));

                return result;
            }
            catch
            {
                return null;
            }
        }

        public async Task<DoctorModel> FindDoctorBySurname(string surname)
        {
            var dbDoctor = await _context.Doctor.Include(d => d.Pacients).FirstOrDefaultAsync(d => d.Surname == surname);

            return dbDoctor != null ? DoctorExtensions.MapDbModel(dbDoctor) : null;
        }

        public async Task<DoctorModel> DeleteDoctor(int id)
        {
            var dbDoctor = await _context.Doctor.SingleOrDefaultAsync(d => d.Id == id);
            if (dbDoctor != null)
            {
                _context.Doctor.Remove(dbDoctor);
                await _context.SaveChangesAsync();
                return DoctorExtensions.MapDbModel(dbDoctor);
            }
            else
                return null;
        }

        public async Task<DoctorModel> CreateNewDoctor(DoctorModel doctor)
        {
            if (doctor == null)
                return null;
            else
            {
                var dbDoctor = new Doctor
                {
                    Name = doctor.Name,
                    Surname = doctor.Surname,
                    Pacients = new List<User>()
                };

                if(doctor.Pacients != null)
                {
                    foreach (UserModel userM in doctor.Pacients)
                    {
                        string salt = Tools.GetSalt();
                        dbDoctor.Pacients.Add(new User
                        {
                            FirstName = userM.FirstName,
                            MiddleName = userM.MiddleName,
                            LastName = userM.LastName,
                            Number = userM.Number,
                            UserName = userM.UserName,                            
                            PasswordHash = Tools.GetHash(userM.Password + userM.UserName + salt),
                            PasswordSalt = salt,
                            Address = AddressExtensions.MapViewModel(userM.AddressModel),
                            HealthCard = HealthCardExtensions.MapViewModel(userM.HealthCardModel)
                        });
                    }
                }
                _context.Doctor.Add(dbDoctor);
                await _context.SaveChangesAsync();
                return DoctorExtensions.MapDbModel(dbDoctor);
            }
        }

        public async Task<DoctorModel> UpdateDoctor(DoctorModel doctorM)
        {
            if (doctorM == null)
                return null;

            var dbDoctor = await _context.Doctor.Include(d => d.Pacients).FirstOrDefaultAsync(d => d.Id == doctorM.Id);

            if (dbDoctor != null)
            {
                dbDoctor.Name = doctorM.Name;
                dbDoctor.Surname = doctorM.Surname;

                if(doctorM.Pacients != null)
                {
                    foreach (UserModel userM in doctorM.Pacients)
                        dbDoctor.Pacients.Add(UserExtensions.MapViewModel(userM));
                }

                _context.Doctor.Update(dbDoctor);
                await _context.SaveChangesAsync();
                return DoctorExtensions.MapDbModel(dbDoctor);
            }
            else
                return null;
        }

        public async Task<DoctorModel> AssignPacient(DoctorModel docM, UserModel userM)
        {
            var dbDoc = await _context.Doctor.Include(d => d.Pacients).FirstOrDefaultAsync(d => d.Id == docM.Id);
            var dbPac = await _context.User.FirstOrDefaultAsync(p => p.Id == userM.Id);

            if (dbDoc == null || dbPac == null)
                return null;
            else
            {
                dbDoc.Pacients.Add(dbPac);
                _context.Doctor.Update(dbDoc);
                await _context.SaveChangesAsync();
                return DoctorExtensions.MapDbModel(dbDoc);
            }
        }

        public async Task<DoctorModel> UnassignPacient(DoctorModel docM, UserModel userM)
        {
            var dbDoc = await _context.Doctor.Include(d=>d.Pacients).FirstOrDefaultAsync(d => d.Id == docM.Id);
            var dbPac = await _context.User.FirstOrDefaultAsync(p => p.Id == userM.Id);

            if (dbDoc == null || dbPac == null)
                return null;
            else
            {
                dbDoc.Pacients.Remove(dbPac);
                _context.Doctor.Update(dbDoc);
                await _context.SaveChangesAsync();
                return DoctorExtensions.MapDbModel(dbDoc);
            }
        }
    }
}
