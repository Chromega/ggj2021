using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    [System.Serializable]
    public struct TerrainEnvironmentObject
    {
        public GameObject envObj;
        public int count;
    }
    public TerrainEnvironmentObject[] terrainObjects;


    // Start is called before the first frame update
    void Start()
    {
        Collider mCollider = GetComponent<MeshCollider>();
        Vector3 mSize = mCollider.bounds.size;
        print(mSize);
        for(int i = 0; i < terrainObjects.Length; i++)
        {
            for(int j = 0; j < terrainObjects[i].count; j++)
            {
                Instantiate(terrainObjects[i].envObj, new Vector3(Random.Range(-mSize.x / 2, mSize.x / 2), 0, Random.Range(-mSize.z / 2, mSize.z / 2)), Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
