using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Comercial_Estefannny.ViewModel
{
    public abstract class ViewModelbase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
