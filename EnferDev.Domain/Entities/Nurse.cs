using EnferDev.Domain.Shared;
using EnferDev.Domain.ValueObjects;
using System;
using Flunt.Validations;
using PaymentContext.Domain.Enuns;

namespace EnferDev.Domain.Entities
{
    public class Nurse : Entity
    {
        public Nurse()
        {

        }
        public Nurse(string name, Document cPF, string coren, DateTime dateBirth, Guid idHospital)
        {
            Name = name;
            CPF = cPF;
            Coren = coren;
            DateBirth = dateBirth;
            IdHospital = idHospital;
            Active = true;

            AddNotifications(new Contract()
                           .Requires()
                           .HasMinLen(name, 5, "Name", "O nome deve conter no minimo 5 caractéres")
                           .HasMinLen(coren, 10, "Coren", "campo coren com o valor inválido!") // pesquisar para fazer validacao.
                           .IsNotEmpty(idHospital, "HospitalId", "*")
                       );
        }

        public string Name { get; private set; }
        public Document CPF { get; private set; }
        public string Coren { get; private set; }
        public DateTime DateBirth { get; private set; }
        public Guid IdHospital { get; private set; }
        public string NameHospital { get; private set; }
        public bool Active { get; private set; }    

        public void setDocument(string number)
        {
            CPF = new Document(number, EDocumentType.CPF);
        }

        public void updateNurse(string name, string cpf, string coren, DateTime dateBirth)
        {
            Name = name;
            CPF = new Document(cpf, EDocumentType.CPF);
            Coren = coren;
            DateBirth = dateBirth;
        }

        public void ActiveNurse()
        {
            Active = true;
        }

        public void DesactiveNurse()
        {
            Active = false;
        }
    }
}
