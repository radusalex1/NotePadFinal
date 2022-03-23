using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

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
        private string _oldContent;
        public string OldContent
        {
            get
            {
                return _oldContent;
            }
            set
            {
                _oldContent = value;
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
                if (_Content != OldContent)
                {
                    Color = "Red";
                }
                else
                {
                    Color = "Green";
                }
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


        /// <summary>
        /// Property for text wrapping.
        /// </summary>
        private TextWrapping _wrap;
        public TextWrapping Wrap
        {
            get { return _wrap; }
            set
            {
                _wrap = value;
                OnPropertyChanged();
                isWrapped = value == TextWrapping.Wrap ? true : false;
            }
        }

        /// <summary>
        /// Text property for text Wrapping when modifing window sizes.
        /// Related to previous property.
        /// </summary>
        private bool _isWrapped;
        public bool isWrapped
        {
            get
            {
                return _isWrapped;
            }
            set
            {
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

