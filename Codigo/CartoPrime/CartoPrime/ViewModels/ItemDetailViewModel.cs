using System;
using System.Diagnostics;
using System.Threading.Tasks;
using CartoPrime.Http.Interfaces;
using CartoPrime.Models;
using CartoPrime.Modules.Base;


using Xamarin.Forms;

namespace CartoPrime.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel<ItemDetailViewModel>
    {

        private string itemId;
        private string text;
        private string description;
        public string Id { get; set; }

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

        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }

        public ItemDetailViewModel(
             IApiService apiService) { }

        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = new { Id = "1", Text = "Thiago", Description = "Teste" };
                Id = item.Id;
                Text = item.Text;
                Description = item.Description;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
