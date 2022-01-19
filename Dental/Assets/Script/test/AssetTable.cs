using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using System.IO;
using UnityEditor;
 
[System.Serializable]
public class addressableGUIDName
{
    public string guid;
    public string name;
}
[System.Serializable]
public class addressableGuidNameList
{
    public List<addressableGUIDName> GuidNameData;

    public bool canBeLoaded(string s) {
        foreach (var item in GuidNameData)
        {
            if (item.name == s)
            {
                return true;
            }
        }
        return false;
    }
}
#if UNITY_EDITOR
[InitializeOnLoad]
public static class AssetTable
{

    static AssetTable()
    {

        addressableGuidNameList addressableListObject = new addressableGuidNameList();
        addressableListObject.GuidNameData = new List<addressableGUIDName>();

        var settings = UnityEditor.AddressableAssets.AddressableAssetSettingsDefaultObject.Settings;
        
        foreach (var group in settings.groups)
        {
            foreach (var entry in group.entries)
            {
                addressableGUIDName newitem = new addressableGUIDName();
                newitem.name = entry.address.ToString();
                newitem.guid = entry.guid.ToString();
                addressableListObject.GuidNameData.Add(newitem);
            }
        }
                
        
        string json = JsonUtility.ToJson(addressableListObject, true);
        var path = Path.Combine(Application.dataPath + "/Resources", "assetTable.json");
        //File.WriteAllText(Application.persistentDataPath + "/Resources/assetTable.json", json);
        try
        {

            if (File.Exists(path))
            {
                File.Delete(path);
            }
            FileStream fileStream = new FileStream(path,
                                       FileMode.OpenOrCreate,
                                       FileAccess.ReadWrite,
                                       FileShare.None);
            if (fileStream.CanWrite)
            {
                byte[] arr = System.Text.Encoding.Default.GetBytes(json);
                fileStream.Write(arr, 0, arr.Length);
            }
            fileStream.Close();
        }
        catch (System.Exception e)
        {
            Debug.Log($"Pizda togo sho {e.ToString()}");
        }
    }
}
#endif