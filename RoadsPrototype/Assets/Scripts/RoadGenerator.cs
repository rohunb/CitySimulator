using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class RoadGenerator : MonoBehaviour 
{
    [SerializeField]
    private float roadWidth = 5.0f;
    [SerializeField]
    private Material roadMaterial;

    //[SerializeField]
    //private Transform startPos;
    //[SerializeField]
    //private Transform endPos;
    
    private RoadGeneratorInput roadGenInput;

    private void Awake()
    {
        Assert.IsTrue(roadWidth > 0.0f);
        Assert.IsNotNull(roadMaterial);

        roadGenInput = gameObject.GetSafeComponent<RoadGeneratorInput>();
    }

    private void Start()
    {
        //Assert.IsNotNull(startPos);
        //Assert.IsNotNull(endPos);
        //Assert.IsTrue(Vector3.Distance(startPos.position, endPos.position) > 0.0f);

        //GenerateRoadMesh(startPos.position, endPos.position);

        roadGenInput.OnCreateRoadInputReceived += GenerateRoadMesh;
    }

    public void GenerateRoadMesh(Vector3 startPos, Vector3 endPos)
    {
        Vector3 roadDirection = (endPos - startPos).normalized;

        Vector3 roadDirLeftNormal = new Vector3(-roadDirection.z, 0.0f, roadDirection.x);
        Vector3 roadDirRightNormal = new Vector3(roadDirection.z, 0.0f, -roadDirection.x);

        Vector3 vertex0 = endPos + roadDirLeftNormal * roadWidth + Vector3.up * 0.2f;
        Vector3 vertex1 = endPos + roadDirRightNormal * roadWidth + Vector3.up * 0.2f;
        Vector3 vertex2 = startPos + roadDirRightNormal * roadWidth + Vector3.up * 0.2f;
        Vector3 vertex3 = startPos + roadDirLeftNormal * roadWidth + Vector3.up * 0.2f;

        Vector3[] vertices = new Vector3[] { vertex0, vertex1, vertex2, vertex3 };

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

        GameObject roadObject = new GameObject("RoadObject");
        
        MeshFilter meshFilter = roadObject.AddComponent<MeshFilter>();
        meshFilter.sharedMesh = new Mesh();

        Mesh mesh = meshFilter.sharedMesh;
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.normals = normals;
        mesh.uv = uvs;
        mesh.triangles = triangles;

        MeshRenderer renderer = roadObject.AddComponent<MeshRenderer>();
        renderer.material = roadMaterial;
    }
}
