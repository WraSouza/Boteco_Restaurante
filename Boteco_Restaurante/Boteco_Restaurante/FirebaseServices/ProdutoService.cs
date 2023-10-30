using Boteco_Restaurante.Helpers;
using Boteco_Restaurante.Model;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boteco_Restaurante.FirebaseServices
{
    internal class ProdutoService
    {
        FirebaseClient firebase;

        public ProdutoService()
        {
            firebase = new FirebaseClient("https://boteco-restaurante-default-rtdb.firebaseio.com/");        
        }

        public async Task<bool> AddProduto(Produto produto)
        {
            if (produto == null)
            {
                throw new Exception("Favor Informar um Produto");                

            }
           
                await firebase.Child("Produto")
                .PostAsync(new Produto()
                {
                    ProdutoNome = produto.ProdutoNome,
                    Categorias = produto.Categorias,
                    CaminhoImagem = produto.CaminhoImagem,
                });

                return true;          
            
        }

        public async Task<List<Produto>> GetProdutos()
        {
            
                return (await firebase.Child("Produto")
                .OnceAsync<Produto>()).Select(item => new Produto
                {
                    ProdutoNome = item.Object.ProdutoNome,
                    CaminhoImagem = item.Object.CaminhoImagem,
                    Categorias = item.Object.Categorias,

                }).ToList();
           
            
        }

        public async Task<bool> UpdateProduto(string nomeproduto, string novoNome,Categoria categoriaproduto, Categoria novaCategoria)
        {           
            
                var produtos = await GetProdutos();

            if (string.IsNullOrEmpty(novoNome))
            {
                var atualizarProduto = (await firebase
                  .Child("Produto")
                  .OnceAsync<Produto>()).Where(a => a.Object.ProdutoNome == nomeproduto && a.Object.Categorias.CategoriaNome == categoriaproduto.CategoriaNome).FirstOrDefault();


                atualizarProduto.Object.ProdutoNome = nomeproduto;
                atualizarProduto.Object.Categorias.CategoriaNome = novaCategoria.CategoriaNome;

                await firebase
               .Child("Produto")
               .Child(atualizarProduto.Key)
               .PutAsync(atualizarProduto.Object);

                return true;
            }

            var atualizaProduto = (await firebase
                  .Child("Produto")
                  .OnceAsync<Produto>()).Where(a => a.Object.ProdutoNome == nomeproduto && a.Object.Categorias.CategoriaNome == categoriaproduto.CategoriaNome).FirstOrDefault();


            atualizaProduto.Object.ProdutoNome = novoNome;
            atualizaProduto.Object.Categorias.CategoriaNome = novaCategoria.CategoriaNome;

            await firebase
           .Child("Produto")
           .Child(atualizaProduto.Key)
           .PutAsync(atualizaProduto.Object);

            return true;


        }
    }
}
