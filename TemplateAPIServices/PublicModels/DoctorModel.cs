using System;
using System.Collections.Generic;
using System.Text;
using TemplateAPIModel;

namespace TemplateAPIServices.PublicModels
{
    public class DoctorModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<UserModel> Pacients { get; set; }
    }
}
