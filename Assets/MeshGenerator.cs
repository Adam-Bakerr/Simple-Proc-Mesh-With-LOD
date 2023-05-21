using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter),typeof(MeshRenderer))]
public class MeshGenerator : MonoBehaviour
{
    Mesh mesh;
    public int[] Triangles;
    public Vector3[] Verticies;
    public bool showGizmos = true;
    MeshFilter meshFilter;
    MeshRenderer meshRenderer;


    public Settings settings;
    public int CellSize;
    private float cellSize = 4;
    
    public int currentLod = 4;
    public GameObject player;

    MeshGenerator()
    {
        
    }

    void Start()
    {
        cellSize = (float)settings.CellSize / settings.MeshSize;
        player = GameObject.FindGameObjectWithTag("Player");
        mesh = new Mesh();
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();

        meshFilter.mesh = mesh;
       
        constructMesh();
    }
    
    public void Update()
    {
        int tempLod = 1;
        foreach (Settings.distanceStruct dis in settings.RenderDistance)
        {
            if (Vector3.Distance(transform.position, player.transform.position)/(settings.MeshSize*cellSize) > dis.Distance)
            {
                tempLod = dis.Lod;
            }
        }
        if (currentLod != tempLod)
        {
            currentLod = tempLod;
            constructMesh();
        }
    }



    public void constructMesh()
    {
        Verticies = new Vector3[((settings.MeshSize / currentLod) + 1) * ((settings.MeshSize / currentLod) +1)];
        for (int x = 0 - (settings.MeshSize / 2), vertexIndex = 0; x <= settings.MeshSize - (settings.MeshSize / 2); x += currentLod)
        {
            for (int z = 0 - (settings.MeshSize / 2); z <= settings.MeshSize - (settings.MeshSize / 2); z += currentLod)
            {
                Verticies[vertexIndex] = new Vector3(z*cellSize, 0, x*cellSize);
                vertexIndex++;
            }
        }
        
        Triangles = new int[(settings.MeshSize / currentLod) * (settings.MeshSize / currentLod) * 6];
        for (int z = 0,tri = 0,vert = 0 ; z < settings.MeshSize / currentLod; z++)
        {
            for (int x = 0; x < settings.MeshSize/ currentLod; x ++)
            {
                Triangles[tri + 2] = vert + 0;
                Triangles[tri + 1] = vert + 1;
                Triangles[tri + 0] = vert + (settings.MeshSize/ currentLod) + 1;
                Triangles[tri + 5] = vert + 1;
                Triangles[tri + 4] = vert + (settings.MeshSize / currentLod) + 2 ;
                Triangles[tri + 3] = vert + (settings.MeshSize / currentLod) + 1;
                vert++;
                tri += 6;
            }
            vert++;
        }
        mesh.Clear();
        mesh.vertices = Verticies;
        mesh.triangles = Triangles;
        mesh.RecalculateNormals();

    }



    /*public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (Verticies.Length > 0 && showGizmos) {
            foreach (Vector3 vert in Verticies)
            {
                Gizmos.DrawSphere(vert, .1f);
            } 
        }
        
    }*/
    


}
