using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  public float forestImprovementTimeIntervalSec = 10f;
  public GameObject terrain;
  private float timeToNextForestImprovement;
  public float forestHealth = 0f;
  public float forestHealthSlowFactor = 1.5f;
  public float speed = 5f;
  private bool growing = false;
  private float currRadius;
  private float targetRadius;

  private bool hasFiredWinSequence = false;
  private int winThreshold = 100;


  public static GameManager Instance { get; private set; }

  private void Awake()
  {
    Instance = this;
  }

  // Start is called before the first frame update
  void Start()
  {
    timeToNextForestImprovement = forestImprovementTimeIntervalSec;
    terrain.GetComponent<MeshRenderer>().material.SetFloat("_GreenRadius", 1.0f + forestHealth);
    currRadius = 1.0f + forestHealth;
  }

  // Update is called once per frame
  void Update()
  {
    timeToNextForestImprovement = Mathf.Max(timeToNextForestImprovement - Time.deltaTime, 0f);
    if (timeToNextForestImprovement == 0)
    {
      print("Current forest health: " + forestHealth);
      growing = true;
      targetRadius = forestHealth / forestHealthSlowFactor;
      timeToNextForestImprovement = forestImprovementTimeIntervalSec;

      if (!hasFiredWinSequence && forestHealth>=winThreshold)
            {
                hasFiredWinSequence = true;
                StartCoroutine(StartCreditsSequence());
            }
        }
    if (growing)
    {
      growRadius();
    }
  }

    IEnumerator StartCreditsSequence()
    {
        if (!UIController.Instance)
            yield break;

        yield return StartCoroutine(UIController.Instance.FadeCredits(true, 1));
        yield return new WaitForSeconds(2f);
        yield return StartCoroutine(UIController.Instance.FadeCredits(false, 1));
    }

  public void IncreaseForestHealth(float inc)
  {
    forestHealth += inc;
  }

  private void growRadius()
  {
    currRadius += Time.deltaTime * speed;
    if (currRadius < targetRadius)
    {
      terrain.GetComponent<MeshRenderer>().material.SetFloat("_GreenRadius", currRadius);
    }
    else
    {
      growing = false;
    }
  }
}
