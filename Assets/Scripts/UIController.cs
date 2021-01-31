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

    bool instructionsReady = false;
    public Camera cam;

    private void Awake()
    {
    }

    IEnumerator Start()
    {
        AsyncOperation loadScene = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("camera-test", UnityEngine.SceneManagement.LoadSceneMode.Additive);
        loadScene.completed += LoadScene_completed;

        yield return new WaitForSeconds(1f);

        yield return new WaitUntil(() => { return loadScene.isDone; });

        yield return StartCoroutine(FadeTitle(false, 1f));
        yield return StartCoroutine(FadeInstructions(true, 1f));
        instructionsReady = true;
    }

    private void LoadScene_completed(AsyncOperation obj)
    {
        cam.gameObject.SetActive(false);
    }

    public void Update()
	{
        if (!instructionsReady)
            return;

		// Call this when you want credits to show
		// StartCoroutine(FadeCredits(true,.002f));

		if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical")!=0) {
			keyNum ++;
		}

		if (keyNum == 3)
        {
            Instructions.transform.Find("01").gameObject.SetActive(false);

            Instructions.transform.Find("02").gameObject.SetActive(true);
		}

		if (Input.GetAxis("Mouse ScrollWheel") != 0 && keyNum > 2 && !mouseWheel)
        {
            Instructions.transform.Find("02").gameObject.SetActive(false);
            Instructions.transform.Find("03").gameObject.SetActive(true);
			mouseWheel = true;
		}

		if (Input.GetButtonUp("Interact") && mouseWheel && !space) {

            Instructions.transform.Find("03").gameObject.SetActive(false);
            Instructions.transform.Find("04").gameObject.SetActive(true);
			space = true;
		}

		if (Input.GetAxis("Raking")>.5f && space && !shift)
        {
            Instructions.transform.Find("04").gameObject.SetActive(false);
            Instructions.transform.Find("05").gameObject.SetActive(true);
			shift = true;
		}

		if (Input.GetButtonUp("Taking") && shift)
        {
            Instructions.transform.Find("05").gameObject.SetActive(false);
        }
	}

	public IEnumerator FadeTitle(bool fadeToBlack = false, float fadeSpeed = .3f)
	{

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
