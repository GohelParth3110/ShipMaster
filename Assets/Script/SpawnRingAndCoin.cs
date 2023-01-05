using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnRingAndCoin : MonoBehaviour
{   
    [SerializeField] private string Ring;
    [SerializeField] private string coin;
    
    [SerializeField] private List<Transform> list_SpawnPointTransform = new List<Transform>();
    private int maxRingSpawn = 2;
    private int maxCoinSpawn = 3;
    private int pesenatageSpawn = 10;


    private void Start()
    {
       
        SpawnCoin();
        SpawnRing();

        SpawnShield();
        SpawnMagnet();

    }

    private void SpawnMagnet()
    {
        int spawnMagnet = Random.Range(0, pesenatageSpawn);
        if (spawnMagnet == 0)
        {
            if (list_SpawnPointTransform.Count == 0)
            {
                return;
            }
            int positionOfMagnet = Random.Range(0, list_SpawnPointTransform.Count);
            RingAndCoinPool.InstanceOfRingAndCoin.SpawnMagnet(list_SpawnPointTransform[positionOfMagnet].position);
            list_SpawnPointTransform.RemoveAt(positionOfMagnet);

        }
        
    }

    private void SpawnShield()
    {
        int spawnShield = Random.Range(0, pesenatageSpawn);

        if (spawnShield == 0)
        {
            if (list_SpawnPointTransform.Count == 0)
            {
                return;
            }
            int positionOfshield = Random.Range(0, list_SpawnPointTransform.Count);
            RingAndCoinPool.InstanceOfRingAndCoin.SpawnShield(list_SpawnPointTransform[positionOfshield].position);
            list_SpawnPointTransform.RemoveAt(positionOfshield);
        }
        
    }

    private void SpawnRing()
    {
        if (list_SpawnPointTransform.Count == 0)
        {
            return;
        }
        int qtyOfRingSpwnPath = Random.Range(1, maxRingSpawn);  // find howmany
                                                                // ringspawn in path
       
        for (int i = 0; i < qtyOfRingSpwnPath; i++)
        {
            int positionOfRing = Random.Range(0, list_SpawnPointTransform.Count);//find
                                                                                 // spawnring position
                                                                                 // Instantiate(Ring,list_SpawnPointTransform[positionOfRing].position,
                                                                                 // list_SpawnPointTransform[positionOfRing].rotation, spawnCoinTransform);

            RingAndCoinPool.InstanceOfRingAndCoin.SpawnRing(list_SpawnPointTransform[positionOfRing].position);

            // spawn Ring

            list_SpawnPointTransform.RemoveAt(positionOfRing);
        }
        

    }
    private void SpawnCoin()
    {

        if (list_SpawnPointTransform.Count == 0)
        {
            return;
        }
        int qtyOfCoinSpawnPath = Random.Range(1, maxCoinSpawn);

        for (int i = 0; i < qtyOfCoinSpawnPath; i++)
        {
            int positionOfcoin = Random.Range(0, list_SpawnPointTransform.Count);
            RingAndCoinPool.InstanceOfRingAndCoin.SpawnCoin(list_SpawnPointTransform[positionOfcoin].position);

            list_SpawnPointTransform.RemoveAt(positionOfcoin);
        }
       
       

    }




}
