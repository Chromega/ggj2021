using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyMovement : MonoBehaviour
{
    public float bunnySpeed = 2f;
    float angle = 0.0f;

    void Start()
    {
        StartCoroutine(ChangeDirection());
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0.0f, angle * Mathf.Rad2Deg, 0.0f);
        transform.Translate(transform.right * bunnySpeed * Time.deltaTime);
        /*transform.rotation = Quaternion.Slerp(transform.rotation,
                                                          Quaternion.LookRotation(
                                                          new Vector3(bunnySpeed * x, 0, -bunnySpeed * z)),
                                                          Time.deltaTime * 30f);*/

    }

    IEnumerator ChangeDirection()
    {
        while (true)
        {
            angle = Random.Range(0, 2 * Mathf.PI);

            float timeToWait = Random.Range(4.0f, 6.0f);

            yield return new WaitForSeconds(timeToWait);
        }
    }
}
