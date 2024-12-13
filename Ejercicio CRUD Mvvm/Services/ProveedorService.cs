using Ejercicio_CRUD_Mvvm.Models;
using EjercicioCRUDMvvm.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EjercicioCRUDMvvm.Services
{
    public class ProveedorService
    {
        private readonly SQLiteAsyncConnection _database;

        // Constructor que inicializa la conexión con la base de datos
        public ProveedorService(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            // Crear la tabla si no existe
            _database.CreateTableAsync<Proveedor>().Wait();
        }

        // Método para obtener todos los proveedores
        public Task<List<Proveedor>> ObtenerProveedoresAsync()
        {
            return _database.Table<Proveedor>().ToListAsync();
        }

        // Método para guardar o actualizar un proveedor
        public Task<int> GuardarProveedorAsync(Proveedor proveedor)
        {
            if (proveedor.Id != 0)
            {
                // Actualizar si el Id ya existe
                return _database.UpdateAsync(proveedor);
            }
            else
            {
                // Insertar un nuevo registro
                return _database.InsertAsync(proveedor);
            }
        }

        // Método para eliminar un proveedor
        public Task<int> EliminarProveedorAsync(Proveedor proveedor)
        {
            return _database.DeleteAsync(proveedor);
        }
    }
}

