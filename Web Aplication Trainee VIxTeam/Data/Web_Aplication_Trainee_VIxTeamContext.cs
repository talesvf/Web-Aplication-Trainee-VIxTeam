using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Web_Aplication_Trainee_VIxTeam.Models;

namespace Web_Aplication_Trainee_VIxTeam.Data
{
    public class Web_Aplication_Trainee_VIxTeamContext : DbContext
    {
        public Web_Aplication_Trainee_VIxTeamContext (DbContextOptions<Web_Aplication_Trainee_VIxTeamContext> options)
            : base(options)
        {
        }

        public DbSet<Web_Aplication_Trainee_VIxTeam.Models.PessoaModel> PessoaModel { get; set; }
    }
}
