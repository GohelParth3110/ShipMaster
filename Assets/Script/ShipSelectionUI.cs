using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using TMPro;
using Cinemachine;

public class ShipSelectionUI : MonoBehaviour
{
    public ShipProperties[] all_Player;
    [SerializeField] private Transform[] all_panelTrasform;
    [SerializeField] private GameObject ui_HomeScreen;
    [SerializeField] private TextMeshProUGUI txt_Coin;
    [SerializeField] private TextMeshProUGUI txt_Shield;
    [SerializeField] private TextMeshProUGUI txt_HorizontalSpeed;
    [SerializeField] private TextMeshProUGUI txt_VerticalSpeed;
    [SerializeField] private TextMeshProUGUI txt_Magnet;
    [SerializeField] private GameObject lockedPanel;
    [SerializeField] private GameObject selectedPanel;
    private int currentShipIndex = 0;
    private int previousShipIndex;
    
    [SerializeField] Vector3 rotation;
    [SerializeField] float scale;
    [SerializeField] private bool isAnimationPlaying;
    [SerializeField]private GameObject[] all_ImgSelected ;
    [SerializeField]private GameObject[] all_ImgLocked;
    [SerializeField] private GameObject closeButton;
    [Header("Ship Animation Properties")]
    [SerializeField] private Vector3 shownPosition;
    [SerializeField] private Vector3 leftPosition;
    [SerializeField] private Vector3 rightPosition;
    [SerializeField] private float rotationAngle;
    [SerializeField] private float animationTime;
    [Header("Ui Animation Properites")]
    [SerializeField] private float animationSec;
    [SerializeField] private RectTransform scrollPanel;
    [SerializeField] private RectTransform closebutton;
    [SerializeField] private RectTransform shipProperitesBGPanel;
    [SerializeField] private Transform[] all_ShipProperites;
    [SerializeField] private float timebetweenTwoShipProperiies;
    [SerializeField] private RectTransform coin;
    [SerializeField] private ParticleSystem die;
    [SerializeField] private RectTransform panel_Lock;
    private bool isUiAnimationPlaying;
    [SerializeField] private float[] All_PositionOfPanel;
    [SerializeField] private RectTransform content;
   



    private void OnEnable()
    {
        currentShipIndex = DataManager.Instance.CurrentPlayerIndex;
        content.anchoredPosition = new Vector2(All_PositionOfPanel[currentShipIndex], 0);
        ShipSeclectionUiStartingAnimation();  // this method All Ui Animation
        DefaultPlayerArrangeMent();     // this method First Player Arrange in Screen

        
    }

   


    public  void Onclick_Buy()
    {
        AudioManager.instance.ButtonSFX();
        int price = ShipManager.Instance.all_ShipPrices[currentShipIndex];

        if (DataManager.Instance.totalCoins < price)
        {
            Debug.Log("No enough coin");
            return;
        }

        DataManager.Instance.SubtractCoin(price);
        ShipManager.Instance.all_UnlockStatus[currentShipIndex] = true;
        DataManager.Instance.UnlockShip(currentShipIndex);
        all_ImgLocked[currentShipIndex].SetActive(false);
        all_ImgSelected[currentShipIndex].SetActive(true);
        Sequence sew = DOTween.Sequence();
        sew.Append(panel_Lock.DOScale(0, animationTime)).AppendCallback(die.Play);
        die.Play();
        ButtonLockedChecking();

        Sequence seq = DOTween.Sequence();
        seq.Append(lockedPanel.transform.DOScale(0, animationTime)).Append(selectedPanel.transform.DOScale(1, animationTime));
        
      
    }
    private void ShipSeclectionUiStartingAnimation()
    {
        scrollPanel.DOAnchorPos(new Vector2(0, 0), animationSec);   //scroll panel move Upword
        closebutton.DOScale(1, animationSec);                   // all button Scale 0to 1;
        coin.DOAnchorPos(new Vector2(0, 0), animationSec);
        shipProperitesBGPanel.DOScale(1, animationSec);
        ShipProperitesUiAnimation();    // line by line All properites animation
        if (ShipManager.Instance.all_UnlockStatus[currentShipIndex])
        {
            selectedPanel.transform.DOScale(1, animationSec);
           

        }
        else
        {
            lockedPanel.transform.DOScale(1, animationSec);
            

        }
    }

    private void DefaultPlayerArrangeMent()
    {
        all_Player[currentShipIndex].gameObject.SetActive(true);    // ship active
        all_Player[currentShipIndex].transform.DOMove(shownPosition, animationTime); // ship animation
       
        PlayerProperites(currentShipIndex);     // getPlayer Properites
        ButtonLockedChecking();                 // checking All unlockShip
        previousShipIndex = currentShipIndex;   //index changing
         GameManager.InstanceOfGameManager.player.gameObject.SetActive(false);
      

        
       
    }

    public void OnSelectedButton()
    {
        AudioManager.instance.ButtonSFX();
        DataManager.Instance.CurrenPlayerSelectionIndex(currentShipIndex);
        Destroy(GameManager.InstanceOfGameManager.player.gameObject);
       all_ImgSelected[currentShipIndex].gameObject.SetActive(false);
        all_Player[currentShipIndex].gameObject.SetActive(false);
        ui_HomeScreen.SetActive(true);
        coin.anchoredPosition = new Vector2(0, 600);
       
        DataManager.Instance.CurrenPlayerSelectionIndex(currentShipIndex);
        GameManager.InstanceOfGameManager.PlayerSelection(currentShipIndex);
        ui_HomeScreen.GetComponent<UiHomeScreen>().StartUiHomeScreen();
        this.gameObject.SetActive(false);
    }
   


    public void OnClose_ButtonClick()
    {
        AudioManager.instance.ButtonSFX();

        all_Player[currentShipIndex].gameObject.SetActive(false);
        Sequence seq = DOTween.Sequence();
        seq.AppendCallback(ShipSelectionCloseAnimation).AppendInterval(timebetweenTwoShipProperiies).
            AppendCallback(ShipProperitesReduceScaleUI).Append(shipProperitesBGPanel.DOScale(0, animationSec)).AppendInterval(animationSec)
            .AppendCallback(CloseSystem);
    }

    public void AppPurchase()
    {
        AudioManager.instance.ButtonSFX();
        all_Player[currentShipIndex].gameObject.SetActive(false);
        Sequence seq = DOTween.Sequence();
        seq.AppendCallback(ShipSelectionCloseAnimation).AppendInterval(timebetweenTwoShipProperiies).
            AppendCallback(ShipProperitesReduceScaleUI).Append(shipProperitesBGPanel.DOScale(0, animationSec)).AppendInterval(animationSec)
            .AppendCallback(AppPurchaseClose);
    }

    private void AppPurchaseClose()
    {
        this.gameObject.SetActive(false);

    }
    private void CloseSystem()
    {
        this.gameObject.SetActive(false);
        ui_HomeScreen.gameObject.SetActive(true);
        lockedPanel.transform.localScale = new Vector3(0, 0, 0);
        selectedPanel.transform.localScale = new Vector3(0, 0, 0);
        GameManager.InstanceOfGameManager.player.gameObject.SetActive(true);
        ui_HomeScreen.GetComponent<UiHomeScreen>().StartUiHomeScreen();
        coin.anchoredPosition = new Vector2(0, 600);
       
    }
    public void OnClick_Ship(int shipIndex)
    {
        AudioManager.instance.ButtonSFX();
        Sequence seq = DOTween.Sequence();
        Sequence sequence = DOTween.Sequence();
        if (currentShipIndex == shipIndex)
        {
            return;
        }
        if (isAnimationPlaying)
        {
            return;
        }
        if (isUiAnimationPlaying)
        {
            return;
        }
        isAnimationPlaying = true;
        previousShipIndex = currentShipIndex;
        currentShipIndex = shipIndex;
          seq.AppendCallback(ShipProperitesReduceScaleUI).AppendInterval(timebetweenTwoShipProperiies).
            Append(shipProperitesBGPanel.DOScale(0,animationSec)).AppendInterval(timebetweenTwoShipProperiies).
            Append(shipProperitesBGPanel.DOScale(1, animationSec)).AppendCallback(ShipProperitesUiAnimation);

        all_Player[currentShipIndex].gameObject.SetActive(true);
        all_ImgSelected[currentShipIndex].SetActive(true);
        all_ImgSelected[previousShipIndex].SetActive(false);
        PlayerProperites(currentShipIndex);

        all_Player[currentShipIndex].transform. DOMove(shownPosition, animationTime);
        all_Player[currentShipIndex].transform.GetChild(0).DOLocalRotate(new Vector3(rotationAngle, 0, 0),
            animationTime, RotateMode.WorldAxisAdd);

        sequence.Append(all_Player[previousShipIndex].transform.DOMove(rightPosition, animationTime)).
           AppendCallback(PlayerGetOriginalPos);
    }
    private void PlayerProperites(int index)
    {
        Sequence seq = DOTween.Sequence();
        Sequence sequence = DOTween.Sequence();
       
        
        txt_Coin.text = ShipManager.Instance.all_ShipPrices[index].ToString();
        txt_Shield.text = all_Player[index].shieldDuration.ToString() + " s";
        txt_HorizontalSpeed.text = all_Player[index].horizontalSpeed.ToString();
        txt_VerticalSpeed.text = all_Player[index].verticalForce.ToString();
        txt_Magnet.text = all_Player[index].magnet.ToString() + " s";

        Transform scaleDownButton;
        Transform scaleUpButton;

        if (ShipManager.Instance.all_UnlockStatus[previousShipIndex])
        {
            scaleDownButton = selectedPanel.transform;
            panel_Lock.gameObject.SetActive(false);
                
        }
        else
        {
            scaleDownButton = lockedPanel.transform;
            panel_Lock.gameObject.SetActive(true);
        }

        if (ShipManager.Instance.all_UnlockStatus[currentShipIndex])
        {
            scaleUpButton = selectedPanel.transform;
            panel_Lock.gameObject.SetActive(false);
        }
        else
        {
            scaleUpButton = lockedPanel.transform;
            panel_Lock.gameObject.SetActive(true);
        }

        seq.Append(scaleDownButton.DOScale(0, animationSec)).AppendInterval(timebetweenTwoShipProperiies).
             Append(scaleUpButton.DOScale(1, animationSec));

        sequence.Append(panel_Lock.DOScale(0, animationSec)).AppendInterval(timebetweenTwoShipProperiies).
            Append(panel_Lock.DOScale(1, animationSec));

 }
    


    private void ButtonLockedChecking()
    {
        for (int i = 0; i < ShipManager.Instance.all_UnlockStatus.Length; i++)
        {
            if (ShipManager.Instance.all_UnlockStatus[i])
            {
                all_ImgLocked[i].SetActive(false);
            }
        }
    }

    private void PlayerGetOriginalPos()
    {
        all_Player[previousShipIndex].transform.position = leftPosition;
        all_Player[previousShipIndex].gameObject.SetActive(false);
        isAnimationPlaying = false;
    }


   
    private void ShipSelectionCloseAnimation()
    {
        scrollPanel.DOAnchorPos(new Vector2(0, -1000), animationSec);
        selectedPanel.transform.DOScale(0, animationSec);
        lockedPanel.transform.DOScale(0, animationSec);
        closeButton.transform.DOScale(0, animationSec);
        
    }

    private void ShipProperitesUiAnimation()
    {
        isUiAnimationPlaying = true;
        Sequence seq = DOTween.Sequence();
        seq.Append(all_ShipProperites[0].DOScale(1, animationSec)).AppendInterval(timebetweenTwoShipProperiies).
           Append(all_ShipProperites[1].DOScale(1, animationSec)).AppendInterval(timebetweenTwoShipProperiies).
             Append(all_ShipProperites[2].DOScale(1, animationSec)).AppendInterval(timebetweenTwoShipProperiies).
              Append(all_ShipProperites[3].DOScale(1, animationSec)).AppendInterval(timebetweenTwoShipProperiies).
              AppendCallback(CompleteUIAnimation);
        
    }
    private void CompleteUIAnimation()
    {
        isUiAnimationPlaying = false;
    }

    private void ShipProperitesReduceScaleUI()
    {
       
        for (int i = 0 ; i < all_ShipProperites.Length; i++)
        {
            all_ShipProperites[i].DOScale(0, animationSec);
        }
    }
}
