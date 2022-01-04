using ToDoList.Operation.Entity;
using ToDoList.Operation.ViewModal;

namespace ToDoList.Operation.Biz
{
    public class bizTask
    {

        public SYS_USERS _User;

        private DbContext context;




        public bizTask(DbContext context)
        {
            this.context = context;
        }


        public SYS_USERS UserInfo
        {
            set
            {
                this._User = value;
            }
        }

        public List<TaskBasicResponse> GetTasks(DateTime? BeginTime, DateTime? EndTime)
        {
            //List<TaskBasicResponse> Result = new List<TaskBasicResponse>();
            var result2 = context.TASK.Where(d => d.DT_BEGIN >= BeginTime && d.DT_BEGIN <= EndTime).ToList();
            var result = context.TASK.Where(d => string.IsNullOrEmpty(d.PK_REF) && (d.DT_BEGIN >= BeginTime && d.DT_BEGIN <= EndTime))
                            .Select(d => new TaskBasicResponse()
                            {
                                DT_BEGIN = d.DT_BEGIN,
                                CD_STATE = d.CD_STATE,
                                DT_CREATE = d.DT_CREATE,
                                DT_FINISH = d.DT_FINISH,
                                PK = d.PK,
                                PK_OWNER = d.PK_OWNER,
                                PK_REF = d.PK_REF,
                                PK_TASKER = d.PK_TASKER,
                                QT_MINUTE = d.QT_MINUTE,
                                DT_UPDATE = d.DT_UPDATE,
                                GN_CONTENT = d.GN_CONTENT,
                                GN_CREATE = d.GN_CREATE,
                                GN_UPDATE = d.GN_UPDATE,
                                NM_SUBJECT = d.NM_SUBJECT,
                                SubTasks = context.TASK.Where(s => s.PK_REF == d.PK)
                                                .Select(s => new TaskBasicResponse()
                                                {
                                                    DT_BEGIN = d.DT_BEGIN,
                                                    CD_STATE = d.CD_STATE,
                                                    DT_CREATE = d.DT_CREATE,
                                                    DT_FINISH = d.DT_FINISH,
                                                    PK = d.PK,
                                                    PK_OWNER = d.PK_OWNER,
                                                    PK_REF = d.PK_REF,
                                                    PK_TASKER = d.PK_TASKER,
                                                    QT_MINUTE = d.QT_MINUTE,
                                                    DT_UPDATE = d.DT_UPDATE,
                                                    GN_CONTENT = d.GN_CONTENT,
                                                    GN_CREATE = d.GN_CREATE,
                                                    GN_UPDATE = d.GN_UPDATE,
                                                    NM_SUBJECT = d.NM_SUBJECT
                                                }).ToList()

                            }).ToList();

            return result;
        }

        /// <summary>
        /// 新增標籤
        /// </summary>
        /// <param name="PostData"></param>
        /// <returns></returns>
        public bool AddTag(TaskTagRequest PostData)
        {
            if (context.TASK_TAGS.Any(d => d.PK_TASK == PostData.PK_TASK && d.PK_TAG == PostData.PK_TAG)
                || context.TAG.Any(d => d.PK == PostData.PK_TAG)) return false;

            var NewData = new TASK_TAGS()
            {
                PK_TAG = PostData.PK_TAG,
                PK_TASK = PostData.PK_TASK,
                DT_CREATE = DateTime.Now
            };

            context.TASK_TAGS.Add(NewData);
            return context.SaveChanges() > 0;
        }
        /// <summary>
        /// 移除標籤
        /// </summary>
        /// <param name="PostData"></param>
        /// <returns></returns>
        public bool RemoveTag(TaskTagRequest PostData)
        {
            var TaskTag = context.TASK_TAGS.FirstOrDefault(d => d.PK_TASK == PostData.PK_TASK && d.PK_TAG == PostData.PK_TAG);
            if (TaskTag == null) return false;

            context.TASK_TAGS.Remove(TaskTag);
            return context.SaveChanges() > 0;
        }

        /// <summary>
        /// 更新狀態
        /// </summary>
        /// <param name="PostData"></param>
        public bool UpdateState(TaskBasicRequest PostData)
        {
            var Task = QueryTask(PostData.PK);
            if (Task == null) return false; 
            Task.CD_STATE = PostData.CD_STATE;
            Task.DT_UPDATE = DateTime.Now;
            Task.DT_FINISH = (PostData.DT_FINISH ?? DateTime.Now);

            context.TASK.Update(Task);
            return context.SaveChanges() > 0;   
        }

        public void AssignUser()
        {

        }
        /// <summary>
        /// 新增任務
        /// </summary>
        /// <param name="PostData"></param>
        /// <returns></returns>
        public bool CreateTask(TaskBasicRequest PostData)
        {
            bool Result = false;
            TASK NewData = new TASK()
            {
                PK = Guid.NewGuid().ToString(),
                DT_CREATE = DateTime.Now,
                DT_BEGIN = PostData.DT_BEGIN ?? DateTime.Now,
                CD_STATE = TaskState.IN_PROC,
                NM_SUBJECT = PostData.NM_SUBJECT,
                GN_CONTENT = PostData.GN_CONTENT,
                PK_REF = PostData.PK_REF,
                PK_OWNER = _User?.NO_ACCOUNT ?? "Empty",
                PK_TASKER = _User?.NO_ACCOUNT ?? "Empty"
            };

            context.TASK.Add(NewData);
            Result = context.SaveChanges() > 0;
            return Result;
        }

        public bool UpdateTask(TaskBasicRequest PostData)
        {
            bool Result = false;
            var Task = QueryTask(PostData.PK);
            if (Task == null) return false;
            //
            Task.NM_SUBJECT = PostData.NM_SUBJECT;
            Task.GN_CONTENT = PostData.GN_CONTENT;
            Task.CD_STATE = PostData.CD_STATE;
            Task.DT_UPDATE = DateTime.Now;
            Task.DT_BEGIN = PostData.DT_BEGIN;
            Task.DT_FINISH = PostData.DT_FINISH;

            context.TASK.Update(Task);
            return context.SaveChanges() > 0;
        }

        public bool RemoveTask(TaskBasicRequest PostData)
        {
            bool Result = false;
            var Task = QueryTask(PostData.PK);
            if (Task == null) return false;
            context.TASK.Remove(Task);
            return context.SaveChanges() > 0;
        }

        public TASK QueryTask(string TaskKey)
        {
            return context.TASK.FirstOrDefault(d => d.PK == TaskKey);
        }



    }
}
