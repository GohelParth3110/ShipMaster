using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class UiHomeScreen : MonoBehaviour
{
    [SerializeField] private GameObject homeScreen;
    [SerializeField] private GameObject button_AddCoin;
    [SerializeField] private GameObject RewardScreen;

    [SerializeField] private GameObject appBuyScreen;
    [SerializeField] private GameObject spawnManager;
    [SerializeField] private GameObject playerSelection;
    [SerializeField] private GameObject playButton;
    [SerializeField] private RectTransform coin;
    [SerializeField] private RectTransform bestScore;
    [SerializeField] private RectTransform score;
    [SerializeField] private TextMeshProUGUI txt_Best;

    [SerializeField] private RectTransform[] button;
    [SerializeField] private RectTransform coinImage;
    [SerializeField] private float rotationAngle;
    [Header("Time")]
    [SerializeField] private float coinAndScoreAnimationTime;
    [SerializeField] private float playAnimationTime;
    [SerializeField] private float buttonAnimationTime;
    [SerializeField] private float twoButtonInterval;
    [SerializeField] private float animationInterval;
    [SerializeField] private float outAnimationTime;
    [SerializeField] private float roationTime;
    [SerializeField] private float coinTransferTime;
    [SerializeField] private GameObject panel_Setting;



    public void OnClick_AppPurchase()
    {
        AudioManager.instance.ButtonSFX();
        coin.DOAnchorPos(new Vector2(600, 0),outAnimationTime);
        if (playerSelection.activeSelf)
        {
            playerSelection.GetComponent<ShipSelectionUI>().AppPurchase();
            Invoke("OnclicAppPurchaseAnimation", outAnimationTime);
        }
        else
        {
            CloseThisUIAniamtion();
            Invoke("OnclicAppPurchaseAnimation", outAnimationTime);
        }

    }
    public void OnCLick_SettingBUtton()
    {
        AudioManager.instance.ButtonSFX();
        CloseThisUIAniamtion();

        Invoke("SettingUiProcedure", outAnimationTime);
    }
    private void SettingUiProcedure()
    {
        panel_Setting.gameObject.SetActive(true);
        panel_Setting.GetComponent<UiSetting>().SettingStartUiAnimation();
    }

    private void OnclicAppPurchaseAnimation()
    {
        this.gameObject.SetActive(false);

        appBuyScreen.SetActive(true);
        appBuyScreen.GetComponent<UiAppPurchase>().UiStartAniamtion();
        button_AddCoin.SetActive(false);

    }
    public void StartUiHomeScreen()
    {
        GameManager.InstanceOfGameManager.player.gameObject.SetActive(true);
        GameManager.InstanceOfGameManager.player.StartAnimation();
        Invoke("PlayButtonActive", GameManager.InstanceOfGameManager.player.animationTime);
        txt_Best.text = DataManager.Instance.bestScore.ToString();

    }
    private void PlayButtonActive()
    {

        playButton.SetActive(true);

        Sequence seq = DOTween.Sequence();

        seq.AppendCallback(ScoreAndCoinAnimation).AppendInterval(animationInterval).AppendCallback(PlayButtonAnimation).
           AppendCallback(CallButtonAnimation);

        coinImage.DORotate(new Vector3(0, 0, rotationAngle), roationTime, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
    }
    public void OnClick_PlayerSelectionButton()
    {
        AudioManager.instance.ButtonSFX();
        CloseThisUIAniamtion();
        Invoke("PlayerSelectionProcedure", outAnimationTime);
    }
    private void PlayerSelectionProcedure()
    {
        playerSelection.SetActive(true);
        this.gameObject.SetActive(false);

    }
    public void Onclick_PlayButtonClick()
    {
        AudioManager.instance.ButtonSFX();
        CloseThisUIAniamtion();
        Invoke("PlayClickButtonProcedure", outAnimationTime);
    }
    private void PlayClickButtonProcedure()
    {
        GameManager.InstanceOfGameManager.isPlayerAlive = true;
        GameManager.InstanceOfGameManager.cineMachineCamera.SetActive(true);
        spawnManager.gameObject.SetActive(true);


        score.gameObject.SetActive(true);
        coin.DOAnchorPos(new Vector2(0, 0), outAnimationTime);

        button_AddCoin.SetActive(false);
        homeScreen.SetActive(false);

    }

    private void CloseThisUIAniamtion()
    {

        bestScore.DOAnchorPos(new Vector2(0, 600), outAnimationTime);
        coin.DOAnchorPos(new Vector2(0, 600), outAnimationTime);

        playButton.transform.DOScale(0, outAnimationTime);
        for (int i = 0; i < button.Length; i++)
        {
            button[i].DOAnchorPos(new Vector2(0, -600), outAnimationTime);
        }
    }


    private void PlayButtonAnimation()
    {
        playButton.transform.DOScale(1, playAnimationTime);
    }

    private void ScoreAndCoinAnimation()
    {
        coin.DOAnchorPos(new Vector2(0, 0), coinAndScoreAnimationTime);
        bestScore.DOAnchorPos(new Vector2(0, 0), coinAndScoreAnimationTime);
    }

    private void CallButtonAnimation()
    {
        StartCoroutine(ButtonAnimation());

    }

    IEnumerator ButtonAnimation()
    {
        for (int i = 0; i < button.Length; i++)
        {
            button[i].DOAnchorPos(new Vector2(0, 0), buttonAnimationTime);

            yield return new WaitForSeconds(twoButtonInterval);
        }
    }




}
