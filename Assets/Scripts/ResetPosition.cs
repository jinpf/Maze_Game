using UnityEngine;
using System.Collections;

using MyGameMap; // for start point
using GamePause; // for pause check
using AutoSet;

public class ResetPosition : MonoBehaviour {

    public GameObject player;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            reset();
        }
    }

    public void reset()
    {
        if (!Pause.isPaused)
        {
            AutoButtonEvent.isAuto = false;
            player.transform.position = new Vector3(MapGenerator.startX, MapGenerator.startY, MapGenerator.startZ);
        }
    }
}
