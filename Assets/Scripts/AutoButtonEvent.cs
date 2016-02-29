using UnityEngine;
using System.Collections;

using MyGameMap;

namespace AutoSet
{
    public class AutoButtonEvent : MonoBehaviour
    {

        public GameObject player;
        public GameObject thirdviewCam;

        private NavMeshAgent agent;
        public static bool isAuto;

        // Use this for initialization
        void Start()
        {

            agent = player.GetComponent<NavMeshAgent>();
            isAuto = false;
        }

        // Update is called once per frame
        void Update()
        {

            if (Input.GetKeyDown(KeyCode.A))
            {
                OnAutoButtonClick();
            }

            if (isAuto)
            {
                Vector3 dst = new Vector3(MapGenerator.endX, MapGenerator.endY, MapGenerator.endZ);
                agent.SetDestination(dst);
            }
            else
            {
                agent.Stop();
                agent.ResetPath();
                if (thirdviewCam.activeInHierarchy)
                {
                    player.transform.rotation = Quaternion.identity;
                }
            }
        }

        public void OnAutoButtonClick()
        {
            if (isAuto)
            {
                isAuto = false;
            }
            else
            {
                isAuto = true;
            }
        }
    }

}

