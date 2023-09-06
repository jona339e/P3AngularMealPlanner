﻿using Meal_Planner_Api.Data;
using Meal_Planner_Api.Interfaces;
using Meal_Planner_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Meal_Planner_Api.Repositories
{
    public class AmountRepository : IAmountRepository
    {
        private readonly DataContext _context;

        public AmountRepository(DataContext context)
        {
            _context = context;
        }
        public bool AmountExists(int id)
        {
            return _context.Amounts.Any(a => a.Id == id);
        }

        public Amount GetAmount(int id)
        {
            return _context.Amounts.FirstOrDefault(a => a.Id == id);
        }

        public ICollection<Amount> GetAmountsFromRecipe(int recipeId)
        {
            // first get all recipes
            // then include recipeIngredients
            // then include ingredients
            // then include amounts
            // then get the recipe that matches the id

            var recipe = _context.Recipes
                                    .Include(ri => ri.RecipeIngredients)
                                    .ThenInclude(i => i.Ingredient)
                                    .ThenInclude(a => a.Amount)
                                    .FirstOrDefault(r => r.Id == recipeId);


            // get all ingredients in the recipe
            // then find all amounts from ingredients

            var amounts = recipe.RecipeIngredients
                .Select(ri => ri.Ingredient.Amount)
                .Where(amount => amount != null)
                .ToList();

            return amounts;

            // not sure if this method will be used. seems like you should get amounts foreach ingredient
            
        }

        public Amount GetAmountForIngredient(int ingredientId)
        {
            // get ingredients then include amounts,
            // then find the ingredient with matching id,
            // then get the amount and return it
            return _context.Ingredients.Include(a => a.Amount)
                                        .FirstOrDefault(i => i.Id == ingredientId)
                                        .Amount;
        }


        public ICollection<Amount> GetAmounts()
        {
            return _context.Amounts.OrderBy(a => a.Id).ToList();
        }
    }
}
