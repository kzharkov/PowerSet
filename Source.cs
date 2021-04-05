using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures
{

    // наследуйте этот класс от HashTable
    // или расширьте его методами из HashTable
    public class PowerSet<T> : HashTable<T>
    {
        int size;

        public PowerSet()
        {
            size = 0;
        }

        public int Size()
        {
            return size;
        }

        public void Put(T value)
        {
            int slot = HashFun(value);
            if (!Get(value))
            {
                size++;
                slots[slot].AddLast(value);
            }
        }

        public bool Get(T value)
        {
            if (Find(value) != -1)
            {
                return true;
            }
            return false;
        }

        public bool Remove(T value)
        {
            if (Get(value))
            {
                slots[HashFun(value)].Remove(value);
                size--;
                return true;
            }
            return false;
        }

        public PowerSet<T> Intersection(PowerSet<T> set2)
        {
            PowerSet<T> result = new PowerSet<T>();
            foreach (var slot in slots)
            {
                foreach (var item in slot)
                {
                    if (set2.Get(item))
                    {
                        result.Put(item);
                    }
                }
            }
            return result;
        }

        public PowerSet<T> Union(PowerSet<T> set2)
        {
            PowerSet<T> result = new PowerSet<T>();
            foreach (var slot in slots)
            {
                foreach (var item in slot)
                {
                    result.Put(item);
                }
            }
            foreach (var slot in set2.slots)
            {
                foreach (var item in slot)
                {
                    result.Put(item);
                }
            }
            return result;
        }

        public PowerSet<T> Difference(PowerSet<T> set2)
        {
            PowerSet<T> result = new PowerSet<T>();
            foreach (var slot in slots)
            {
                foreach (var item in slot)
                {
                    result.Put(item);
                }
            }
            foreach (var slot in set2.slots)
            {
                foreach (var item in slot)
                {
                    result.Remove(item);
                }
            }
            return result;
        }

        public bool IsSubset(PowerSet<T> set2)
        {
            foreach (var slot in set2.slots)
            {
                foreach (var item in slot)
                {
                    if (!Get(item))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public override bool Equals(object _set2)
        {
            var set2 = _set2 as PowerSet<T>;

            if (set2 == null) return false;

            if (Size() != set2.Size()) return false;
            if (IsSubset(set2) && set2.IsSubset(this)) return true;
            return false;
        }
    }

    public class HashTable<T>
    {
        public int sze;
        public LinkedList<T>[] slots;

        public HashTable()
        {
            sze = 20000;
            slots = new LinkedList<T>[sze];
            for (int i = 0; i < sze; i++) slots[i] = new LinkedList<T>();
        }

        public int HashFun(T value)
        {
            if (typeof(T) == typeof(string))
            {
                int sum = 0;
                Array.ForEach((value as string).ToCharArray(), delegate (char i) { sum += i; });
                return sum % sze;
            }
            return (value.GetHashCode() & 0x7FFFFFFF) % sze;
        }

        public int Find(T value)
        {
            int slot = HashFun(value);
            if (slots[slot].Find(value) != null)
            {
                return slot;
            }
            return -1;
        }
    }
}