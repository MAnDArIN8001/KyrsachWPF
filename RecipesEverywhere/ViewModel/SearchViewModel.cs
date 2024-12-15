using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.DirectoryServices.ActiveDirectory;
using System.Windows.Documents;
using System.Windows.Input;
using RecepiesEverywhere.Models;
using RecipesEverywhere.Model;
using RecipesEverywhere.Services;
using RecipesEverywhere.Utilites;
using RecipesEverywhere.Utilites.Commands;

namespace RecipesEverywhere.ViewModel
{
    public class SearchViewModel : ViewModelBase
    {
        private string _titleProp = "";

        private RecipeService _recipeService;

        public ObservableCollection<Recipe> Recipes { get; private set; } = new();

        public string TitleProp
        {
            get => _titleProp;
            set
            {
                _titleProp = value;
                
                OnPropertyChanged(nameof(TitleProp));
            }
        }

        public ICommand SearchCommand { get; private set; }
        

        public SearchViewModel()
        {
            _recipeService = RecipeService.Instance;

            SearchCommand = new RelayCommand(Search);
        }

        private async void Search(object? sender)
        {
            if (_titleProp.Trim().Length == 0)
            {
                return;
            }

            var response = await _recipeService.LoadRecipeByTitleAsync(TitleProp);
            
            Recipes.Clear();

            foreach (var recipe in response.Data)
            {
                Recipes.Add(recipe);
            }
        }
    }
}