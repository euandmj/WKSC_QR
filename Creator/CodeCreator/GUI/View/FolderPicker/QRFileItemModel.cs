using System;
using System.Collections.Generic;
using System.IO;

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
        }

        public IComparable Comparator { get; protected set; }

        public string Path { get; }
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

    class QRFileItemComparer
        : IEqualityComparer<QRFileItemModel>, IComparer<QRFileItemModel>
    {
        public int Compare(QRFileItemModel x, QRFileItemModel y)
        {
            return x.CompareTo(y);
        }

        public bool Equals(QRFileItemModel x, QRFileItemModel y)
        {
            return x.Equals(y);
        }

        public int GetHashCode(QRFileItemModel obj)
        {
            return obj.GetHashCode();
        }
    }

    public enum SortMode
    {
        Path,
        Cell
    }

}
