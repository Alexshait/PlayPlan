using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayPlan.DataModel
{
    internal class PlayPlanContextInitializer
    {
        private readonly string _connectionString;

        public PlayPlanContextInitializer(string connectionString)
        {
            _connectionString = connectionString;
        }

        public PlayPlanContext CreateDbContext()
        {
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlite(_connectionString).Options;

            return new PlayPlanContext();
        }
    }
}
