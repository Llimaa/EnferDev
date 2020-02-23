using EnferDev.Domain.Shared;
using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnferDev.Domain.Entities
{
    public class Address : Notifiable
    {
        public Address()
        {

        }
        public Address(string street, string number, string city, string state, string country, string zipCode)
        {
            IdAddress = Guid.NewGuid();
            Street = street;
            Number = number;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipCode;

            AddNotifications(new Contract()
                .Requires()
                .HasMaxLen(street, 40, "Adddress.Street", "O campo rua deve conter no maximo 40 Caractéres")
                .HasMinLen(street, 3, "Adddress.Street", "O campo rua deve conter no minimo 3 Caractéres")
            );
        }
        public Guid IdAddress { get; private set; }
        public string Street { get; private set; }
        public string Number { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Country { get; private set; }
        public string ZipCode { get; private set; }

        public void update(string street, string number, string city, string state, string country, string zipCode)
        {
            Street = street;
            Number = number;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipCode;
        }
    }
}
