using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class TestManager : MonoBehaviour
{
    [Serializable]
    public class Count
    {
        public int minimum;
        public int maximum;

        public Count(int min, int max)
        {
            minimum = min;
            maximum = max;
        }
    }

    public int columns;
    public int rows;
    public Count wallCount = new Count(5, 9);
    public GameObject exit;
    public GameObject[] floorTiles;
    public GameObject[] wallTiles;
    public GameObject[] BackGroundTiles;

    private Transform boardHolder;
    private List<Vector3> gridPositions = new List<Vector3>(); 

    void InitialiseList()
    {
        gridPositions.Clear();

        for (int i = 0; i < columns -1; ++i)
        {
            for (int j = 0; j < rows -1; ++j)
            {
                gridPositions.Add(new Vector3(i, j, 0f));
            }
        }
    }

    void BoardSetup()
    {
        boardHolder = new GameObject("Board").transform;

        for(int i = -1; i < columns + 1; ++i)
        {
            for(int j = -1; j < rows + 1; ++j)
            {
                GameObject toInstantiate = BackGroundTiles[0]; 

                if(j==0)
                    toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];

                if(i == -1 || i == columns ||  j == -1 || j == rows)
                {
                   // toInstantiate = wallTiles[Random.Range(0, wallTiles.Length)];
                }

                GameObject instance = Instantiate(toInstantiate,new Vector3(i,j,0f), Quaternion.identity);


                instance.transform.SetParent(boardHolder);
            }
        }
    }

    Vector3 RandomPosition()
    {
        int randomIndex = Random.Range(0,gridPositions.Count);
        Vector3 randomPosition = gridPositions[randomIndex];   
        gridPositions.RemoveAt(randomIndex);
        return randomPosition;
    }

    void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum)
    {
        int objectCount = Random.Range (minimum, maximum + 1);

        for(int i = 0; i < objectCount; ++i)
        {
            Vector3 randomPosition = RandomPosition();
            GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];
            Instantiate(tileChoice,randomPosition,Quaternion.identity);
        }


    }

    public void SetupScene(int level)
    {
        BoardSetup();

        InitialiseList();

        LayoutObjectAtRandom(wallTiles,wallCount.minimum,wallCount.maximum);

    }


}
