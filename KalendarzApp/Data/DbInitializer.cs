using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KalendarzApp.Models;
using Microsoft.EntityFrameworkCore;

namespace KalendarzApp.Data
{
    public static class DbInitializer
    {
        public static void EnsureDatabase()
        {
            using var context = new DataContext();
            context.Database.Migrate();
        }
    }
}
