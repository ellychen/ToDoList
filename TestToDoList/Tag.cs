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
    internal class Tag : ITestEnv
    {
        private bizTag biz;
        [SetUp]
        public void Begin()
        {
            biz = new bizTag(this._DbContext);
        }

        [Test]
        public void Run()
        {            
            RunTest("AddTag1", AddTag1);
            RunTest("QueryTags", QueryTags);            
            RunTest("UpdateTag1", UpdateTag1);
            RunTest("RemoveTag1", RemoveTag1);

        }

        public void AddTag1()
        {
            var PostData = new TagRequest()
            {
                NM_TAG = "突發工作"

            };

            var Result = biz.AddTag(PostData);
            Assert.IsTrue(Result);
        }

        public void UpdateTag1()
        {
            var PostData = new TagRequest()
            {
                PK = "b0ffb610-5c3c-4e91-8084-72c30d013fd2",
                NM_TAG = "每日工作"
            };

            var Result = biz.UpdateTag(PostData);
            Assert.IsTrue(Result);
        }

        public void QueryTags()
        {
            var Result = biz.QueryTags();
            Assert.IsNotNull(Result);
        }

        public void RemoveTag1()
        {
            var PostData = new TagRequest()
            {
                PK = "b0ffb610-5c3c-4e91-8084-72c30d013fd2"
            };

            var Result = biz.RemoveTag(PostData);
            Assert.IsTrue(Result);
        }


    }
}
