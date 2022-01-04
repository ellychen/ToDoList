using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoList.Operation.Entity
{
    public class TASK_TAGS
    { 
        public string PK_TASK { get; set; }  
        public string? PK_TAG { get; set; }  
        public DateTime DT_CREATE { get; set; } 

    }


    public class TAG
    {        
        public string PK { get; set; }  
        public string? NM_TAG { get; set; }

    }
}
