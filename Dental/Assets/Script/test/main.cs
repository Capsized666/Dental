using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct main
{
    public obj4load[] objectList;
    public obj4load getByName(string name)
    {

        foreach (var item in objectList)
        {
            if (name == item.name)
            {
                return item;
            }
        }
        obj4load ver = new obj4load();
        ver.empty();
        return ver;
    }
    public void AddObj(obj4load obj)
    {

        List<obj4load> vrem = new List<obj4load>();

        if (objectList != null)
        {
            foreach (var item in objectList)
            {
                vrem.Add(item);
            }
        }
        vrem.Add(obj);
        objectList = vrem.ToArray();
    }
    public bool containsObj(obj4load obj)
    {
        for (int i = 0; i < objectList.Length; i++)
        {
            if (objectList[i].Equals(obj))
            {
                return true;
            }
        }
        return false;
    }

    public bool containsObj(GameObject obj)
    {
        foreach (var item in objectList)
        {
            obj4load nobj = new obj4load();
            nobj = nobj.convertGO(obj);
            if (item.Equals(nobj))
            {
                return true;
            }
        }
        return false;
    }

    public obj4load getByHashCode(int hash)
    {
        foreach (var item in objectList)
        {
            if (item.HashCode == hash)
            {
                return item;
            }
        }
        return new obj4load();
    }

    public obj4load findParent(string name, string parent)
    {
        foreach (var item in objectList)
        {
            if (item.child != null & item.name == parent)
            {
                foreach (var chil in item.child)
                {
                    if (chil == name)
                    {
                        return item;
                    }
                }
            }
        }
        return new obj4load();
    }
}
