using System;
using UnityEngine;

public class RingAndCoinPool : MonoBehaviour
{
    public static RingAndCoinPool InstanceOfRingAndCoin;
    [SerializeField] private Transform[] all_Ring;
    
    [SerializeField] private Transform[] all_Coin;
    [SerializeField] private Transform[] all_CoinBody;
    [SerializeField] private Transform[] all_Shield;
    [SerializeField] private Transform[] all_ShieldBody;
    [SerializeField] private Transform[] all_Magnet;
    [SerializeField] private Transform[] all_MagnetBody;
    [SerializeField] private float ringRotateSpeed;
    [SerializeField] private float coinRotationSpeed;
    [SerializeField]private float shieldRotation;
  
    private int currentRingIndex = 0;
    private int currentCoinIndex = 0;
    private int currentShieldIndex = 0;
    private int currentMagenetIndex = 0;


    private void Awake()
    {
        InstanceOfRingAndCoin = this;
    }

    private void Update()
    {
        RingRotation();
        CoinRotation();
        ShieldRotation();
    }

    private void CoinRotation()
    {
        for (int i = 0; i < all_Coin.Length; i++)
        {
            all_Coin[i].transform.Rotate(0, coinRotationSpeed,0);
        }
    }

    private void RingRotation()
    {
        for (int i = 0; i < all_Ring.Length; i++)
        {
            all_Ring[i].transform.Rotate(0, 0, ringRotateSpeed);
        }
    }

    private void ShieldRotation()
    {
        for (int i = 0; i < all_Shield.Length; i++)
        {
            all_Shield[i].transform.Rotate(0,0,shieldRotation);
        }
    }


    public void SpawnRing(Vector3 position)
    {
        all_Ring[currentRingIndex].gameObject.SetActive(true);
        all_Ring[currentRingIndex].position = position;
        all_Ring[currentRingIndex].localScale = new Vector3(2, 2, 2);
        


        currentRingIndex += 1;

        if(currentRingIndex >= all_Ring.Length)
        {
            currentRingIndex = 0;
        }

    }
    public void  SpawnCoin(Vector3 position)
    {
        all_Coin[currentCoinIndex].gameObject.SetActive(true);
        all_CoinBody[currentCoinIndex].gameObject.SetActive(true);
        all_Coin[currentCoinIndex].position = position;


        currentCoinIndex += 1;

        if (currentCoinIndex >= all_Coin.Length)
        {
            currentCoinIndex = 0;  
        }

    }
    public void SpawnShield(Vector3  position)
    {
        all_Shield[currentShieldIndex].gameObject.SetActive(true);
        all_ShieldBody[currentShieldIndex].gameObject.SetActive(true);
        all_Shield[currentShieldIndex].position = position;
        currentShieldIndex++;
        if (currentShieldIndex>=all_Shield.Length)
        {
            currentShieldIndex = 0;
        }
    }
    public void SpawnMagnet(Vector3 position)
    {
        all_Magnet[currentMagenetIndex].gameObject.SetActive(true);
        all_MagnetBody[currentMagenetIndex].gameObject.SetActive(true);
        all_Magnet[currentMagenetIndex].position = position;
        currentMagenetIndex++;
        if (currentMagenetIndex>=all_Magnet.Length)
        {
            currentMagenetIndex = 0;
        }
    }


}
