using PruebaP3JhoelSuarez.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaP3JhoelSuarez.Interfaces
{
    public interface IBDDJsuarez
    {
        Task SavePeliculaAsync(PeliculaJsuarez pelicula);
        Task<List<PeliculaJsuarez>> GetPeliculasAsync();
    }
}
