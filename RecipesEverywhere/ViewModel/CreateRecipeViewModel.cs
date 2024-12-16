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
using RecepiesEverywhere.Annotations;

namespace RecepiesEverywhere.ViewModel
{
    internal class CreateRecipeViewModel : ViewModelBase
    {
        private string _title;
        private string _text;
        private string _picture;
        private int _statusId;
        private ObservableCollection<Models.Status> _statuses;
        private string _exceptionMessage;

        private UserService _userService;
        private RecipeService _recipeService;


        public string ExceptionMessage
        {
            get => _exceptionMessage;
            set
            {
                _exceptionMessage = value;
                OnPropertyChanged(nameof(ExceptionMessage));
            }
        }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                OnPropertyChanged();
            }
        }

        public string Picture
        {
            get => _picture;
            set
            {
                _picture = value;
                OnPropertyChanged();
            }
        }

        public int StatusId
        {
            get => _statusId;
            set
            {
                _statusId = value;
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


        public CreateRecipeViewModel()
        {
            CreateRecipeCommand = new RelayCommand(CreateRecipe);
            _userService = UserService.Instance;
            LoadStatuses();
        }

        private void CreateRecipe(object sender)
        {

            if (string.IsNullOrWhiteSpace(_title)
                || string.IsNullOrWhiteSpace(_text)
                || string.IsNullOrWhiteSpace(_picture)
                || _statusId == default)
            {
                
                ExceptionMessage = "All fields must be entered";
                return;
            }

            var recipe = new Recipe
            {
                Title = Title,
                Text = Text,
                Picture = Picture,
                StatusId = StatusId,
                AuthorId = _userService.User.Id

            };

            if (!RecipeService.Instance.Create(recipe!))
            {
                ExceptionMessage = "Something was wrong"; 
                return;
            }
            NavigationService.Instance.TryChangePage(nameof(RecipePage), recipe);
        }
    }
}
