using GUI.View.FolderPicker;
using GUI.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for RecentQRFolder.xaml
    /// </summary>
    public partial class RecentQRFolder : UserControl
    {
        protected readonly QRFolder_ViewModel vm;


        public RecentQRFolder()
        {
            InitializeComponent();

            DataContext = vm = new QRFolder_ViewModel(@"C:\WKSC_SCNR\out", new QRFileItemComparer());

        }

        private void _container_Selected(object sender, RoutedEventArgs e)
        {

        }
    }


    public class QRFolder_ViewModel
        : BaseViewModel
    {
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        private readonly IComparer<QRFileItemModel> _comparer = new QRFileItemComparer();
        public SortedSet<QRFileItemModel> Items { get; set; }
        public int Width { get; set; }

        private readonly FileSystemWatcher _watcher;

        public SortMode SortMode { get; set; }
        public string Path { get => _watcher.Path; }

        public QRFolder_ViewModel(string path, IComparer<QRFileItemModel> comparer)
        {
            if (!Directory.Exists(path))
                throw new ArgumentNullException(nameof(path), "path does not exist");

            Items = new SortedSet<QRFileItemModel>(_comparer = comparer ?? throw new ArgumentNullException(nameof(comparer)));
            //_comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
            _watcher = new FileSystemWatcher(path)
            {
                EnableRaisingEvents = true,
                NotifyFilter = NotifyFilters.Attributes
                                 | NotifyFilters.CreationTime
                                 | NotifyFilters.DirectoryName
                                 | NotifyFilters.FileName
                                 | NotifyFilters.LastAccess
                                 | NotifyFilters.LastWrite
                                 | NotifyFilters.Security
                                 | NotifyFilters.Size,
                IncludeSubdirectories = false
            };


            _watcher.Changed += OnFileEvent;
            _watcher.Created += OnFileEvent;
            _watcher.Renamed += OnFileEvent;
            _watcher.Deleted += OnFileEvent;

            Init();

        }

        protected virtual void Init()
        {
            foreach (var f in Directory.GetFiles(Path))
            {
                try
                {

                    Items.Add(new QRFileItemModel(Width, SortMode, f));
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void Clean()
        {
            var files = new HashSet<string>(Directory.GetFiles(Path));

            Items.RemoveWhere((file) => !files.Contains(file.Path));
        }



        private void OnFileEvent(object sender, FileSystemEventArgs e)
        {
            // an event has happened to a file
            // key is
            _ = e.FullPath;
            // fetch it out of of SortedSet
            var fromArgs = new QRFileItemModel(Width, SortMode, e.FullPath);
            //var relevent = _items(item);
            // if the event is deleted, remove it from the set
            // etc

            _ = Items.TryGetValue(fromArgs, out var fromCache);

            _semaphore.Wait();

            switch (e.ChangeType)
            {
                case WatcherChangeTypes.Changed: OnChanged(fromCache, fromArgs, e); break;
                case WatcherChangeTypes.Created: OnCreated(fromCache, fromArgs, e); break;
                case WatcherChangeTypes.Renamed: OnRenamed(fromArgs, e); break;
                case WatcherChangeTypes.Deleted: OnDeleted(fromCache, e); break;
            }
            OnPropertyChanged(nameof(Items));
            _semaphore.Release();
        }

        protected virtual void OnChanged(QRFileItemModel fromCache, QRFileItemModel newItem, FileSystemEventArgs e)
        {
            // not sure - raise info details?
            // p much a delete old one, add new one
            Items.Remove(fromCache);
            Items.Add(newItem);
        }

        protected virtual void OnCreated(QRFileItemModel fromCache, QRFileItemModel newItem, FileSystemEventArgs e)
        {
            Items.Add(newItem);
        }

        protected virtual void OnRenamed(QRFileItemModel newItem, FileSystemEventArgs e)
        {
            Clean();
            Items.Add(newItem);
        }

        protected virtual void OnDeleted(QRFileItemModel fromCache, FileSystemEventArgs e)
        {
            Items.Remove(fromCache);
        }

    }
}
