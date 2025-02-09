namespace ASP_MVC.Models.Comment
{
	public class CommentDetails
	{
		public Guid Comment_Id { get; set; }
		public string Content { get; set; }
		public string CreatedBy { get; set; }
		public DateTime CreatedAt { get; set; }
		public string ModifiedBy { get; set; }  // Optionnel, si tu veux aussi suivre les modifications
		public DateTime? ModifiedAt { get; set; }  // Date de modification, si applicable
	}
}

