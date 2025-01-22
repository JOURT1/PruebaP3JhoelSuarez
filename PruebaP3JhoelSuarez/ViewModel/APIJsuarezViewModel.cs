using Newtonsoft.Json;
using PruebaP3JhoelSuarez.Interfaces;
using PruebaP3JhoelSuarez.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
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
                Resultado = "Por favor, ingrese un nombre de una película.";
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

                        var nuevaPelicula = new PeliculaJsuarez
                        {
                            PeliculaName = pelicula.title,
                            Genero = pelicula.genre?.Count > 0 ? pelicula.genre[0] : "N/A",
                            ActorPrincipal = pelicula.actors?.Count > 0 ? pelicula.actors[0] : "N/A",
                            Awards = pelicula.awards ?? "N/A",
                            Website = pelicula.website ?? "N/A",
                            Jsuarez_NombreBD = "Jsuarez"
                        };

                        await _databaseService.SavePeliculaAsync(nuevaPelicula);

                        // Notifica al listado para que se actualice.
                        var listaViewModel = new BDDJsuarezViewModel(_databaseService);
                        await listaViewModel.CargarPeliculas();

                        Resultado =
                            $"Título: {nuevaPelicula.PeliculaName}\n" +
                            $"Género: {nuevaPelicula.Genero}\n" +
                            $"Actor Principal: {nuevaPelicula.ActorPrincipal}\n" +
                            $"Awards: {nuevaPelicula.Awards}\n" +
                            $"Sitio Web: {nuevaPelicula.Website}\n" +
                            $"Nombre: {nuevaPelicula.Jsuarez_NombreBD}\n";
                    }
                    else
                    {
                        Resultado = "No se encontró una película con ese nombre.";
                    }
                }
                catch (Exception ex)
                {
                    Resultado = $"Error al buscar la película: {ex.Message}";
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
