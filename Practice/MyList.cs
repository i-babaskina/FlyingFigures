using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice
{
    class MyList<T> : IList<T>
    {

        public Node<T> Node;
        private int count;
        public bool isReadOnly;

        public T this[int index]
        {

            get
            {
                if (index == 0) return Node.Value;
                if (index >= count) throw new IndexOutOfRangeException();
                for (int i = 0; i < index; i++)
                {
                    Node = Node.Next;
                }
                return Node.Value;
                
            }

            set
            {
                if (index == 0) Node.Value = value;
                if (index >= count) throw new IndexOutOfRangeException();
                for (int i = 0; i < index; i++)
                {
                    Node = Node.Next;
                }
                Node.Value = value;
            }
        }

        public int Count
        {
            get { return count; }
        }

        public bool IsReadOnly
        {
            get
            {
                return isReadOnly;
            }
        }

        public void Add(T item)
        {
            if (isReadOnly) throw new Exception("List is readonly");
            while (Node.Next != null)
            {
                Node = Node.Next;
            }
            Node.Next = new Node<T>();
            Node.Next.Value = item;
        }

        public void Clear()
        {
            Node = null;
        }

        public bool Contains(T item)
        {
            do
            {
                if (Node.Value.Equals(item)) return true;
                Node = Node.Next;
            } while (Node.Next != null);
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public int IndexOf(T item)
        {
            if (Node.Value.Equals(item)) return 0;
            int i = 1;
            do
            {
                if (Node.Value.Equals(item)) return i;
                i++;
                Node = Node.Next;
            } while (Node.Next != null);
            return -1;
        }

        public void Insert(int index, T item)
        {
            
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
