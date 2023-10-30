using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Boteco_Restaurante.Helpers
{
    public class Mensagem
    {
        public static void MensagemErroConexao()
        {
            Application.Current.MainPage.DisplayAlert("Ops!", "Algo Deu Errado. Verifique Sua Conexão de Internet.", "OK");
        }      

        public static void MensagemCadastroSucesso()
        {
            Application.Current.MainPage.DisplayAlert("", "Cadastro Realizado Com Sucesso.", "OK");
        }

        public static void MensagemNenhumaCategoriaCadastrada()
        {
            Application.Current.MainPage.DisplayAlert("Ops.", "Não Existem Categorias Cadastradas Ainda. Adicione Uma Primeiramente", "OK");
        }

        public static void MensagemDadosIncompletos()
        {
            Application.Current.MainPage.DisplayAlert("Ops.", "Todos os Campos Devem Ser Informados.", "OK");
        }

        public static void MensagemNenhumaAlteracaoRealizada()
        {
            Application.Current.MainPage.DisplayAlert("", "Nenhuma Alteração Foi Realizada.", "OK");
        }

        public static void MensagemAtualizacaoRealizada()
        {
            Application.Current.MainPage.DisplayAlert("", "Alteração Realizada Com Sucesso.", "OK");
        }

        public static void MensagemDadosJaCadastrados()
        {
            Application.Current.MainPage.DisplayAlert("", "Dados Informados Já Existem No Sistema.", "OK");
        }


    }
}
