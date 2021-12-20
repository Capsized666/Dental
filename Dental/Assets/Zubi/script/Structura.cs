using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structura : MonoBehaviour
{
    Mesh cMesh;
    public bool ShowGizmo=true;
    public float Hardnes;



    private void OnTriggerEnter(Collider other)
    {
        //foreach (var item in other)
        //{
        //    print($"{gameObject.name}>{item}>{item.point}>");
        //
        //}
        
        //print($"{gameObject.name}>{other.gameObject.name}");
        //var Otransform = other.transform;
        //var Omesh = other.gameObject.GetComponent<MeshFilter>().mesh;
        //var OGO = other.gameObject;
        //if (OGO.GetComponent<Structura>())
        //{
        //    var ostruc = OGO.GetComponent<Structura>();
        //    if (ostruc.Hardnes>Hardnes)
        //    {
        //
        //    }
        //}

    }
    private void OnTriggerStay(Collider other)
    {
        
    }
    private void OnTriggerExit(Collider other)
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {//collision.GetContacts(collision.contacts)
     //foreach (var item in collision.contacts)
     //{
     //    print($"{gameObject.name}>{item}>{item.point}>");
     //
     //}
        
        var OGO = collision.gameObject;
        if (OGO.GetComponent<Structura>())
        {
            var ostruc = OGO.GetComponent<Structura>();
            if (ostruc.Hardnes>Hardnes)
            {
                //var Omesh   = collision.gameObject.GetComponent<MeshFilter>().mesh;
                var vertes  = cMesh.vertices;
                var tiang   = cMesh.triangles;

                List<Vector3> newvert = new List<Vector3>();
                
                for (int i = 0; i < vertes.Length; i++)
                {
                    newvert.Add(vertes[i]);
                }
                foreach (var item in collision.contacts)
                {
                    var point= new Vector3(
                         item.point.x,//+transform.position.x,
                         item.point.y,//+transform.position.y,
                         item.point.z//+transform.position.z  
                    );
                    point = new Vector3(
                         point.x-transform.position.x, //-transform.position.x OGO.transform.position.x- transform.position.x,//*Mathf.Sin(transform.rotation.eulerAngles.x),
                         point.y-transform.position.y, //-transform.position.y OGO.transform.position.y- transform.position.y,//*Mathf.Sin(transform.rotation.eulerAngles.y),
                         point.z-transform.position.z //-transform.position.z OGO.transform.position.z- transform.position.z// * Mathf.Sin(transform.rotation.eulerAngles.z)
                    );
                    //point = transform.rotation * point;

                    point = Quaternion.Euler(-transform.rotation.eulerAngles.x,
                                             -transform.rotation.eulerAngles.y,
                                             -transform.rotation.eulerAngles.z) * point;


                    newvert.Add(point);
                }

                cMesh.Clear();
                cMesh.vertices = newvert.ToArray();
                cMesh.triangles = tiang;
                cMesh.RecalculateNormals();
            }
        }
    }
        

        




    void OnDrawGizmos()
    {
        if (ShowGizmo)
        {

            if (cMesh==null)
            {
                return;
            }
            var vert = cMesh.vertices;
            for (int i = 0; i < vert.Length; i++)
            {
                vert[i] = new Vector3(
                        vert[i].x*transform.lossyScale.x,//*Mathf.Sin(transform.rotation.eulerAngles.x),
                        vert[i].y*transform.lossyScale.y,//*Mathf.Sin(transform.rotation.eulerAngles.y),
                        vert[i].z*transform.lossyScale.z// * Mathf.Sin(transform.rotation.eulerAngles.z)
                    );

                vert[i] = Quaternion.Euler(transform.rotation.eulerAngles.x,
                    transform.rotation.eulerAngles.y,
                    transform.rotation.eulerAngles.z) * vert[i];
            } 
            
            for (int i = 0; i < cMesh.vertexCount; i++)
            {
                Gizmos.DrawSphere(vert[i]+transform.position, .009f);
            }
            
        }
    }


    void Awake()
    {
        cMesh = GetComponent<MeshFilter>().mesh;
    }

    void Update()
    {
        //DrawGizmos();
    }
}
