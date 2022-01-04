using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ToDoList
{
    public class MyDbContextManager
    {

        private Dictionary<string, DbContextOptionsBuilder> _OptionsBuilders { get; set; }

        public MyDbContextManager()
        {
            _OptionsBuilders = new Dictionary<string, DbContextOptionsBuilder>();
        }

        public void Add(string DbName, DbContextOptionsBuilder optionsBuilder)
        {
            _OptionsBuilders.Add(DbName, optionsBuilder);


        }


        public DbContextOptions GetOptions(string DbName = "")
        {

            if (string.IsNullOrEmpty(DbName))
                return _OptionsBuilders.FirstOrDefault().Value?.Options ?? null;
            else
                return _OptionsBuilders[DbName]?.Options ?? null;
        }


    }
}
