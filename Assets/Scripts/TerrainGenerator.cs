using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public Rect exclusionZone;
    float totalRadius = 50;

    [System.Serializable]
    public struct TerrainEnvironmentObject
    {
        public GameObject envObj;
        public int count;
    }
    public TerrainEnvironmentObject[] terrainObjects;

    public GameObject[] goodObjects;
    public float spawnDelay = 10.0f;
    float currDelay = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        Collider mCollider = GetComponent<MeshCollider>();
        Vector3 mSize = mCollider.bounds.size;
        float exclusionRadius = 10f;

        for(int i = 0; i < terrainObjects.Length; i++)
        {
            for(int j = 0; j < terrainObjects[i].count; j++)
            {
                float theta = Random.Range(0, 2 * Mathf.PI);
                //sqrt(rnd() * (R1 ^ 2 - R2 ^ 2) + R2 ^ 2)
                //float distance = Mathf.Sqrt(Random.Range(0, 1) * (exclusionRadius * exclusionRadius - totalRadius * totalRadius) + totalRadius * totalRadius);// Random.Range(exclusionRadius, totalRadius) * Mathf.Sqrt(Random.Range(0, 1)); ;
                float distance = Mathf.Sqrt(Random.Range(exclusionRadius * exclusionRadius, totalRadius * totalRadius));
                float x = distance * Mathf.Cos(theta);
                float z = distance * Mathf.Sin(theta);

                GameObject newTerrainObject = Instantiate(terrainObjects[i].envObj, new Vector3(x, 0, z), Quaternion.identity);
                Vector3 randomRotationVector = new Vector3(0, Random.Range(0, 360), 0);
                newTerrainObject.transform.Rotate(randomRotationVector);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        currDelay -= Time.deltaTime;
        if(currDelay < 0)
        {
            print("SPAWN GOOD ITEM");
            currDelay = spawnDelay;// * (70 / GameManager.Instance.forestHealth));
            SpawnGoodItems();
        }
    }

    void SpawnGoodItems()
    {
        float spawnCount = GameManager.Instance.forestHealth / 5;
        for (int i = 0; i < spawnCount && GameManager.Instance.forestHealth > 10.0f; i++)
        {
            float exclusionRadius = 10f;
            GameObject newGoodItem = goodObjects[Random.Range(0, goodObjects.Length)];
            float forestHealthRadius = GameManager.Instance.forestHealth - 15;// / 2;

            float theta = Random.Range(0, 2 * Mathf.PI);
            //float distance = Random.Range(exclusionRadius, exclusionRadius + forestHealthRadius);
            float distance = Mathf.Sqrt(Random.Range(exclusionRadius * exclusionRadius, (exclusionRadius + forestHealthRadius) * (exclusionRadius + forestHealthRadius)));
            float x = distance * Mathf.Cos(theta);
            float z = distance * Mathf.Sin(theta);


            GameObject newTerrainObject = Instantiate(newGoodItem, new Vector3(x, 0, z), Quaternion.identity);// Busts the scale of the objects, gameObject.transform);
            Vector3 randomRotationVector = new Vector3(0, Random.Range(0, 360), 0);
            newTerrainObject.transform.Rotate(randomRotationVector);
        }
    }
}
