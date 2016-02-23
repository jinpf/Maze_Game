using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

using MyGameTimeCount; // for TimeCount class
using MyGameMap;

public class PlayerController : MonoBehaviour {

    public Camera FirstViewCamera;
    public float moveSpeed = 3.0f;
    public float gravity = 9f;
    public float rotSpeed = 0.3f;

    public Canvas winCanvas;
    public Text timeTextG;
    public Text timeTextW;
    public Text leveltext;

    private Transform m_transform;
    private CharacterController m_ch;

    // touch swipe
    private Touch initTouch = new Touch();

    // camera
    private Transform m_camTransform;
    private Vector3 m_camRot;

    // Use this for initialization
    void Start () {

        m_transform = this.transform;
        m_ch = this.GetComponent<CharacterController>();

        // get first view camera
        m_camTransform = FirstViewCamera.transform;
        m_camTransform.position = m_transform.position;
        m_camTransform.rotation = m_transform.rotation;
        m_camRot = m_camTransform.eulerAngles;

    }
	
	// Update is called once frame
	void Update () {
        // rotate
        if (FirstViewCamera.isActiveAndEnabled)
        {
            handleTouchSwipe();
#if UNITY_EDITOR
            handleMouseSwipe();
#endif
        }

        // move
        handlePlayerMove();
    }

    // use Joystick to move player and camera
    void handlePlayerMove()
    {
        float xm = 0, ym = 0, zm = 0, moveHorizontal, moveVertical;
        moveHorizontal = CrossPlatformInputManager.GetAxis("Horizontal");
        moveVertical = CrossPlatformInputManager.GetAxis("Vertical");

        // if keyboard available
        moveHorizontal += Input.GetAxis("Horizontal");
        moveVertical += Input.GetAxis("Vertical");

        xm += moveHorizontal * moveSpeed * Time.deltaTime;
        zm += moveVertical * moveSpeed * Time.deltaTime;
        // gravity
        ym -= gravity * Time.deltaTime;

        m_ch.Move(m_transform.TransformDirection(new Vector3(xm, ym, zm)));
        m_camTransform.position = m_transform.position;
    }

    // rotate the player and first view camera
    void handleTouchSwipe()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began && touch.position.x > Screen.width / 2)
            {
                initTouch = touch;
            }
            else if (touch.phase == TouchPhase.Moved && touch.position.x > Screen.width / 2)
            {
                // swiping
                if (initTouch.position.x < Screen.width / 2)
                {
                    initTouch = touch;
                }
                else
                {
                    float deltaX = touch.position.x - initTouch.position.x;
                    float deltaY = touch.position.y - initTouch.position.y;
                    m_camRot.x -= deltaY * Time.deltaTime * rotSpeed;
                    m_camRot.y += deltaX * Time.deltaTime * rotSpeed;

                    // keep in range
                    if (m_camRot.x < -30)
                        m_camRot.x = -30;
                    if (m_camRot.x > 30)
                        m_camRot.x = 30;

                    m_camTransform.eulerAngles = m_camRot;

                    // keep player`s direction same as camera
                    Vector3 camrot = m_camTransform.eulerAngles;
                    camrot.x = 0; camrot.z = 0; // keep 
                    m_transform.eulerAngles = camrot;
                }
            }
            else if (touch.phase == TouchPhase.Ended && touch.position.x > Screen.width / 2)
            {
                initTouch = new Touch();
            }
        }
    }

    void handleMouseSwipe()
    {
        float rh = Input.GetAxis("Mouse X");
        float rv = Input.GetAxis("Mouse Y");

        m_camRot.x -= rv;
        m_camRot.y += rh;

        // keep in range
        if (m_camRot.x < -30)
            m_camRot.x = -30;
        if (m_camRot.x > 30)
            m_camRot.x = 30;

        m_camTransform.eulerAngles = m_camRot;

        // keep player`s direction same as camera
        Vector3 camrot = m_camTransform.eulerAngles;
        camrot.x = 0; camrot.z = 0; // keep 
        m_transform.eulerAngles = camrot;
    }

    // get treasure
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Treasure"))
        {
            Destroy(other.gameObject);
            TimeCount.pause = true;
            timeTextW.text = timeTextG.text;
            leveltext.text = "Level : " + MapGenerator.level.ToString();
            winCanvas.gameObject.SetActive(true);
        }
    }
}
