using UnityEngine;
using System.Collections;

using MyGameTimeCount; // for TimeCount class
using GamePause; // for Pause class

public class PauseButtonEvent : MonoBehaviour {
	
	public void PauseButtonClick()
    {
        if (!Pause.isPaused && !TimeCount.pause)
        {
            Pause.DoPause();
        }
    }
}
