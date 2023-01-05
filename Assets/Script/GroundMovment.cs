using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMovment : MonoBehaviour
{
   
    [SerializeField] private float offset;
    [SerializeField] private float maxDistance;
    public Transform playerTransform;
    public Transform[] all_BackgroundChild;
    
    private void Update()
    {
        if (!GameManager.InstanceOfGameManager.isPlayerAlive) { return;}
        ScrollBackGround();
    }

    private void ScrollBackGround()
    {
        for (int i = 0; i < all_BackgroundChild.Length; i++)
        {
            if (playerTransform.position.x- all_BackgroundChild[i].
                transform.position.x > maxDistance)
            {
                if(i == 0)
                {
                    all_BackgroundChild[i].transform.position =
                new Vector3(all_BackgroundChild[all_BackgroundChild.Length-1].position.x
                                                                                + offset, 
                       all_BackgroundChild[i].transform.position.y, 
                       all_BackgroundChild[i].transform.position.z);
                }
                else
                {
                    all_BackgroundChild[i].transform.position =
                        new Vector3(all_BackgroundChild[i - 1].position.x + offset,
                        all_BackgroundChild[i].transform.position.y, 
                        all_BackgroundChild[i].transform.position.z);
                }
               
            }
        }
    }
}
