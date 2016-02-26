using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using MyGameTimeCount;

public class WinButtonEvent : MonoBehaviour {

    
    public GameObject Map;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            handleContinueButton();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            handleExitButton();
        }
    }

	public void handleContinueButton()
    {

        Map.SendMessage("RebuildWorld");
        TimeCount.timespend = 0;
        TimeCount.pause = false;
        Time.timeScale = 1;
        this.gameObject.SetActive(false);
    }

    public void handleExitButton()
    {
#if UNITY_ANDROID
        //Quit the application
        Application.Quit();
#endif

        //If we are running in the editor
#if UNITY_EDITOR
        //Stop playing the scene
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
