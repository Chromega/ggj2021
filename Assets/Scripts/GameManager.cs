using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  public float forestImprovementTimeIntervalSec = 10f;
  public GameObject terrain; 
  private float timeToNextForestImprovement;
  private float forestHealth = 0f;

  public static GameManager Instance { get; private set; }

  private void Awake()
  {
    Instance = this;
  }

  // Start is called before the first frame update
  void Start()
  {
    timeToNextForestImprovement = forestImprovementTimeIntervalSec;
    terrain.GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_GreenRadius", 1.0f + forestHealth);
  }

  // Update is called once per frame
  void Update()
  {
    timeToNextForestImprovement = Mathf.Max(timeToNextForestImprovement - Time.deltaTime, 0f);
    if (timeToNextForestImprovement == 0)
    {
      // TODO: trigger trees/flowers growing based on forest health value
      print("Current forest health: " + forestHealth);
      timeToNextForestImprovement = forestImprovementTimeIntervalSec;
    }
  }

  public void IncreaseForestHealth(float inc)
  {
    forestHealth += inc;
    terrain.GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_GreenRadius", 1.0f + forestHealth);
  }
}
