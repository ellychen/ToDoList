using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestToDoList.Common
{
    public class DbOption
    {

        private static ToDoList.Operation.DbContext _DbContext = null;
        public static ToDoList.Operation.DbContext GetContext
        {
            get
            {
                if (_DbContext == null)
                {
                    string ConnectionString = "{YourConnection}  ";
                    _DbContext = new ToDoList.Operation.DbContext(
                        new DbContextOptionsBuilder().UseSqlServer(ConnectionString).Options);
                }
                return _DbContext;
            }
        }

    }
}
