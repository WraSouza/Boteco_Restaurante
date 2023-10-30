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
using Xamarin.Essentials;

namespace Boteco_Restaurante.ViewModel
{
    partial class ProdutoDetailViewModel : ObservableObject
    {
        public ObservableCollection<Categoria> Categorias { get; private set; } = new ObservableCollection<Categoria>();
        CategoriaService categoriaService = new CategoriaService();
        ProdutoService produtoService = new ProdutoService();      

        private bool atualizaProduto;

        [ObservableProperty]
        public string nomeproduto;

        [ObservableProperty]
        public string newname;

        [ObservableProperty]
        public Categoria categoriaselecionada;

        [ObservableProperty]
        public string categoria;

        [ObservableProperty]
        public string caminhoimagem;

        [ObservableProperty]
        public bool setentryvisible;

        [ObservableProperty]
        public bool setpickerenable;

        [ObservableProperty]
        public bool setbuttonvisible;

        public ProdutoDetailViewModel()
        {
            Setentryvisible = false;

            Setpickerenable = false;

            Setbuttonvisible = false;            

            PreencherTela();

            BuscaCategorias();
        }

        [RelayCommand]
        async void AtualizarProduto()
        {
            Produto produto = new Produto();
            

            if (string.IsNullOrEmpty(Newname))
            {
                if(Categoriaselecionada == null)
                {
                    Mensagem.MensagemNenhumaAlteracaoRealizada();
                    
                }
                else
                {
                    Categoria categoriaCorrente = new Categoria(Categoria);                    

                    produto.AtualizarProduto(Nomeproduto, Categoriaselecionada);

                    atualizaProduto = await produtoService.UpdateProduto(Nomeproduto, Newname,categoriaCorrente, produto.Categorias);

                    Mensagem.MensagemAtualizacaoRealizada();
                }

                return;

            }            

            Categoria categoriaAtual = new Categoria(Categoria);

            if(Categoriaselecionada != null)
            {
                produto.AtualizarProduto(Newname, Categoriaselecionada);

                atualizaProduto = await produtoService.UpdateProduto(Nomeproduto, produto.ProdutoNome, categoriaAtual, Categoriaselecionada);

                Mensagem.MensagemAtualizacaoRealizada();

                return;
            }

            produto.AtualizarProduto(Newname, categoriaAtual);

            if(Categoriaselecionada!= null)
            {
                atualizaProduto = await produtoService.UpdateProduto(Nomeproduto, produto.ProdutoNome, categoriaAtual, Categoriaselecionada);

                Mensagem.MensagemAtualizacaoRealizada();

                return;
            }

            atualizaProduto = await produtoService.UpdateProduto(Nomeproduto, produto.ProdutoNome, categoriaAtual, categoriaAtual);

            Mensagem.MensagemAtualizacaoRealizada();

        }

        [RelayCommand]
        void HabilitarCampos()
        {
            Setpickerenable = true;

            Setentryvisible = true;

            Setbuttonvisible = true;

        }

        async void BuscaCategorias()
        {           

            bool verificaConexao = Conectividade.VerificaConectividade();

            if (verificaConexao)
            {                

                var listaCategorias = await categoriaService.GetCategorias();
                
                    foreach (var dados in listaCategorias.Where(x => x.CategoriaNome != Categoria))
                    {                        
                        Categorias.Add(dados);
                    }

                    return;                
            }
            
        }

        async void PreencherTela()
        {
            Nomeproduto = Preferences.Get("ProdutoNome", "default_value");
            Categoria = Preferences.Get("Categoria", "default_value");
            Caminhoimagem = Preferences.Get("CaminhoImagem", "default_value");
           
        }
    }
}
