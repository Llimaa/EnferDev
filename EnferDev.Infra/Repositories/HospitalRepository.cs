using Dapper;
using EnferDev.Domain.Entities;
using EnferDev.Domain.Repositories;
using EnferDev.Infra.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnferDev.Infra.Repositories
{
    public class HospitalRepository : IHospitalRepository
    {
        private readonly IDB _dB;

        public HospitalRepository(IDB dB)
        {
            _dB = dB;
        }

        public async Task Create(Hospital hospital)
        {
            using (var db = _dB.GetCon())
            {
                var query = "	INSERT INTO [dbo].[Hospital] " +
                            "	       ([Id]				 " +
                            "	       ,[Name]				 " +
                            "	       ,[CNPJ]				 " +
                            "	       ,[Active]			 " +
                            "	       ,[IdAddress])		 " +
                            "	 VALUES						 " +
                            "	       (@Id					 " +
                            "	       ,@Name				 " +
                            "	       ,@CNPJ				 " +
                            "	       ,@Active 			 " +
                            "	       ,@IdAddress)			 ";

                await db.ExecuteAsync(query, new
                {
                    @Id = hospital.Id,
                    @Name = hospital.Name,
                    @CNPJ = hospital.CNPJ.Number,
                    @Active = hospital.Active,
                    @IdAddress = hospital.Address.IdAddress
                });
            }
        }

        public async Task Update(Hospital hospital)
        {

            var query = "UPDATE [dbo].[Hospital] SET Name = @Name, CNPJ = @CNPJ, Active = @Active WHERE Id = @Id";

            using (var db = _dB.GetCon())
            {

                await db.QueryAsync<Hospital>(query, new
                {
                    Id = hospital.Id,
                    Name = hospital.Name,
                    CNPJ = hospital.CNPJ.Number,
                    Active = hospital.Active,
                });
            }
        }

        public async Task<bool> DocumentExists(string document)
        {
            using (var db = _dB.GetCon())
            {
                var query = "SELECT COUNT(Id) FROM [dbo].[Hospital] WHERE CNPJ = @CNPJ";
                return await db.QueryFirstAsync<bool>(query, new { CNPJ = document });
            }
        }

        public async Task<IEnumerable<Hospital>> GetAll()
        {
            using (var db = _dB.GetCon())
            {
                var query = "SELECT  H.Active, H.IdAddress, H.Name, H.Id, H.Name, H.CNPJ, A.ZipCode, A.Id AS IdAddress, A.Street, A.Number, A.City, A.State, A.Country " +
                            "FROM [dbo].[Hospital] AS H INNER JOIN[dbo].[Address] AS A ON H.IdAddress = A.Id ORDER BY H.Id";

                return await db.QueryAsync<Hospital, string, Address, Hospital>(query, map: (_hospital, document, address) =>
                   {
                       _hospital.setDocument(document);
                       _hospital.setAddress(address);
                       return _hospital;
                   }, splitOn: "Id,CNPJ,ZipCode");
            }
        }

        public async Task<IEnumerable<Hospital>> GetAllActive(bool active)
        {
            var query = "SELECT  H.Active, H.IdAddress, H.Name, H.Id, H.Name, H.CNPJ, A.ZipCode, A.Id AS IdAddress, A.Street, A.Number, A.City, A.State, A.Country " +
                         "FROM [dbo].[Hospital] AS H INNER JOIN[dbo].[Address] AS A ON H.IdAddress = A.Id  WHERE H.Active = @Active";

            using (var db = _dB.GetCon())
            {
                return await db.QueryAsync<Hospital, string, Address, Hospital>(query, map: (_hospital, document, address) =>
                {
                    _hospital.setDocument(document);
                    _hospital.setAddress(address);
                    return _hospital;
                }, new { Active = active }, splitOn: "Id,CNPJ, ZipCode");
            }
        }

        public async Task<IEnumerable<Hospital>> GetAllDesactive(bool active)
        {
            var query = "SELECT  H.Active, H.IdAddress, H.Name, H.Id, H.Name, H.CNPJ, A.ZipCode, A.Id AS IdAddress, A.Street, A.Number, A.City, A.State, A.Country " +
                          "FROM [dbo].[Hospital] AS H INNER JOIN[dbo].[Address] AS A ON H.IdAddress = A.Id  WHERE H.Active = @Active";

            using (var db = _dB.GetCon())
            {
                return await db.QueryAsync<Hospital, string, Address, Hospital>(query, map: (_hospital, document, address) =>
                {
                    _hospital.setDocument(document);
                    _hospital.setAddress(address);
                    return _hospital;
                }, new { Active = active }, splitOn: "Id,CNPJ, ZipCode");
            }
        }

        public async Task<Hospital> GetById(Guid id)
        {
            var query = "SELECT  H.Active, H.IdAddress, H.Name, H.Id, H.Name, H.CNPJ, A.ZipCode, A.Id AS IdAddress, A.Street, A.Number, A.City, A.State, A.Country " +
                           "FROM [dbo].[Hospital] AS H INNER JOIN[dbo].[Address] AS A ON H.IdAddress = A.Id  WHERE H.Id = @Id";
            using (var db = _dB.GetCon())
            {
                var response = await db.QueryAsync<Hospital, string, Address, Hospital>(query, map: (_hospital, document, address) =>
                {
                    _hospital.setDocument(document);
                    _hospital.setAddress(address);
                    return _hospital;
                }, new { Id = id }, splitOn: "Active,CNPJ, ZipCode");
                return response.FirstOrDefault();
            }
        }
    }
}
