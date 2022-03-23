using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NotePadFinal
{
    public class TabVM : INotifyPropertyChanged
    {
      
       
            private string _Header;
        public string Header
        {
            get => _Header;
            set
            {
                _Header = value;
                OnPropertyChanged();
            }
        }

        bool _IsPlaceholder = false;
        public bool IsPlaceholder
        {
            get => _IsPlaceholder;
            set
            {
                _IsPlaceholder = value;
                OnPropertyChanged();
            }
        }

        private string _Content;
        public string Content
        {
            get
            {
                return _Content;
            }
            set
            {
                _Content = value;
                OnPropertyChanged();
            }
        }

        private string _Path;
        public string Path
        {
            get
            {
                return _Path;
            }
            set
            {
                _Path = value;
                OnPropertyChanged();
            }
        }

        private string _Color = "Red";
        public string Color
        {
            get
            {
                return _Color;
            }
            set
            {
                _Color = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}

