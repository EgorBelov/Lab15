using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace LAB_15.Models
{
    public class MyCollection<T> : IEnumerable<T>
        where T : ICloneable, new()// кольцевой связный список
    {
        public Node<T> head; // головной/первый элемент
        public Node<T> tail; // последний/хвостовой элемент
        public int count;  // количество элементов в списке
        public int Count { get { return count; } }
        public bool IsEmpty { get { return count == 0; } }
        public string TextView
        {
            get
            {
                var text = MakeTextView();
                return text;
            }
        }

        public string MakeTextView()
        {
            var text = string.Empty;

            foreach (var item in this)
            {
                text += $"\t{item}\n";
            }
            if (text == string.Empty)
                return "Коллекция пустая";

            return text;
        }

        public MyCollection()
        {

        }
        public MyCollection(int a)
        {
            for (int i = 0; i < a; i++)
            {
                T temp = new T();

                Add(temp);
            }
        }
        public Node<T> this[int index] //итератор
        {
            get
            {
                if (index < Count && index >= 0)
                {
                    Node<T> p = this.head;
                    for (int i = 0; i < index; i++)
                    {
                        p = p.Next;
                    }
                    return p;
                }
                else
                    return null;
            }
            set
            {
                if (index < Count && index >= 0)
                {
                    Node<T> p = this.head;
                    for (int i = 0; i < index; i++)
                    {
                        p = p.Next;
                    }
                    p.Data = value.Data;
                }
            }
        }
        // добавление элемента
        public virtual void Add(T data)
        {
            Node<T> node = new Node<T>(data);
            // если список пуст
            if (head == null)
            {
                head = node;
                tail = node;
                tail.Next = head;
            }
            else
            {
                node.Next = head;
                tail.Next = node;
                tail = node;
            }
            count++;
        }
        public virtual void AddDefault()
        {
            T data = new T();
            Node<T> node = new Node<T>(data);
            // если список пуст
            if (head == null)
            {
                head = node;
                tail = node;
                tail.Next = head;
            }
            else
            {
                node.Next = head;
                tail.Next = node;
                tail = node;
            }
            count++;
        }
        public void Print()
        {
            Console.WriteLine("Список:");
            if (head == null)
            {
                Console.WriteLine("Пустой список!");
            }
            else
            {
                Node<T> p = head;
                while (p != tail)
                {
                    Console.WriteLine(p);
                    p = p.Next;
                }
                Console.WriteLine(p);
                Console.WriteLine();
            }
        }
        public bool RemoveByIndex(int index)
        {
            int count = 0;
            Node<T> current = head;
            Node<T> previous = current;
            if (IsEmpty) return false;

            while (count <= index)
            {
                if (count == index)
                {
                    previous.Next = current.Next;
                    current = null;
                }
                else
                {
                    previous = current;
                    current = current.Next;
                }
                count++;
            }

            return true;
        }

        public virtual void AddFew(T[] data1)
        {
            foreach (var item in data1)
            {
                Add(item);
            }
        }
        public void RemoveFew(T[] data1)
        {
            foreach (var item in data1)
            {
                Remove(item);
            }
        }

        public virtual bool Remove(T data)
        {
            Node<T> current = head;
            Node<T> previous = null;

            if (IsEmpty) return false;

            do
            {
                if (current.Data.Equals(data))
                {
                    // Если узел в середине или в конце
                    if (previous != null)
                    {
                        // убираем узел current, теперь previous ссылается не на current, а на current.Next
                        previous.Next = current.Next;

                        // Если узел последний,
                        // изменяем переменную tail
                        if (current == tail)
                            tail = previous;
                    }
                    else // если удаляется первый элемент
                    {

                        // если в списке всего один элемент
                        if (count == 1)
                        {
                            head = tail = null;
                        }
                        else
                        {
                            head = current.Next;
                            tail.Next = current.Next;
                        }
                    }
                    count--;
                    return true;
                }

                previous = current;
                current = current.Next;
            } while (current != head);

            return false;
        }

        public virtual bool RemoveIndex(int index)
        {
            if (index < Count && index >= 0)
            {
                this.RemoveIndex(index);

                return true;
            }
            else
                return false;
        }


        public void Clear()
        {
            head = null;
            tail = null;
            count = 0;
        }

        public bool Contains(T data)
        {
            Node<T> current = head;
            if (current == null) return false;
            do
            {
                if (current.Data.Equals(data))
                    return true;
                current = current.Next;
            }
            while (current != head);
            return false;
        }

        public MyCollection<T> Clone() // клонирование стека
        {
            MyCollection<T> clone = new MyCollection<T>(); // создаем новый стек для заполнения его клонами элементов

            foreach (var item in this)
            {

                clone.Add(item);
            }
            return clone;
        }

        public object ShallowCopy()
        {
            return MemberwiseClone();
        }
        // реализация интерфейса IEnumerable
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this).GetEnumerator();
        }
        public IEnumerator<T> GetEnumerator()
        {
            Node<T> current = head;
            do
            {
                if (current != null)
                {
                    yield return current.Data;
                    current = current.Next;
                }
            }
            while (current != head);
        }
    }
}