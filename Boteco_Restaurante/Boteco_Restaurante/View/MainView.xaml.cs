using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Boteco_Restaurante.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainView : Shell
    {
        public MainView()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(TodosProdutosView), typeof(TodosProdutosView));

            Routing.RegisterRoute(nameof(AdicionarView), typeof(AdicionarView));

            Routing.RegisterRoute(nameof(AdicionarProdutoView), typeof(AdicionarProdutoView));

            Routing.RegisterRoute(nameof(ProdutoDetailView), typeof(ProdutoDetailView));             
        }
    }
}