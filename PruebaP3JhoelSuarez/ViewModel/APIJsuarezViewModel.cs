using Newtonsoft.Json;
using PruebaP3JhoelSuarez.Interfaces;
using PruebaP3JhoelSuarez.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PruebaP3JhoelSuarez.ViewModel
{
    public class APIJsuarezViewModel : INotifyPropertyChanged
    {
        private string _peliculaNombre;
        public string PeliculaNombre
        {
            get => _peliculaNombre;
            set { _peliculaNombre = value; OnPropertyChanged(); }
        }

        private string _resultado;
        public string Resultado
        {
            get => _resultado;
            set { _resultado = value; OnPropertyChanged(); }
        }

        public ICommand BuscarCommand { get; }
        public ICommand LimpiarCommand { get; }

        private readonly IBDDJsuarez _databaseService;

        public APIJsuarezViewModel(IBDDJsuarez databaseService)
        {
            _databaseService = databaseService;
            BuscarCommand = new Command(async () => await BuscarPelicula());
            LimpiarCommand = new Command(Limpiar);
        }

        private async Task BuscarPelicula()
        {
            if (string.IsNullOrEmpty(PeliculaNombre))
            {
                Resultado = "Por favor ingrese un nombre de una pelicula.";
                return;
            }

            var url = $"https://freetestapi.com/api/v1/movies?search={PeliculaNombre}";
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var response = await client.GetStringAsync(url);
                    var peliculas = JsonConvert.DeserializeObject<List<dynamic>>(response);

                    if (peliculas != null && peliculas.Count > 0)
                    {
                        var pelicula = peliculas[0];
                        var nuevoPelicula = new PeliculaJsuarez
                        {
                            PeliculaName = pelicula["title"],
                            Genero = pelicula["genre"],
                            Awards = pelicula["awards"],
                            Website = pelicula["website"],
                            Jsuarez_NombreBD = "Jsuarez"
                        };

                        Resultado =
                            $"Nombre de la Pelicula: {nuevoPelicula.PeliculaName}\n" +
                            $"Genero: {nuevoPelicula.Genero}\n" +
                            $"Awards: {nuevoPelicula.Awards}\n" +
                            $"Website: {nuevoPelicula.Website}\n" +
                            $"Nombre: Jsuarez";

                        await _databaseService.SavePeliculaAsync(nuevoPelicula);
                    }
                    else
                    {
                        Resultado = "No se encontraron una pelicula con ese nombre.";
                    }
                }
                catch (Exception ex)
                {
                    Resultado = $"Error al buscar la pelicula: {ex.Message}";
                }
            }
        }



        private void Limpiar()
        {
            PeliculaNombre = string.Empty;
            Resultado = string.Empty;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
