﻿using RecepiesEverywhere.ViewModel;
using System.Windows.Controls;

namespace RecepiesEverywhere.View
{
    public partial class CreateRecipe : UserControl
    {
        public CreateRecipe()
        {
            InitializeComponent();
            DataContext = new CreateRecipeViewModel();
        }
    }
}