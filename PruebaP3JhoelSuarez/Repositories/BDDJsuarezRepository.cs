using PruebaP3JhoelSuarez.Interfaces;
using PruebaP3JhoelSuarez.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaP3JhoelSuarez.Repositories
{
    public class BDDJsuarezRepository : IBDDJsuarez
    {
        private readonly SQLiteAsyncConnection _database;

        public BDDJsuarezRepository(string dbPath)
        {
            var databasePath = Path.Combine(dbPath, "JsuarezBDD.db");
            _database = new SQLiteAsyncConnection(databasePath);
            _database.CreateTableAsync<PeliculaJsuarez>().Wait();
        }

        public Task SavePeliculaAsync(PeliculaJsuarez pelicula)
        {
            return _database.InsertAsync(pelicula);
        }

        public Task<List<PeliculaJsuarez>> GetPeliculasAsync()
        {
            return _database.Table<PeliculaJsuarez>().ToListAsync();
        }

    }
}
