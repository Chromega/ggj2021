using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	public CanvasGroup Title;
	public CanvasGroup Credits;
	public CanvasGroup Instructions;

	int keyNum = 0;
	bool mouseWheel = false;
	bool space = false;
	bool shift = false;

	public void Update()
	{

		StartCoroutine(FadeTitle(false,.003f));
		StartCoroutine(FadeInstructions(true,.05f));


		// Call this when you want credits to show
		// StartCoroutine(FadeCredits(true,.002f));

		if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow)) {
			keyNum ++;
		}

		if (keyNum == 3) {
			RawImage image = Instructions.transform.Find("01").GetComponent<RawImage>();
			image.color = new Color(1,1,1,0);

			Instructions.transform.Find("02").gameObject.SetActive(true);
		}

		if (Input.GetAxis("Mouse ScrollWheel") != 0 && keyNum > 2) {
			RawImage image = Instructions.transform.Find("02").GetComponent<RawImage>();
			image.color = new Color(1,1,1,0);
			Instructions.transform.Find("03").gameObject.SetActive(true);
			mouseWheel = true;
		}

		if (Input.GetKeyUp(KeyCode.Space) && mouseWheel) {

			RawImage image = Instructions.transform.Find("03").GetComponent<RawImage>();
			image.color = new Color(1,1,1,0);
			Instructions.transform.Find("04").gameObject.SetActive(true);
			space = true;
		}

		if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift) && space) {
			RawImage image = Instructions.transform.Find("04").GetComponent<RawImage>();
			image.color = new Color(1,1,1,0);
			Instructions.transform.Find("05").gameObject.SetActive(true);
			shift = true;
		}

		if (Input.GetKeyUp(KeyCode.F) && shift) {
			RawImage image = Instructions.transform.Find("05").GetComponent<RawImage>();
			image.color = new Color(1,1,1,0);
		}
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

	public IEnumerator FadeInstructions(bool fadeToBlack = false, float fadeSpeed = .3f)
	{

		yield return new WaitForSeconds(7);

		if (fadeToBlack)
		{
			while (Instructions.alpha < 1)
			{
				Instructions.alpha += fadeSpeed * Time.deltaTime;
				yield return null;
			}
		} else
		{
			while (Instructions.alpha > 0)
			{
				Instructions.alpha -= fadeSpeed * Time.deltaTime;
				yield return null;
			}
		}
	}
	
}
