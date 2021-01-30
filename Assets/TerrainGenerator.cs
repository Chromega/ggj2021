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
                GameObject newTerrainObject = Instantiate(terrainObjects[i].envObj, new Vector3(Random.Range(-mSize.x / 2, mSize.x / 2), 0, Random.Range(-mSize.z / 2, mSize.z / 2)), Quaternion.identity);// Busts the scale of the objects, gameObject.transform);
                Vector3 randomRotationVector = new Vector3(0, Random.Range(0, 360), 0);
                newTerrainObject.transform.Rotate(randomRotationVector);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
