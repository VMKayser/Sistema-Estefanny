using CommunityToolkit.Mvvm.ComponentModel;

namespace Comercial_Estefannny.ViewModel
{
    /// <summary>
    /// Clase base moderna para ViewModels utilizando CommunityToolkit.Mvvm
    /// con source generators para .NET 8.0
    /// </summary>
    public abstract partial class ViewModelbase : ObservableObject
    {
        // La implementación de INotifyPropertyChanged es generada automáticamente
        // por el source generator de CommunityToolkit.Mvvm
        // 
        // Para usar propiedades observables, utiliza:
        // [ObservableProperty]
        // private string _miPropiedad = string.Empty;
        // 
        // Esto generará automáticamente:
        // - La propiedad pública MiPropiedad
        // - Las notificaciones OnPropertyChanged
        // - Validación de cambios
    }
}
