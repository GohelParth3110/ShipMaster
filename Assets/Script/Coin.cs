using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private GameObject coinBody;
    [SerializeField] private BoxCollider coinCollider;
    [SerializeField] private ParticleSystem coinCollection;
    [SerializeField] private float coinRotation = 50;
   [SerializeField] private int coinMagnetSpeed = 50;
    [SerializeField]private int magnetRange = 15;
    private float minimumDistance = 0.5f;
   
    
  
   
    private string playerTag = "Player";


    private void OnEnable()
    {
        coinBody.SetActive(true);
    }


    private void Update()
    {

        if (GameManager.InstanceOfGameManager.isMagnetEffectPlay)
        {
            

            float distanceToPlayer = Vector2.Distance(transform.position, 
                GameManager.InstanceOfGameManager.player.transform.position);
            
            if (Mathf.Abs(distanceToPlayer) <= magnetRange && coinBody.activeSelf)
            {
               
                transform.position = Vector3.MoveTowards(transform.position,
                    GameManager.InstanceOfGameManager.player.transform.position,
                    coinMagnetSpeed * Time.deltaTime);

                transform.Rotate(coinRotation, 0, 0);

                float closeDistance = Vector2.Distance(transform.position,
                GameManager.InstanceOfGameManager.player.transform.position);

                if (Mathf.Abs(distanceToPlayer) <= minimumDistance)
                {

                    coinBody.SetActive(false);
                }

            }
            
        }
       
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag(playerTag))
        {
            coinCollection.Play();
            AudioManager.instance.CoinCollectionSfx();
            GameManager.InstanceOfGameManager.CoinCollected(1);
            coinBody.SetActive(false);

        }


    }

  
   
}
