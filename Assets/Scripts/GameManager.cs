using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  public float forestImprovementTimeIntervalSec = 10f;
  private float timeToNextForestImprovement;
  private float forestHealth = 0f;

  // Start is called before the first frame update
  void Start()
  {
    timeToNextForestImprovement = forestImprovementTimeIntervalSec;
  }

  // Update is called once per frame
  void Update()
  {
    timeToNextForestImprovement = Mathf.Max(timeToNextForestImprovement - Time.deltaTime, 0f);
    if (timeToNextForestImprovement == 0)
    {
      // TODO: trigger trees/flowers growing based on forest health value
      timeToNextForestImprovement = forestImprovementTimeIntervalSec;
    }
  }

  public void IncreaseForestHealth(float inc)
  {
    forestHealth += inc;
  }
}
