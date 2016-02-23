using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace MyGameTimeCount
{
    public class TimeCount : MonoBehaviour
    {
        private Text timeText;
        public static float timespend;
        // public static bool isCounting = false;
        public static bool pause = true;

        // Use this for initialization
        void Start()
        {
            timeText = this.GetComponent<Text>();
            // isCounting = true;
            pause = false;
            timespend = 0;
        }

        // Update is called once per frame
        void Update()
        {
            if (!pause)
            {
                timespend += Time.deltaTime;
                updateTime((int)timespend);
            }
        }

        void updateTime(int ts)
        {
            int h, m, s;
            s = ts % 60; ts /= 60;
            m = ts % 60; ts /= 60;
            h = ts;
            timeText.text = h.ToString() + ":" + m.ToString() + ":" + s.ToString();

        }
    }
}

