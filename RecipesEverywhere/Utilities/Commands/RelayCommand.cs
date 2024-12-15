﻿using System;
using System.Windows.Input;

namespace RecipesEverywhere.Utilites.Commands
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;
        private Action<int> rate;

        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }


        public bool CanExecute(object parameter) => _canExecute is null || _canExecute(parameter);

        public void Execute(object parameter) => _execute(parameter);
    }
}