using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetDetetctor : MonoBehaviour
{
   
    [SerializeField] private GameObject magnetBody;
    
   

   
    private void OnTriggerEnter(Collider other)
    {
       if (other.gameObject.CompareTag("Player"))
        {
            AudioManager.instance.MagnetCollectionSFX();
            GameManager.InstanceOfGameManager.player.MagnetEffect();
            magnetBody.SetActive(false);
            
        }
    }
  
   

  

}
