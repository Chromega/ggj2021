using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  private int numWeeds = 0;
  private int numBunnies = 0;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  public void OnWeedSpawned()
  {
    numWeeds += 1;
    UpdateBunnies();
  }

  public void OnWeedCleanedUp()
  {
    numWeeds -= 1;
    UpdateBunnies();
  }

  void UpdateBunnies()
  {
    int desiredNumBunnies = Mathf.Max((12 - numWeeds) / 2, 0);
    while (numBunnies < desiredNumBunnies)
    {
      SpawnBunny();
    }
    while (numBunnies > desiredNumBunnies)
    {
      RemoveBunny();
    }
  }

  void SpawnBunny()
  {
    // TODO: actually spawn
    numBunnies += 1;
  }

  void RemoveBunny()
  {
    // TODO: actually remove
    numBunnies -= 1;
  }
}
