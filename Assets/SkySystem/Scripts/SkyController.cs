using UnityEngine;
using System.Collections;

namespace SkyControl
{
    public class SkyController : MonoBehaviour
    {

        public Camera[] cams;
        public Material[] mats;
        private static Camera[] cs;
        private static Material[] ms;

        // Use this for initialization
        void Start()
        {
            cs = cams;
            ms = mats;
            changeSky();
        }

        /* for test
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                changeSky();
            }
        }
        */

        public static void changeSky()
        {
            int index = Random.Range(0,ms.Length);
            for (int i = 0; i < cs.Length; ++i)
            {
                cs[i].GetComponent<Skybox>().material = ms[index];
            }
        }
    }

}

