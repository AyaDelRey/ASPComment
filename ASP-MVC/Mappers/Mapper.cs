using ASP_MVC.Models.Cocktail;
using ASP_MVC.Models.Comment;
using ASP_MVC.Models.User;
using BLL.Entities;

namespace ASP_MVC.Mappers
{
    internal static class Mapper
    {
        #region Users
        public static UserListItem ToListItem(this User user)
        {
            if (user is null) throw new ArgumentNullException(nameof(user));
            return new UserListItem()
            {
                User_Id = user.User_Id,
                First_Name = user.First_Name,
                Last_Name = user.Last_Name
            };
        }

        public static UserDetails ToDetails(this User user)
        {
            if (user is null) throw new ArgumentNullException(nameof(user));
            return new UserDetails()
            {
                User_Id = user.User_Id,
                First_Name = user.First_Name,
                Last_Name = user.Last_Name,
                Email = user.Email,
                CreatedAt = DateOnly.FromDateTime(user.CreatedAt),
                Cocktails = user.Cocktails.Select(bll => bll.ToListItem())
            };
        }

        public static User ToBLL(this UserCreateForm user)
        {
            if (user is null) throw new ArgumentNullException(nameof(user));
            return new User(
                Guid.Empty,
                user.First_Name,
                user.Last_Name,
                user.Email,
                user.Password,
                DateTime.Now,
                null,
                "User"
                );
            /*return new User(
                user.First_Name,
                user.Last_Name,
                user.Email,
                user.Password);*/
        }

        public static UserEditForm ToEditForm(this User user)
        {
            if (user is null) throw new ArgumentNullException(nameof(user));
            return new UserEditForm()
            {
                First_Name = user.First_Name,
                Last_Name = user.Last_Name,
                Email = user.Email
            };
        }

        public static User ToBLL(this UserEditForm user)
        {
            if (user is null) throw new ArgumentNullException(nameof(user));
            /*return new User(
                Guid.Empty,
                user.First_Name,
                user.Last_Name,
                user.Email,
                "********",
                DateTime.Now,
                null,
                "User"
                );*/
            return new User(
                user.First_Name,
                user.Last_Name,
                user.Email);
        }

        public static UserDelete ToDelete(this User user)
        {
            if (user is null) throw new ArgumentNullException(nameof(user));
            return new UserDelete()
            {
                First_Name = user.First_Name,
                Last_Name = user.Last_Name,
                Email = user.Email
            };
        }
        #endregion
        #region Cocktails
        public static CocktailListItem ToListItem(this Cocktail cocktail)
        {
            if (cocktail is null) throw new ArgumentNullException(nameof(cocktail));
            return new CocktailListItem()
            {
                Cocktail_Id = cocktail.Cocktail_Id,
                Name = cocktail.Name,
                Description = cocktail.Description
            };
        }

        public static CocktailDetails ToDetails(this Cocktail cocktail)
        {
            if (cocktail is null) throw new ArgumentNullException(nameof(cocktail));
            return new CocktailDetails()
            {
                Cocktail_Id = cocktail.Cocktail_Id,
                Name = cocktail.Name,
                Description = cocktail.Description,
                Instructions = cocktail.Instructions,
                CreatedAt = cocktail.CreatedAt,
                Creator = (cocktail.Creator is null) ? null : $"{cocktail.Creator.First_Name} {cocktail.Creator.Last_Name}",
                CreatedBy = cocktail.CreatedBy
            };
        }

        public static Cocktail ToBLL(this CocktailCreateForm cocktail)
        {
            if (cocktail is null) throw new ArgumentNullException(nameof(cocktail));
            return new Cocktail(
                Guid.Empty,
                cocktail.Name,
                cocktail.Description,
                cocktail.Instructions,
                DateOnly.FromDateTime(DateTime.Now),
                cocktail.CreatedBy
                );
        }

        public static CocktailEditForm ToEditForm(this Cocktail cocktail)
        {
            if (cocktail is null) throw new ArgumentNullException(nameof(cocktail));
            return new CocktailEditForm()
            {
                Name = cocktail.Name,
                Description = cocktail.Description,
                Instructions= cocktail.Instructions
            };
        }

        public static Cocktail ToBLL(this CocktailEditForm cocktail)
        {
            if (cocktail is null) throw new ArgumentNullException(nameof(cocktail));
            return new Cocktail(
                Guid.Empty,
                cocktail.Name,
                cocktail.Description,
                cocktail.Instructions,
                DateOnly.FromDateTime(DateTime.Now),
                Guid.Empty
                );
        }

        public static CocktailDelete ToDelete(this Cocktail cocktail)
        {
            if (cocktail is null) throw new ArgumentNullException(nameof(cocktail));
            return new CocktailDelete()
            {
                Name= cocktail.Name,
                Description= cocktail.Description,
                CreatedBy = cocktail.CreatedBy
            };
        }
		#endregion





		#region Comments


		// Convertir un Comment en CommentDetails (pour afficher les détails d'un commentaire)
		public static CommentDetails ToDetails(this Comment comment)
		{
			if (comment is null) throw new ArgumentNullException(nameof(comment));
			return new CommentDetails()
			{
				Comment_Id = comment.Comment_Id,
				Content = comment.Content,
				CreatedBy = $"{comment.CreatedBy} {comment.CreatedBy}", // Nom complet de l'auteur
				CreatedAt = comment.CreatedAt,
			};
		}
		// Convertir un CommentCreateForm en Comment BLL (pour la création d'un commentaire)
		public static Comment ToBLL(this CommentCreateForm form)
		{
			if (form is null) throw new ArgumentNullException(nameof(form));
			return new Comment(
				Guid.Empty, // Utiliser Guid.Empty ou générer un GUID si nécessaire
				form.Content,
				form.CreatedBy,
				DateTime.Now
			);
		}

		// Convertir un CommentEditForm en Comment BLL (pour la modification d'un commentaire)
		public static Comment ToBLL(this CommentEditForm form)
		{
			if (form is null) throw new ArgumentNullException(nameof(form));
			return new Comment(
				form.Comment_Id, // Assurez-vous que Comment_Id est bien passé pour l'édition
				form.Content,
				form.CreatedBy, // Garder le CreatedBy tel quel
				DateTime.Now // Mettre à jour la date de modification
			);
		}

		// Convertir un CommentDelete en Comment BLL (pour la suppression d'un commentaire)
		public static CommentDelete ToDelete(this Comment comment)
		{
			if (comment is null) throw new ArgumentNullException(nameof(comment));
			return new CommentDelete()
			{
				CommentId = comment.Comment_Id, // L'ID du commentaire à supprimer
                Content = comment.Content, // Le contenu du commentaire
                CreatedAt = comment.CreatedAt, // La date de création du commentaire
				CreatedBy = comment.CreatedBy // L'auteur du commentaire
			};
		}

		public static CommentListItem ToListItem(this Comment comment)
		{
			if (comment is null) throw new ArgumentNullException(nameof(comment));
			return new CommentListItem()
			{
				Comment_Id = comment.Comment_Id,
				Content = comment.Content,
				CreatedBy = $"{comment.CreatedBy}", // Si tu veux afficher le nom complet de l'auteur
				CreatedAt = comment.CreatedAt
			};
		}

		#endregion
	}
}
