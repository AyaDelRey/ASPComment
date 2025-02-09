namespace ASP_MVC.Models.Comment
{
	public class CommentListItem
	{
		public Guid Comment_Id { get; set; }
		public string Content { get; set; }
		public string CreatedBy { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}
