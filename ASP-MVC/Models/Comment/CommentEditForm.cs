using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;

namespace ASP_MVC.Models.Comment
{
	public class CommentEditForm
	{
		[DisplayName("Contenu du commentaire : ")]
		[Required(ErrorMessage = "Le champ 'Contenu du commentaire' est obligatoire.")]
		[MinLength(2, ErrorMessage = "Le champ 'Contenu du commentaire' doit contenir au minimum 2 caractères.")]
		[MaxLength(500, ErrorMessage = "Le champ 'Contenu du commentaire' doit contenir au maximum 500 caractères.")]
		public string Content { get; set; }

		[HiddenInput]
		public Guid CommentId { get; set; }
        public Guid CreatedBy { get; internal set; }
        public Guid Comment_Id { get; internal set; }
    }
}

