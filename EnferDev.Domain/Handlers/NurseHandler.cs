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
    public class NurseHandler :
        IHandler<CreateNurseCommand>,
        IHandler<UpdateNurseCommand>,
        IHandler<ActiveNurseCommand>,
        IHandler<DesactiveNurseCommand>
    {

        private readonly INurseRepository _nurseRepository;
        private readonly IAddressRepositoty _addressRepositoty;

        public NurseHandler(INurseRepository nurseRepository, IAddressRepositoty addressRepositoty)
        {
            _nurseRepository = nurseRepository;
            _addressRepositoty = addressRepositoty;
        }

        public async Task<ICommandResult> Handler(CreateNurseCommand command)
        {
            command.Validate();
            if (!command.Valid)
                return new GenericCommandResult(false, "Ops, parece que algo está errado!", command.Notifications);

            var isDocument = await _nurseRepository.DocumentExists(command.CPF);
            if (isDocument)
                return new GenericCommandResult(false, "Ops, já temos um enfermeio cadastrado com esse CPF", command.Notifications);

            var document = new Document(command.CPF, EDocumentType.CPF);
            var nurse = new Nurse(command.Name, document, command.Coren, command.DateBirth, command.HospitalId);

            await _nurseRepository.Create(nurse);
            return new GenericCommandResult(true, "Enfermeiro cadastrado", nurse);
        }

        public async Task<ICommandResult> Handler(UpdateNurseCommand command)
        {
            command.Validate();
            if (!command.Valid)
                return new GenericCommandResult(false, "Ops, parece que algo está errado!", command.Notifications);

            var nurse = await _nurseRepository.GetById(command.Id);
            nurse.updateNurse(command.Name, command.CPF, command.Coren, command.DateBirth);

            if (nurse.Invalid)
                return new GenericCommandResult(false, "ocoreu algum erro", command.Notifications);

            await _nurseRepository.Update(nurse);

            return new GenericCommandResult(true, "Dados do Enfermeiro atualizado", nurse);
        }

        public async Task<ICommandResult> Handler(ActiveNurseCommand command)
        {
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, "Ops, parece que algo esta errado!", command.Notifications);

            var nurse = await _nurseRepository.GetById(command.Id);
            nurse.ActiveNurse();

            await _nurseRepository.Update(nurse);

            return new GenericCommandResult(true, "Dados do enfermeiro atualizado", nurse);
        }

        public async Task<ICommandResult> Handler(DesactiveNurseCommand command)
        {
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, "Ops, parece que algo esta errado!", command.Notifications);

            var nurse = await _nurseRepository.GetById(command.Id);
            nurse.DesactiveNurse();

            await _nurseRepository.Update(nurse);

            return new GenericCommandResult(true, "Dados do enfermeiro atualizado", nurse);
        }
    }
}
