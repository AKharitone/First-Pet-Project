using System;
using System.Collections.Generic;
using System.Text;

namespace TemplateAPIModel
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<User> Pacients { get; set; }
    }
}
