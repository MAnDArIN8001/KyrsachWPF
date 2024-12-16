using System.Collections.ObjectModel;
using System.Windows.Input;
using RecepiesEverywhere.Models;
using RecipesEverywhere.Services;
using RecipesEverywhere.Utilites.Commands;
using RecipesEverywhere.View;

namespace RecipesEverywhere.ViewModel
{
    public class HomeViewModel : Utilites.ViewModelBase
    {
        public ObservableCollection<RecipeViewModel> Recipes { get; set; } = new();

        private const int RecipesInPages = 5;
        private List<List<Recipe>> _pages;
        private RecipeService _recipeService;
        private string _query;
        private int _pageIndex;
        public string Query
        {
            get => _query;
            set
            {
                _query = value;
                OnPropertyChanged();
            }
        }

        public ICommand SearchCommand { get; set; }
        public ICommand NextPageCommand { get; set; }
        public ICommand PreviousPageCommand { get; set; }


        public HomeViewModel()
        {
            _recipeService = RecipeService.Instance;
            SearchCommand = new RelayCommand(SearchRecipes);
            NextPageCommand = new RelayCommand(NextPage);
            PreviousPageCommand = new RelayCommand(PreviousPage);
            LoadRecipes();
        }

        private void NextPage(object obj)
        {
            if (_pageIndex >= _pages.Count - 1)
                return;
            _pageIndex++;
            UpdateRecipesView(_pages[_pageIndex]);
        }

        private void PreviousPage(object obj)
        {
            if (_pageIndex <= 0)
                return;
            _pageIndex--;
            UpdateRecipesView(_pages[_pageIndex]);
        }

        private void LoadRecipes()
        {
            var recipes = _recipeService.LoadAll();
            _pages = SplitList(recipes, RecipesInPages);
            UpdateRecipesView(_pages[_pageIndex]);
        }
        static List<List<T>> SplitList<T>(List<T> list, int n)
        {
            List<List<T>> sublists = new List<List<T>>();

            for (int i = 0; i < list.Count; i += n)
            {
                List<T> sublist = list.GetRange(i, Math.Min(n, list.Count - i));
                sublists.Add(sublist);
            }

            return sublists;
        }
        private void UpdateRecipesView(List<Recipe>? recipes)
        {
            if (recipes is null)
            {
                return;
            }
            Recipes.Clear();
            foreach (var recipe in recipes)
            {
                var recipeVM = new RecipeViewModel(recipe);

                Recipes.Add(recipeVM);
            }
        }

        private void SearchRecipes(object sender) 
        {
            var recipes = _recipeService.Search(_query);
            _pages = SplitList(recipes, RecipesInPages);
            _pageIndex = 0;
            UpdateRecipesView(_pages[_pageIndex]);
        }
    }
}