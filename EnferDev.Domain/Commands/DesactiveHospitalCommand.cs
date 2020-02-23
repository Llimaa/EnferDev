using EnferDev.Domain.Shared.Commands;
using Flunt.Notifications;
using Flunt.Validations;
using System;

namespace EnferDev.Domain.Commands
{
    public class DesactiveHospitalCommand :Notifiable, ICommand
    {
        public Guid Id { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract()
            .Requires()
            .IsNotEmpty(Id, "Id", "Valor inválido")
        );
        }
    }
}
