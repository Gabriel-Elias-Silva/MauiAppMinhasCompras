using Microsoft.Maui.Controls;
using MauiApp2.ViewModels;

namespace MauiApp2.Views
{
    public partial class ListaProduto : ContentPage
    {
        private ProdutosViewModel ViewModel => BindingContext as ProdutosViewModel;

        public ListaProduto()
        {
            InitializeComponent();
        }

        private void OnSearchBarTextChanged(object sender, TextChangedEventArgs e)
        {
            ViewModel?.FiltrarProdutos(e.NewTextValue);
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            // Tela de cadastro de produto
            await Navigation.PushAsync(new NovoProduto());
        }
    }
}
