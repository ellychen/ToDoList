
using Microsoft.EntityFrameworkCore.Storage;
using NUnit.Framework;
using System;
using TestToDoList.Common;
using ToDoList;
using ToDoList.Operation;

namespace TestToDoList
{
    public class ITestEnv
    {
        protected bool Commit = false;
        protected DbContext _DbContext;
        protected IDbContextTransaction ContextTransaction = null;
        [SetUp]
        public void Setup()
        {
            _DbContext = DbOption.GetContext;
            ContextTransaction = _DbContext.Database.BeginTransaction();
        }


        [TearDown]
        public void Finish()
        {
            //執行結束
            Transaction(Commit);
        }

        private void Transaction(bool Committe = false)
        {
            if (ContextTransaction != null)
            {
                if (Committe)
                {
                    ContextTransaction.Commit();
                }
                else
                {
                    ContextTransaction.Rollback();
                }
            }

            Console.WriteLine("Transaction Finish");
        }



        [Test]
        public void RunTest(string ActionName , Action Method)
        {            
            Console.WriteLine($"Action Method : {ActionName}");
            Console.WriteLine($"Begin Run : {DateTime.Now} ");
            Method();
            Console.WriteLine($"End Run : {DateTime.Now} ");
        }

    }
}
