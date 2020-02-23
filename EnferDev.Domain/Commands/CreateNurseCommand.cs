using EnferDev.Domain.Shared;
using EnferDev.Domain.Shared.Commands;
using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Domain.Enuns;
using System;

namespace EnferDev.Domain.Commands
{
    public class CreateNurseCommand : Notifiable, ICommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CPF { get; set; }
        public string Coren { get; set; }
        public DateTime DateBirth { get; set; }
        public Guid HospitalId { get; set; }
        public Guid NameHospital { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract()
          .Requires()
          .HasMinLen(Name, 3, "Name", "O campo nome deve te no minimo 3 caractéres")
          .HasMinLen(CPF, 11, "CPF", "O campo cpf inválido")
          .HasMaxLen(CPF, 11, "CPF", "O campo cpf inválido")
          .HasMinLen(Coren, 10, "Coren", "o campo coren deve te no minimo 4 caractéres") // olhar validacao para esse campo
          .IsLowerOrEqualsThan(DateBirth.AddYears(18), DateTime.Now, "DateBirth", "Idade minima de 18 anos")
          .IsNotEmpty(HospitalId, "Hospital", "O campo hospital precisa ser preenchido")
          .IsTrue(DocumentValidate.validate(EDocumentType.CPF, CPF), "Document.Number", "Documento inválido")
          );
        }
    }
}
