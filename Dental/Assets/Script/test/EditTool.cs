#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


[ExecuteInEditMode]
public class EditTool : MonoBehaviour
{
    public TextAsset spawnObject;
    public string nameofAsset;
    

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        
    }

     public void WriteObjects() {
        var comend = Comendant.Instance;
        //comend.gameObject.transform.childCount;
        //print(path);

        var path = Path.Combine(Application.dataPath + "/Resources", nameofAsset+".json");
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
                byte[] arr = System.Text.Encoding.Default.GetBytes(comend.gameObject.transform.childCount.ToString());
                fileStream.Write(arr, 0, arr.Length);
            }
            fileStream.Close();
        }
        catch (System.Exception e)
        {
            print($"Pizda togo sho {e.ToString()}");
        }
       
    }
}
#endif