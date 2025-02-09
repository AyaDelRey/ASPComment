using ASP_MVC.Handlers;
using ASP_MVC.Handlers.ActionFilters;
using ASP_MVC.Mappers;
using ASP_MVC.Models.Cocktail;
using ASP_MVC.Models.Comment;
using BLL.Entities;
using Common.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASP_MVC.Controllers
{
    public class CocktailController : Controller
    {
        private ICocktailRepository<Cocktail> _cocktailRepository;
		private ICommentRepository<Comment> _commentRepository; // Ajout du repository des commentaires
		private SessionManager _sessionManager;

        public CocktailController(
            ICocktailRepository<Cocktail> cocktailRepository,
            SessionManager sessionManager,
			ICommentRepository<Comment> commentRepository// Injection du repository des commentaires
			)
        {
            _cocktailRepository = cocktailRepository;
            _sessionManager = sessionManager;
			_commentRepository = commentRepository;// Initialisation du repository des commentaires

		}

		// GET: CocktailController
		public ActionResult Index()
        {
            try
            {
                IEnumerable<CocktailListItem> model = _cocktailRepository.Get().Select(bll => bll.ToListItem());
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }

		[HttpGet]
		// GET: CommentController/Create
		[ConnectionNeeded] // Cette ligne force la connexion pour accéder à cette action
		public ActionResult CreateComment(Guid cocktailId)
		{
			var model = new CommentCreateForm { CocktailId = cocktailId };
			return PartialView("_CommentCreateForm", model); // Vue partielle pour le formulaire
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[ConnectionNeeded]
		public ActionResult CreateComment(CommentCreateForm form)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return PartialView("_Comments", form); // Réafficher le formulaire avec les erreurs
				}
				// Appel à CommentRepository pour insérer le commentaire
				_commentRepository.Insert(form.ToBLL());

				// Rediriger vers la page de détails du cocktail avec les commentaires mis à jour
				return RedirectToAction("Details", "Cocktail", new { id = form.CocktailId });
			}
			catch (Exception ex)
			{
				// En cas d'erreur, retourne la vue partielle avec l'erreur
				return PartialView("_Comments", form);
			}
		}



		// GET: CocktailController/Details/5
		public ActionResult Details(Guid id)
        {
            try
            {
				// Récupère les détails du cocktail
				CocktailDetails cocktailDetails = _cocktailRepository.Get(id).ToDetails();

				// Récupère les commentaires associés au cocktail
				var comments = _commentRepository.GetByCocktailId(id) // Utilise GetByCocktailId pour récupérer les commentaires
					.Select(c => c.ToListItem()); // Mapper les commentaires en CommentListItem

				// Ajoute les commentaires à ton modèle de cocktail
				cocktailDetails.Comments = comments;

				// Enregistre le cocktail comme visité
				_sessionManager.AddVisitedCocktail(cocktailDetails.Cocktail_Id, cocktailDetails.Name);

				return View(cocktailDetails); // Passe les détails du cocktail et les commentaires à la vue
			}
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: CocktailController/Create
        [ConnectionNeeded]
        public ActionResult Create()
        {
            return View();
        }

        // POST: CocktailController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ConnectionNeeded]
        public ActionResult Create(CocktailCreateForm form)
        {
            try
            {
                if (!ModelState.IsValid) throw new ArgumentException(nameof(form));
                Guid id = _cocktailRepository.Insert(form.ToBLL());
                return RedirectToAction(nameof(Details), new { id });
            }
            catch
            {
                return View();
            }
        }

        // GET: CocktailController/Edit/5
        //[ConnectionNeeded("Details", "Cocktail", true)]
        [IsCreator]
        public ActionResult Edit(Guid id)
        {
            try
        {
                /* Si nous devions vérifier si l'utilisateur connecté est le créateur, nous devrions passez par ces instructions
                 * Mais il est préférable de le définir dans un attribut IAuthorizationFilter
                Cocktail cocktail = _cocktailRepository.Get(id);
                if (!(_sessionManager.ConnectedUser?.User_Id == cocktail.CreatedBy)) throw new InvalidOperationException("Vous n'êtes pas l'auteur de ce cocktail!");
                CocktailEditForm model = cocktail.ToEditForm();*/
                CocktailEditForm model = _cocktailRepository.Get(id).ToEditForm();
                return View(model);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: CocktailController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[ConnectionNeeded]
        [IsCreator]
        public ActionResult Edit(Guid id, CocktailEditForm form)
        {
            try
            {
                if (!ModelState.IsValid) throw new ArgumentException(nameof(form));
                _cocktailRepository.Update(id, form.ToBLL());
                return RedirectToAction(nameof(Details), new { id });
            }
            catch
            {
                return View();
            }
        }

        // GET: CocktailController/Delete/5
        //[ConnectionNeeded]
        [IsCreator]
        public ActionResult Delete(Guid id)
        {
            try
            {
                CocktailDelete model = _cocktailRepository.Get(id).ToDelete();
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: CocktailController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[ConnectionNeeded]
        [IsCreator]
        public ActionResult Delete(Guid id, CocktailDelete form)
        {
            try
            {
                _cocktailRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
