using System.Collections.Generic;

namespace GUI.View.FolderPicker
{
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


   
}
