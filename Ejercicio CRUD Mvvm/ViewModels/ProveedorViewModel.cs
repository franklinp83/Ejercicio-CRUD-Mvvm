using EjercicioCRUDMvvm.Models;
using EjercicioCRUDMvvm.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EjercicioCRUDMvvm.ViewModels
{
    public class ProveedorViewModel : BaseViewModel
    {
        private readonly ProveedorService _proveedorService;

        public ObservableCollection<Proveedor> Proveedores { get; } = new();

        public Proveedor NuevoProveedor { get; set; } = new();

        public ICommand GuardarCommand { get; }
        public ICommand EliminarCommand { get; }

        public ProveedorViewModel(ProveedorService proveedorService)
        {
            _proveedorService = proveedorService;

            GuardarCommand = new Command(async () => await GuardarProveedor());
            EliminarCommand = new Command<Proveedor>(async (proveedor) => await EliminarProveedor(proveedor));

            CargarProveedores();
        }

        private async Task GuardarProveedor()
        {
            if (string.IsNullOrWhiteSpace(NuevoProveedor.Nombre) ||
                string.IsNullOrWhiteSpace(NuevoProveedor.Direccion) ||
                string.IsNullOrWhiteSpace(NuevoProveedor.Telefono) ||
                string.IsNullOrWhiteSpace(NuevoProveedor.Correo) ||
                string.IsNullOrWhiteSpace(NuevoProveedor.RazonSocial))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Todos los campos son obligatorios.", "OK");
                return;
            }

            await _proveedorService.GuardarProveedorAsync(NuevoProveedor);

            NuevoProveedor = new Proveedor();

            await CargarProveedores();
        }

        private async Task EliminarProveedor(Proveedor proveedor)
        {
            if (proveedor != null)
            {
                await _proveedorService.EliminarProveedorAsync(proveedor);

                await CargarProveedores();
            }
        }

        private async Task CargarProveedores()
        {
            Proveedores.Clear();

            var proveedores = await _proveedorService.ObtenerProveedoresAsync();
            foreach (var proveedor in proveedores)
            {
                Proveedores.Add(proveedor);
            }
        }
    }
}
