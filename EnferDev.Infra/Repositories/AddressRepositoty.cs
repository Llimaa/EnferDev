using Dapper;
using EnferDev.Domain.Entities;
using EnferDev.Domain.Repositories;
using EnferDev.Infra.Infra;
using System;
using System.Threading.Tasks;

namespace EnferDev.Infra.Repositories
{
    public class AddressRepositoty : IAddressRepositoty
    {
        private readonly IDB _dB;

        public AddressRepositoty(IDB dB)
        {
            _dB = dB;
        }

        public async Task Create(Address address)
        {
            using (var db = _dB.GetCon())
            {
                var query = " INSERT INTO [dbo].[Address]				" +
                            "       ([Id]							    " +
                            "       ,[Street]							" +
                            "       ,[Number]							" +
                            "       ,[City]								" +
                            "       ,[State]							" +
                            "       ,[Country]							" +
                            "       ,[ZipCode])							" +
                            " VALUES									" +
                            "        (@Id							    " +
                            "       ,@Street							" +
                            "       ,@Number							" +
                            "       ,@City								" +
                            "       ,@State								" +
                            "       ,@Country							" +
                            "       ,@ZipCode)							";

                await db.ExecuteAsync(query, new
                {
                    Id = address.IdAddress,
                    @Street = address.Street,
                    @Number = address.Number,
                    @City = address.City,
                    @State = address.State,
                    @Country = address.Country,
                    @ZipCode = address.ZipCode
                });
            }
        }

        public async Task Update(Address address)
        {
            using (var db = _dB.GetCon())
            {
                var query = " UPDATE [dbo].[Address]			" +
                            "    SET [Id]	    = @Id    		" +
                            "     ,[Street]	    = @Street		" +
                            " 	  ,[Number]		= @Number		" +
                            " 	  ,[City]		= @City			" +
                            " 	  ,[State]		= @State		" +
                            " 	  ,[Country]	= @Country		" +
                            " 	  ,[ZipCode]	= @ZipCode		" +
                            " WHERE Id = @Id		            ";

                await db.ExecuteAsync(query, new
                {
                    Id = address.IdAddress,
                    Street = address.Street,
                    Number = address.Number,
                    City = address.City,
                    State = address.State,
                    Country = address.Country,
                    ZipCode = address.ZipCode,
                });
            }
        }

        public async Task<Address> GetById(Guid id)
        {
            using (var db = _dB.GetCon())
            {
                var query = "	SELECT [Id]                    " +
                            "		  ,[Street]				   " +
                            "		  ,[Number]				   " +
                            "		  ,[City]				   " +
                            "		  ,[State]				   " +
                            "		  ,[Country]			   " +
                            "		  ,[ZipCode]			   " +
                            "   FROM [dbo].[Address]		   " +
                            "   WHERE Id = @Id ";
                return await db.QueryFirstOrDefaultAsync<Address>(query, new { Id = id });
            }
        }
    }
}
