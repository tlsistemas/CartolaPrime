using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using CartoPrime.Http.Interfaces;
using CartoPrime.Models;
using CartoPrime.Modules.Base;


using Xamarin.Forms;

namespace CartoPrime.ViewModels
{
    public class NewItemViewModel : BaseViewModel<NewItemViewModel>
    {
        private string text;
        private string description;

        public NewItemViewModel(
             IApiService apiService)
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(text)
                && !String.IsNullOrWhiteSpace(description);
        }

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            Item newItem = new Item()
            {
                Id = Guid.NewGuid().ToString(),
                Text = Text,
                Description = Description
            };



            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
