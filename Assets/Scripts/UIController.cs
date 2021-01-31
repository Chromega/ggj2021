using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	public CanvasGroup Title;

	public void Update()
	{
		StartCoroutine(FadeTitle(false,.003f));
	}

	public IEnumerator FadeTitle(bool fadeToBlack = false, float fadeSpeed = .3f)
	{

		yield return new WaitForSeconds(3);

		if (fadeToBlack)
		{
			while (Title.alpha < 1)
			{
				Title.alpha += fadeSpeed * Time.deltaTime;
				yield return null;
			}
		} else
		{
			while (Title.alpha > 0)
			{
				Title.alpha -= fadeSpeed * Time.deltaTime;
				yield return null;
			}
		}
	}
	
}
