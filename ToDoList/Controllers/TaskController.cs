using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.Common;
using ToDoList.Operation.Biz;
using ToDoList.Operation.ViewModal;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {

        private bizTask biz { get; set; }
        private ToDoList.Operation.DbContext context;
        public TaskController(IServiceProvider Prov)
        {
            var ContextManage = Prov.GetService<MyDbContextManager>();
            context = new ToDoList.Operation.DbContext(ContextManage.GetOptions());
            biz = new bizTask(context);
        }
        /// <summary>
        /// 範圍內任務
        /// </summary>
        /// <param name="DT_START"></param>
        /// <param name="DT_END"></param>
        /// <returns></returns>
        [HttpGet("List")]
        public IActionResult GetList(string DT_START, string DT_END)
        {
            DateTime? BeginTime = DT_START.ToDateTime(DateTime.Today);
            DateTime? EndTime = DT_END.ToDateTime(DateTime.Today);

            return GetList(BeginTime, EndTime);
        }

        private IActionResult GetList(DateTime? DT_START, DateTime? DT_END)
        {
            ResposneMessage output = new ResposneMessage();

            output.Data = biz.GetTasks(DT_START, DT_END);

            return Ok(output);
        }
        /// <summary>
        /// 日任務
        /// </summary>
        /// <returns></returns>
        [HttpGet("Day/List")]
        public IActionResult GetDayList()
        {
            //當日任務
            DateTime DT_START = DateTime.Today;
            DateTime DT_END = DateTime.Today.AddDays(1);

            return GetList(DT_START, DT_END);

        }
        /// <summary>
        /// 週任務
        /// </summary>
        /// <returns></returns>
        [HttpGet("Week/List")]
        public IActionResult GetWeekList()
        {
            DateTime DT_START = DateTime.Today.GetWeekFirstDate();
            DateTime DT_END = DT_START.AddDays(7);

            return GetList(DT_START, DT_END);
        }

        /// <summary>
        /// 當月任務
        /// </summary>
        /// <returns></returns>
        [HttpGet("Month/List")]
        public IActionResult GetMonthList()
        {

            DateTime DT_START = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime DT_END = DT_START.AddMonths(1);

            return GetList(DT_START, DT_END);
        }
        /// <summary>
        /// 新增任務
        /// </summary>
        /// <param name="PostData"></param>
        /// <returns></returns>
        [HttpPut()]
        public IActionResult CreateTask(TaskBasicRequest PostData)
        {
            ResposneMessage output = new ResposneMessage();
            biz.CreateTask(PostData);
            return Ok(output);
        }
        /// <summary>
        /// 更新任務
        /// </summary>
        /// <param name="PostData"></param>
        /// <returns></returns>
        [HttpPost()]
        public IActionResult UpdateTask(TaskBasicRequest PostData)
        {
            ResposneMessage output = new ResposneMessage();
            biz.UpdateTask(PostData);
            return Ok(output);
        }

        /// <summary>
        /// 刪除任務
        /// </summary>
        /// <param name="PostData"></param>
        /// <returns></returns>
        [HttpDelete()]
        public IActionResult RemoveTask(TaskBasicRequest PostData)
        {
            ResposneMessage output = new ResposneMessage();
            biz.RemoveTask(PostData);
            return Ok(output);
        }
        /// <summary>
        /// 新增標籤
        /// </summary>
        /// <param name="PostData"></param>
        /// <returns></returns>
        [HttpPut("Tag")]
        public IActionResult AddTag(TaskTagRequest PostData)
        {
            ResposneMessage output = new ResposneMessage();
            biz.AddTag(PostData);
            return Ok(output);
        }
        /// <summary>
        /// 刪除標籤
        /// </summary>
        /// <param name="PostData"></param>
        /// <returns></returns>
        [HttpDelete("Tag")]
        public IActionResult RemoveTag(TaskTagRequest PostData)
        {
            ResposneMessage output = new ResposneMessage();
            biz.RemoveTag(PostData);
            return Ok(output);
        }
        /// <summary>
        /// 詳細任務內容
        /// </summary>
        /// <param name="PK"></param>
        /// <returns></returns>
        [HttpGet("{PK}")]
        public IActionResult QueryTaskDetail(string PK)
        {
            ResposneMessage output = new ResposneMessage();
            output.Data = biz.QueryTask(PK);
            return Ok(output);
        }

        /// <summary>
        /// 啟動蕃茄鐘
        /// </summary>
        /// <returns></returns>
        [HttpPost("Promodoro/Start")]
        public IActionResult StartPromodoro()
        {
            ResposneMessage output = new ResposneMessage();

            return Ok(output);
        }
        /// <summary>
        /// 停止蕃茄鐘
        /// </summary>
        /// <returns></returns>
        [HttpPost("Promodoro/Stop")]
        public IActionResult StopPromodoro()
        {
            ResposneMessage output = new ResposneMessage();

            return Ok(output);
        }

    }



    /// <summary>
    /// 任務清單
    /// </summary>
    public class TaskResponse
    {


    }

    /// <summary>
    /// 詳細內容
    /// </summary>
    public class TaskDetailResponse
    {
        public string PK { get; set; }

        public string NM_SUBJECT { get; set; }
        public string GN_CONTENT { get; set; }
        public DateTime? DT_TASK { get; set; }
        public DateTime? DT_APPLY { get; set; }
        public DateTime? DT_FINISH { get; set; }
        public DateTime? DT_PRE_FINISH { get; set; }
        public string PK_OWNER { get; set; }
        public string PK_EXECUTOR { get; set; }
        public string CD_STATE { get; set; }
        public string NM_STATE { get; set; }


    }

}
