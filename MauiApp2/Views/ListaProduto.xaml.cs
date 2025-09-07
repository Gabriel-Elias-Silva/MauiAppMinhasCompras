using MauiApp2.Models;
using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MauiApp2.Views
{
    public partial class ListaProduto : ContentPage
    {

        private ObservableCollection<Produto> lista = new();

        public ListaProduto()
        {
            InitializeComponent();
            lst_produtos.ItemsSource = lista;
        }


        protected async override void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                List<Produto> tmp = await App.Db.GetAll();

                tmp.ForEach(i => lista.Add(i));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "OK");
            }
        }


        private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                string q = e.NewTextValue;
                lista.Clear();


                List<Produto> tmp = await App.Db.Search(q);
                tmp.ForEach(i => lista.Add(i));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }

        private async void ToolbarItemTotal_Clicked(object sender, EventArgs e)
        {
            double soma = lista.Sum(i => i.Total);
            string msg = $"O total é {soma:C}";
            await DisplayAlert("Total dos Produtos", msg, "OK");
        }
        private async Task MenuItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                MenuItem selecinado = sender as MenuItem;
                Produto p = selecinado.BindingContext as Produto;
                bool confirm = await DisplayAlert("Tem certeza?", $"Remover {p.Descricao}?", "Sim", "Não");
                if (confirm)
                {
                    await App.Db.Delete(p.Id);
                    lista.Remove(p);

                }

            }
            catch (Exception ex)
            {
               await DisplayAlert("Ops", ex.Message, "OK");
            }
}

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new NovoProduto());
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }
    }
}
