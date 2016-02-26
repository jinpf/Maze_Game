using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using MyGameTimeCount; // for TimeCount class
using MyGameMap;

namespace GamePause
{
    public class Pause : MonoBehaviour
    {

        private static Text levelText;
        public Text lText;
        public static bool isPaused;                               //Boolean to check if the game is paused or not

        private static ShowPanels showPanels;                       //Reference to the ShowPanels script used to hide and show UI panels
        private StartOptions startScript;                   //Reference to the StartButton script

        //Awake is called before Start()
        void Awake()
        {
            //Get a component reference to ShowPanels attached to this object, store in showPanels variable
            showPanels = GetComponent<ShowPanels>();
            //Get a component reference to StartButton attached to this object, store in startScript variable
            startScript = GetComponent<StartOptions>();
            levelText = lText;
            isPaused = false;
        }

        // Update is called once per frame
        void Update()
        {

            //Check if the Cancel button in Input Manager is down this frame (default is Escape key) and that game is not paused, and that we're not in main menu
            if (Input.GetButtonDown("Cancel") && !isPaused && !startScript.inMainMenu && !TimeCount.pause)
            {
                //Call the DoPause function to pause the game
                DoPause();
            }
            //If the button is pressed and the game is paused and not in main menu
            else if (Input.GetButtonDown("Cancel") && isPaused && !startScript.inMainMenu && !TimeCount.pause)
            {
                //Call the UnPause function to unpause the game
                UnPause();
            }

        }

        public static void DoPause()
        {
            //Set isPaused to true
            isPaused = true;
            //Set time.timescale to 0, this will cause animations and physics to stop updating
            Time.timeScale = 0;
            levelText.text = "Level : " + MapGenerator.level.ToString();
            //call the ShowPausePanel function of the ShowPanels script
            showPanels.ShowPausePanel();
        }


        public void UnPause()
        {
            //Set isPaused to false
            isPaused = false;
            //Set time.timescale to 1, this will cause animations and physics to continue updating at regular speed
            Time.timeScale = 1;
            //call the HidePausePanel function of the ShowPanels script
            showPanels.HidePausePanel();
        }


    }
}


