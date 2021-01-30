using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public Rect exclusionZone;

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
        for(int i = 0; i < terrainObjects.Length; i++)
        {
            for(int j = 0; j < terrainObjects[i].count; j++)
            {
                float x = exclusionZone.center.x;
                float z = exclusionZone.center.y;


                while (x > exclusionZone.xMin && x < exclusionZone.xMax && z > exclusionZone.yMin && z < exclusionZone.yMax)
                {
                    x = Random.Range(-mSize.x / 2, mSize.x / 2);
                    z = Random.Range(-mSize.z / 2, mSize.z / 2);
                }

                GameObject newTerrainObject = Instantiate(terrainObjects[i].envObj, new Vector3(x, 0, z), Quaternion.identity);// Busts the scale of the objects, gameObject.transform);
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
