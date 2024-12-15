using System.Windows.Input;
using RecipesEverywhere.Model;
using RecipesEverywhere.Utilites.Commands;
using RecipesEverywhere.Services;
using RecepiesEverywhere.Models;

namespace RecipesEverywhere.ViewModel
{
    public class RecipeViewModel : Utilites.ViewModelBase
    {
        private Recipe _recipeModelModel;

        private NavigationService _navigationService;

        public ICommand NavigationCommand { get; private set; }

        public int AuthorID
        {
            get => (int)_recipeModelModel.AuthorId!;
            set
            {
                _recipeModelModel.AuthorId = value;
                OnPropertyChanged("AuthorID");
            }
        }

        public int RecipeID
        {
            get => _recipeModelModel.Id;
            set
            {
                _recipeModelModel.Id = value;
                OnPropertyChanged(nameof(RecipeID));
            }
        }

        public string Title
        {
            get => _recipeModelModel.Title;
            set
            {
                _recipeModelModel.Title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        public string Text
        {
            get => _recipeModelModel.Text;
            set
            {
                _recipeModelModel.Text = value;
                OnPropertyChanged(nameof(Text));
            }
        }

        public RecipeViewModel(Recipe recipeModel)
        {
            _recipeModelModel = recipeModel;
            _navigationService = NavigationService.Instance;

            NavigationCommand = new RelayCommand(OpenRecipePage);
        }

        private void OpenRecipePage(object page)
        {
            if (page is string)
            {
                _navigationService.TryChangePage(page as string, _recipeModelModel);
            }
        }
    }
}