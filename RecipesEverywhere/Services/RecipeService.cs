using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using Microsoft.EntityFrameworkCore;
using RecepiesEverywhere.Models;
using RecipesEverywhere.Model;
using RecipesEverywhere.Model.DTO;
using RecipesEverywhere.Model.Enum;
using RecipesEverywhere.Utilites;

namespace RecipesEverywhere.Services
{
    public class RecipeService
    {
        #region Singleton

        private static RecipeService _instance;

        public static RecipeService Instance => _instance ??= new RecipeService();

        #endregion
        public User user => UserService.Instance.User;
        private RecipeService() { }
        private bool FilterAvailableRecipes(Recipe recipe) => CurrenUserIsAuthor(recipe) || recipe.StatusId == (int)StatusEnum.Public;

        public bool CurrenUserIsAuthor(Recipe recipe) => user.Id == recipe.AuthorId;

        private bool PermissionToCRUT(Recipe recipe) => CurrenUserIsAuthor(recipe) || UserService.Instance.IsAdmin;

        

        public List<Recipe> LoadAll()
        {
            using (var context = new RecipeDbContext())
            {
                return context.Recipes.Include(r => r.Status).Where(FilterAvailableRecipes).ToList();
            }
        }

        public Recipe Load(int recipeId)
        {
            using (var context = new RecipeDbContext())
            {
                var recipe = context.Recipes.Where(FilterAvailableRecipes).FirstOrDefault(recipeDb => recipeDb.Id == recipeId);

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

        public List<Recipe> LoadPostsFromUser(int userId)
        {

            using (var context = new RecipeDbContext())
            {
                var recipesOfUser = context.Recipes.Where((recipe) => recipe.AuthorId == userId).ToList();

                if (recipesOfUser is null)
                {
                    throw new Exception("User don't have any recipes");
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
                try
                {
                    context.Add(recipe);
                    context.SaveChanges();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }

            }
            return true;
        }
        internal bool Delete(Recipe recipe)
        {
            if (recipe.AuthorId == null && PermissionToCRUT(recipe))
                return false;

            using (var context = new RecipeDbContext())
            {
                try
                {
                    // Удаляем связанные записи в таблице Marks
                    var marksToDelete = context.Marks.Where(m => m.RecipeId == recipe.Id);
                    context.Marks.RemoveRange(marksToDelete);

                    // Удаляем рецепт
                    context.Remove(recipe);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }

            }
            return true;
        }
        internal bool Update(Recipe recipe)
        {
            if (recipe.AuthorId == null && PermissionToCRUT(recipe))
                return false;

            using (var context = new RecipeDbContext())
            {
                try
                {
                    context.Update(recipe);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }
            }
            return true;
        }

        internal List<Recipe> Search(string query)
        {
            using (var context = new RecipeDbContext())
            {
                try
                {
                    query = query.ToLower();
                    var recipes = context.Recipes
                        .Where(FilterAvailableRecipes)
                        .Where(recipeDb => recipeDb.Title.ToLower().Contains(query))
                        .ToList();
                    return recipes;
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return null;
                }
            }
        }
    }
}