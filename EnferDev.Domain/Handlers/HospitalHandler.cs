using EnferDev.Domain.Commands;
using EnferDev.Domain.Entities;
using EnferDev.Domain.Repositories;
using EnferDev.Domain.Shared.Commands;
using EnferDev.Domain.Shared.Handlers;
using EnferDev.Domain.ValueObjects;
using PaymentContext.Domain.Enuns;
using System.Threading.Tasks;

namespace EnferDev.Domain.Handlers
{
    public class HospitalHandler :
        IHandler<CreateHospitalCommand>,
        IHandler<UpdateHospitalCommand>,
        IHandler<ActiveHospitalCommand>,
        IHandler<DesactiveHospitalCommand>

    {
        private readonly IHospitalRepository _hospitalRepository;
        private readonly IAddressRepositoty _addressRepositoty;

        public HospitalHandler(IHospitalRepository hospitalRepository, IAddressRepositoty addressRepositoty)
        {
            _hospitalRepository = hospitalRepository;
            _addressRepositoty = addressRepositoty;
        }

        public async Task<ICommandResult> Handler(CreateHospitalCommand command)
        {

            var isDocument = await _hospitalRepository.DocumentExists(command.CNPJ);
            if (isDocument)
                return new GenericCommandResult(false, "Ops, já temos um Hospital cadastrado com esse CNPJ", command.Notifications);

            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, "Ops, CNPJ inválido!", command.Notifications);

            var address = new Address(command.Street, command.Number, command.State, command.City, command.Country, command.ZipCode);
            var document = new Document(command.CNPJ, EDocumentType.CNPJ);
            var hospital = new Hospital(command.Name, document, address);

            await _addressRepositoty.Create(address);
            await _hospitalRepository.Create(hospital);
            return new GenericCommandResult(true, "Hospital cadastrado", hospital);
        }

        public async Task<ICommandResult> Handler(UpdateHospitalCommand command)
        {
            command.Validate();
            if (!command.Valid)
                return new GenericCommandResult(false, "Ops, parece que algo está errado!", command.Notifications);

            var hospital = await _hospitalRepository.GetById(command.Id);
            var address = await _addressRepositoty.GetById(hospital.IdAddress);

            address.update(command.Street, command.Number, command.City, command.State, command.Country, command.ZipCode);

            hospital.updateHospital(command.Name, command.CNPJ);

            if (hospital.Notifications.Count > 0 || address.Notifications.Count > 0)
                return new GenericCommandResult(false, "ocoreu algum erro", command.Notifications);

            await _hospitalRepository.Update(hospital);
            await _addressRepositoty.Update(address);

            return new GenericCommandResult(true, "Dados do hospital atualizado", hospital);
        }

        public async Task<ICommandResult> Handler(ActiveHospitalCommand command)
        {
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, "Ops, parece que algo esta errado!", command.Notifications);

            var hospital = await _hospitalRepository.GetById(command.Id);
            hospital.ActiveHospital();

            await _hospitalRepository.Update(hospital);

            return new GenericCommandResult(true, "Dados do hospital atualizado", hospital);
        }

        public async Task<ICommandResult> Handler(DesactiveHospitalCommand command)
        {
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, "Ops, parece que algo esta errado!", command.Notifications);

            var hospital = await _hospitalRepository.GetById(command.Id);
            hospital.DesactiveHospital();

            await _hospitalRepository.Update(hospital);

            return new GenericCommandResult(true, "Dados do hospital atualizado", hospital);
        }
    }
}
