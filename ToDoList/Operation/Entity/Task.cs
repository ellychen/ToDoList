using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Operation.Entity
{

    public class TASK
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
        
    }

    public class TASK_TIMES
    {
        public string PK { get; set; }
        public int? NO_SEQ { get; set; }
        public DateTime? DT_BEGIN { get; set; }
        public DateTime? DT_END { get; set; }
        [Display(Name = "完成時間點")]
        public string? YN_FINISH { get; set; }

    }




    public class TASK_HISTORY
    {
        [Display(Name = "主鍵")]
        public string PK { get; set; }

        [Display(Name = "異動人員")]
        public string? PK_USER { get; set; }
        [Display(Name = "異動時間")]
        public DateTime? DT_CHANGE { get; set; }
        [Display(Name = "異動內容")]
        public string? GN_LOG { get; set; }
    }


}
