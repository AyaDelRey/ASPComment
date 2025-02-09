using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
	public class Comment
	{
		public Guid Comment_Id { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public Guid Concern { get; set; }  // Référence à Cocktail
		public DateTime CreatedAt { get; set; }
		public Guid? CreatedBy { get; set; }  // Peut être null si l'utilisateur est supprimé
		public byte? Note { get; set; }

		public User? User { get; set; }  // Relation avec User
		public Cocktail? Cocktail { get; set; }  // Relation avec Cocktail
	}
}
