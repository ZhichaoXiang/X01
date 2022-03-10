using System.ComponentModel;

namespace X01
{
    public class BindableSource : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventHandler(propertyName));
        }
        public void SetProperty()
        {
        }
    }
}
