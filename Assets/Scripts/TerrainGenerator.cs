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

    [System.Serializable]
    public struct SpawnableGoodObjects
    {
        public GameObject obj;
        public float weight;
    }
    public SpawnableGoodObjects[] goodObjects;
    public float spawnDelay = 10.0f;
    float currDelay = 1.0f;
    public float minGoodHealth = 5.0f;

    [System.Serializable]
    public struct HelperSpawns
    {
        public CritterHelper helper;
        public float forestHealthThreshold;
    }
    public HelperSpawns[] helpers;
    List<bool> spawnTracker = new List<bool>();

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
                float loopCount = 0f;
                float distance = Mathf.Sqrt(Random.Range(exclusionRadius * exclusionRadius, totalRadius * totalRadius));

                float x = distance * Mathf.Cos(theta);
                float z = distance * Mathf.Sin(theta);

                while (IntersectsSand(new Vector3(x, 0, z)))
                {
                    distance = Mathf.Sqrt(Random.Range(exclusionRadius * exclusionRadius, totalRadius * totalRadius));

                    x = distance * Mathf.Cos(theta);
                    z = distance * Mathf.Sin(theta);
                    loopCount++;
                    if(loopCount > 10f)
                    {
                        break;
                    }
                }
                

                GameObject newTerrainObject = Instantiate(terrainObjects[i].envObj, new Vector3(x, 0, z), Quaternion.identity);
                Vector3 randomRotationVector = new Vector3(0, Random.Range(0, 360), 0);
                newTerrainObject.transform.Rotate(randomRotationVector);
            }
        }

        for (int i = 0; i < helpers.Length; i++)
        {
            HelperSpawns newHelper = helpers[i];
            spawnTracker.Add(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        currDelay -= Time.deltaTime;
        if(currDelay < 0)
        {
            currDelay = spawnDelay;// * (70 / GameManager.Instance.forestHealth));
            SpawnGoodItems();
        }

        for(int i = 0; i < helpers.Length; i++)
        {
            HelperSpawns newHelper = helpers[i];
            if(!spawnTracker[i] && newHelper.forestHealthThreshold < GameManager.Instance.forestHealth)
            {              
                // Find good spot to spawn
                float x = 0;
                float z = 0;
                var viewPoint = Camera.main.WorldToViewportPoint(new Vector3(x, 0, z));
                bool pointOnCamera = true;
                int loopCount = 0;
                bool failedSpawn = false;

                while(pointOnCamera)
                {
                    x = Random.Range(-50, 50);
                    z = Random.Range(-50, 50);
                    pointOnCamera = (viewPoint.x > 0 && viewPoint.x < 1 && viewPoint.z > 0 && viewPoint.z < 1);
                    loopCount++;
                    if(loopCount > 10)
                    {
                        failedSpawn = true;
                        print("No valid location for helper found");
                        break;
                    }
                }
                if(!failedSpawn)
                {
                    CritterHelper newCritter = Instantiate(newHelper.helper, new Vector3(x, 0, z), Quaternion.identity);
                    spawnTracker[i] = true;
                }
            }
        }
    }

    void SpawnGoodItems()
    {
        float spawnCount = GameManager.Instance.forestHealth / 5;
        for (int i = 0; i < spawnCount && GameManager.Instance.forestHealth > minGoodHealth; i++)
        {
            float exclusionRadius = 10f;
            GameObject newGoodItem = goodObjects[Random.Range(0, goodObjects.Length)].obj;

            float totalWeight = 0;
            float currentWeight = 0;
            for(int j = 0; j < goodObjects.Length; j++)
            {
                totalWeight += goodObjects[j].weight;
            }
            float randomWeight = Random.Range(0, totalWeight);

            foreach (SpawnableGoodObjects spawnableGoodObject in goodObjects)
            {
                currentWeight += spawnableGoodObject.weight;
                if(randomWeight <= currentWeight)
                {
                    newGoodItem = spawnableGoodObject.obj;
                    break;
                }
            }
            float forestHealthRadius = GameManager.Instance.forestHealth - 15;// / 2;

            float theta = Random.Range(0, 2 * Mathf.PI);
            //float distance = Random.Range(exclusionRadius, exclusionRadius + forestHealthRadius);
            float distance = Mathf.Sqrt(Random.Range(exclusionRadius * exclusionRadius, (exclusionRadius + forestHealthRadius) * (exclusionRadius + forestHealthRadius)));
            float x = distance * Mathf.Cos(theta);
            float z = distance * Mathf.Sin(theta);

            while(IntersectsSand(new Vector3(x, 0, z)))
            {
                distance = Mathf.Sqrt(Random.Range(exclusionRadius * exclusionRadius, (exclusionRadius + forestHealthRadius) * (exclusionRadius + forestHealthRadius)));
                x = distance * Mathf.Cos(theta);
                z = distance * Mathf.Sin(theta);
            }

            GameObject newTerrainObject = Instantiate(newGoodItem, new Vector3(x, 0, z), Quaternion.identity);// Busts the scale of the objects, gameObject.transform);
            Vector3 randomRotationVector = new Vector3(0, Random.Range(0, 360), 0);
            newTerrainObject.transform.Rotate(randomRotationVector);
        }
    }

    bool IntersectsSand(Vector3 locationToCheck)
    {
        GameObject[] sandpits = GameObject.FindGameObjectsWithTag("sand");
        foreach(GameObject sandpit in sandpits)
        {
            if (Vector3.Distance(locationToCheck, sandpit.transform.position) < 5.0f)
            {
                return true;
            }
        }
        return false;
    }
}
