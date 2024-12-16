using RecepiesEverywhere.Models;
using RecipesEverywhere.Utilites.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using RecepiesEverywhere.View;
using Microsoft.EntityFrameworkCore;
using RecipesEverywhere.Model.Enum;
using System.Collections.ObjectModel;
using RecipesEverywhere.Services;
using RecipesEverywhere.Utilites;

namespace RecepiesEverywhere.ViewModel
{
    internal class UpdateRecipeViewModel
    {

        private ObservableCollection<Models.Status> _statuses;
        private string _exceptionMessage;

        private UserService _userService;
        private RecipeService _recipeService;

        private Recipe _recipe;

        public ObservableCollection<Tag> Tags { get; set; } = new(); // Collection of tags
        public Tag SelectedTags
        {
            get => _recipe.Tags.FirstOrDefault();
            set
            {
                _recipe.Tags = new List<Tag>() { value};
                OnPropertyChanged();
            }
        }

        public string ExceptionMessage
        {
            get => _exceptionMessage;
            set
            {
                _exceptionMessage = value;
                OnPropertyChanged();
            }
        }

        public string Title
        {
            get => _recipe.Title;
            set
            {
                _recipe.Title = value;
                OnPropertyChanged();
            }
        }

        public string Text
        {
            get => _recipe.Text;
            set
            {
                _recipe.Text = value;
                OnPropertyChanged();
            }
        }

        public string Picture
        {
            get => _recipe.Picture;
            set
            {
                _recipe.Picture = value;
                OnPropertyChanged();
            }
        }

        public int StatusId
        {
            get => _recipe.StatusId;
            set
            {
                _recipe.StatusId = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Models.Status> Statuses
        {
            get => _statuses;
            set
            {
                _statuses = value;
                OnPropertyChanged();
            }
        }

        private void LoadStatuses()
        {
            using (var context = new RecipeDbContext())
            {
                var statuses = context.Statuses.ToList();
                Statuses = new ObservableCollection<Models.Status>(statuses);
            }
        }
        public ICommand CreateRecipeCommand { get; }


        public UpdateRecipeViewModel(Recipe recipe)
        {
            _recipe = recipe;
            CreateRecipeCommand = new RelayCommand(Update);
            LoadStatuses();
            LoadTags(recipe);
        }


        private void LoadTags(Recipe recipe)
        {
            using (var context = new RecipeDbContext())
            {
                var tags = context.Tags.ToList();
                SelectedTags = context.Recipes
                    .Where(r => r.Id == recipe.Id)
                    .Include(r => r.Tags)
                    .SelectMany(r => r.Tags) // Извлекаем все теги
                    .FirstOrDefault(); // Получаем первый тег или null // Получаем коллекцию тегов; // Получаем первый рецепт или null


                Tags.Clear();
                foreach (Tag tag in tags)
                {

                    Tags.Add(tag);
                }
            }
        }

        private void Update(object sender)
        {

            if (_recipe.Title.Trim().Length == 0
                || _recipe.Text?.Trim().Length == 0
                || _recipe.Picture?.Trim().Length == 0)
            {
                ExceptionMessage = "All fields must be entered";

            }

            if (!RecipeService.Instance.Update(_recipe!))
            {
                ExceptionMessage = "Something happens wrong"; //Мне лень тут что то делать
                return;
            }
            NavigationService.Instance.TryChangePage(nameof(RecipePage), _recipe);

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
