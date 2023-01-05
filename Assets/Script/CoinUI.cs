using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinUI : MonoBehaviour
{

   [SerializeField] private float currentCoinAmount;
    [SerializeField] private int targetCoins;
    private float changeRate = 1f;

    public TextMeshProUGUI txt_CoinValue;
    

    private void Start()
    {
        txt_CoinValue.text = DataManager.Instance.totalCoins.ToString();
        currentCoinAmount = DataManager.Instance.totalCoins;
        targetCoins = DataManager.Instance.totalCoins;
    }

    private void Update()
    {
        if(currentCoinAmount != targetCoins)
        {
            currentCoinAmount = Mathf.Lerp(currentCoinAmount, targetCoins, changeRate * Time.deltaTime);
            txt_CoinValue.text = currentCoinAmount.ToString("F0");

            float difference = targetCoins - currentCoinAmount;

            if (Mathf.Abs(difference) < 2)
            {
                currentCoinAmount = targetCoins;
                txt_CoinValue.text = currentCoinAmount.ToString("F0");
            }
        }
    }

    public void CoinAmountUpdated()
    {       
        targetCoins = DataManager.Instance.totalCoins;
        Debug.Log(targetCoins+"target");
    }
}
