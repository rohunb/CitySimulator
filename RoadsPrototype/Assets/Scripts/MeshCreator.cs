using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class MeshCreator : MonoBehaviour 
{
    void Start()
    {
        Vector3[] vertices = new Vector3[]
        {
            new Vector3(-1.0f, 0.0f, 1.0f),
            new Vector3(1.0f, 0.0f, 1.0f),
            new Vector3(1.0f, 0.0f, -1.0f),
            new Vector3(-1.0f, 0.0f, -1.0f),
        };

        Vector3[] normals = new Vector3[]
        {
            new Vector3(0.0f, 1.0f, 0.0f),
            new Vector3(0.0f, 1.0f, 0.0f),
            new Vector3(0.0f, 1.0f, 0.0f),
            new Vector3(0.0f, 1.0f, 0.0f)
        };

        Vector2[] uvs = new Vector2[]
        {
            new Vector2(0, 1),
            new Vector2(1, 1),
            new Vector2(1, 0),
            new Vector2(0, 0)
        };

        int[] triangles = new int[]
        {
            0, 1, 2,
            2, 3, 0
        };

        MeshFilter meshFilter = GetComponent<MeshFilter>();

        if (meshFilter.sharedMesh == null)
        {
            meshFilter.sharedMesh = new Mesh();
        }

        Mesh mesh = meshFilter.sharedMesh;

        Assert.IsNotNull(mesh);

        mesh.Clear();

        mesh.vertices = vertices;
        mesh.normals = normals;
        mesh.uv = uvs;
        mesh.triangles = triangles;


    }
}
