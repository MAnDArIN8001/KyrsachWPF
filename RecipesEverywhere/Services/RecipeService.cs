using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

        public List<Recipe> LoadAll()
        {
            using (var context = new RecipeDbContext())
            {
                return context.Recipes.ToList();
            }
        }

        public Recipe Load(int recipeId)
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

        public  List<Recipe> LoadPostsFromUser(int userId)
        {

            using (var context = new RecipeDbContext())
            {
                var recipesOfUser = context.Recipes.Where((recipe) => recipe.AuthorId == userId).ToList();

                if (recipesOfUser is null)
                {
                    throw new Exception("User dont have any recipes");
                }
                return recipesOfUser;
            }

        }

        public bool Create(RecipeDTO recipeDto)
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

        internal bool Create(Recipe recipe)
        {
            if (recipe.AuthorId == null)
                return false;

            using (var context = new RecipeDbContext())
            {
                context.Add(recipe);
                context.SaveChanges();

            }
            return true;
        }
        internal bool Update(Recipe recipe)
        {
            if (recipe.AuthorId == null)
                return false;

            using (var context = new RecipeDbContext())
            {
                context.Update(recipe);
                context.SaveChanges();
            }
            return true;
        }
    }
}