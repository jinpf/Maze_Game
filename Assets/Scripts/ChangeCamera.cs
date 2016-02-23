using UnityEngine;
using System.Collections;

public class ChangeCamera : MonoBehaviour {

    public GameObject firstviewCam;
    public GameObject thirdviewCam;
    public GameObject littlemapCam;
    public GameObject player;

    private Quaternion playerRot;

    void Start()
    {
        playerRot = Quaternion.identity;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            changeCamera();
        }
    }

    public void changeCamera()
    {
        if (firstviewCam.activeInHierarchy)
        {
            playerRot = player.transform.rotation;
            player.transform.rotation = Quaternion.identity;
            firstviewCam.SetActive(false);
            littlemapCam.SetActive(false);
            thirdviewCam.SetActive(true);
        }
        else
        {
            player.transform.rotation = playerRot;
            thirdviewCam.SetActive(false);
            firstviewCam.SetActive(true);
            littlemapCam.SetActive(true);
        }
    }
}
