using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppXamarin001
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginaPrincipal : ContentPage
    {
        public PaginaPrincipal()
        {
            InitializeComponent();
        }

        private async void cmdCrudProductos_Clicked(object sender, EventArgs e)
        {
            var crudProd = new CrudProductos();
            await Navigation.PushAsync(crudProd);
        }
    }
}