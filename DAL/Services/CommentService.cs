using Common.Repositories;
using DAL.Entities;
using DAL.Mappers;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;

namespace DAL.Services
{
	public class CommentService : BaseService, ICommentRepository<Comment>
	{
		public CommentService(IConfiguration config) : base(config, "Main-DB") { }

		public IEnumerable<Comment> Get()
		{
			using (SqlConnection connection = new SqlConnection(_connectionString))
			using (SqlCommand command = connection.CreateCommand())
			{
				command.CommandText = "SP_Comment_GetAll";
				command.CommandType = CommandType.StoredProcedure;
				connection.Open();
				using (SqlDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						yield return reader.ToComment();
					}
				}
			}
		}

		public Comment Get(Guid comment_id)
		{
			using (SqlConnection connection = new SqlConnection(_connectionString))
			using (SqlCommand command = connection.CreateCommand())
			{
				command.CommandText = "SP_Comment_GetById";
				command.CommandType = CommandType.StoredProcedure;
				command.Parameters.AddWithValue(nameof(comment_id), comment_id);
				connection.Open();
				using (SqlDataReader reader = command.ExecuteReader())
				{
					if (reader.Read())
					{
						return reader.ToComment();
					}
					else
					{
						throw new ArgumentOutOfRangeException(nameof(comment_id));
					}
				}
			}
		}

		public Guid Insert(Comment comment)
		{
			using (SqlConnection connection = new SqlConnection(_connectionString))
			using (SqlCommand command = connection.CreateCommand())
			{
				command.CommandText = "SP_Comment_Insert";
				command.CommandType = CommandType.StoredProcedure;
				command.Parameters.AddWithValue(nameof(Comment.CreatedBy), comment.CreatedBy);
				command.Parameters.AddWithValue(nameof(Comment.Concern), comment.Concern);
				command.Parameters.AddWithValue(nameof(Comment.Title), comment.Title);
				command.Parameters.AddWithValue(nameof(Comment.Content), comment.Content);
				command.Parameters.AddWithValue(nameof(Comment.Note), (object)comment.Note ?? DBNull.Value);
				connection.Open();
				return (Guid)command.ExecuteScalar();
			}
		}

		public void Update(Guid comment_id, Comment comment)
		{
			using (SqlConnection connection = new SqlConnection(_connectionString))
			using (SqlCommand command = connection.CreateCommand())
			{
				command.CommandText = "SP_Comment_Update";
				command.CommandType = CommandType.StoredProcedure;
				command.Parameters.AddWithValue(nameof(comment_id), comment_id);
				command.Parameters.AddWithValue(nameof(Comment.Title), comment.Title);
				command.Parameters.AddWithValue(nameof(Comment.Content), comment.Content);
				command.Parameters.AddWithValue(nameof(Comment.Note), (object)comment.Note ?? DBNull.Value);
				connection.Open();
				command.ExecuteNonQuery();
			}
		}

		public void Delete(Guid comment_id)
		{
			using (SqlConnection connection = new SqlConnection(_connectionString))
			using (SqlCommand command = connection.CreateCommand())
			{
				command.CommandText = "SP_Comment_Delete";
				command.CommandType = CommandType.StoredProcedure;
				command.Parameters.AddWithValue(nameof(comment_id), comment_id);
				connection.Open();
				command.ExecuteNonQuery();
			}
		}

		public IEnumerable<Comment> GetByUserId(Guid user_id)
		{
			using (SqlConnection connection = new SqlConnection(_connectionString))
			using (SqlCommand command = connection.CreateCommand())
			{
				command.CommandText = "SP_Comment_GetByUserId";
				command.CommandType = CommandType.StoredProcedure;
				command.Parameters.AddWithValue(nameof(user_id), user_id);
				connection.Open();
				using (SqlDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						yield return reader.ToComment();
					}
				}
			}
		}

		public IEnumerable<Comment> GetByCocktailId(Guid cocktail_id)
		{
			using (SqlConnection connection = new SqlConnection(_connectionString))
			using (SqlCommand command = connection.CreateCommand())
			{
				command.CommandText = "SP_Comment_GetByCocktailId";
				command.CommandType = CommandType.StoredProcedure;
				command.Parameters.AddWithValue(nameof(cocktail_id), cocktail_id);
				connection.Open();
				using (SqlDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						yield return reader.ToComment();
					}
				}
			}
		}
	}
}
