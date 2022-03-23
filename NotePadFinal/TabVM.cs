using System.ComponentModel;
using System.Runtime.CompilerServices;

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
                if(_Content!=OldContent)
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
                //if(Path!=null && Path!=string.Empty)
                //{
                //    if (Content != OldContent)
                //    {
                //        _Color = "Red";
                //    }
                //    else
                //    {
                //        _Color = "Green";
                //    }
                //    OnPropertyChanged();
                //}

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

