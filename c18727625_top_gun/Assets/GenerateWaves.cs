using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateWaves : MonoBehaviour
{
    int heightScale = 5;
    float detailScale = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        //get the plane mesh filter
        Mesh mesh = this.GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;

        //for each vertex in the plane..
        for(int v = 0; v < vertices.Length; v++)
        {
            //assign the height of the vertex to be a value random value that allows for smooth transition when joining 2 planes
            vertices[v].y = Mathf.PerlinNoise((vertices[v].x + this.transform.position.x)/detailScale,
                     (vertices[v].z + this.transform.position.z)/detailScale)*heightScale;
        }

        mesh.vertices = vertices;
        mesh.RecalculateBounds();
        //update the normals after modifying the vertices
        mesh.RecalculateNormals();
        //allow for collision on the plane
        this.gameObject.AddComponent<MeshCollider>();
        
    }
}
