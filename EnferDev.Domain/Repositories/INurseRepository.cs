using EnferDev.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnferDev.Domain.Repositories
{
    public interface INurseRepository
    {
        Task Create(Nurse nurse);
        Task Update(Nurse nurse);
        Task<Nurse> GetById(Guid id);
        Task<IEnumerable<Nurse>> GetByHospital(Guid idHospital);
        Task<IEnumerable<Nurse>> GetAll();
        Task<IEnumerable<Nurse>> GetAllActive(bool active);
        Task<IEnumerable<Nurse>> GetAllDesactive(bool active);
        Task<bool> DocumentExists(string document);

    }
}
