using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;
public class UiSetting : MonoBehaviour
{
    [SerializeField] private RectTransform panel_BackGround;
    [SerializeField] private RectTransform panel_Music;
    [SerializeField] private RectTransform panel_Sound;
    [SerializeField] private RectTransform button_Rate;
    [SerializeField] private RectTransform button_Close;
    [SerializeField] private float animationTime;
    [SerializeField] private float outAnimationTime;
    [SerializeField] private bool isMusic;
    [SerializeField] private bool isSound;
    [SerializeField] private GameObject imageOfMusicTickBox;
    [SerializeField] private GameObject imageOfSoundTickBox;
    [SerializeField] private UiHomeScreen uiHomeScreen;





    public void OnClick_MusiC()
    {
        AudioManager.instance.ButtonSFX();
        if (DataManager.Instance.musicValue == 1)
        {
            DataManager.Instance.MusicValue(0);
            imageOfMusicTickBox.SetActive(false);
            isMusic = false;
        }
        else
        {
            DataManager.Instance.MusicValue(1);
            imageOfMusicTickBox.SetActive(true);
            isMusic = true;
        }

    }
   

    public void OnClick_Sound()
    {
        AudioManager.instance.ButtonSFX();
        if (DataManager.Instance.soundValue == 0)
        {
            DataManager.Instance.SoundValue(1);
            imageOfSoundTickBox.SetActive(true);
            isSound = false;
        }
        else
        {
            DataManager.Instance.SoundValue(0);
            imageOfSoundTickBox.SetActive(false);
            isSound = true;
        }


    }
    private void Start()
    {
        
        if (DataManager.Instance.musicValue == 0)
        {
            imageOfMusicTickBox.SetActive(false);
        }
        else
        {
            imageOfMusicTickBox.SetActive(true);
        }


        if (DataManager.Instance.soundValue == 0)
        {
            imageOfSoundTickBox.SetActive(false);
        }
        else

            imageOfSoundTickBox.SetActive(true);

    }
    public void SettingStartUiAnimation()
    {
        GameManager.InstanceOfGameManager.player.gameObject.SetActive(false);
        Sequence seq = DOTween.Sequence();
        seq.Append(panel_BackGround.DOScale(1, animationTime)).AppendCallback(StartUiInfoAniamtion).
            Append(button_Close.DOScale(1, animationTime));
    }
    private void StartUiInfoAniamtion()
    {
        panel_Music.DOAnchorPos(new Vector2(0, 0), animationTime);
        panel_Sound.DOAnchorPos(new Vector2(0, 0), animationTime);
        button_Rate.DOScale(1, animationTime);
    }

    public void OnClick_CloseButton()
    {
        CloseUiAnimation();
       

    }
    private void CloseUiAnimation()
    {
        panel_BackGround.DOScale(0, outAnimationTime);
        panel_Music.DOAnchorPos(new Vector2(-1500, 0), outAnimationTime);
        panel_Sound.DOAnchorPos(new Vector2(1500, 0), outAnimationTime);
        button_Rate.DOScale(0, outAnimationTime);
        button_Close.DOScale(0, outAnimationTime);

        Invoke("StartHomeScreen", outAnimationTime);
    }

    private void StartHomeScreen()
    {
        this.gameObject.SetActive(false);
        uiHomeScreen.gameObject.SetActive(true);
        uiHomeScreen.StartUiHomeScreen();
       
    }
}
