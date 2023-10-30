using Boteco_Restaurante.FirebaseServices;
using Boteco_Restaurante.Helpers;
using Boteco_Restaurante.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Boteco_Restaurante.ViewModel
{
    partial class AdicionarViewModel : ObservableObject
    {
        CategoriaService categoriaService = new CategoriaService();

        [RelayCommand]
        private async Task AbrirAdicionarProdutoView()
        {
            var route = $"{nameof(View.AdicionarProdutoView)}";
            await Shell.Current.GoToAsync(route);
        }

        [RelayCommand]
        async Task AdicionarCategoria()
        {
            bool verificaConexao = Conectividade.VerificaConectividade();

            if (verificaConexao)
            {
                string nomeCategoria = await Application.Current.MainPage.DisplayPromptAsync("", "Informe Nome da Categoria");

                if(nomeCategoria != null)
                {

                    Categoria novaCategoria = new Categoria(nomeCategoria.ToUpper());

                    bool confirmaCategoria = await categoriaService.SeCategoriaExiste(novaCategoria);

                    if (confirmaCategoria)
                    {
                        Mensagem.MensagemDadosJaCadastrados();

                        return;
                    }

                    bool confirmaCadastroCategoria = await categoriaService.AddCategoria(novaCategoria);

                    if (confirmaCadastroCategoria)
                    {
                        Mensagem.MensagemCadastroSucesso();
                    }

                    return;
                }

                return;
            }

            Mensagem.MensagemErroConexao();
        }
    }
}
