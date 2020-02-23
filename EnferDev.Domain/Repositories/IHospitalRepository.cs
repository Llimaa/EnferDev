using EnferDev.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnferDev.Domain.Repositories
{
    public interface IHospitalRepository
    {
        Task Create(Hospital hospital);
        Task Update(Hospital hospital);
        Task<Hospital> GetById(Guid id);
        Task<IEnumerable<Hospital>> GetAll();
        Task<IEnumerable<Hospital>> GetAllActive(bool active);
        Task<IEnumerable<Hospital>> GetAllDesactive(bool active);
        Task<bool> DocumentExists(string document);
    }
}
