using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Operation.Biz;

namespace TestToDoList
{
    internal class SysUsers : ITestEnv
    {
        private bizTask biz;
        [SetUp]
        public void Begin()
        {
            biz = new bizTask(this._DbContext);
        }
    }
}
