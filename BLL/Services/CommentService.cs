using BLL.Entities;
using BLL.Mappers;
using Common.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CommentService : ICommentRepository<Comment>
    {
        private ICommentRepository<DAL.Entities.Comment> _commentRepository;
        private IUserRepository<DAL.Entities.User> _userService;

        public CommentService(
            ICommentRepository<DAL.Entities.Comment> commentRepository,
            IUserRepository<DAL.Entities.User> userService
            )
        {
            _commentRepository = commentRepository;
            _userService = userService;
        }

        public void Delete(Guid comment_id)
        {
            _commentRepository.Delete(comment_id);
        }

        public IEnumerable<Comment> Get()
        {
            IEnumerable<Comment> comments = _commentRepository.Get().Select(dal => dal.ToBLL());
            foreach (var comment in comments)
            {
                // Associer l'utilisateur qui a créé le commentaire
                comment.Creator = _userService.Get(comment.CreatedBy).ToBLL();
            }
            return comments;
        }

		public Comment Get(Guid comment_id)
		{
			var comment = _commentRepository.Get(comment_id).ToBLL();

			// Tenter de récupérer l'utilisateur associé au commentaire
			var creator = _userService.Get(comment.CreatedBy);

			// Si l'utilisateur existe, on l'associe au commentaire, sinon on laisse Creator null
			if (creator != null)
			{
				comment.Creator = creator.ToBLL();
			}

			return comment;
		}


		public IEnumerable<Comment> GetByCocktailId(Guid cocktail_id)
        {
            return _commentRepository.GetByCocktailId(cocktail_id).Select(dal => dal.ToBLL());
        }

        public Guid Insert(Comment comment)
        {
            return _commentRepository.Insert(comment.ToDAL());
        }

        public void Update(Guid comment_id, Comment comment)
        {
            _commentRepository.Update(comment_id, comment.ToDAL());
        }

        public IEnumerable<Comment> GetByUserId(Guid user_id)
        {
            return _commentRepository.GetByUserId(user_id).Select(dal => dal.ToBLL());
        }
    }
}
