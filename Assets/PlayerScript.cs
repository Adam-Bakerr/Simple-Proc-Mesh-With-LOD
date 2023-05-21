using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Vector3 moveSpeed;
    public bool move = false;
    public Settings settings;
    public Vector3 currentChunk;
    public int renderDistance = 100;
    public int chunksViewable;
    public List<GameObject> chunksVisibleLastFrame = new List<GameObject>();
    void Start()
    {
        chunksViewable = Mathf.RoundToInt(renderDistance / settings.CellSize);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (move)
        {
            transform.position += moveSpeed * Time.deltaTime;
        }

        updateVisibleChunks();
    }

    public void updateVisibleChunks()
    {
        foreach (GameObject chunk in chunksVisibleLastFrame)
        {
            chunk.SetActive(false);
        }
        chunksVisibleLastFrame.Clear();
        int currentChunkPositionX = Mathf.RoundToInt(transform.position.x / settings.CellSize);
        int currentChunkPositionZ = Mathf.RoundToInt(transform.position.z / settings.CellSize);
        currentChunk = new Vector3(currentChunkPositionX,0, currentChunkPositionZ);
        for (int yOffset = -chunksViewable; yOffset <= chunksViewable; yOffset++)
        {
            for (int xOffset = -chunksViewable; xOffset <= chunksViewable; xOffset++)
            {
                GameObject tempChunk;
                Vector3 tempChunkVector3 = currentChunk;
                tempChunkVector3.x += xOffset;
                tempChunkVector3.z += yOffset;
                if (settings.LoadedMeshes.TryGetValue(tempChunkVector3, out tempChunk))
                {
                    settings.LoadedMeshes[tempChunkVector3].SetActive(true);
                    chunksVisibleLastFrame.Add(settings.LoadedMeshes[tempChunkVector3]);
                }
                else
                {
                    tempChunk = Instantiate(settings.mesh, tempChunkVector3 * settings.CellSize,Quaternion.identity);
                    settings.LoadedMeshes.Add(tempChunkVector3, tempChunk);
                    chunksVisibleLastFrame.Add(settings.LoadedMeshes[tempChunkVector3]);
                }
            }
        }
    }
}
