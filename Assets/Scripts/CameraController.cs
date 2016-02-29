using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject player;
    private Vector3 offset;

	// Use this for initialization
	void Start () {

        if (this.name.Equals("LittleMapCamera"))
        {
            offset = new Vector3(0f, 18f, 0f);
        }
            
        else if (this.name.Equals("ThirdViewCamera"))
        {
            offset = new Vector3(0f, 5.5f, -4.5f);
        }
            
        // Debug.Log(this.name + " offset : " + offset.ToString());

    }
	
	// Update is called once per frame
	void LateUpdate () {
        transform.position = player.transform.position + offset;
	}
}
