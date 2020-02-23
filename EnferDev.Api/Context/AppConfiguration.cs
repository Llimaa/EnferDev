using EnferDev.Infra.Infra;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnferDev.Api.Context
{
    public class AppConfiguration: IDbConfiguration
    {
        private readonly IConfiguration _dbConfiguration;

        public AppConfiguration(IConfiguration dbConfiguration)
        {
            _dbConfiguration = dbConfiguration;
        }

        public string connectionString => _dbConfiguration.GetConnectionString("DefaultConnection");
    }
}
