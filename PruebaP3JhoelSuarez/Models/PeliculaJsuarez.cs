using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaP3JhoelSuarez.Models
{
    internal class PeliculaJsuarez
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string PeliculaName { get; set; }
        public string Genero { get; set; }
        public string Awards { get; set; }
        public string Website { get; set; }
        public string Jsuarez_NombreBD { get; set; }
    }
}
