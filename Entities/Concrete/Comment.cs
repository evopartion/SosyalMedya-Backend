namespace Entities.Concrete
{
    public class Comment
    {
        public int ID { get; set; }
        public int ArticleID { get; set; }
        public int UserID { get; set; }
        public string CommentText { get; set; }
        public DateTime CommentDate { get; set; }
    }
}
