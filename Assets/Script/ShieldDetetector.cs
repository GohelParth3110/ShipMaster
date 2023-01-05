using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldDetetector : MonoBehaviour
{
    [SerializeField] private float unbleTime = 0.05f;
    [SerializeField] private GameObject shieldBody;   
    private void OnTriggerEnter(Collider other)
    {
       
        if (other.gameObject.CompareTag("Player"))
        {
            AudioManager.instance.ShieldSFX();
            GameManager.InstanceOfGameManager.player.ShieldEffect();   
            shieldBody.SetActive(false);
            
        }
    }

  
}
