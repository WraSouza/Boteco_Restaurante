using System;
using System.Collections.Generic;
using System.Text;

namespace Boteco_Restaurante.Model
{
    public class Categoria
    {
        public Categoria(string categoriaNome)
        {
            
                CategoriaNome = categoriaNome;
            
            
        }

        public Categoria()
        {

        }

        public void UpdateCategoria(string categoriaNome)
        {
            CategoriaNome = categoriaNome;
        }

        public string CategoriaNome { get; set; }
    }
}
