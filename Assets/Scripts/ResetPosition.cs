using UnityEngine;
using System.Collections;
using MyGameMap;

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
        player.transform.position = new Vector3(MapGenerator.startX, MapGenerator.startY, MapGenerator.startZ);
    }
}
