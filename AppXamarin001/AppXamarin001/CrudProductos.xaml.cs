using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppXamarin001
{
    public partial class CrudProductos : ContentPage
    {
        string apiUrl = "https://cloudcomputingapi2.azurewebsites.net/api/Productos";

        public CrudProductos()
        {
            InitializeComponent();
        }

        private void cmdUpdate_Clicked(object sender, EventArgs e)
        {
            using (var client = new HttpClient())
            {
                var url = $"{apiUrl}/{txtId.Text}";
                client.BaseAddress = new Uri(url);
                client
                    .DefaultRequestHeaders
                    .Accept
                    .Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                var json = JsonConvert.SerializeObject(new Producto {
                    Id = int.Parse(txtId.Text),
                    Nombre = txtNombre.Text,
                    Existencia = double.Parse(txtExistencia.Text),
                    PrecioUnitario = double.Parse(txtPrecioUnitario.Text),
                    IVA = double.Parse(txtIVA.Text),
                    ClasificacionId = int.Parse(txtClasificacionID.Text)
                });

                var rqst = new HttpRequestMessage(HttpMethod.Put, url);
                rqst.Content = new StringContent(json, Encoding.UTF8, "application/json");

                var resp = client.SendAsync(rqst);
                resp.Wait();

                //json = resp.Result.Content.ReadAsStringAsync().Result;
                //var prod = JsonConvert.DeserializeObject<Producto>(json);
            }
        }


        private void cmdDelete_Clicked(object sender, EventArgs e)
        {
            using (var client = new HttpClient())
            {
                var url = $"{apiUrl}/{txtId.Text}";
                client.BaseAddress = new Uri(url);
                client
                    .DefaultRequestHeaders
                    .Accept
                    .Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                var resp = client.DeleteAsync(url);
                resp.Wait();

                txtId.Text = "";
                txtNombre.Text = "";
                txtExistencia.Text = string.Empty;
                txtPrecioUnitario.Text = string.Empty;
                txtIVA.Text = string.Empty;
                txtClasificacionID.Text = string.Empty;
            }
        }

        private void cmdReadOne_Clicked(object sender, EventArgs e)
        {
            using (var webClient = new HttpClient())
            {
                var resp = webClient.GetStringAsync(apiUrl + "/" + txtId.Text);
                resp.Wait();

                var json = resp.Result;
                var prod = Newtonsoft.Json.JsonConvert.DeserializeObject<Producto>(json);

                txtId.Text = prod.Id.ToString();
                txtNombre.Text = prod.Nombre;
                txtExistencia.Text = prod.Existencia.ToString();
                txtIVA.Text = prod.IVA.ToString();
                txtPrecioUnitario.Text = prod.PrecioUnitario.ToString();
                txtClasificacionID.Text = prod.ClasificacionId.ToString();
            }
        }

        private void cmdInsert_Clicked(object sender, EventArgs e)
        {
            using (var webClient = new HttpClient()) {
                webClient.BaseAddress = new Uri(apiUrl);
                webClient
                    .DefaultRequestHeaders
                    .Accept
                    .Add( System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json") );

                var json = Newtonsoft.Json.JsonConvert.SerializeObject(new Producto
                {
                    Id = int.Parse(txtId.Text),
                    Nombre = txtNombre.Text,
                    Existencia = double.Parse(txtExistencia.Text),
                    PrecioUnitario = double.Parse(txtPrecioUnitario.Text),
                    IVA = double.Parse(txtIVA.Text),
                    ClasificacionId = int.Parse(txtClasificacionID.Text)
                });

                var request = new HttpRequestMessage(HttpMethod.Post, apiUrl);
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");

                var resp = webClient.SendAsync(request);
                resp.Wait();

                json = resp.Result.Content.ReadAsStringAsync().Result;
                var prod = JsonConvert.DeserializeObject<Producto>(json);

                txtId.Text = prod.Id.ToString();
            }
        }

        private async void cmdRegesar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
