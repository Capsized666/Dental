using System;
using UnityEngine;

[Serializable]
public struct obj4load
{
    public int HashCode;
    public string name;
    public float[] position;
    public float[] rotation;
    public float[] scale;
    public string parent;
    public string[] child;

    public void fillFields(Transform t)
    {
        name = t.gameObject.name;
        position = new float[3]{
                t.position.x,
                t.position.y,
                t.position.z
                };
        rotation = new float[4]{
                t.rotation.w,
                t.rotation.x,
                t.rotation.y,
                t.rotation.z
                };
        scale = new float[3] {
                t.lossyScale.x,
                t.lossyScale.y,
                t.lossyScale.z
                };
        if (t.parent == null)
        {
            parent = "root";
        }
        else
        {
            parent = t.parent.name;
        }
        if (t.childCount != 0)
        {
            child = new string[t.childCount];
            for (int i = 0; i < t.childCount; i++)
            {
                child[i] = t.GetChild(i).gameObject.name;
            }
        }

    }
    public void getPos(out Vector3 p, out Quaternion r, out Vector3 s)
    {
        p = new Vector3(position[0], position[1], position[2]);
        r = new Quaternion(rotation[0], rotation[1], rotation[2], rotation[3]);
        s = new Vector3(scale[0], scale[1], scale[2]);
    }
    public void empty()
    {
        position = new float[3]{
                0,
                0,
                0
                };
        var q = Quaternion.identity;
        rotation = new float[4]{
                q.w,
                q.x,
                q.y,
                q.z
                };
        scale = new float[3] {
                1,
                1,
                1
                };
        parent = "root";
        child = null;
        name = "empty";
    }
    public bool compare(obj4load cobj)
    {
        return (name == cobj.name)
            & (position[0] == cobj.position[0])
            & (position[1] == cobj.position[1])
            & (position[2] == cobj.position[2])

            & (rotation[0] == cobj.rotation[0])
            & (rotation[1] == cobj.rotation[1])
            & (rotation[2] == cobj.rotation[2])
            & (rotation[3] == cobj.rotation[3])


            & (scale[0] == cobj.scale[0])
            & (scale[1] == cobj.scale[1])
            & (scale[2] == cobj.scale[2])


            & (parent == cobj.parent)

            & (compareChild(cobj.child));
    }
    bool compareChild(string[] args)
    {
        if (child == null && args == null)
        {
            return true;
        }
        if (child != null && args == null)
        {
            return false;
        }
        if (args.Length != child.Length)
        {
            return false;
        }
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] != child[i])
            {
                return false;
            }
        }
        return true;
    }
    public obj4load convertGO(GameObject obj)
    {
        obj4load nobj = new obj4load();
        nobj.fillFields(obj.transform);
        //nobj.name = obj.name;
        return nobj;
    }

    internal bool compareTransform(GameObject item)
    {
        return (item.name == name) &
            (item.transform.position[0] == position[0])
            & (position[1] == item.transform.position[1])
            & (position[2] == item.transform.position[2])

            & (rotation[0] == item.transform.rotation[0])
            & (rotation[1] == item.transform.rotation[1])
            & (rotation[2] == item.transform.rotation[2])
            & (rotation[3] == item.transform.rotation[3])


            & (scale[0] == item.transform.lossyScale[0])
            & (scale[1] == item.transform.lossyScale[1])
            & (scale[2] == item.transform.lossyScale[2])

            ;
    }
}
