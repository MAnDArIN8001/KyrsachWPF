using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Documents;
using Microsoft.EntityFrameworkCore;
using RecepiesEverywhere.Models;
using RecipesEverywhere.Model;
using RecipesEverywhere.Model.DTO;
using RecipesEverywhere.Utilites;

namespace RecipesEverywhere.Services
{
    public class RecipeService
    {
        #region Singleton

        private static RecipeService _instance;

        public static RecipeService Instance => _instance ??= new RecipeService();

        #endregion

        private RecipeService() { }

        public List<Recipe> LoadAllRecipes()
        {
            using (var context = new RecipeDbContext())
            {
                return context.Recipes.ToList();
            }
        }

        public Recipe LoadRecipe(int recipeId)
        {
            using (var context = new RecipeDbContext())
            {
                var recipe = context.Recipes.FirstOrDefault(recipeDb => recipeDb.Id == recipeId);

                return recipe;
            }
        }

        public async Task<Response<List<Recipe>>> LoadRecipeByTitleAsync(string title)
        {
            Response<List<Recipe>> response = new();
            
            try
            {
                using (var context = new RecipeDbContext())
                {
                    var recipes = await context.Recipes
                        .Where(recipeDb => EF.Functions.Like(recipeDb.Title.ToLower(), $"%{title.ToLower()}%"))
                        .ToListAsync();

                    response.Success = true;
                    response.Data = recipes;
                    
                    return response;
                }
            }
            catch (Exception exception)
            {
                response.Message = "Something was wrong";
                response.Success = false;

                return response;
            }
        }

        public async Task<Response<List<Recipe>>> LoadPostsFromUser(int userId)
        {
            Response<List<Recipe>> response = new();

            try
            {
                using (var context = new RecipeDbContext())
                {
                    var recipesOfUser =
                        await context.Recipes.Where((recipe) => recipe.AuthorId == userId).ToListAsync();

                    if (recipesOfUser is null)
                    {
                        throw new Exception("User dont have any recipes");
                    }

                    response.Data = recipesOfUser;
                }
            }
            catch (Exception exception)
            {
                response.Message = exception.Message;
                response.Success = false;

                return response;
            }

            return response;
        }

        public bool CreateRecipe(RecipeDTO recipeDto)
        {
            try
            {
                using (var context = new RecipeDbContext())
                {
                    var recipe = new Recipe()
                    {
                        Text = recipeDto.Text,
                        Title = recipeDto.Title,
                        Picture = recipeDto.Picture
                    };

                    context.Recipes.Add(recipe);
                }
            }
            catch (Exception exception)
            {
                return false;
            }

            return true;
        }
    }
}