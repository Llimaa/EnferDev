using EnferDev.Domain.Shared;
using EnferDev.Domain.Shared.Commands;
using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Domain.Enuns;
using System;

namespace EnferDev.Domain.Commands
{
    public class CreateHospitalCommand : Notifiable, ICommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CNPJ { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract()
            .Requires()
            .HasMinLen(Name, 3, "Name", "O campo nome deve te no minimo 3 caractéres")
            .HasMinLen(CNPJ, 3, "CNPJ", "CNPJ inválido")
            .HasMaxLen(CNPJ, 14, "CNPJ", "CNPJ inválido")
            .HasMinLen(City, 4, "City", "o campo cidade deve te no minimo 4 caractéres")
            .HasMinLen(Street, 4, "State", "o campo estado deve te no minimo 4 caractéres")
            .HasMinLen(Street, 3, "Country", "o campo country deve te no minimo 3 caractéres")
            .IsTrue(DocumentValidate.validate(EDocumentType.CNPJ, CNPJ), "Document.Number", "Documento inválido")
        );
        }
    }
}
