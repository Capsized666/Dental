using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class Interactiv

{
    public Interactiv()
    {
    }

    public Interactiv(string n, string l) {
        name = n;
        link = l;
    }

    public string name;

    public float[] pos;

    public float[] rot;

    public float[] scl;

    public string link;


    public Vector3 getPos()
    {
        if (pos.Length == 0)
        {
            return Vector3.one;
        }
        return new Vector3(pos[0], pos[1], pos[2]);
    }
    public Vector3 getScl()
    {
        if (scl.Length == 0)
        {
            return Vector3.one;
        }
        return new Vector3(scl[0], scl[1], scl[2]);
    }

    public Quaternion getRot()
    {
        if (rot.Length == 0)
        {
            return Quaternion.identity;
        }
        return new Quaternion(rot[0], rot[1], rot[2], rot[3]);
    }

    public void modTransform(Transform t) {
        pos = new float[3] { t.position.x, t.position.y, t.position.z };
        rot = new float[4] { t.rotation.x, t.rotation.y, t.rotation.z, t.rotation.w };
        scl = new float[3] { t.lossyScale.x, t.lossyScale.y, t.lossyScale.z };
    }
}

[System.Serializable]
public class Conects
{
    public Conects(string[] args) {
        name = args;

        pos = new float[3] {1,2,3};
    }   

    public string[] name;

    public float[] pos;

    public float[] rot;

    public float[] scl;

    public string[] drawTo;

    public void setPos(Transform t) {
        pos = new float[3] {t.position.x, t.position.y, t.position.z };
    }
    
    public int Hierarchy()
    {
        return name.Length;
    }



    public Vector3 getPos()
    {
        if (pos.Length == 0)
        {
            return Vector3.one;
        }
        return new Vector3(pos[0], pos[1], pos[2]);
    }
    public Vector3 getScl()
    {
        if (scl.Length==0)
        {
            return Vector3.one;
        }
        return new Vector3(scl[0], scl[1], scl[2]);
    }

    public Quaternion getRot()
    {
        if (rot.Length == 0)
        {
            return Quaternion.identity;
        }
        return new Quaternion(rot[0], rot[1], rot[2], rot[3]);
    }

    public void modTransform(Transform t)
    {
        pos = new float[3] { t.position.x, t.position.y, t.position.z };
        rot = new float[4] { t.rotation.x, t.rotation.y, t.rotation.z, t.rotation.w };
        scl = new float[3] { t.lossyScale.x, t.lossyScale.y, t.lossyScale.z };
    }
}

[System.Serializable]
public class Point
{
    public Point(){
        cordin = new float[2] { 0.35f, 0.3f, };
    }

    public string name;

    public string skyBox;

    public string skyLink;

    public float[] cordin;

    public Conects[] conects;

    public Interactiv[] interactiv;

    public int GetInteractivCount()
    {
        return interactiv.Length;
    }

    public Vector2 GetCordin()
    {
        return new Vector2(cordin[0], cordin[1]);
    }

    public Interactiv GetObjByName(string name)
    {
        foreach (var item in interactiv)
        {
            if (
            item.name == name
                )
            {
                return item;
            }

        }

        return null;
    }

    public Interactiv[] GetArrayByName(string name)
    {
        var l2r = new List<Interactiv>();
        foreach (var item in interactiv)
        {
            if (item.name == name)
            {
                l2r.Add(item);
            }
        }

        return l2r.ToArray();
    }

    public void setCordinate(float[] args) {
        cordin = new float[2] { args[0], args[1] };
    }
}

[System.Serializable]
public class rooms
{
    public string name;

    public Point[] point;

    public Point getPointByName(string nam)
    {
        for (int i = 0; i < point.Length; i++)
        {
            if (nam == point[i].name)
            {
                return point[i];
            }
        }
        return null;
    }

    public string[] getAllPoint()
    {
        var rl = new List<string>();
        foreach (var item in point)
        {
            rl.Add(item.name);
        }
        return rl.ToArray();
    }
}

[System.Serializable]
public class building
{
    public string Name;

    public int Floor;

    public rooms[] Rooms;

    public rooms getRoomByName(string name)
    {
        for (int i = 0; i < Rooms.Length; i++)
        {
            if (name == Rooms[i].name)
            {
                return Rooms[i];
            }
        }
        return null;
    }

    public string[] getAllRooms() {
        
        var rl = new List<string>();
        foreach (var item in Rooms)
        {
            rl.Add(item.name);
        }
        
        return rl.ToArray();
    }




}

[System.Serializable]
public class BuldingList
{
    public building[] Building;

    public building[] getBuildByName(string name)
    {
        var lb = new List<building>();
        for (int i = 0; i < Building.Length; i++)
        {
            if (name == Building[i].Name)
            {
               lb.Add(Building[i]);
            }
        }
        return lb.ToArray();
    }

    public building getFloorInBuildByName(string name, int floor)
    {
        for (int i = 0; i < Building.Length; i++)
        {
            if (name == Building[i].Name & Building[i].Floor == floor)
            {
                return Building[i];
            }
        }
        return null;
    }

    public Vector2[] ListPointsOfMap(string name, int floor)
    {
        List<Vector2> listVector = new List<Vector2>();
        var build = getFloorInBuildByName(name, floor);
        foreach (var room in build.Rooms)
        {
            foreach (var point in room.point)
            {
                listVector.Add(point.GetCordin());
            }
        }
        return listVector.ToArray();
    }

    public string[] getAllBuilding() {
        var returnList = new Dictionary<string,bool>();
        var list4ret = new List<string>();
        
        for (int i = 0; i < Building.Length; i++)
        {
            if (!returnList.ContainsKey(Building[i].Name))
            {
                returnList.Add( Building[i].Name, false);
            }
        }

        foreach (var item in returnList)
        {
            list4ret.Add(item.Key);
        } 
        
        return list4ret.ToArray();
    }

    public string[] getAllFloarInBuilding(string build)
    {
        var bild = getBuildByName(build);
        var list4ret = new List<string>();
        foreach (var item in bild)
        {
            list4ret.Add(
                item.Floor.ToString()
                );
        }
        return list4ret.ToArray();
    }
}

public class GuideMaster : MonoBehaviour
{
    public TextAsset jsonInstruction;
    [Space]
    public BuldingList currentBuid = new BuldingList();
   
    [Space]
    public string   CurrentBuild    = "Main";
    public int      CurrentFloor    = 1     ;
    public string   CurrentRoom     = "Hall";
    public string   CurrentPos      = "pos0";

    [Space]
    public GuideBeh     gb  ;
    public GuideMapBeh  gmb ;
    public MapObjBeh    mob ;
    
    [Space]
    public string path;

    //public GuideBeh gb;

    private void Awake()
    {
        currentBuid = JsonUtility.FromJson<BuldingList>(jsonInstruction.text);
        gb.setGM(this);
        gmb.setGM(this);
        mob.setGM(this);
    }

    private void Start()
    {
    }

    public void Moving(string[] args)
    {
        if (args.Length == 1)
        {
            CurrentPos = args[0];
        }
        if (args.Length == 2)
        {
            CurrentRoom = args[0];
            CurrentPos = args[1];
        }
        if (args.Length == 3)
        {
            CurrentFloor = int.Parse(args[0]);
            CurrentRoom = args[1];
            CurrentPos = args[2];
        }
        if (args.Length == 4)
        {
            CurrentBuild = args[0];
            CurrentFloor = int.Parse(args[1]);
            CurrentRoom = args[2];
            CurrentPos = args[3];
        }
        gb.Moving();
    }

    public string[] GetCurrentPos()
    {
        var posit =new string[4] {
            CurrentBuild,
            CurrentFloor.ToString(),
            CurrentRoom,
            CurrentPos};
        return posit;
    }

    public Point GetPointInfo() {
        var room = GetRoomInfo();
        var point = room.getPointByName(CurrentPos);
        return point;
    }

    public rooms GetRoomInfo() {
        var build = GetBuildInfo();
        var room = build.getRoomByName(CurrentRoom);
        return room;
    }
    
    public building GetBuildInfo()
    {
        var build = currentBuid.getFloorInBuildByName(CurrentBuild, CurrentFloor);
        return build;
    }

    public Point GetSpecificPos(string[] args)
    {
        var build = currentBuid.getFloorInBuildByName(args[0], int.Parse(args[1])).
                                getRoomByName(args[2]).getPointByName(args[3])
                                ;
        return build;
    }

    [ContextMenu("Update Data")]
    public void SavePos() {
        var p       = GetPointInfo();
        var Np2d    = gb.Points2Draw;
        var Nio     = gb.IntObj;
        var npl     = mob.pointList;

        
        foreach (var item in npl)
        {
            var q = item.GetComponent<PointBeh>();
            var tmpP = GetSpecificPos(q.statement);
            tmpP.setCordinate(new float[2] { q.xpos, q.ypos });
            //print($"{q.xpos} {q.ypos}");
            //print($"{tmpP.cordin[0]} {tmpP.cordin[1]}");
        }  


        List<Conects> NConect = new List<Conects>();
        foreach (var item in Np2d)
        {
            var iCon = item.GetComponent<ConnectBeh>();
            var conect = iCon.statement;
            conect.modTransform(item.transform);
            NConect.Add(conect);
        }

        List<Interactiv> NInteractive = new List<Interactiv>();
        foreach (var item in Nio)
        {
            var interactiv = new Interactiv();
            interactiv.name = item.name;
            interactiv.modTransform(item.transform);
            if (item.GetComponent<InfoTableImageBeh>())
            {
                interactiv.link =  item.GetComponent<InfoTableImageBeh>().Link;
            }
            if (item.GetComponent<InfoTableMoviBeh>())
            {
                
                interactiv.link = item.GetComponent<InfoTableMoviBeh>().Link;
            }
            NInteractive.Add(interactiv);
        }

        p.interactiv = NInteractive.ToArray();       
        p.conects = NConect.ToArray();
    }


    [ContextMenu("Write To Json")]
    public void JsonWrite() {
        SavePos();
        var njson = JsonUtility.ToJson(currentBuid,true);
        try
        {
            //Pass the filepath and filename to the StreamWriter Constructor
            StreamWriter stream = new StreamWriter(path,false);
            stream.Write(njson);
            stream.Close();
        }
        catch (Exception e)
        {
            print("Exception: " + e.Message);
        }
     
        print("json is write");
    }

    [ContextMenu("Add Conection")]
    public void AddConection()
    {
        SavePos();
        var oldcon = new List<Conects>();
        foreach (var item in GetPointInfo().conects)
        {oldcon.Add(item);}
        var args = new string[4] {
        CurrentBuild,
        CurrentFloor.ToString(),
        CurrentRoom,
        CurrentPos
        };
        oldcon.Add(new Conects(args));
        GetPointInfo().conects = oldcon.ToArray();
        gb.updateOfAll();

    }
    
    [ContextMenu("Add Position")]
    public void AddPosition() {
        SavePos();
        var oldroom = new List<Point>();
        foreach (var item in GetRoomInfo().point)
        {oldroom.Add(item);}
        oldroom.Add(new Point());
        GetRoomInfo().point = oldroom.ToArray();
        gb.updateOfAll();
    }
    
    [ContextMenu("Add Room")]
    public void AddRoom()
    {
        SavePos();
        var oldroom = new List<rooms>();
        foreach (var item in GetBuildInfo().Rooms)
        { oldroom.Add(item); }
        oldroom.Add(new rooms());
        GetBuildInfo().Rooms = oldroom.ToArray();
        gb.updateOfAll();
    }
    
    [ContextMenu("Add flor & building")]
    public void AddFB()
    {
        SavePos();
        var oldfb = new List<building>();
        foreach (var item in currentBuid.Building)
        { oldfb.Add(item); }
        oldfb.Add(new building());
        currentBuid.Building = oldfb.ToArray();
        /*
         */
        gb.updateOfAll();
    }
    
    [ContextMenu("Add InfoTableImage")]
    public void AddInteractImage()
    {
        SavePos();
        var oldcon = new List<Interactiv>();
        foreach (var item in GetPointInfo().interactiv)
        { oldcon.Add(item);}
        oldcon.Add(new Interactiv("InfoTableImage", "kanev"));
        GetPointInfo().interactiv = oldcon.ToArray();
        gb.updateOfAll();
        
    }
    
    [ContextMenu("Add InfoTableVideo")]
    public void AddInteractVideo()
    {
        SavePos();
        var oldcon = new List<Interactiv>();
        foreach (var item in GetPointInfo().interactiv)
        { oldcon.Add(item); }
        oldcon.Add(new Interactiv("InfoTableMovi", "mozg"));
        GetPointInfo().interactiv = oldcon.ToArray();
        gb.updateOfAll();
        
        
    }

}

    