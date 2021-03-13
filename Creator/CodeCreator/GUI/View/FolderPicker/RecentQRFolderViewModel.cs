using GUI.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Threading;

namespace GUI.View.FolderPicker
{
    public class RecentQRFolderViewModel
        : BaseViewModel, INotifyCollectionChanged
    {
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
        private readonly FileSystemWatcher _watcher;
        private readonly Dispatcher disp = Dispatcher.CurrentDispatcher;

        public int Width { get; set; }
        public SortMode SortMode { get; set; }
        public string Path { get => _watcher.Path; }
        public ObservableCollection<QRFileItemModel> Items { get; set; }

        public QRFileItemModel SelectedItem { get; set; }

        public RecentQRFolderViewModel(string path, IComparer<QRFileItemModel> comparer)
        {
            if (!Directory.Exists(path))
                throw new ArgumentNullException(nameof(path), "path does not exist");

            Items = new ObservableCollection<QRFileItemModel>();
            SortMode = SortMode.Date;
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
                    throw;
                }
            }

            
        }

        private void OnCollectionChanged(NotifyCollectionChangedAction action, object item)
        {
            //CollectionChanged?.Invoke(this,
            //    new NotifyCollectionChangedEventArgs(action, item));
        }

        private void Sort()
        {
            var sorted = Items.OrderByDescending(x => x.Comparator).ToList();
            for (int i = 0; i < sorted.Count; i++)
            {
                Items.Move(Items.IndexOf(sorted[i]), i);
            }
        }

        private void Clean()
        {
            var files = new HashSet<string>(Directory.GetFiles(Path));

            try
            {
                for (int i = 0; i < Items.Count; ++i)
                {
                    var file = Items[i];

                    if (!files.Contains(file.Path))
                        Items.RemoveAt(i);
                }

            }
            catch (KeyNotFoundException)
            { throw; }
        }



        private void OnFileEvent(object sender, FileSystemEventArgs e)
        {
            // fetch it out of of SortedSet
            
            var fromArgs = new QRFileItemModel(Width, SortMode, e.FullPath);

            var fromCache = Items.FirstOrDefault(x => x.Path == e.FullPath);


            switch (e.ChangeType)
            {
                case WatcherChangeTypes.Changed:
                    OnChanged(fromCache, fromArgs, e);
                    OnCollectionChanged(NotifyCollectionChangedAction.Remove, fromCache);
                    break;
                case WatcherChangeTypes.Created:
                    OnCreated(fromCache, fromArgs, e);
                    OnCollectionChanged(NotifyCollectionChangedAction.Remove, fromCache);
                    break;
                case WatcherChangeTypes.Renamed:
                    OnRenamed(fromArgs, e);
                    OnCollectionChanged(NotifyCollectionChangedAction.Replace, fromCache);
                    break;
                case WatcherChangeTypes.Deleted:
                    OnDeleted(fromCache, e);
                    OnCollectionChanged(NotifyCollectionChangedAction.Remove, fromCache);
                    break;
            }
        }

        protected virtual void OnChanged(QRFileItemModel fromCache, QRFileItemModel newItem, FileSystemEventArgs e)
        {
            // not sure - raise info details?
            // p much a delete old one, add new one
            disp.Invoke(() =>
            {

                Items.Remove(fromCache);
                Items.Add(newItem);
            });
        }

        protected virtual void OnCreated(QRFileItemModel fromCache, QRFileItemModel newItem, FileSystemEventArgs e)
        {
            disp.Invoke(() =>
            {

                Items.Add(newItem);
            });
        }

        protected virtual void OnRenamed(QRFileItemModel newItem, FileSystemEventArgs e)
        {
            disp.Invoke(() =>
            {
                Clean();
                Items.Add(newItem);

            });
        }

        protected virtual void OnDeleted(QRFileItemModel fromCache, FileSystemEventArgs e)
        {
            disp.Invoke(() =>
            {
                Items.Remove(fromCache);
            });
        }

    }
}
