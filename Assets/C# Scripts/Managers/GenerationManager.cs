using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GenerationManager : MonoBehaviour
{
    private enum RoomType
    {
        Empty = 0,
        Enemy = 1,
        Entrance = 2,
        Exit = 3,
        Loot = 4,
        Shop = 5,
        Event = 6,
    }


    private void Awake()
    {
        
    }


    private IEnumerator GenerationTest()
    {
        while (true)
        {
            int[,] grid = GenerateDefaultGrid();
            yield return new WaitForSeconds(10f);
        }
    }

    public void GenerateLevel()
    {

    }



    //Default Weights Dictionary (Split into parts of 20
    Dictionary<RoomType, int> defaultWeights = new Dictionary<RoomType, int>()
    {
        {RoomType.Enemy, 5},
        {RoomType.Loot, 13},
        {RoomType.Shop, 16},
        {RoomType.Event, 20},
    };

    private int[,] GenerateDefaultGrid()
    {
        int[,] grid = new int[4, 4];

        int breakCount = 0;

        //Fill 9 Rooms with Enemies
        for (int i = 0; i < 8; i++)
        {
            bool validPlacement = false;
            while (!validPlacement && breakCount < 256)
            {
                int randX = Random.Range(0, 4);
                int randY = Random.Range(0, 4);

                if (grid[randX, randY] == 0)
                {
                    validPlacement = true;
                    grid[randX, randY] = (int)RoomType.Enemy;
                }

                breakCount++;
                if (breakCount >= 256)
                {
                    //Reload Generation
                    return GenerateDefaultGrid();
                }
            }
        }

        //Place an Entrance
        while (true && breakCount < 256)
        {
            int randX = Random.Range(0, 4);
            int randY = Random.Range(0, 4);

            if (grid[randX, randY] == 0)
            {
                grid[randX, randY] = (int)RoomType.Entrance;
                break;
            }

            breakCount++;
            if (breakCount >= 256)
            {
                //Reload Generation
                return GenerateDefaultGrid();
            }
        }

        //Place an Exit
        while (true && breakCount < 256)
        {
            int randX = Random.Range(0, 4);
            int randY = Random.Range(0, 4);

            if (grid[randX, randY] == 0)
            {
                grid[randX, randY] = (int)RoomType.Exit;
                break;
            }

            breakCount++;
            if (breakCount >= 256)
            {
                //Reload Generation
                return GenerateDefaultGrid();
            }
        }

        //Fill the rest on weights
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                if (grid[x, y] == 0)
                {
                    int randomRoomType = Random.Range(1, 21);

                    if (randomRoomType <= defaultWeights[RoomType.Enemy])
                    {
                        grid[x, y] = (int)RoomType.Enemy;
                        continue;
                    }

                    if (randomRoomType <= defaultWeights[RoomType.Loot])
                    {
                        grid[x, y] = (int)RoomType.Loot;
                        continue;
                    }

                    if (randomRoomType <= defaultWeights[RoomType.Shop])
                    {
                        grid[x, y] = (int)RoomType.Shop;
                        continue;
                    }

                    if (randomRoomType <= defaultWeights[RoomType.Event])
                    {
                        grid[x, y] = (int)RoomType.Enemy;
                        continue;
                    }
                }
            }
        }

        return grid;
    }
}
