﻿using Microsoft.WindowsAPICodePack.Shell;
using System;
using System.IO;
using System.Windows.Media;

namespace GUI.View.FolderPicker
{
    public class QRFileItemModel
      : IComparable<QRFileItemModel>
    {
        private SortMode _mode;
        private readonly int _width;

        public QRFileItemModel(int w, SortMode mode, string path)
        {
            _width = w;
            Path = path;
            Mode = mode;


            if (File.Exists(path))
            {
                using (var f = ShellFile.FromFilePath(path))
                {
                    Image = f.Thumbnail.SmallBitmapSource;
                }
            }
        }


        public ImageSource Image { get; }
        public IComparable Comparator { get; protected set; }
        public string Path { get; }
        public string Name => Path.Substring(Path.LastIndexOf('\\'));
        public int X { get; set; }
        public int Y { get; set; }
        public DateTime Created { get; set; }


        public SortMode Mode
        {
            get => _mode;
            set
            {
                switch(value)
                {
                    case SortMode.Path:
                        Comparator = Path;
                        break;
                    case SortMode.Date:
                        Comparator = Created;
                        break;
                    case SortMode.Cell:
                        Comparator = X + Y;
                        break;
                    default:
                        throw new ArgumentNullException("unrecognised SortMode");
                }
                _mode = value;
            }
        }

        public int CompareTo(QRFileItemModel other)
        {
            return Comparator.CompareTo(other.Comparator);
        }

        public override int GetHashCode()
        {
            return Comparator.GetHashCode();
        }
    }
}
