using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RunTimeSet<T> : ScriptableObject
{
    private List<T> items = new();

    public T GetListMember(int x)
    {
        return items[x];
    }

    public List<T> GetList()
    {
        return this.items;
    }

    public void Add(T t)
    {
        if (!items.Contains(t)) items.Add(t);
    }

    public void Remove(T t)
    {
        if (items.Contains(t)) items.Remove(t);
    }

}
