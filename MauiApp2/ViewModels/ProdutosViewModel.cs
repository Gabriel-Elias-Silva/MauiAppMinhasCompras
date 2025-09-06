using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using MauiApp2.Models;

namespace MauiApp2.ViewModels
{
    public class ProdutosViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Produto> _produtos;
        private ObservableCollection<Produto> _produtosOriginais;

        public ObservableCollection<Produto> Produtos
        {
            get => _produtos;
            set
            {
                _produtos = value;
                OnPropertyChanged(nameof(Produtos));
            }
        }

        public ProdutosViewModel()
        {
            _produtos = new ObservableCollection<Produto>();
            _produtosOriginais = new ObservableCollection<Produto>();
            Task.Run(async () => await CarregarProdutosAsync());
        }

        public async Task CarregarProdutosAsync()
        {
            var lista = await App.Db.GetAllProdutosAsync();
            _produtosOriginais = new ObservableCollection<Produto>(lista);
            Produtos = new ObservableCollection<Produto>(_produtosOriginais);
        }

        public void FiltrarProdutos(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
            {
                Produtos = new ObservableCollection<Produto>(_produtosOriginais);
            }
            else
            {
                var filtrados = _produtosOriginais
                    .Where(p => !string.IsNullOrEmpty(p.Descricao) &&
                                p.Descricao.Contains(texto, System.StringComparison.OrdinalIgnoreCase));

                Produtos = new ObservableCollection<Produto>(filtrados);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string nome) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nome));
    }
}
