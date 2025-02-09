using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
	public class Comment
	{
        private Guid empty;
        private DateTime now;

        public Guid Comment_Id { get; set; }
		public Guid CreatedBy { get; set; }  // Référence à l'utilisateur qui a créé le commentaire
		public Guid Concern { get; set; }    // Référence à l'entité concernée (par exemple, Cocktail)
		public string Title { get; set; }
		public string Content { get; set; }
		public byte? Note { get; set; }  // Note optionnelle, si applicable
		public DateTime CreatedAt { get; set; }
		public User Creator { get; set; }
        public object CocktailId { get; set; }

        // Constructeur
        public Comment(Guid comment_Id, Guid createdBy, Guid concern, string title, string content, byte? note, DateTime createdAt)
		{
			Comment_Id = comment_Id;
			CreatedBy = createdBy;
			Concern = concern;
			Title = title;
			Content = content;
			Note = note;
			CreatedAt = createdAt;
		}

        public Comment(Guid empty, string content, Guid createdBy, DateTime now)
        {
            this.empty = empty;
            Content = content;
            CreatedBy = createdBy;
            this.now = now;
        }
    }
}
