using System;
using System.Collections.Generic;

namespace Common.Repositories
{
	public interface ICommentRepository<TComment> : ICRUDRepository<TComment, Guid>
	{
		IEnumerable<TComment> GetByUserId(Guid userId);
		IEnumerable<TComment> GetByCocktailId(Guid cocktailId);
	}
}

