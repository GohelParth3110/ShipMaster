using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{

    public static DataManager Instance;

    private PlayerPrefKeys userKeys = new PlayerPrefKeys();
    public int totalCoins;
    public int bestScore;
    public int musicValue;
    public int soundValue;
    public bool hasPurchasedNoAds;
    public int CurrentPlayerIndex;


    private void Awake()
    {
        Instance = this;

        //PlayerPrefs.DeleteAll();
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey(userKeys.key_TotalCoins))
        {
            GetUserData();
        }
        else
        {
            SaveFirstTimeData();
        }
    }

    private void SaveFirstTimeData()
    {

        PlayerPrefs.SetInt(userKeys.Key_Noads, 0);

        for (int i = 0; i < ShipManager.Instance.all_UnlockStatus.Length; i++)
        {
            if (ShipManager.Instance.all_UnlockStatus[i])
            {
                PlayerPrefs.SetInt(userKeys.key_Ship + i, 1);
            }
            else
            {
                PlayerPrefs.SetInt(userKeys.key_Ship + i, 0);
            }
        }

        PlayerPrefs.SetInt(userKeys.key_TotalCoins, totalCoins);
        PlayerPrefs.SetInt(userKeys.key_BestScore, 0);
        PlayerPrefs.SetInt(userKeys.key_ShipSelectionIndex, 0);
        PlayerPrefs.SetInt(userKeys.key_Music, 1);
        PlayerPrefs.SetInt(userKeys.key_Sound, 1);



        AudioManager.instance.PlayBackgroundMusic();

        FindObjectOfType<AdsManager>().CallDefaultAds();
    }



    private void GetUserData()
    {
        totalCoins = PlayerPrefs.GetInt(userKeys.key_TotalCoins);
        bestScore = PlayerPrefs.GetInt(userKeys.key_BestScore);
        musicValue = PlayerPrefs.GetInt(userKeys.key_Music, 1);
        soundValue = PlayerPrefs.GetInt(userKeys.key_Sound, 1);
        CurrentPlayerIndex = PlayerPrefs.GetInt(userKeys.key_ShipSelectionIndex);


        for (int i = 0; i < ShipManager.Instance.all_UnlockStatus.Length; i++)
        {
            if (PlayerPrefs.GetInt(userKeys.key_Ship + i) == 1)
            {
                ShipManager.Instance.all_UnlockStatus[i] = true;
            }
            else
            {
                ShipManager.Instance.all_UnlockStatus[i] = false;

            }
        }

        if (musicValue == 1)
        {
            AudioManager.instance.PlayBackgroundMusic();
        }

        int adValue = PlayerPrefs.GetInt(userKeys.Key_Noads);

        if (adValue == 0)
        {
            hasPurchasedNoAds = false;
            FindObjectOfType<AdsManager>().CallDefaultAds();

            // AdsManager.Instance.CallDefaultAds();
        }
        else
        {
            hasPurchasedNoAds = true;
        }
    }

    public void AddCoin(int coin)
    {
        totalCoins += coin;
        GameManager.InstanceOfGameManager.ui_Coin.CoinAmountUpdated();
        PlayerPrefs.SetInt(userKeys.key_TotalCoins, totalCoins);



    }
    public void SubtractCoin(int coin)
    {

        totalCoins -= coin;
        GameManager.InstanceOfGameManager.ui_Coin.CoinAmountUpdated();

        PlayerPrefs.SetInt(userKeys.key_TotalCoins, totalCoins);

    }
    public void BestScore(int score)
    {
        bestScore = score;
        PlayerPrefs.SetInt(userKeys.key_BestScore, bestScore);
    }

    public void CurrenPlayerSelectionIndex(int index)
    {
        CurrentPlayerIndex = index;

        PlayerPrefs.SetInt(userKeys.key_ShipSelectionIndex, CurrentPlayerIndex);
    }

    public void MusicValue(int value)
    {
        musicValue = value;
        PlayerPrefs.SetInt(userKeys.key_Music, musicValue);

    }
    public void SoundValue(int value)
    {
        soundValue = value;
        PlayerPrefs.SetInt(userKeys.key_Sound, soundValue);

    }

    public void NoAdsPackage()
    {
        hasPurchasedNoAds = true;
        PlayerPrefs.SetInt(userKeys.Key_Noads, 1);

    }


    public void UnlockShip(int Index)
    {
        PlayerPrefs.SetInt(userKeys.key_Ship + Index, 1);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            PlayerPrefs.DeleteAll();
        }
    }

}
