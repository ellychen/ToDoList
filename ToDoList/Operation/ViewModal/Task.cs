using System.ComponentModel.DataAnnotations;

namespace ToDoList.Operation.ViewModal
{

    public class TaskListRequest
    {

    }

    public class TaskBasicRequest
    {
        /// <summary>
        /// 主鍵
        /// </summary>        
        [Display(Name = "主鍵")]
        public string PK { get; set; }
        [Display(Name = "父鍵")]
        public string? PK_REF { get; set; }
        [Display(Name = "開始時間")]
        public string? NM_SUBJECT { get; set; }
        public string? GN_CONTENT { get; set; }
        public DateTime? DT_BEGIN { get; set; }
        [Display(Name = "建立時間")]
        public DateTime? DT_CREATE { get; set; }
        [Display(Name = "完成時間")]
        public DateTime? DT_FINISH { get; set; }
        [Display(Name = "任務狀態")]
        public string? CD_STATE { get; set; }
        [Display(Name = "任務擁有者")]
        public string? PK_OWNER { get; set; }
        [Display(Name = "任務執行者")]
        public string? PK_TASKER { get; set; }
        [Display(Name = "執行時間")]
        public int? QT_MINUTE { get; set; }


    }
    public class TaskBasicResponse
    {
        /// <summary>
        /// 主鍵
        /// </summary>                
        [Display(Name = "主鍵")]
        public string PK { get; set; }
        [Display(Name = "父鍵")]
        public string? PK_REF { get; set; }
        public string? NM_SUBJECT { get; set; }
        public string? GN_CONTENT { get; set; }
        [Display(Name = "開始時間")]
        public DateTime? DT_BEGIN { get; set; }
        [Display(Name = "建立時間")]
        public DateTime? DT_CREATE { get; set; }
        [Display(Name = "完成時間")]
        public DateTime? DT_FINISH { get; set; }
        [Display(Name = "任務狀態")]
        public string? CD_STATE { get; set; }
        [Display(Name = "任務擁有者")]
        public string? PK_OWNER { get; set; }
        [Display(Name = "任務執行者")]
        public string? PK_TASKER { get; set; }
        [Display(Name = "執行時間")]
        public int? QT_MINUTE { get; set; }
        public DateTime? DT_UPDATE { get; set; }
        public string? GN_UPDATE { get; set; }
        public string? GN_CREATE { get; set; }

        public List<TaskBasicResponse> SubTasks { get; set; }

    }

    public class TaskTagRequest
    {
        public string PK_TASK { get; set; } 
        public string PK_TAG { get; set; }
    }

    public class TaskTagResponse
    {
        public string PK_TAG { get; set; }
        public string NM_TAG { get; set; }
    }


}
