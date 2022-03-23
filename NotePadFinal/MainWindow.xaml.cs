using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NotePadFinal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        int TabIndex = 1;
        ObservableCollection<TabVM> Tabs = new ObservableCollection<TabVM>();

        private int currentTab = 0;


        public MainWindow()
        {
            InitializeComponent();
            var tab1 = new TabVM()
            {
                Header = $"Tab {TabIndex}",
                Content = string.Empty,
                Path = string.Empty

            };

            Tabs.Add(tab1);
            AddNewPlusButton();

            MyTabControl.ItemsSource = Tabs;
            MyTabControl.SelectionChanged += MyTabControl_SelectionChanged;

            TreeViewInit();

        }

        private void TreeViewInit()
        {
            TreeViewItem c = new TreeViewItem();
            c.Header = "C:";
            c.Expanded += new RoutedEventHandler(OnExpanded);
            TreeViewItem fakeChild = new TreeViewItem();
            fakeChild.Header = "fakeChild";
            c.Items.Add(fakeChild);
            c.Tag = "C:\\";
            TreeView.Items.Add(c);
        }

        private void IterateSubdirectories(TreeViewItem item, string path)
        {
            string[] directories = Directory.GetDirectories(path);
            foreach (string directory in directories)
            {
                TreeViewItem folder = new TreeViewItem();
                folder.Header = System.IO.Path.GetFileName(directory);
                IterateSubdirectories(folder, directory);
                string[] files = Directory.GetFiles(directory);
                foreach (string file in files)
                {
                    TreeViewItem currentFile = new TreeViewItem();
                    currentFile.Header = System.IO.Path.GetFileName(file);
                    currentFile.Tag = file;
                    currentFile.MouseDoubleClick += TreeViewItem_MouseDoubleClick;
                    folder.Items.Add(currentFile);
                }
                item.Items.Add(folder);
            }
        }

        private void MyTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl)
            {
                var pos = MyTabControl.SelectedIndex;
                if (pos != 0 && pos == Tabs.Count - 1) //last tab
                {
                    var tab = Tabs.Last();
                    ConvertPlusToNewTab(tab);
                    AddNewPlusButton();
                }

                currentTab = pos;

              /*  if(Tabs[currentTab].Path!=null && Tabs[currentTab].Path != "" && Tabs[currentTab].Content != File.ReadAllText(Tabs[currentTab].Path))
                {
                    Tabs[currentTab].Color = "Red";
                }*/
            }
            
        }

        void ConvertPlusToNewTab(TabVM tab)
        {
            //Do things to make it a new tab.
            TabIndex++;
            tab.Header = $"Tab {TabIndex}";
            tab.IsPlaceholder = false;
            tab.Content = string.Empty;
        }
        /// <summary>
        /// Ads new tab
        /// </summary>
        void AddNewPlusButton()
        {
            var plusTab = new TabVM()
            {
                Header = "+",
                IsPlaceholder = true
            };
            Tabs.Add(plusTab);
        }

        private void OnTabCloseClick(object sender, RoutedEventArgs e)
        {
            var tab = (sender as Button).DataContext as TabVM;
            if (Tabs.Count > 2)
            {
                var index = Tabs.IndexOf(tab);
                if (index == Tabs.Count - 2)//last tab before [+]
                {
                    MyTabControl.SelectedIndex--;
                }
                Tabs.RemoveAt(index);
            }
        }

        private void OnExpanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem currentItem = e.Source as TreeViewItem;
            if ((File.GetAttributes(currentItem.Tag.ToString()) & FileAttributes.Directory) == FileAttributes.Directory)
                ///crapa cand intru in foldere goale.
                if ((currentItem.Items[0] as TreeViewItem).Header.ToString() == "fakeChild")
                {
                    currentItem.Items.Remove(currentItem.Items[0]);
                    string[] directories = Directory.GetDirectories(currentItem.Tag.ToString());
                    foreach (string directory in directories)
                    {
                        TreeViewItem newItem = new TreeViewItem();
                        newItem.Header = System.IO.Path.GetFileName(directory);
                        newItem.Tag = directory;
                        newItem.Expanded += OnExpanded;
                        TreeViewItem fakeChild = new TreeViewItem();
                        fakeChild.Header = "fakeChild";
                        newItem.Items.Add(fakeChild);
                        currentItem.Items.Add(newItem);
                    }

                    string[] files = Directory.GetFiles(currentItem.Tag.ToString());
                    foreach (string file in files)
                    {
                        TreeViewItem currentFile = new TreeViewItem();
                        currentFile.Header = System.IO.Path.GetFileName(file);
                        currentFile.Tag = file;
                        currentFile.MouseDoubleClick += TreeViewItem_MouseDoubleClick;
                        currentItem.Items.Add(currentFile);
                    }
                }
        }

        private void TreeViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Tabs[currentTab].Content = File.ReadAllText((sender as TreeViewItem).Tag.ToString());
            Tabs[currentTab].Path = (sender as TreeViewItem).Tag.ToString();
            Tabs[currentTab].Header = System.IO.Path.GetFileName((sender as TreeViewItem).Tag.ToString());
            Tabs[currentTab].OldContent = Tabs[currentTab].Content;

        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("123");
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                Tabs[currentTab].Content = File.ReadAllText(openFileDialog.FileName);
                Tabs[currentTab].Path = openFileDialog.FileName;
                Tabs[currentTab].Header = Path.GetFileName(openFileDialog.FileName);
                Tabs[currentTab].OldContent = Tabs[currentTab].Content;
            }
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (Tabs[currentTab].Path == string.Empty || Tabs[currentTab].Path==null)
            {
                Save_as_Click(sender, e);
            }
            else
            {
                File.WriteAllText(Tabs[currentTab].Path, Tabs[currentTab].Content);
            }
 
            Tabs[currentTab].OldContent = Tabs[currentTab].Content;
            Tabs[currentTab].Color = "Green";
        }
        private void Save_as_Click(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text File (*.txt)|*.txt";
            if (saveFileDialog.ShowDialog() == true)
            {
                Tabs[currentTab].Path = saveFileDialog.FileName;
                Tabs[currentTab].Header = Path.GetFileName(saveFileDialog.FileName);

                File.WriteAllText(saveFileDialog.FileName, Tabs[currentTab].Content);
            }
        }

        private void Open_About(object sender, RoutedEventArgs e)
        {
            var about = new About();
            about.Show();
        }

        private void Open_Search(object sender, RoutedEventArgs e)
        {

        }
        private void Open_SearchAll(object sender, RoutedEventArgs e)
        {

        }
        private void Open_Replace(object sender, RoutedEventArgs e)
        {

        }
        private void Open_ReplaceAll(object sender, RoutedEventArgs e)
        {

        }
    }
}
