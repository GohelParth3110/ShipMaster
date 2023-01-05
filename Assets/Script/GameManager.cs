using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using System;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public int testValue= 0;


    public static GameManager InstanceOfGameManager;
    
    [Header("Player")]
    public PlayerMoveMent player;
    [SerializeField] private PlayerMoveMent[] all_Player;
    [SerializeField] private Vector3 rotation;
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] private GroundMovment ground;
    [SerializeField] private PathSpawnManager pathSpawnManager;
    [SerializeField] private UiHomeScreen UiHomeScreen;
  

    [Header("Game Data")]
    public bool isPlayerAlive;
    public bool isMagnetEffectPlay;
    public GameObject cineMachineCamera;
    public int currentScore;
    public int coinCollectedInThisRound; 


    [Header("Panel")]
    [SerializeField] private GameObject panel_Revive;
    [SerializeField] private GameObject gameOverScreen;
     public GameObject panel_GameOver;
     public UiGamePlay ui_Gameplay;

    [Header("UI Scripts")]
    public CoinUI ui_Coin;

    private void Awake()
    {
        InstanceOfGameManager = this;
        isPlayerAlive = false;
        
    }
   


  

    public void PlayerSelection(int Index)
    {
        player = Instantiate(all_Player[Index], transform.position,transform.rotation);
        player.gameObject.SetActive(true);
        player.transform.position = new Vector3(0, 0, 0);
        player.transform.rotation = Quaternion.Euler(rotation);
        cinemachineVirtualCamera.Follow = player.transform;
        ground.playerTransform = player.transform;
        pathSpawnManager.player = player.transform;
    }
    private void Update()
    {

        if (!isPlayerAlive)
        {
            isMagnetEffectPlay = false;
        }
    }

    public void ShowReviveScreen()
    {
        
        if (FindObjectOfType<AdsManager>().IsRewardLoaded())
        {
            panel_Revive.gameObject.SetActive(true);
        }
        else
        {
            PlayerGameOver();
        }
       
        
    }
    
   
    public void PlayerGameOver()
    {
        CheckHighScore();
        gameOverScreen.SetActive(true);

        
    }

    public void CoinCollected(int amount)
    {
        coinCollectedInThisRound += amount;
        DataManager.Instance.AddCoin(amount);
        ui_Coin.CoinAmountUpdated();
       
        
    }

    public void UpdateScore(int index)
    {
        currentScore += index;     
    }

    private void CheckHighScore()
    {
        if (currentScore > DataManager.Instance.bestScore)
        {
            DataManager.Instance.BestScore(currentScore);
        }
    }
}
