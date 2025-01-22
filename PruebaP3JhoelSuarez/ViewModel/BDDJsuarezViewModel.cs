using PruebaP3JhoelSuarez.Interfaces;
using PruebaP3JhoelSuarez.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PruebaP3JhoelSuarez.ViewModel
{
    public class BDDJsuarezViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<PeliculaJsuarez> Peliculas { get; } = new();
        private readonly IBDDJsuarez _databaseService;

        public ICommand ActualizarCommand { get; }
        public ICommand AbrirEnlaceCommand { get; }

        public BDDJsuarezViewModel(IBDDJsuarez databaseService)
        {
            _databaseService = databaseService;
            ActualizarCommand = new Command(async () => await CargarPeliculas());
            AbrirEnlaceCommand = new Command<string>(async (url) => await AbrirEnlace(url));
            CargarPeliculas();
        }

        public async Task AgregarPelicula(PeliculaJsuarez nuevaPelicula)
        {
            await _databaseService.SavePeliculaAsync(nuevaPelicula);
            await CargarPeliculas();
        }

        public async Task CargarPeliculas()
        {
            var peliculas = await _databaseService.GetPeliculasAsync();
            Peliculas.Clear();
            foreach (var pelicula in peliculas)
            {
                Peliculas.Add(pelicula);
            }
        }


        private async Task AbrirEnlace(string url)
        {
            if (!string.IsNullOrWhiteSpace(url))
            {
                await Launcher.OpenAsync(new Uri(url));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
