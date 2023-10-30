using Boteco_Restaurante.FirebaseServices;
using Boteco_Restaurante.Helpers;
using Boteco_Restaurante.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Storage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Boteco_Restaurante.ViewModel
{
    partial class AdicionarProdutoViewModel : ObservableObject
    {
        public ObservableCollection<Categoria> Categorias { get; private set; } = new ObservableCollection<Categoria>();
        CategoriaService categoriaService = new CategoriaService();
        ProdutoService produtoService = new ProdutoService();

        Stream caminhoPhoto;

        private string imagemURL;
        private string referenciaImagem;

        [ObservableProperty]
        public string nomeproduto;

        [ObservableProperty]
        public Categoria categoria;

        [ObservableProperty]
        public string caminhoimagem;

        [ObservableProperty]
        public ImageSource caminhoImagem;

        public AdicionarProdutoViewModel()
        {
            BuscaQuantidadeCategoria();
        }

        public Command SelecionarImagem
        {
            get
            {
                return new Command(async (e) =>
                {
                    var photo = await MediaPicker.PickPhotoAsync();
                    var stream = await LoadPhotoAsync(photo);
                });
            }
        }

        private async Task<string> StoreImages(Stream imageStream)
        {
          
            string nomeProduto = Nomeproduto;
            string imgurl = null;
            string storageImage = null;
            if (nomeProduto != null)
            {
                storageImage = await new FirebaseStorage("boteco-restaurante.appspot.com")
              .Child("Fotos")
              .Child(nomeProduto + ".jpg")
              .PutAsync(imageStream);
                imgurl = storageImage;
               
            }

            return imgurl;

        }

        async Task<Stream> LoadPhotoAsync(FileResult photo)
        {
            if (photo == null)
            {
                return null;
            }

            var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
            var stream = await photo.OpenReadAsync();
            caminhoPhoto = stream;
            CaminhoImagem = photo.FullPath;

            return stream;
        }

        [RelayCommand]
        async Task CadastrarProduto()
        {
            bool verificaConexao = Conectividade.VerificaConectividade();

            if(verificaConexao)
            {
                              
                    if(caminhoPhoto == null)
                    {
                        await Application.Current.MainPage.DisplayAlert("Erro", "Você Deve Selecionar Uma Imagem de Cadastro", "OK");

                        return;
                    }

                if (string.IsNullOrEmpty(Nomeproduto) || Categoria == null)
                {
                    Mensagem.MensagemDadosIncompletos();

                    return;
                }
                imagemURL = await StoreImages(caminhoPhoto);

                    referenciaImagem = imagemURL.ToString();                   

               
                    var novoProduto = new Produto(Nomeproduto, referenciaImagem, Categoria);

                    bool adicionaProduto = await produtoService.AddProduto(novoProduto);

                    if (adicionaProduto)
                    {
                        Mensagem.MensagemCadastroSucesso();
                    }
                

                return;
            }

            Mensagem.MensagemErroConexao();
        }

        async void BuscaQuantidadeCategoria()
        {
            bool verificaConexao = Conectividade.VerificaConectividade();

            if(verificaConexao)
            {
                var quantidadeCategorias = await categoriaService.GetCategorias();


                if (quantidadeCategorias.Count > 0)
                {
                    foreach(var dados in quantidadeCategorias)
                    {
                        Categorias.Add(dados);
                    }
                    
                    return;
                }

                await Application.Current.MainPage.DisplayAlert("", "Não Há Categoria Cadastrada Ainda.Você Deverá Cadastrar Uma Antes de Prosseguir", "OK");

                string result = await Application.Current.MainPage.DisplayPromptAsync("", "Informe Nome da Categoria");

                try
                {
                    Categoria novaCategoria = new Categoria(result.ToUpper());

                    Categorias.Add(novaCategoria);

                    bool confirmaCadastroCategoria = await categoriaService.AddCategoria(novaCategoria);

                    if (confirmaCadastroCategoria)
                    {
                        Mensagem.MensagemCadastroSucesso();
                    }


                    return;
                }
                catch(ArgumentNullException ex) 
                {
                    await Application.Current.MainPage.DisplayAlert("",ex.Message.ToString(),"OK");

                    return;
                }
                finally
                {

                }

                
            }
            else
            {
                Mensagem.MensagemErroConexao();
            }           

        }
    }
}
