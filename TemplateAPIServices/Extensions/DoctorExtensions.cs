using System.Collections.Generic;
using TemplateAPIModel;
using TemplateAPIServices.PublicModels;
using TemplateShared;

namespace TemplateAPIServices.Extensions
{
    public static class DoctorExtensions
    {
        public static DoctorModel MapDbModel(Doctor dbDoctor)
        {
            if (dbDoctor == null)
                return null;

            List<UserModel> result = new List<UserModel>();

            if(dbDoctor.Pacients != null)
            {
                foreach (User user in dbDoctor.Pacients)
                    result.Add(UserExtensions.MapDbModel(user));
            }

            return new DoctorModel
            {
                Id = dbDoctor.Id,
                Name = dbDoctor.Name,
                Surname = dbDoctor.Surname,
                Pacients = result
            };
        }

        public static Doctor MapViewModel(DoctorModel doctor)
        {
            if (doctor == null)
                return null;

            List<User> result = new List<User>();
            foreach (UserModel user in doctor.Pacients)
                result.Add(UserExtensions.MapViewModel(user));

            return new Doctor
            {
                Id = doctor.Id,
                Name = doctor.Name,
                Surname = doctor.Surname,
                Pacients = result
            };
        }
    }
}
