using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
public class UiAppPurchase : MonoBehaviour
{
    [SerializeField] private GameObject background;
    [SerializeField] private RectTransform panel_Coin;
    [SerializeField] private GameObject backgroundHeader;
    [SerializeField] private TextMeshProUGUI txt_reward;
    [SerializeField] private GameObject uiHomeScreen;
    [SerializeField] private GameObject panel_Reward;
    [SerializeField] private List<GameObject> all_Offer;
    [SerializeField] private GameObject button_Close;
    [SerializeField] private GameObject button_AddCoin;
    [SerializeField] private float animationTime;
    [SerializeField] private float closeAniamationTime;
    [SerializeField] private float timeBetweentwoOffer;
    [SerializeField] private float intervalTwoAniamtion;
    [SerializeField] private RectTransform content;



    private void OnEnable()
    {
        content.anchoredPosition = new Vector2(0, 0);
    }



    public void Start()
    {
        UiStartAniamtion();
        if (DataManager.Instance.hasPurchasedNoAds)
        {
            all_Offer[0].gameObject.SetActive(false);
        }
    }

    public void OnClick_PurchaseItem(int index)
    {
        AudioManager.instance.ButtonSFX();
        IAPManager.Instance.BuyConsumable(index);
    }

    public void CoinPurchaseCompleted(int coinAmount)
    {
        closeUiAniamtion();
        Invoke("PurchaseButtonClick", animationTime);
        DataManager.Instance.AddCoin(coinAmount);
        StartCoroutine(CoinProcedure(coinAmount));
    }

  

    public void NoAdsPurchaseCompleted()
    {
        closeUiAniamtion();
        Invoke("PurchaseButtonClick", animationTime);
        DataManager.Instance.NoAdsPackage();

        all_Offer[0].SetActive(false);
        all_Offer.Remove(all_Offer[0]);
        //AdsManager.Instance.PurchasedNoAds();

        FindObjectOfType<AdsManager>().PurchasedNoAds();

        StartCoroutine(NoAdProcedure());
    }


    public void onCLick_CloseButton()
    {
        closeUiAniamtion();

        Invoke("CloseButtonClick", animationTime);
        AudioManager.instance.ButtonSFX();

    }

    private void closeScreenrocedure()
    {
       
        this.gameObject.SetActive(false);
        uiHomeScreen.SetActive(true);
       
        button_AddCoin.SetActive(true);
        uiHomeScreen.GetComponent<UiHomeScreen>().StartUiHomeScreen();


    }

    private void PurchaseButtonClick()
    {
        panel_Reward.gameObject.SetActive(true);
    }
    private void CloseButtonClick()
    {
        panel_Reward.gameObject.SetActive(false);
    }
    

    IEnumerator  CoinProcedure(int coin)
    {
       
        yield return new WaitForSeconds(closeAniamationTime);
        this.gameObject.SetActive(false);
        uiHomeScreen.SetActive(true);
        
        uiHomeScreen.GetComponent<UiHomeScreen>().StartUiHomeScreen();
       
        panel_Reward.SetActive(true);
        panel_Reward.GetComponent<UiPanelReward>().RewardSystem(coin);
        panel_Coin.anchoredPosition = new Vector2(0, 0);
        
    }

    IEnumerator NoAdProcedure()
    {

        yield return new WaitForSeconds(closeAniamationTime);
        this.gameObject.SetActive(false);
        uiHomeScreen.SetActive(true);

        uiHomeScreen.GetComponent<UiHomeScreen>().StartUiHomeScreen();

        panel_Reward.SetActive(true);
        panel_Reward.GetComponent<UiPanelReward>().RewardNoAd();
        panel_Coin.anchoredPosition = new Vector2(0, 0);

    }

    public void UiStartAniamtion()
    {
       
        Sequence start = DOTween.Sequence();
        GameManager.InstanceOfGameManager.player.gameObject.SetActive(false);
        start.AppendCallback(BackgroundAnimation).AppendCallback(CallingOffer).Append(button_Close.transform.DOScale(1, animationTime));

    }
    private void BackgroundAnimation()
    {
        background.transform.DOScale(1, animationTime);
        backgroundHeader.transform.DOScale(1, animationTime);
    }

    private void CallingOffer()
    {
        StartCoroutine(OfferAnimation());
    }

    IEnumerator OfferAnimation()
    {
        for (int i = 0; i < all_Offer.Count; i++)
        {
            all_Offer[i].transform.DOScale(1, animationTime);
            yield return new WaitForSeconds(timeBetweentwoOffer);
        }
    }

    private void closeUiAniamtion() 
    {
        Sequence seq = DOTween.Sequence();
        seq.AppendCallback(CloseUiProductAnimation).AppendInterval(closeAniamationTime).AppendCallback(CloseBgAnition).
            AppendInterval(closeAniamationTime).AppendCallback(closeScreenrocedure);
        
        
    }
    private void CloseUiProductAnimation()
    {
        button_Close.transform.DOScale(0, closeAniamationTime);
        for (int i = 0; i < all_Offer.Count; i++)
        {
            all_Offer[i].transform.DOScale(0, closeAniamationTime);
        }
    }
    private  void CloseBgAnition()
    {
        background.transform.DOScale(0, closeAniamationTime);
        backgroundHeader.transform.DOScale(0, closeAniamationTime);
    }
}
