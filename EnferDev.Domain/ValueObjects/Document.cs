using Flunt.Validations;
using PaymentContext.Domain.Enuns;
using Flunt.Notifications;
using EnferDev.Domain.Shared;

namespace EnferDev.Domain.ValueObjects
{
    public class Document : Notifiable
    {
        public Document(string number, EDocumentType type)
        {
            Number = number;
            Type = type;

            AddNotifications(new Contract()
                .Requires()
                //.IsTrue(DocumentValidate.validate(Type, Number), "Document.Number", "Documento inválido")
            );
        }

        public string Number { get; private set; }
        public EDocumentType Type { get; private set; }

    }
}
