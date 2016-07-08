using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice
{
    class MyCollection : ICollection<Figure>
    {
        private ArrayList arr = new ArrayList();
        private bool isReadonly;
        public int Count
        {
            get
            {
                return arr.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return isReadonly;
            }

            set
            {
                isReadonly = value;
            }
        }

        public void Add(Figure item)
        {
            if (isReadonly) throw new Exception("Collection is readonly! You can't add item at readonly collection/");
            else arr.Add(item);
        }

        public void Clear()
        {
            arr.Clear();
        }

        public bool Contains(Figure item)
        {
            //foreach (Figure f in arr)
            //{
            //    if (f.Equals(item)) return true;
            //}
            //return false;
            return arr.Contains(item);
        }

        public void CopyTo(Figure[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<Figure> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool Remove(Figure item)
        {
            //Figure[] figures = new Figure[arr.Length - 1];
            //if (!Contains(item)) return true;
            //for (int i = 0; i < arr.Length; i++)
            //{
            //    if (!arr[i].Equals(item)) figures[i] = arr[i];
            //}
            //arr = figures;
            //return true;
            if (arr.IndexOf(item) == -1) return false;
            arr.Remove(item);
            return true;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public Figure this[int index]
        {
            get
            {
                if (index >= arr.Count)
                    throw new ArgumentOutOfRangeException(); 
                else return (Figure)arr[index];
            }
            set
            {
                if (index >= arr.Count)
                    throw new ArgumentOutOfRangeException();
                else
                    arr[index] = value;
            }
        }
    }
}
