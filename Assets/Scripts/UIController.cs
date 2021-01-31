using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	public CanvasGroup Title;
	public CanvasGroup Credits;

	public void Update()
	{
		StartCoroutine(FadeTitle(false,.003f));

		// StartCoroutine(FadeCredits(true,.002f));
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

	public IEnumerator FadeCredits(bool fadeToBlack = false, float fadeSpeed = .3f)
	{

		if (fadeToBlack)
		{
			while (Credits.alpha < 1)
			{
				Credits.alpha += fadeSpeed * Time.deltaTime;
				yield return null;
			}
		} else
		{
			while (Credits.alpha > 0)
			{
				Credits.alpha -= fadeSpeed * Time.deltaTime;
				yield return null;
			}
		}
	}
	
}
