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
    public class NurseRepository : INurseRepository
    {
        private readonly IDB _dB;

        public NurseRepository(IDB dB)
        {
            _dB = dB;
        }

        public async Task Create(Nurse nurse)
        {
            using (var db = _dB.GetCon())
            {
                var query = "	INSERT INTO [dbo].[Nurse] " +
                            "           ([Id]			  " +
                            "           ,[IdHospital]	  " +
                            "	        ,[Name]      	  " +
                            "           ,[CPF]			  " +
                            "           ,[COREN]		  " +
                            "           ,[DateBirth]	  " +
                            "           ,[Active])		  " +
                            "     VALUES				  " +
                            "           (@Id			  " +
                            "           ,@IdHospital	  " +
                            "           ,@Name	          " +
                            "           ,@CPF			  " +
                            "           ,@COREN			  " +
                            "           ,@DateBirth		  " +
                            "           ,@Active)		  ";

                await db.ExecuteAsync(query, new
                {
                    @Id = nurse.Id,
                    @IdHospital = nurse.IdHospital,
                    @Name = nurse.Name,
                    @CPF = nurse.CPF.Number,
                    @Coren = nurse.Coren,
                    @DateBirth = nurse.DateBirth,
                    @Active = nurse.Active,
                });
            }
        }

        public async Task Update(Nurse nurse)
        {
            using (var db = _dB.GetCon())
            {
                var query = "	UPDATE [dbo].[Nurse]                  " +
                            "	   SET [Id] = @Id					  " +
                            "	      ,[IdHospital] = @IdHospital	  " +
                            "	      ,[CPF] = @CPF					  " +
                            "	      ,[COREN] = @COREN				  " +
                            "	      ,[DateBirth] = @DateBirth		  " +
                            "	      ,[Active] = @Active			  " +
                            "	      ,[Name] = @Name				  " +
                            "	 WHERE Id = @Id						  ";

                await db.QueryAsync<Nurse>(query, new
                {
                    Id = nurse.Id,
                    IdHospital = nurse.IdHospital,
                    CPF = nurse.CPF.Number,
                    COREN = nurse.Coren,
                    DateBirth = nurse.DateBirth,
                    Active = nurse.Active,
                    Name = nurse.Name,
                });
            }
        }

        public async Task<IEnumerable<Nurse>> GetAll()
        {
            var query = "SELECT H.[Name] AS NameHospital, [dbo].[Nurse].Active,[dbo].[Nurse].Id, COREN, DateBirth, [dbo].[Nurse].Name, IdHospital, CPF FROM [dbo].[Nurse] " +
                        "INNER JOIN Hospital AS H ON H.Id = Nurse.IdHospital";

            using (var db = _dB.GetCon())
            {
                return await db.QueryAsync<Nurse, string, Nurse>(query, map: (_nurse, document) =>
                 {
                     _nurse.setDocument(document);
                     return _nurse;
                 }, splitOn: "CPF");
            }
        }



        public async Task<Nurse> GetById(Guid id)
        {
            var query = "SELECT H.[Name] AS NameHospital, [dbo].[Nurse].Active,[dbo].[Nurse].Id, COREN, DateBirth, [dbo].[Nurse].Name, IdHospital, CPF FROM [dbo].[Nurse] " +
             "INNER JOIN Hospital AS H ON H.Id = Nurse.IdHospital WHERE [dbo].[Nurse].Id = @Id";

            using (var db = _dB.GetCon())
            {
                var response = await db.QueryAsync<Nurse, string, Nurse>(query, map: (_nurse, document) =>
                {
                    _nurse.setDocument(document);
                    return _nurse;
                }, new { Id = id }, splitOn: "CPF");
                return response.FirstOrDefault();
            }
        }

        public async Task<bool> DocumentExists(string document)
        {
            using (var db = _dB.GetCon())
            {
                var query = "SELECT COUNT(Id) FROM [dbo].[Nurse] WHERE CPF = @CPF";
                return await db.QueryFirstOrDefaultAsync<bool>(query, new { CPF = document });
            }
        }

        public async Task<IEnumerable<Nurse>> GetAllActive(bool active)
        {
            var query = "SELECT H.[Name] AS NameHospital, [dbo].[Nurse].Name, [dbo].[Nurse].Active,[dbo].[Nurse].Id, COREN, DateBirth, IdHospital, CPF FROM [dbo].[Nurse] " +
            "INNER JOIN Hospital AS H ON H.Id = Nurse.IdHospital WHERE [dbo].[Nurse].Active = @Active";

            using (var db = _dB.GetCon())
            {
                return await db.QueryAsync<Nurse, string, Nurse>(query, map: (_nurse, document) =>
                {
                    _nurse.setDocument(document);
                    return _nurse;
                }, new { Active = active }, splitOn: "CPF");
            }
        }

        public async Task<IEnumerable<Nurse>> GetAllDesactive(bool active)
        {
            var query = "SELECT H.[Name] AS NameHospital, [dbo].[Nurse].Active,[dbo].[Nurse].Id, COREN, DateBirth, [dbo].[Nurse].Name, IdHospital, CPF FROM [dbo].[Nurse] " +
                        "INNER JOIN Hospital AS H ON H.Id = Nurse.IdHospital WHERE [dbo].[Nurse].Active = @Active";

            using (var db = _dB.GetCon())
            {
                return await db.QueryAsync<Nurse, string, Nurse>(query, map: (_nurse, document) =>
                {
                    _nurse.setDocument(document);
                    return _nurse;
                }, new { Active = active }, splitOn: "CPF");
            }
        }

        public async Task<IEnumerable<Nurse>> GetByHospital(Guid idHospital)
        {
            var query = "SELECT H.[Name] AS NameHospital, [dbo].[Nurse].Active,[dbo].[Nurse].Id, COREN, DateBirth, [dbo].[Nurse].Name, IdHospital, CPF FROM [dbo].[Nurse] " +
                       "INNER JOIN Hospital AS H ON H.Id = Nurse.IdHospital WHERE IdHospital = @IdHospital";

            using (var db = _dB.GetCon())
            {
                return await db.QueryAsync<Nurse, string, Nurse>(query, map: (_nurse, document) =>
                {
                    _nurse.setDocument(document);
                    return _nurse;
                }, new { IdHospital = idHospital }, splitOn: "CPF");
            }
        }
    }
}
