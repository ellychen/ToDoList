
using Microsoft.EntityFrameworkCore.Storage;
using NUnit.Framework;
using System;
using TestToDoList.Common;
using ToDoList;
using ToDoList.Operation;
using ToDoList.Operation.Biz;
using ToDoList.Operation.ViewModal;

namespace TestToDoList
{
    public class Task : ITestEnv
    {

        private bizTask biz;
        [SetUp]
        public void Begin()
        {
            biz = new bizTask(this._DbContext);
        }


        [Test]
        public void Run()
        {
            //RunTest("AddTask1", AddTask1);
            RunTest("QueryTask1", QueryTask1);
            //RunTest("UpdateTask1", UpdateTask1);
            //RunTest("RemoveTask1", RemoveTask1);
            
            //RunTest("AddTask2", AddTask2);
        }

        public void QueryTask1()
        {
            DateTime? BeginTime = DateTime.Today;
            DateTime? EndTime = DateTime.Today.AddDays(1);
            var Result = biz.GetTasks(BeginTime, EndTime);

            Assert.IsNotNull(Result);
        }


        public void AddTask1()
        {
            var PostData = new TaskBasicRequest()
            {
                NM_SUBJECT = "上午",
                GN_CONTENT = ""
            };

            biz.CreateTask(PostData);


            Assert.IsNotNull(PostData);
        }

        public void AddTask2()
        {
            var PostData = new TaskBasicRequest()
            {
                PK_REF = "173cf88d-f0ef-4864-a16d-e2b5b30fc490",
                NM_SUBJECT = "打掃",
                GN_CONTENT = ""
            };

            biz.CreateTask(PostData);


            Assert.IsNotNull(PostData);
        }


        public void RemoveTask1()
        {
            var PostData = new TaskBasicRequest()
            {
                PK = "4cd3aadd-c31e-4227-a7be-c7954b565f40"
            };

            var result = biz.RemoveTask(PostData);
            Assert.IsTrue(result);
        }

        public void UpdateTask1()
        {
            var PostData = new TaskBasicRequest()
            {
                PK = "4cd3aadd-c31e-4227-a7be-c7954b565f40",
                DT_BEGIN = DateTime.Today,
                NM_SUBJECT = "突發事件"
            };

            var result = biz.UpdateTask(PostData);
            Assert.IsTrue(result);
        }






    }
}