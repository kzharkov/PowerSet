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
            int slot = SeekSlot(value);
            if (slot != -1 && slots[slot] == null)
            {
                slots[slot] = value;
                size++;
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
            if (Find(value) != -1)
            {
                slots[SeekSlot(value)] = default;
                size--;
                return true;
            }
            return false;
        }

        public PowerSet<T> Intersection(PowerSet<T> set2)
        {
            PowerSet<T> result = new PowerSet<T>();
            foreach (T item in slots)
            {
                if (item != null && set2.Get(item))
                {
                    result.Put(item);
                }
            }
            return result;
        }

        public PowerSet<T> Union(PowerSet<T> set2)
        {
            PowerSet<T> result = new PowerSet<T>();
            foreach (T item in slots)
            {
                if (item != null)
                {
                    result.Put(item);
                }
            }
            foreach (T item in set2.slots)
            {
                if (item != null)
                {
                    result.Put(item);
                }
            }
            return result;
        }

        public PowerSet<T> Difference(PowerSet<T> set2)
        {
            PowerSet<T> result = new PowerSet<T>();
            foreach (T item in slots)
            {
                if (item != null)
                {
                    result.Put(item);
                }
            }
            foreach (T item in set2.slots)
            {
                if (item != null)
                {
                    result.Remove(item);
                }
            }
            return result;
        }

        public bool IsSubset(PowerSet<T> set2)
        {
            foreach (T item in set2.slots)
            {
                if (item != null && !Get(item))
                {
                    return false;
                }
            }
            return true;
        }
    }

    public class HashTable<T>
    {
        public int sze;
        public int step;
        public T[] slots;

        public HashTable()
        {
            sze = 20000;
            step = 3;
            slots = new T[sze];
            for (int i = 0; i < sze; i++) slots[i] = default;
        }

        public int HashFun(T value)
        {
            return (value.GetHashCode() & 0x7FFFFFFF) % sze;
        }

        public int SeekSlot(T value)
        {
            int slot = HashFun(value);
            int startSlot = slot;
            while (slots[slot] != null && !slots[slot].Equals(value))
            {
                slot += step;
                slot %= sze;
                if (slot == startSlot) return -1;
            }
            return slot;
        }

        public int Find(T value)
        {
            int slot = HashFun(value);
            int startSlot = slot;
            while (slots[slot] != null)
            {
                if (slots[slot].Equals(value))
                {
                    return slot;
                }
                slot += step;
                slot %= sze;
                if (slot == startSlot) break;
            }
            return -1;
        }
    }
}