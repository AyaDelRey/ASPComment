using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;

namespace ASP_MVC.Models.Comment
{
	public class CommentDelete
	{
		[HiddenInput]
		public Guid CommentId { get; set; }
        public string Content { get; internal set; }
        public DateTime CreatedAt { get; internal set; }
        public Guid CreatedBy { get; internal set; }
    }
}
