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
    public class CategoriaService
    {
        FirebaseClient firebase;

        public CategoriaService()
        {
            firebase = new FirebaseClient("https://boteco-restaurante-default-rtdb.firebaseio.com/");            
        }

        public async Task<bool> SeCategoriaExiste(Categoria _categoria)
        {
            var categoria = (await firebase.Child("Categoria")
                .OnceAsync<Categoria>())
                .Where(u => u.Object.CategoriaNome == _categoria.CategoriaNome)
                .FirstOrDefault();

            return (categoria != null);
        }

        public async Task<bool> AddCategoria(Categoria categoria)
        {
            await firebase.Child("Categoria")
                .PostAsync(new Categoria()
                {
                    CategoriaNome = categoria.CategoriaNome
                });

            return true;
        }

        public async Task<List<Categoria>> GetCategorias()
        {
            return (await firebase.Child("Categoria")
                .OnceAsync<Categoria>()).Select(item => new Categoria
                {
                   CategoriaNome = item.Object.CategoriaNome
                }).ToList();
        }
    }
}
