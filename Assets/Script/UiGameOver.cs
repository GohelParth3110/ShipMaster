using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
public class UiGameOver : MonoBehaviour
{
    [SerializeField] private GameObject homeScreen;
    [SerializeField] private GameObject panel_Play;
    [SerializeField] private RectTransform panelBackGround;
    [SerializeField] private RectTransform panelPeppar;
    [SerializeField] private RectTransform heder;
    [SerializeField] private RectTransform homeButton;
    [SerializeField] private RectTransform x2Button;
    [SerializeField] private float x2Roation;
    [SerializeField] private float x2Time;
    [SerializeField] private RectTransform coin;
    [SerializeField] private float rotationAngle;
    [SerializeField] private float coinRotationTime;
    [SerializeField] private float backgroundTime;
    [SerializeField] private float scoreTime;
    [SerializeField] private float twoScoreTime;
    [SerializeField] private float buttonTime;
    [SerializeField] private float animationTime;
    [SerializeField] private float outAnimationTime;
    [SerializeField] private RectTransform[] score;
    [SerializeField] private TextMeshProUGUI txt_LastScore;
    [SerializeField] private TextMeshProUGUI txt_BestScore;
    [SerializeField] private TextMeshProUGUI txt_Coin;
    [SerializeField] GameObject panel_Fade;
    [SerializeField] private Image image_GameOver;
    [SerializeField] private GameObject button_2X;
    // Start is called before the first frame update
    [SerializeField] private float currentCoin;
    [SerializeField] private float TargetAmmount;


    private void Start()
    {
        txt_LastScore.text = GameManager.InstanceOfGameManager.currentScore.ToString();
        txt_BestScore.text = DataManager.Instance.bestScore.ToString();
        txt_Coin.text = GameManager.InstanceOfGameManager.coinCollectedInThisRound.ToString();
            StartAnimation();

      
       
    }
    
    

    

    private void StartAnimation()
    {
        if (!FindObjectOfType<AdsManager>().IsRewardLoaded())
        {
            button_2X.SetActive(false);
        }
        Sequence seq = DOTween.Sequence();
        seq.AppendCallback(BackGroundAnimation).AppendInterval(animationTime).
            AppendCallback(CallingScoreAnimation).AppendInterval(animationTime).AppendCallback(ButtonAnimation)
            .AppendInterval(buttonTime).AppendCallback(FindObjectOfType<AdsManager>().ShowInterstitialAd);
        currentCoin = GameManager.InstanceOfGameManager.coinCollectedInThisRound;
         TargetAmmount = GameManager.InstanceOfGameManager.coinCollectedInThisRound * 2;
    }

    
    private void HomeButtonClickProcedure()
    {
        panel_Fade.SetActive(true);
        button_2X.SetActive(true);
        image_GameOver.DOFade(1, 1);
        Invoke("LoadScene", 1f);

      
       
    }

    public void OnClic_2Xbutton()
    {
        AudioManager.instance.ButtonSFX();
        button_2X.SetActive(false);
        FindObjectOfType<AdsManager>().ShowRewardAd();
    }

    public void CallingCourtine()
    {
        StartCoroutine(CoinCollectionAnimation());
    }
    IEnumerator CoinCollectionAnimation( )
    {
        TargetAmmount = GameManager.InstanceOfGameManager.coinCollectedInThisRound;
        float currentTime = 0;
        float maxTime = 1f;


        while (currentTime < maxTime)
        {
            currentCoin = Mathf.Lerp(currentCoin, TargetAmmount, currentTime / maxTime);
            txt_Coin.text = currentCoin.ToString("F0");
            currentTime += Time.deltaTime;
            yield return null;
        }
    }

    private void LoadScene()
    {
      SceneManager.LoadScene(0);
    }

    
    
    private void HomeButtonClickAnimation()
    {
        panelBackGround.DOAnchorPos(new Vector2(0, 2000), outAnimationTime);
        panelPeppar.DOAnchorPos(new Vector2(0, 2000), outAnimationTime);
        panelPeppar.DOScale(0, outAnimationTime);
        heder.DOAnchorPos(new Vector2(0, 600), outAnimationTime);
        homeButton.DOScale(0, outAnimationTime);
        x2Button.DOScale(0, outAnimationTime);
        for (int i = 0; i < score.Length; i++)
        {
            if (i % 2 == 0)
            {
                score[i].DOAnchorPos(new Vector2(-2000, 0), outAnimationTime);
            }
            else
            {
                score[i].DOAnchorPos(new Vector2(2000, 0), outAnimationTime);
            }
        }
    }

    public void PressHomeButton()
    {
        AudioManager.instance.ButtonSFX();
        HomeButtonClickAnimation();
        Invoke("HomeButtonClickProcedure", outAnimationTime);
    }

    private void BackGroundAnimation()
    {
        panelBackGround.DOScale(1, backgroundTime);
        panelPeppar.DOScale(1, backgroundTime);
        heder.DOAnchorPos(new Vector2(0, 0), backgroundTime);
    }
    private void ButtonAnimation()
    {
        homeButton.DOScale(1, buttonTime);
        x2Button.DOScale(1, buttonTime);
        x2Button.DORotate(new Vector3(0, 0, x2Roation), x2Time, RotateMode.Fast).
            SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        coin.DORotate(new Vector3(0, 0, rotationAngle), coinRotationTime, RotateMode.FastBeyond360).
            SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
    }

    private void CallingScoreAnimation()
    {
        StartCoroutine(ScoreAnimation());
    }

    IEnumerator ScoreAnimation()
    {
        for (int i = 0; i < score.Length; i++)
        {
            score[i].DOAnchorPos(new Vector2(0, 0), scoreTime);

            yield return new WaitForSeconds(twoScoreTime);
        }
    }
}

