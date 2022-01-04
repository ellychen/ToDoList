using ToDoList.Operation.Entity;
using ToDoList.Operation.ViewModal;

namespace ToDoList.Operation.Biz
{
    public class bizTag
    {

        public SYS_USERS _User;

        private DbContext context;




        public bizTag(DbContext context)
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

        /// <summary>
        /// 新增標籤
        /// </summary>
        public bool AddTag(TagRequest PostData)
        {
            if (QuerySameTagText(PostData.NM_TAG)) return false;

            var NewData = new TAG()
            {
                PK = Guid.NewGuid().ToString(),
                NM_TAG = PostData.NM_TAG
            };

            context.TAG.Add(NewData);
            return context.SaveChanges() > 0;
        }
        /// <summary>
        /// 更新標籤
        /// </summary>
        public bool UpdateTag(TagRequest PostData)
        {
            var Tag = QueryTag(PostData.PK);
            if (Tag == null) return false;
            Tag.NM_TAG = PostData.NM_TAG;
            context.TAG.Update(Tag);
            return context.SaveChanges() > 0;
        }
        /// <summary>
        /// 有無相同名稱標籤
        /// </summary>
        /// <param name="NM_TAG"></param>
        /// <returns></returns>
        private bool QuerySameTagText(string NM_TAG, string PK = "")
        {
            return context.TAG.Any(d => d.NM_TAG == NM_TAG );
        }
        /// <summary>
        /// 查詢所有標籤
        /// </summary>
        public List<TagResponse> QueryTags()
        {
            var Result = context.TAG.Select(d => new TagResponse()
            {
                PK = d.PK,
                NM_TAG = d.NM_TAG
            }).ToList();

            return Result;
        }

        /// <summary>
        /// 單一查詢
        /// </summary>
        /// <param name="PK"></param>
        /// <returns></returns>
        public TAG QueryTag(string PK)
        {
            return context.TAG.FirstOrDefault(d => d.PK == PK);
        }

        /// <summary>
        /// 移除標籤
        /// </summary>
        /// <param name="PostData"></param>
        public bool RemoveTag(TagRequest PostData)
        {
            var Tag = QueryTag(PostData.PK);
            if (Tag == null) return false;
            context.TAG.Remove(Tag);
            return context.SaveChanges() > 0;
        }

    }
}
