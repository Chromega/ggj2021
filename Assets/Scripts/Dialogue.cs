using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    bool showing = false;
    public Canvas canvas;
    public UnityEngine.UI.Image image;
    public UnityEngine.UI.AspectRatioFitter fitter;

    public Sprite[] sprites;

    private void Start()
    {
        canvas.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!showing)
        {
            if (other.tag == "Player")
            {
                canvas.enabled = true;
                showing = true;
                image.sprite = sprites[Random.Range(0, sprites.Length)];
                fitter.aspectRatio = image.sprite.textureRect.width / (float)image.sprite.textureRect.height;
                StartCoroutine(DelayedHide());
            }
        }
    }

    IEnumerator DelayedHide()
    {
        yield return new WaitForSeconds(3.0f);
        canvas.enabled = false;
        showing = false;
    }
}
