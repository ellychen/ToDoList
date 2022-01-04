namespace ToDoList.Operation.ViewModal
{
    public class TagRequest
    {
        public string PK { get; set; }
        public string NM_TAG { get; set; }

    }

    public class TagResponse
    {
        public string PK { get; set; }
        public string NM_TAG { get; set; }
    }


    public class TaskTagsResponse
    {
        public string PK_TASK { get; set; }
        public string PK_TAG { get; set; }
        public string NM_TAG { get; set; }
        public DateTime DT_CREATE { get; set; }

    }

}
