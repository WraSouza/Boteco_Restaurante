using Boteco_Restaurante.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boteco_Restaurante.Model
{
    public class Produto
    {
        public Produto(string produtoNome, string caminhoImagem, Categoria categorias)
        {
           
                ProdutoNome = produtoNome;
                CaminhoImagem = caminhoImagem;
                Categorias = categorias;            
            
        }

        public void AtualizarProduto(string produtoNome, Categoria categorias)
        {
            ProdutoNome = produtoNome;
            Categorias = categorias;
        }

        public Produto()
        {

        }

        public string ProdutoNome { get;  set; }
        public string CaminhoImagem { get; set; }
        public Categoria Categorias { get; set; }
    }
}
