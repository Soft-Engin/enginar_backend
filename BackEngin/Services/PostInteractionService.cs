﻿using DataAccess.Repositories;
using Models.InteractionModels;
using Models;
using BackEngin.Services.Interfaces;
using Models.DTO;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Microsoft.IdentityModel.Tokens;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BackEngin.Services
{
    public class PostInteractionService : IPostInteractionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PostInteractionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Toggle Like for Blog
        public async Task<bool> ToggleLikeBlog(string userId, int blogId)
        {
            var existingLike = await _unitOfWork.Blog_Likes.FindAsync(l => l.UserId == userId && l.BlogId == blogId);

            if (existingLike.Any())
            {
                // Unlike if it already exists
                _unitOfWork.Blog_Likes.Delete(existingLike.First());
                await _unitOfWork.CompleteAsync();
                return false; // Unlike
            }
            else
            {
                // Like if it doesn't exist
                var blogLike = new Blog_Likes
                {
                    UserId = userId,
                    BlogId = blogId,
                    CreatedAt = DateTime.UtcNow
                };
                await _unitOfWork.Blog_Likes.AddAsync(blogLike);
                await _unitOfWork.CompleteAsync();
                return true; // Like
            }
        }

        // Toggle Like for Recipe
        public async Task<bool> ToggleLikeRecipe(string userId, int recipeId)
        {
            var existingLike = await _unitOfWork.Recipe_Likes.FindAsync(l => l.UserId == userId && l.RecipeId == recipeId);

            if (existingLike.Any())
            {
                // Unlike if it already exists
                _unitOfWork.Recipe_Likes.Delete(existingLike.First());
                await _unitOfWork.CompleteAsync();
                return false; // Unlike
            }
            else
            {
                // Like if it doesn't exist
                var recipeLike = new Recipe_Likes
                {
                    UserId = userId,
                    RecipeId = recipeId,
                    CreatedAt = DateTime.UtcNow
                };
                await _unitOfWork.Recipe_Likes.AddAsync(recipeLike);
                await _unitOfWork.CompleteAsync();
                return true; // Like
            }
        }

        // Toggle Like for Event
        public async Task<bool> ToggleLikeEvent(string userId, int eventId)
        {
            var existingLike = await _unitOfWork.Event_Likes.FindAsync(l => l.UserId == userId && l.EventId == eventId);

            if (existingLike.Any())
            {
                // Unlike if it already exists
                _unitOfWork.Event_Likes.Delete(existingLike.First());
                await _unitOfWork.CompleteAsync();
                return false; // Unlike
            }
            else
            {
                // Like if it doesn't exist
                var eventLike = new Event_Likes
                {
                    UserId = userId,
                    EventId = eventId,
                    CreatedAt = DateTime.UtcNow
                };
                await _unitOfWork.Event_Likes.AddAsync(eventLike);
                await _unitOfWork.CompleteAsync();
                return true; // Like
            }
        }

        // Toggle Bookmark for Blog
        public async Task<bool> ToggleBookmarkBlog(string userId, int blogId)
        {
            var existingBookmark = await _unitOfWork.Blog_Bookmarks.FindAsync(b => b.UserId == userId && b.BlogId == blogId);

            if (existingBookmark.Any())
            {
                // Remove bookmark if it exists
                _unitOfWork.Blog_Bookmarks.Delete(existingBookmark.First());
                await _unitOfWork.CompleteAsync();
                return false; // Unbookmark
            }
            else
            {
                // Add bookmark if it doesn't exist
                var bookmark = new Blog_Bookmarks
                {
                    UserId = userId,
                    BlogId = blogId,
                    CreatedAt = DateTime.UtcNow
                };
                await _unitOfWork.Blog_Bookmarks.AddAsync(bookmark);
                await _unitOfWork.CompleteAsync();
                return true; // bookmark
            }
        }

        // Toggle Bookmark for Recipe
        public async Task<bool> ToggleBookmarkRecipe(string userId, int recipeId)
        {
            var existingBookmark = await _unitOfWork.Recipe_Bookmarks.FindAsync(b => b.UserId == userId && b.RecipeId == recipeId);

            if (existingBookmark.Any())
            {
                // Remove bookmark if it exists
                _unitOfWork.Recipe_Bookmarks.Delete(existingBookmark.First());
                await _unitOfWork.CompleteAsync();
                return false; // Unbookmark
            }
            else
            {
                // Add bookmark if it doesn't exist
                var bookmark = new Recipe_Bookmarks
                {
                    UserId = userId,
                    RecipeId = recipeId,
                    CreatedAt = DateTime.UtcNow
                };
                await _unitOfWork.Recipe_Bookmarks.AddAsync(bookmark);
                await _unitOfWork.CompleteAsync();
                return true; // bookmark
            }
        }

        // Toggle Bookmark for Event
        public async Task<bool> ToggleBookmarkEvent(string userId, int eventId)
        {
            var existingBookmark = await _unitOfWork.Event_Bookmarks.FindAsync(b => b.UserId == userId && b.EventId == eventId);

            if (existingBookmark.Any())
            {
                // Remove bookmark if it exists
                _unitOfWork.Event_Bookmarks.Delete(existingBookmark.First());
                await _unitOfWork.CompleteAsync();
                return false; // Unbookmark
            }
            else
            {
                // Add bookmark if it doesn't exist
                var bookmark = new Event_Bookmarks
                {
                    UserId = userId,
                    EventId = eventId,
                    CreatedAt = DateTime.UtcNow
                };
                await _unitOfWork.Event_Bookmarks.AddAsync(bookmark);
                await _unitOfWork.CompleteAsync();
                return true; // bookmark
            }
        }

        // Comment on Blog
        public async Task<CommentDTO> CommentOnBlog(string userId, int blogId, CommentRequestDTO commentRequest)
        {
            if (commentRequest.Images.IsNullOrEmpty() && commentRequest.Text.IsNullOrEmpty())
            {
                throw new ArgumentException("The comment must have text or image");
            }

            if (!(await _unitOfWork.Blogs.FindAsync(b => b.Id == blogId)).Any())
            {
                throw new Exception("Blog with the provided blogId does not exist");
            }

            var comment = new Blog_Comments
            {
                UserId = userId,
                BlogId = blogId,
                CommentText = commentRequest.Text,
                Images = commentRequest.Images,
                ImagesCount = commentRequest.Images?.Length ?? 0,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.Blog_Comments.AddAsync(comment);
            await _unitOfWork.CompleteAsync();
            var user = await _unitOfWork.Users.FindAsync(u => u.Id == userId);


            return new CommentDTO
            {
                Id = comment.Id,
                Object_id = comment.BlogId,
                Text = comment.CommentText,
                ImagesCount = comment.ImagesCount,
                Timestamp = comment.CreatedAt,
                UserId = userId,
                UserName = user.FirstOrDefault()?.UserName ?? "Unknown",
            };
        }

        // Comment on Recipe
        public async Task<CommentDTO> CommentOnRecipe(string userId, int recipeId, CommentRequestDTO commentRequest)
        {
            if (commentRequest.Images.IsNullOrEmpty() && commentRequest.Text.IsNullOrEmpty())
            {
                throw new ArgumentException("The comment must have text or image");
            }

            if (!(await _unitOfWork.Recipes.FindAsync(r => r.Id == recipeId)).Any())
            {
                throw new Exception("Recipe with the provided recipeId does not exists");
            }


            var comment = new Recipe_Comments
            {
                UserId = userId,
                RecipeId = recipeId,
                CommentText = commentRequest.Text,
                Images = commentRequest.Images,
                ImagesCount = commentRequest.Images?.Length ?? 0,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.Recipe_Comments.AddAsync(comment);
            await _unitOfWork.CompleteAsync();

            var user = await _unitOfWork.Users.FindAsync(u => u.Id == userId);

            return new CommentDTO
            {
                Id = comment.Id,
                Object_id = comment.RecipeId,
                Text = comment.CommentText,
                ImagesCount = comment.ImagesCount,
                Timestamp = comment.CreatedAt,
                UserId = userId,
                UserName = user.FirstOrDefault()?.UserName ?? "Unknown",
            };
        }

        // Comment on Event
        public async Task<CommentDTO> CommentOnEvent(string userId, int eventId, CommentRequestDTO commentRequest)
        {
            if (commentRequest.Images.IsNullOrEmpty() && commentRequest.Text.IsNullOrEmpty())
            {
                throw new ArgumentException("The comment must have text or image");
            }

            if (!(await _unitOfWork.Events.FindAsync(b => b.Id == eventId)).Any())
            {
                throw new Exception("Event with the provided eventId does not exist");
            }

            var comment = new Event_Comments
            {
                UserId = userId,
                EventId = eventId,
                CommentText = commentRequest.Text,
                Images = commentRequest.Images,
                ImagesCount = commentRequest.Images?.Length ?? 0,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.Event_Comments.AddAsync(comment);
            await _unitOfWork.CompleteAsync();
            var user = await _unitOfWork.Users.FindAsync(u => u.Id == userId);


            return new CommentDTO
            {
                Id = comment.Id,
                Object_id = comment.EventId,
                Text = comment.CommentText,
                ImagesCount = comment.ImagesCount,
                Timestamp = comment.CreatedAt,
                UserId = userId,
                UserName = user.FirstOrDefault()?.UserName ?? "Unknown",
            };
        }

        // Update Blog Comment
        public async Task<CommentDTO> UpdateBlogComment(string userId, int commentId, CommentRequestDTO commentRequest)
        {
            var comment = await _unitOfWork.Blog_Comments.GetByIdAsync(commentId);
            if (comment == null || comment.UserId != userId)
                throw new UnauthorizedAccessException("You cannot update this comment.");

            if (commentRequest.Images.IsNullOrEmpty() && commentRequest.Text.IsNullOrEmpty())
            {
                throw new ArgumentException("The comment must have text or image");
            }

            comment.CommentText = commentRequest.Text;
            comment.CreatedAt = DateTime.UtcNow;
            comment.Images = commentRequest.Images;
            comment.ImagesCount = commentRequest.Images?.Length ?? 0;

            _unitOfWork.Blog_Comments.Update(comment);
            await _unitOfWork.CompleteAsync();

            var user = await _unitOfWork.Users.FindAsync(u => u.Id == userId);

            return new CommentDTO
            {
                Id = comment.Id,
                Text = comment.CommentText,
                ImagesCount = comment.ImagesCount,
                Object_id = comment.BlogId,
                Timestamp = comment.CreatedAt,
                UserId = userId,
                UserName = user.FirstOrDefault()?.UserName ?? "Unknown",
            };
        }

        // Update Recipe Comment
        public async Task<CommentDTO> UpdateRecipeComment(string userId, int commentId, CommentRequestDTO commentRequest)
        {
            var comment = await _unitOfWork.Recipe_Comments.GetByIdAsync(commentId);
            if (comment == null || comment.UserId != userId)
                throw new UnauthorizedAccessException("You cannot update this comment.");

            if (commentRequest.Images.IsNullOrEmpty() && commentRequest.Text.IsNullOrEmpty())
            {
                throw new ArgumentException("The comment must have text or image");
            }

            comment.CommentText = commentRequest.Text;
            comment.CreatedAt = DateTime.UtcNow;
            comment.Images = commentRequest.Images;
            comment.ImagesCount = commentRequest.Images?.Length ?? 0;

            _unitOfWork.Recipe_Comments.Update(comment);
            await _unitOfWork.CompleteAsync();

            var user = await _unitOfWork.Users.FindAsync(u => u.Id == userId);

            return new CommentDTO
            {
                Id = comment.Id,
                Text = comment.CommentText,
                ImagesCount = comment.ImagesCount,
                Object_id = comment.RecipeId,
                Timestamp = comment.CreatedAt,
                UserId = userId,
                UserName = user.FirstOrDefault()?.UserName ?? "Unknown",
            };
        }


        // Update Event Comment
        public async Task<CommentDTO> UpdateEventComment(string userId, int commentId, CommentRequestDTO commentRequest)
        {
            var comment = await _unitOfWork.Event_Comments.GetByIdAsync(commentId);
            if (comment == null || comment.UserId != userId)
                throw new UnauthorizedAccessException("You cannot update this comment.");

            comment.CommentText = commentRequest.Text;
            comment.CreatedAt = DateTime.UtcNow;
            comment.Images = commentRequest.Images;

            _unitOfWork.Event_Comments.Update(comment);
            await _unitOfWork.CompleteAsync();

            return new CommentDTO
            {
                Id = comment.Id,
                Object_id = comment.EventId,
                Text = comment.CommentText,
                ImagesCount = comment.Images?.Length ?? 0,
                UserId = comment.UserId,
                Timestamp = comment.CreatedAt
            };
        }


        // Delete Blog Comment
        public async Task DeleteBlogComment(string userId, int commentId)
        {
            var comment = await _unitOfWork.Blog_Comments.GetByIdAsync(commentId);
            if (comment == null)
                throw new UnauthorizedAccessException("You cannot delete this comment.");

            _unitOfWork.Blog_Comments.Delete(comment);
            await _unitOfWork.CompleteAsync();
        }

        // Delete Recipe Comment
        public async Task DeleteRecipeComment(string userId, int commentId)
        {
            var comment = await _unitOfWork.Recipe_Comments.GetByIdAsync(commentId);
            if (comment == null)
                throw new UnauthorizedAccessException("You cannot delete this comment.");

            _unitOfWork.Recipe_Comments.Delete(comment);
            await _unitOfWork.CompleteAsync();
        }

        // Delete Event Comment
        public async Task DeleteEventComment(string userId, int commentId)
        {
            var comment = await _unitOfWork.Event_Comments.GetByIdAsync(commentId);
            if (comment == null)
                throw new UnauthorizedAccessException("You cannot delete this comment.");

            _unitOfWork.Event_Comments.Delete(comment);
            await _unitOfWork.CompleteAsync();
        }

        // Check if the user has liked a blog
        public async Task<bool> IsBlogLiked(string userId, int blogId)
        {
            var existingLike = await _unitOfWork.Blog_Likes.FindAsync(l => l.UserId == userId && l.BlogId == blogId);
            return existingLike.Any();
        }

        // Check if the user has bookmarked a blog
        public async Task<bool> IsBlogBookmarked(string userId, int blogId)
        {
            var existingBookmark = await _unitOfWork.Blog_Bookmarks.FindAsync(b => b.UserId == userId && b.BlogId == blogId);
            return existingBookmark.Any();
        }

        // Check if the user has liked a recipe
        public async Task<bool> IsRecipeLiked(string userId, int recipeId)
        {
            var existingLike = await _unitOfWork.Recipe_Likes.FindAsync(l => l.UserId == userId && l.RecipeId == recipeId);
            return existingLike.Any();
        }

        // Check if the user has bookmarked a recipe
        public async Task<bool> IsRecipeBookmarked(string userId, int recipeId)
        {
            var existingBookmark = await _unitOfWork.Recipe_Bookmarks.FindAsync(b => b.UserId == userId && b.RecipeId == recipeId);
            return existingBookmark.Any();
        }

        // Check if the user has liked a event
        public async Task<bool> IsEventLiked(string userId, int eventId)
        {
            var existingLike = await _unitOfWork.Event_Likes.FindAsync(l => l.UserId == userId && l.EventId == eventId);
            return existingLike.Any();
        }

        // Check if the user has bookmarked a event
        public async Task<bool> IsEventBookmarked(string userId, int eventId)
        {
            var existingBookmark = await _unitOfWork.Event_Bookmarks.FindAsync(b => b.UserId == userId && b.EventId == eventId);
            return existingBookmark.Any();
        }

        // Get the number of likes for a blog
        public async Task<int> GetBlogLikeCount(int blogId)
        {
            return await _unitOfWork.Blog_Likes.CountAsync(l => l.BlogId == blogId);
        }

        // Get the number of bookmarks for a blog
        public async Task<int> GetBlogBookmarkCount(int blogId)
        {
            return await _unitOfWork.Blog_Bookmarks.CountAsync(b => b.BlogId == blogId);
        }

        // Get the number of likes for a recipe
        public async Task<int> GetRecipeLikeCount(int recipeId)
        {
            return await _unitOfWork.Recipe_Likes.CountAsync(l => l.RecipeId == recipeId);
        }

        // Get the number of bookmarks for a recipe
        public async Task<int> GetRecipeBookmarkCount(int recipeId)
        {
            return await _unitOfWork.Recipe_Bookmarks.CountAsync(b => b.RecipeId == recipeId);
        }

        // Get the number of likes for a event
        public async Task<int> GetEventLikeCount(int eventId)
        {
            return await _unitOfWork.Event_Likes.CountAsync(l => l.EventId == eventId);
        }

        // Get the number of bookmarks for a event
        public async Task<int> GetEventBookmarkCount(int eventId)
        {
            return await _unitOfWork.Event_Bookmarks.CountAsync(b => b.EventId == eventId);
        }

        // Get paginated comments for a blog
        public async Task<PaginatedResponseDTO<CommentDTO>> GetBlogComments(int blogId, int pageNumber, int pageSize)
        {
            var (comments, totalCount) = await _unitOfWork.Blog_Comments.GetPaginatedAsync(
                filter: c => c.BlogId == blogId, // Filter by blog ID
                pageNumber: pageNumber,
                pageSize: pageSize
            );

            // finds user names of the recipe users
            var userIds = comments.Select(b => b.UserId).Distinct().ToList();
            var users = await _unitOfWork.Users.FindAsync(u => userIds.Contains(u.Id));
            var userDictionary = users.ToDictionary(u => u.Id, u => u.UserName);

            var commentDtos = comments.Select(c => new CommentDTO
            {
                Id = c.Id,
                Object_id = c.BlogId,
                Text = c.CommentText,
                ImagesCount = c.ImagesCount,
                Timestamp = c.CreatedAt,
                UserId = c.UserId,
                UserName = userDictionary.ContainsKey(c.UserId) ? userDictionary[c.UserId] : "Unknown",
            }).ToList();

            return new PaginatedResponseDTO<CommentDTO>
            {
                Items = commentDtos,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        // Get paginated comments for a recipe
        public async Task<PaginatedResponseDTO<CommentDTO>> GetRecipeComments(int recipeId, int pageNumber, int pageSize)
        {
            var (comments, totalCount) = await _unitOfWork.Recipe_Comments.GetPaginatedAsync(
                filter: c => c.RecipeId == recipeId, // Filter by recipe ID
                pageNumber: pageNumber,
                pageSize: pageSize
            );

            // finds user names of the recipe users
            var userIds = comments.Select(b => b.UserId).Distinct().ToList();
            var users = await _unitOfWork.Users.FindAsync(u => userIds.Contains(u.Id));
            var userDictionary = users.ToDictionary(u => u.Id, u => u.UserName);

            var commentDtos = comments.Select(c => new CommentDTO
            {
                Id = c.Id,
                Object_id = c.RecipeId,
                Text = c.CommentText,
                ImagesCount = c.ImagesCount,
                Timestamp = c.CreatedAt,
                UserId = c.UserId,
                UserName = userDictionary.ContainsKey(c.UserId) ? userDictionary[c.UserId] : "Unknown",
            }).ToList();

            return new PaginatedResponseDTO<CommentDTO>
            {
                Items = commentDtos,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        // Get paginated comments for a event
        public async Task<PaginatedResponseDTO<CommentDTO>> GetEventComments(int eventId, int pageNumber, int pageSize)
        {
            var (comments, totalCount) = await _unitOfWork.Event_Comments.GetPaginatedAsync(
                filter: c => c.EventId == eventId, // Filter by event ID
                pageNumber: pageNumber,
                pageSize: pageSize
            );

            // finds user names of the event users
            var userIds = comments.Select(b => b.UserId).Distinct().ToList();
            var users = await _unitOfWork.Users.FindAsync(u => userIds.Contains(u.Id));
            var userDictionary = users.ToDictionary(u => u.Id, u => u.UserName);

            var commentDtos = comments.Select(c => new CommentDTO
            {
                Id = c.Id,
                Object_id = c.EventId,
                Text = c.CommentText,
                ImagesCount = c.ImagesCount,
                Timestamp = c.CreatedAt,
                UserId = c.UserId,
                UserName = userDictionary.ContainsKey(c.UserId) ? userDictionary[c.UserId] : "Unknown",
            }).ToList();

            return new PaginatedResponseDTO<CommentDTO>
            {
                Items = commentDtos,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<byte[]?> GetBlogCommentImage(int commentId, int imageIndex)
        {
            var comment = await _unitOfWork.Blog_Comments.GetByIdAsync(commentId);
            if (comment == null || comment.Images == null || imageIndex < 0 || imageIndex >= comment.ImagesCount)
                return null;

            return comment.Images[imageIndex];
        }

        public async Task<byte[]?> GetRecipeCommentImage(int commentId, int imageIndex)
        {
            var comment = await _unitOfWork.Recipe_Comments.GetByIdAsync(commentId);
            if (comment == null || comment.Images == null || imageIndex < 0 || imageIndex >= comment.ImagesCount)
                return null;

            return comment.Images[imageIndex];
        }

        public async Task<byte[]?> GetEventCommentImage(int commentId, int imageIndex)
        {
            var comment = await _unitOfWork.Event_Comments.GetByIdAsync(commentId);
            if (comment == null || comment.Images == null || imageIndex < 0 || imageIndex >= comment.ImagesCount)
                return null;

            return comment.Images[imageIndex];
        }

        public async Task<string> GetOwnerId(int objId, ObjectType type)
        {
            switch (type)
            {
                case ObjectType.Blog:
                    var obj = await _unitOfWork.Blogs.GetByIdAsync(objId);
                    if (obj == null)
                        throw new Exception("Blog with the provided blogId does not exist");
                    return GetOwnerId(obj);
                case ObjectType.Recipe:
                    var obj1 = await _unitOfWork.Recipes.GetByIdAsync(objId);
                    if (obj1 == null)
                        throw new Exception("Recipe with the provided recipeId does not exist");
                    return GetOwnerId(obj1);
                case ObjectType.Event:
                    var obj2 = await _unitOfWork.Events.GetByIdAsync(objId);
                    if (obj2 == null)
                        throw new Exception("Event with the provided eventId does not exist");
                    return GetOwnerId(obj2);
                case ObjectType.BlogComment:
                    var obj3 = await _unitOfWork.Blog_Comments.GetByIdAsync(objId);
                    if (obj3 == null)
                        throw new Exception("Blog comment with the provided commentId does not exist");
                    return GetOwnerId(obj3);
                case ObjectType.RecipeComment:
                    var obj4 = await _unitOfWork.Recipe_Comments.GetByIdAsync(objId);
                    if (obj4 == null)
                        throw new Exception("Recipe comment with the provided commentId does not exist");
                    return GetOwnerId(obj4);
                case ObjectType.EventComment:
                    var obj5 = await _unitOfWork.Event_Comments.GetByIdAsync(objId);
                    if (obj5 == null)
                        throw new Exception("Event comment with the provided commentId does not exist");
                    return GetOwnerId(obj5);
                default:
                    throw new Exception("Unsupported object type in GetOwnerId");

            }
        }

        public string GetOwnerId(Events obj)
        {
            return obj.CreatorId;
        }
        public string GetOwnerId(Blogs obj)
        {
            return obj.UserId;
        }
        public string GetOwnerId(Recipes obj)
        {
            return obj.UserId;
        }
        public string GetOwnerId(Recipe_Comments obj)
        {
            return obj.UserId;
        }
        public string GetOwnerId(Blog_Comments obj)
        {
            return obj.UserId;

        }
        public string GetOwnerId(Event_Comments obj)
        {
            return obj.UserId;
        }


    }
    public enum ObjectType
    {
        Blog,
        Recipe,
        Event,
        BlogComment,
        RecipeComment,
        EventComment
    }

}
