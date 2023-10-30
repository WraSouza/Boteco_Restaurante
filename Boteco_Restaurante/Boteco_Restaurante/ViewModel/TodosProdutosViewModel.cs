using Boteco_Restaurante.FirebaseServices;
using Boteco_Restaurante.Helpers;
using Boteco_Restaurante.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Boteco_Restaurante.ViewModel
{
    partial class TodosProdutosViewModel : ObservableObject
    {
        public ObservableCollection<Produto> Produtos { get; private set; } = new ObservableCollection<Produto>();
        public ObservableCollection<Categoria> Categorias { get; private set; } = new ObservableCollection<Categoria>();
        ProdutoService produtoService = new ProdutoService();
        private readonly string todos = "TODOS";        
        CategoriaService categoriaService = new CategoriaService();

        [ObservableProperty]
        public bool isrefreshing;

        public TodosProdutosViewModel()
        {
            BuscaProdutos();
           
            BuscarCategorias();
        }

        async void BuscarCategorias()
        {
            var listaCategorias = await categoriaService.GetCategorias();

            Categoria todos= new Categoria("TODOS");

            Categorias.Add(todos);

            if (listaCategorias != null)
            {
                foreach (var categoria in listaCategorias)
                {
                    Categorias.Add(categoria);
                }
            }
        }

        [RelayCommand]
        async Task BuscarProdutosCategoriaSelecionada(Categoria categoria)
        {
            var produtos = await produtoService.GetProdutos();

            Produtos.Clear();

            if(categoria.CategoriaNome == todos)
            {
                foreach (var produto in produtos)
                {
                    Produtos.Add(produto);
                }
            }
            foreach(var produto in produtos.Where(a => a.Categorias.CategoriaNome == categoria.CategoriaNome))
            {
                Produtos.Add(produto);
            }
        }

        [RelayCommand]
        async Task AbrirProdutoDetailView(Produto produto)
        {
            if(produto == null)
            {
                return;
            }

            Preferences.Set("ProdutoNome", produto.ProdutoNome);
            Preferences.Set("CaminhoImagem", produto.CaminhoImagem);
            Preferences.Set("Categoria", produto.Categorias.CategoriaNome);
            var route = $"{nameof(View.ProdutoDetailView)}";
            await Shell.Current.GoToAsync(route);
        }

        [RelayCommand]
        void AtualizarTela()
        {
            bool verificaConexao = Conectividade.VerificaConectividade();

            if (verificaConexao)
            {
                Produtos.Clear();
                BuscaProdutos();

                Isrefreshing = false;

                return;
            }

            Mensagem.MensagemErroConexao();
        }

        async void BuscaProdutos()
        {
            bool hasConectivity = Conectividade.VerificaConectividade();

            if(hasConectivity)
            {
               
                    var listaProdutos = await produtoService.GetProdutos();

                    if (listaProdutos.Count > 0)
                    {
                        foreach (var produto in listaProdutos)
                        {
                            Produtos.Add(produto);
                        }

                        return;
                    }               

                return;
            }

            Mensagem.MensagemErroConexao();
        }
    }
}
