using UnityEngine;
using System.Collections;

public class HelpButtonEvent : MonoBehaviour {

    public GameObject helpPanel;
    public GameObject[] helps;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            handleHelpButton();
        }
    }

	public void handleHelpButton()
    {
        if (helpPanel.activeInHierarchy)
        {
            helpPanel.SetActive(false);
            for (int i = 0; i < helps.Length; ++i)
            {
                helps[i].SetActive(false);
            }

            // Debug.Log("disable!");
        }
        else
        {
            helpPanel.SetActive(true);
            for (int i = 0; i < helps.Length; ++i)
            {
                helps[i].SetActive(true);
            }
            // Debug.Log("enable!");
        }
    }
}
