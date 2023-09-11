﻿using Meal_Planner_Api.Data;
using Meal_Planner_Api.Interfaces;
using Meal_Planner_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Meal_Planner_Api.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private readonly DataContext _context;

        public RatingRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateRating(Rating rating)
        {
            _context.SaveChanges();
            return Save();
        }

        public Rating GetRating(int id)
        {
            return _context.Ratings.FirstOrDefault(x => x.Id == id);
        }

        public ICollection<Rating> GetRatings()
        {
            return _context.Ratings.OrderBy(x => x.Id).ToList();
        }

        public ICollection<Rating> GetRatingsForRecipe(int recipeId)
        {
            var recipe = _context.Recipes.Include(x => x.RecipeRating)
                .ThenInclude(x => x.Rating)
                .FirstOrDefault(x => x.Id == recipeId);

            var rating = recipe.RecipeRating.Select(x => x.Rating).Where(x => x != null).ToList();

            return rating;


        }

        public ICollection<Rating> GetRatingsForUser(int userId)
        {
            // returns all the ratings the user have given

            var user = _context.Users.Include(x => x.UserRating)
                .ThenInclude(x => x.Rating)
                .FirstOrDefault(x => x.Id ==  userId);

            if (user == null)
                return null;

            var rating = user.UserRating.Select(x => x.Rating)
                .Where(x => x != null).ToList();

            return rating;
        }

        public Rating GetRecipeUserRating(int userID, int recipeId)
        {
            // get the rating a user made on a recipe

            var userRatings = GetRatingsForUser(userID);

            // find the recipe inside the userRatings

            

            //TODO: After implementing user and rating binding, and the GetRatingsForUser method, complete this method.
            throw new NotImplementedException();

        }

        public bool ratingExists(int id)
        {
            return _context.Ratings.Any(x => x.Id == id);
        }

        public bool recipeRatingsExists(int recipeId)
        {
            var recipe = _context.Recipes.Include(x => x.RecipeRating)
                .ThenInclude(x => x.Rating)
                .FirstOrDefault(x => x.Id == recipeId);

            return recipe.RecipeRating.Any();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
