using System;
using System.Collections.Generic;

interface IRepository<T> where T : Product
{
    bool Add(T item);
    T GetByKey(string key);
    List<T> GetAll();
    bool Exists(string key);
    bool Remove(string key);
    bool UpdateQuantity(string key, int newQuantity);
    List<T> Find(Func<T, bool> filter);
}