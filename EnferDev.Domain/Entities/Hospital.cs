using EnferDev.Domain.Shared;
using EnferDev.Domain.ValueObjects;
using Flunt.Validations;
using PaymentContext.Domain.Enuns;
using System;

namespace EnferDev.Domain.Entities
{
    public class Hospital : Entity
    {
        public Hospital()
        {

        }
        public Hospital(string name, Document cNPJ, Address address)
        {
            Name = name;
            CNPJ = cNPJ;
            Address = address;
            Active = true;

            AddNotifications(new Contract()
               .Requires()
               .HasMinLen(name, 3, "Name", "O nome deve conter no minimo 3 caractéres")
           );
        }

        public Guid IdAddress { get; private set; }
        public string Name { get; private set; }
        public Document CNPJ { get; private set; }
        public Address Address { get; private set; }
        public bool Active { get; private set; }

        public void setDocument(string number)
        {
            CNPJ = new Document(number, EDocumentType.CNPJ);
        }

        public void updateHospital(string name, string cnpj)
        {
            Name = name;
            CNPJ = new Document(cnpj, EDocumentType.CPF);
        }

        public void setAddress(Address address)
        {
            IdAddress = address.IdAddress;
            Address = address;
        }

        public void ActiveHospital()
        {
            Active = true;
        }

        public void DesactiveHospital()
        {
            Active = false;
        }
    }
}