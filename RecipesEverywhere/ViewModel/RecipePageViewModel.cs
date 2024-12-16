using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;
using RecepiesEverywhere.Annotations;
using RecepiesEverywhere.Models;
using RecepiesEverywhere.View;
using RecipesEverywhere.Model;
using RecipesEverywhere.Services;
using RecipesEverywhere.Utilites.Commands;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace RecipesEverywhere.ViewModel
{
    public class RecipePageViewModel : Utilites.ViewModelBase, INotifyPropertyChanged
    {
        private float _averageMark = 0;
        private float _usersMark = 0;

        private readonly Recipe _recipeModelModel;
        private NavigationService _navigationService;

        public float AverageMark
        {
            get => _averageMark;
            set
            {
                if (_averageMark != value)
                {
                    _averageMark = value;
                    OnPropertyChanged();
                }
            }
        }

        public Recipe RecipeModel => _recipeModelModel;

        public ICommand NavigateCommand { get; private set; }
        public ICommand RateCommand { get; private set; }

        private bool _updateVisibility;
        public bool UpdateVisibility
        {
            get => _updateVisibility;
            private set
            {
                if (_updateVisibility != value)
                {
                    _updateVisibility = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _marksVisibility;
        public bool UnMarksVisibility
        {
            get => !_marksVisibility && UserService.Instance.IsAuthorized;
        }
        public bool MarksVisibility
        {
            get => _marksVisibility && UserService.Instance.IsAuthorized;
            private set
            {
                if (_marksVisibility != value)
                {
                    _marksVisibility = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(UnMarksVisibility));
                }
            }
        }

        public float UsersMark
        {
            get => _usersMark;
            set
            {
                if (_usersMark != value)
                {
                    _usersMark = value;
                    OnPropertyChanged();
                }
            }
        }

        public RecipePageViewModel(Recipe recipeModelModel)
        {
            _recipeModelModel = recipeModelModel;
            _navigationService = NavigationService.Instance;
            NavigateCommand = new RelayCommand(Navigate);
            RateCommand = new RelayCommand(Rate);
            UpdateVisibility = UserService.Instance.User.Id == recipeModelModel.AuthorId;

            using (var context = new RecipeDbContext())
            {
                AverageMark = (float)context.Marks
                    .Where(m => m.RecipeId == _recipeModelModel.Id)
                    .Select(m => m.MarkValue)
                    .DefaultIfEmpty()
                    .Average();

                UsersMark = context.Users
                    .Include(u => u.Marks)
                    .Where(u => u.Id == UserService.Instance.User.Id)
                    .SelectMany(u => u.Marks
                        .Where(m => m.RecipeId == RecipeModel.Id)
                        .Select(m => m.MarkValue))
                    .FirstOrDefault();

                MarksVisibility = UsersMark == default;
            }

        }

        private void Rate(object parameter)
        {
            if (parameter is string ratingString && int.TryParse(ratingString, out int rating))
            {
                using (var context = new RecipeDbContext())
                {
                    var user = UserService.Instance.User;

                    // Check if the user has already rated the recipe
                    var existingMark = context.Marks
                        .FirstOrDefault(m => m.RecipeId == RecipeModel.Id && m.UserId == user.Id);

                    if (existingMark != null)
                    {
                        // Update existing rating
                        existingMark.MarkValue = rating;
                    }
                    else
                    {
                        // Add new rating
                        var newMark = new Mark
                        {
                            RecipeId = RecipeModel.Id,
                            UserId = user.Id,
                            MarkValue = rating,
                        };
                        var mark = context.Marks.Add(newMark);
                        context.SaveChanges();

                    }


                    // Update the average mark and user's mark
                    AverageMark = (float)context.Marks
                        .Where(m => m.RecipeId == RecipeModel.Id)
                        .Select(m => m.MarkValue)
                        .DefaultIfEmpty()
                        .Average();

                    UsersMark = rating; // Update the user's mark to the new rating
                    MarksVisibility = false;
                }
            }
        }

        private void Navigate(object sender)
        {
            _navigationService.TryChangePage(nameof(UpdateRecipe), RecipeModel);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
