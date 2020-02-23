using EnferDev.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace EnferDev.Domain.Repositories
{
    public interface IAddressRepositoty
    {
        Task Create(Address address);
        Task Update(Address address);
        Task<Address> GetById(Guid Id);
    }
}
