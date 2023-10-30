using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace Boteco_Restaurante.Helpers
{
    public class Conectividade
    {
        public static bool VerificaConectividade()
        {
            bool HasConectivity = false;
            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
            {
                HasConectivity = true;
            }

            return HasConectivity;
        }
    }
}
