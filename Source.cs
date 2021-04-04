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
            if (slots[slot] == null)
            {
                size++;
            }
            slots[slot] = value;
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
                slots[HashFun(value)] = default;
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
        public T[] slots;

        public HashTable()
        {
            sze = 17;
            slots = new T[sze];
            for (int i = 0; i < sze; i++) slots[i] = default;
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
            if (slots[slot] != null && slots[slot].Equals(value))
            {
                return slot;
            }
            return -1;
        }
    }
}