using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Titile_Map : MonoBehaviour {

    public GameObject player;
    public GameObject treasurePfb;
    public GameObject wallPfb;
    public GameObject groundPfb;
    public float unit = 2f;

    private int[,] map;

    void Awake()
    {
        GenerateMap();
        BuildWorld();
    }

    void GenerateMap()
    {
        int [,] temp = { { 1,1,1,1,1,1,1,1,1,1,1,1,1},
                         { 1,0,0,0,0,0,0,0,0,0,0,0,1},
                         { 1,1,1,1,1,1,1,1,1,1,1,0,1},
                         { 1,0,0,0,1,0,0,0,0,0,1,0,1},
                         { 1,0,1,0,1,2,1,1,1,0,1,0,1},
                         { 1,0,1,0,1,0,0,0,1,0,1,0,1},
                         { 1,0,1,0,1,0,3,0,1,0,1,0,1},
                         { 1,0,1,0,1,0,0,0,1,0,1,0,1},
                         { 1,0,1,0,1,1,1,1,1,0,1,0,1},
                         { 1,0,1,0,0,0,0,0,0,0,1,0,1},
                         { 1,0,1,1,1,1,1,1,1,1,1,0,1},
                         { 1,0,0,0,0,0,0,0,0,0,0,0,1},
                         { 1,1,1,1,1,1,1,1,1,1,1,1,1}};

        map = temp;

    }

    void BuildWorld()
    {
        // prevent player from hitting last map`s treasure 
        player.transform.position = new Vector3(0, -2f, 0);

        // init ground
        GameObject Ground = Instantiate(groundPfb);
        Ground.gameObject.transform.localScale = new Vector3(map.GetLength(1) * unit/10 , 1, map.GetLength(0) * unit/10 );
        //objs.AddLast(Ground); // mark for destory

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
                    player.transform.position = new Vector3(x, 0.5f, z);
                }
                else if (temp == 3)
                {
                    // treasure
                    float x = (j - jm) * unit;
                    float z = (im - i) * unit;
                    // this will be destoryed by player
                    GameObject Treasure = Instantiate(treasurePfb);
                    Treasure.transform.position = new Vector3(x+0.2f, 0.5f, z-0.2f);
                }

            }
        }

    }

    void GenerateWall(int i, int j, float im, float jm)
    {
        float x = (j - jm) * unit;
        float z = (im - i) * unit;
        GameObject Wall = Instantiate(wallPfb);
        //objs.AddLast(Wall); // mark for destory
        Wall.gameObject.transform.position = new Vector3(x, 0.5f, z);
        Wall.gameObject.transform.localScale = new Vector3(0.5f, 1f, 0.5f);
        // joint wall
        if (i > 0 && map[i - 1, j] == 1)
        {
            GameObject JointWall = Instantiate(wallPfb);
            //objs.AddLast(JointWall); // mark for destory
            JointWall.gameObject.transform.position = new Vector3(x, 0.5f, z + unit / 2);
            JointWall.gameObject.transform.localScale = new Vector3(0.5f, 1f, unit - 0.5f);
        }
        if (j > 0 && map[i, j - 1] == 1)
        {
            GameObject JointWall = Instantiate(wallPfb);
            //objs.AddLast(JointWall); // mark for destory
            JointWall.gameObject.transform.position = new Vector3(x - unit / 2, 0.5f, z);
            JointWall.gameObject.transform.localScale = new Vector3(unit - 0.5f, 1f, 0.5f);
        }

    }

}
