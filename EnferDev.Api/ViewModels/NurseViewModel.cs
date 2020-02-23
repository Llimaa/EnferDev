using System;

namespace EnferDev.Api.ViewModels
{
    public class NurseViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CPF { get; set; }
        public string Coren { get; set; }
        public DateTime DateBirth { get; set; }
        public Guid IdHospital { get; set; }
        public string NameHospital { get; set; }
        public bool Active { get; set; }
    }
}
