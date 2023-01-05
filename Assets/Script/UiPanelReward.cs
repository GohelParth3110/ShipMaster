using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
public class UiPanelReward : MonoBehaviour
{
    [SerializeField]private RectTransform  panel_Reward;
    [SerializeField] private UiHomeScreen uiHomeScreen;
    [SerializeField] private float diffrentPosition;
    [SerializeField] private float animationTime;
    [SerializeField] private float intervalTime;
    [SerializeField] private GameObject button_Close;
    [SerializeField] private TextMeshProUGUI txt_CoinReward;

   
    public void RewardSystem(int coin)
    {
        button_Close.SetActive(false);
        txt_CoinReward.text = coin.ToString() + " Rewarded ";
        Sequence seq = DOTween.Sequence();
        panel_Reward.anchoredPosition = new Vector2(0, 1000);
        seq.Append(panel_Reward.DOAnchorPos(new Vector2(0, -diffrentPosition), animationTime)).
            Append(panel_Reward.DOAnchorPos(new Vector2(0, 0), animationTime)).AppendInterval(intervalTime)
            .Append(panel_Reward.DOAnchorPos(new Vector2(0, diffrentPosition), animationTime)).
            Append(panel_Reward.DOAnchorPos(new Vector2(0, -1000), animationTime)).AppendInterval(0)
            .AppendCallback(CloseProcedure);


    }

    public void RewardNoAd()
    {
        button_Close.SetActive(false);
        txt_CoinReward.text = " No Ad Pack Purchased ";
        Sequence seq = DOTween.Sequence();
        panel_Reward.anchoredPosition = new Vector2(0, 1000);
        seq.Append(panel_Reward.DOAnchorPos(new Vector2(0, -diffrentPosition), animationTime)).
            Append(panel_Reward.DOAnchorPos(new Vector2(0, 0), animationTime)).AppendInterval(intervalTime)
            .Append(panel_Reward.DOAnchorPos(new Vector2(0, diffrentPosition), animationTime)).
            Append(panel_Reward.DOAnchorPos(new Vector2(0, -1000), animationTime)).AppendInterval(0)
            .AppendCallback(CloseProcedure);

    }


    private void CloseProcedure()
    {
        this.gameObject.SetActive(false);
        button_Close.SetActive(true);
        panel_Reward.anchoredPosition = new Vector2(0, 1000);
       
    }
}
