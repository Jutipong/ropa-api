using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Entities.DdContextTcrb
{
    public class DdContextTcrb : TcrbContext
    {
        private readonly AppSittingModel _configuration;

        public DdContextTcrb(IOptions<AppSittingModel> configuration)
        {
            _configuration = configuration.Value;
        }

        public DdContextTcrb(DbContextOptions<TcrbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_configuration.ConnectionStrings.TCRBDB);
            }
        }
    }
}
