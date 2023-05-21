using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Settings : ScriptableObject
{
    //Size Of Chunk In Units
    public float CellSize = 4;
    //Size Of Mesh In Verts
    public int MeshSize;
    //Mesh GameObject
    public GameObject mesh;
    [System.Serializable]
    public struct distanceStruct
    {
        public int Distance;
        public int Lod;
    }


    public distanceStruct[] RenderDistance;

    //Coords Of Mesh And Mesh Itself
    public Dictionary<Vector3, GameObject> LoadedMeshes = new();

}
