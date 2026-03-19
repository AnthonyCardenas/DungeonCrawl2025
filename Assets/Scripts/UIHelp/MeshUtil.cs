using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//Look at Jaredbest code monkey github

public static class MeshUtil
{

    private static Quaternion[] cachedQuaternionEulerArr;

    private static void CacheQuaternionEuler()
    {
        if(cachedQuaternionEulerArr != null) return;
        cachedQuaternionEulerArr = new Quaternion[360];
        for( int i = 0; i < 360; i++ )
        {
            cachedQuaternionEulerArr[i] = Quaternion.Euler(0, 0, i);
        }
    }
    
    private static Quaternion GetQuaternionEuler(float rotFloat) {
        int rot = Mathf.RoundToInt(rotFloat);
        rot = rot % 360;
        if (rot < 0) rot += 360;
        //if (rot >= 360) rot -= 360;
        if (cachedQuaternionEulerArr == null) CacheQuaternionEuler();
        return cachedQuaternionEulerArr[rot];
    }

    public static void CreateEmptyMeshArrays(int quadCount, out Vector3[] vertices, out Vector2[]uvs, out int[] triangles)
    {
        vertices = new Vector3[4 * quadCount];
        uvs = new Vector2[4 * quadCount];
        triangles = new int[6 * quadCount];
    }

     public static void AddToMeshArrays(Vector3[] vertices, Vector2[] uvs, int[] triangles, int index, Vector3 pos, float rot, Vector3 baseSize, Vector2 uv00, Vector2 uv11) {
		//Relocate vertices
		int vIndex = index*4;
		int vIndex0 = vIndex;
		int vIndex1 = vIndex+1;
		int vIndex2 = vIndex+2;
		int vIndex3 = vIndex+3;

        baseSize *= .5f;

        bool skewed = baseSize.x != baseSize.y;
        if (skewed) {
			vertices[vIndex0] = pos+GetQuaternionEuler(rot)*new Vector3(-baseSize.x,  baseSize.y);
			vertices[vIndex1] = pos+GetQuaternionEuler(rot)*new Vector3(-baseSize.x, -baseSize.y);
			vertices[vIndex2] = pos+GetQuaternionEuler(rot)*new Vector3( baseSize.x, -baseSize.y);
			vertices[vIndex3] = pos+GetQuaternionEuler(rot)*baseSize;
		} else {
			vertices[vIndex0] = pos+GetQuaternionEuler(rot-270)*baseSize;
			vertices[vIndex1] = pos+GetQuaternionEuler(rot-180)*baseSize;
			vertices[vIndex2] = pos+GetQuaternionEuler(rot- 90)*baseSize;
			vertices[vIndex3] = pos+GetQuaternionEuler(rot-  0)*baseSize;
		}
		
		//Relocate UVs
		uvs[vIndex0] = new Vector2(uv00.x, uv11.y);
		uvs[vIndex1] = new Vector2(uv00.x, uv00.y);
		uvs[vIndex2] = new Vector2(uv11.x, uv00.y);
		uvs[vIndex3] = new Vector2(uv11.x, uv11.y);
		
		//Create triangles
		int tIndex = index*6;
		
		triangles[tIndex+0] = vIndex0;
		triangles[tIndex+1] = vIndex3;
		triangles[tIndex+2] = vIndex1;
		
		triangles[tIndex+3] = vIndex1;
		triangles[tIndex+4] = vIndex3;
		triangles[tIndex+5] = vIndex2;
    }

    // public static Mesh CreateEmptyMesh()
    // {
    //     Mesh mesh = new Mesh();
    //     mesh.vertices = new Vector3[0];
    //     mesh.uv = new Vector2[0];
    //     mesh.triangles = new int[0];
    //     return mesh;
    // }

    // public static Mesh CreateMesh(Vector3 pos, float rot, Vector3 baseSize, Vector2 uv00, Vecto2 uv11)
    // {
    //     return AddToMesh(null, pos, rot, baseSize, uv00, uv11);
    // }

    // public static Mesh AddToMesh(Vector3 pos, float rot, Vector3 baseSize, Vector2 uv00, Vector2 uv11)
    // {
    //     // Initialize Variables
    //     if(mesh== null)
    //     {
    //         mesh = CreateEmptyMesh();
    //     }
    //     Vector3[] vertices = new Vector3[4 + mesh.vertices.Length];
    //     Vector2[] uvs = new Vector2[4 + mesh.uv.Length];
    //     int[] triangles = new int[6 + mesh.triangles.Length];

    //     // If preexisting, copy over
    //     Mesh mesh = new Mesh();
    //     mesh.vertices.CopyTo(vertices, 0);
    //     mesh.uv.CopyTo(uvs, 0);
    //     mesh.CopyTo(triangles, 0);

    //     // Vertices for a square
    //     int index = vertices.Length / 4 - 1;
    //     // Relocate Vertices
    //     int vIndex = index^4;
    //     int vIndex0 = vIndex;
    //     int vIndex1 = vIndex+1;
    //     int vIndex2 = vIndex+2;
    //     int vIndex3 = vIndex+3;


    //     // place the vertices
    //     uv[0] = new Vector3(0,   0); 
    //     uv[0] = new Vector3(0,   100); 
    //     uv[0] = new Vector3(100, 100); 
    //     uv[0] = new Vector3(100, 0); 

    //     // place each corner
    //     uv[0] = new Vector2(0, 0); 
    //     uv[0] = new Vector2(0, 1); 
    //     uv[0] = new Vector2(1, 1); 
    //     uv[0] = new Vector2(1, 0); 

    //     // place each triangle
    //     triangles[0] = 0; 
    //     triangles[1] = 1; 
    //     triangles[2] = 2; 

    //     triangles[3] = 0; 
    //     triangles[4] = 2; 
    //     triangles[5] = 1; 

    //     mesh.vertices = vertices;
    //     mesh.uv = uv;
    //     mesh.triangles = triangles;

    //     return mesh;
    // }

    // private void AddToMeshArrays() {}

    // private void CreateTileMesh()
    // {
    // }

}