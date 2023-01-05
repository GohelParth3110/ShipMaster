using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiGamePlay : MonoBehaviour
{
    [SerializeField] private Image rewiveImage;
    [SerializeField] private float delayStart;
    [SerializeField] private TextMeshProUGUI txt_Second;
    [SerializeField] private GameObject Revive;
    public bool isRewardReviveButtonPress;
    
    private float currentSecond = 3;

    private void Update()
    {
       

        rewiveImage.fillAmount -= 1.0f / 3 * Time.deltaTime;
         currentSecond -=   Time.deltaTime;

        txt_Second.text = Mathf.Round(currentSecond).ToString();
        if (rewiveImage.fillAmount <= 0)
        {
            GameManager.InstanceOfGameManager.PlayerGameOver();
            gameObject.SetActive(false);
        }
    }

    public void OnClick_ReviveButton()
    {
        AudioManager.instance.ButtonSFX();
        FindObjectOfType<AdsManager>().ShowRewardAd();
        Revive.gameObject.SetActive(false);

    }

    public void PlayCurrentGame()
    {
        Vector3 Player = GameManager.InstanceOfGameManager.player.transform.position;

        GameManager.InstanceOfGameManager.player.transform.position = new Vector3(Player.x, 15, Player.z);
       
        Revive.gameObject.SetActive(false);
       
        GameManager.InstanceOfGameManager.isPlayerAlive = true;
        
        GameManager.InstanceOfGameManager.player.body.gameObject.SetActive(true);

        GameManager.InstanceOfGameManager.player.ShieldEffect();
    
    }
}
