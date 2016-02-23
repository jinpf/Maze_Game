using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MyGameMap
{
    public class MapGenerator : MonoBehaviour
    {
        public static int level = 1;
        public GameObject player;
        public GameObject treasurePfb;
        public GameObject wallPfb;
        public GameObject groundPfb;
        public float unit = 1.5f;

        public static float startX = 0, startY = 0, startZ = 0;
 
        private LinkedList<GameObject> objs = new LinkedList<GameObject>();
        private int[,] map;
        private int[] directionX = { 0, 1, 0, -1 }, directionY = { -1, 0, 1, 0 };

        // Use this for initialization
        void Awake()
        {
            GenerateMap();
            BuildWorld();
        }

        // GenerateMap accoroding to the level
        void GenerateMap()
        {
            /*
            if (level == 0)
            {
                int [,] temp = { { 1,1,1,1,1},
                                 { 1,0,0,3,1},
                                 { 1,0,0,0,1},
                                 { 1,2,0,1,1},
                                 { 1,1,1,1,1}};
                map = temp;
            }
            else if (level == 1)
            {
                int [,] temp = { { 1,1,1,1,1,1,1,1,1,1,1,1,1},
                                 { 1,2,0,0,0,0,0,0,0,0,0,0,1},
                                 { 1,1,1,1,1,1,1,1,1,1,1,0,1},
                                 { 1,0,0,0,1,0,0,0,0,0,1,0,1},
                                 { 1,0,1,0,1,0,1,1,1,0,1,0,1},
                                 { 1,0,1,0,1,0,0,0,1,0,1,0,1},
                                 { 1,0,1,0,1,0,3,0,1,0,1,0,1},
                                 { 1,0,1,0,1,0,0,0,1,0,1,0,1},
                                 { 1,0,1,0,1,1,1,1,1,0,1,0,1},
                                 { 1,0,1,0,0,0,0,0,0,0,1,0,1},
                                 { 1,0,1,1,1,1,1,1,1,1,1,0,1},
                                 { 1,0,0,0,0,0,0,0,0,0,0,0,1},
                                 { 1,1,1,1,1,1,1,1,1,1,1,1,1}};
                map = temp;
            } */
            
            int size;
            if (level < 16)
            {
                size = (level + 3) * 2 + 1;
                unit = 1.5f;
            }
            else
            {
                size = (level - 11) * 2 + 1;
                unit = 1f;
            }

            map = new int[size, size];
            // init
            for (int i = 0; i < size; ++i)
                for (int j = 0; j < size; ++j)
                    map[i, j] = 1;

            int mstartX, mstartY, mendX, mendY;
            mstartX = Random.Range(1, size - 1);
            mstartY = Random.Range(1, size - 1);
            GenerateMaze(mstartX, mstartY, size-1, size-1);
            
            //Generate End Point
            while(true)
            {
                mendX = Random.Range(1, size - 1);
                mendY = Random.Range(1, size - 1);
                if (map[mendY, mendX] == 0 && (System.Math.Abs(mendX-mstartX)>(size-5)/2 || System.Math.Abs(mendY - mstartY) > (size - 5) / 2))
                    break;
            }
            map[mstartY, mstartX] = 2;
            map[mendY, mendX] = 3;
    
        }

        void GenerateMaze(int currentX, int currentY, int maxX, int maxY)
        {
            map[currentY, currentX] = 0;
            while (true)
            {
                List<int> neighborList = new List<int>();
                for (int i = 0; i < 4; ++i)
                {
                    int nX = currentX + directionX[i], nY = currentY + directionY[i];
                    if (nX > 0 && nX < maxX && nY > 0 && nY < maxY && map[nY,nX] == 1)
                    {
                        // of occupied neighbors of the candidate cell must be 0
                        int count = 0;
                        for (int j = 0; j < 4; ++j)
                        {
                            int eX = nX + directionX[j], eY = nY + directionY[j];
                            if (eX > 0 && eX < maxX && eY > 0 && eY < maxY && map[eY, eX] == 0)
                                count += 1;
                        }
                        if (count == 1)
                            neighborList.Add(i);
                    }
                }
                // randomly select a neighbor
                if (neighborList.Count > 0)
                {
                    int dir = neighborList[Random.Range(0,neighborList.Count)];
                    currentX += directionX[dir];
                    currentY += directionY[dir];
                    GenerateMaze(currentX, currentY, maxX, maxY);
                }
                else
                {
                    // all direction tried, go back
                    break;
                }
            }
        }

        void BuildWorld()
        {
            // prevent player from hitting last map`s treasure 
            player.transform.position = new Vector3(0, -2f, 0);

            // init ground
            GameObject Ground = Instantiate(groundPfb);
            Ground.gameObject.transform.localScale = new Vector3(map.GetLength(1) * unit / 10, 1, map.GetLength(0) * unit / 10);
            objs.AddLast(Ground); // mark for destory

            // init map
            int im = map.GetLength(0) / 2, jm = map.GetLength(1) / 2;
            for (int i = 0; i < map.GetLength(0); ++i)
            {
                for (int j = 0; j < map.GetLength(1); ++j)
                {
                    int temp = map[i, j];
                    if (temp == 1)
                    {
                        // Wall
                        GenerateWall(i, j, im, jm);
                    }
                    else if (temp == 2)
                    {
                        // Player
                        float x = (j - jm) * unit;
                        float z = (im - i) * unit;
                        startX = x; startY = 1.5f; startZ = z;
                        player.transform.position = new Vector3(x, 2.5f, z);
                        if (level < 16)
                        {
                            player.transform.localScale = new Vector3(1, 1, 1);
                        }
                        else
                        {
                            player.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
                        }
                        
                    }
                    else if (temp == 3)
                    {
                        // treasure
                        float x = (j - jm) * unit;
                        float z = (im - i) * unit;
                        // this will be destoryed by player
                        GameObject Treasure = Instantiate(treasurePfb);
                        Treasure.transform.position = new Vector3(x, 0.5f, z);
                    }
                    
                }
            }

        }

        void DestoryWorld()
        {
            while (objs.Count != 0)
            {
                Destroy(objs.Last.Value);
                objs.RemoveLast();
            }
        }

        void RebuildWorld()
        {
            DestoryWorld();
            level += 1;
            GenerateMap();
            BuildWorld();
        }

        void GenerateWall(int i, int j, float im, float jm)
        {
            float x = (j - jm) * unit;
            float z = (im - i) * unit;
            GameObject Wall = Instantiate(wallPfb);
            objs.AddLast(Wall); // mark for destory
            Wall.gameObject.transform.position = new Vector3(x, 0.5f, z);
            if (level < 16)
            {
                // lose type
                Wall.gameObject.transform.localScale = new Vector3(0.5f, 1f, 0.5f);
                // joint wall
                if (i > 0 && map[i - 1, j] == 1)
                {
                    GameObject JointWall = Instantiate(wallPfb);
                    objs.AddLast(JointWall); // mark for destory
                    JointWall.gameObject.transform.position = new Vector3(x, 0.5f, z + unit / 2);
                    JointWall.gameObject.transform.localScale = new Vector3(0.5f, 1f, unit - 0.5f);
                }
                if (j > 0 && map[i, j - 1] == 1)
                {
                    GameObject JointWall = Instantiate(wallPfb);
                    objs.AddLast(JointWall); // mark for destory
                    JointWall.gameObject.transform.position = new Vector3(x - unit / 2, 0.5f, z);
                    JointWall.gameObject.transform.localScale = new Vector3(unit - 0.5f, 1f, 0.5f);
                }
            }
            else
            {
                // hard type
                Wall.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
            }

        }

    }
}


