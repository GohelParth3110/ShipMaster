using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;


    //[SerializeField] private AudioClip LiveMusic;

    public AudioSource audio_MusicShield;
    public AudioSource audio_MusicMagnet;
    public AudioSource audio_Music;
    public AudioSource sound_CoinCollect;
    public AudioSource sound_DieSfx;
    public AudioSource sound_MagnetCollect;
    public AudioSource sound_ShieldCollect;
    public AudioSource sound_RingTouch;
    public AudioSource sound_RingPass;
    public AudioSource sound_ButtonClick;



    private void Awake()
    {
        DontDestroyOnLoad(this);

        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        instance = this;
    }

    public void PlayBackgroundMusic()
    {
        audio_Music.Play();
    }

    public void StopBackgroundMusic()
    {
        audio_Music.Stop();
    }

    public void DieSFX()
    {
        if (DataManager.Instance.soundValue == 0)
        {
            return;
        }

        sound_DieSfx.Play();
    }
    public void ButtonSFX()
    {
        if (DataManager.Instance.soundValue == 0)
        {
            return;
        }

        sound_ButtonClick.Play();

    }
    public void CoinCollectionSfx()
    {
        if (DataManager.Instance.soundValue == 0)
        {
            return;
        }
        sound_CoinCollect.Play();
    }
    public void RingTouchSFX()
    {
        if (DataManager.Instance.soundValue == 0)
        {
            return;
        }
        sound_RingTouch.Play();
    }

    public void RingPassSFX()
    {
        if (DataManager.Instance.soundValue == 0)
        {
            return;
        }
        sound_RingPass.Play();
    }
    public void ShieldSFX()
    {
        if (DataManager.Instance.soundValue == 0)
        {
            return;
        }
        sound_ShieldCollect.Play();
    }
    public void MagnetCollectionSFX()
    {
        if (DataManager.Instance.soundValue == 0)
        {
            return;
        }
        sound_CoinCollect.Play();
    }

    public void PlayMagnetRunningSound()
    {
        if (DataManager.Instance.soundValue == 0)
        {
            return;
        }
        audio_MusicMagnet.Play();
    }
    public void StopMagnetRunningSound()
    {
        if (DataManager.Instance.soundValue == 0)
        {
            return;
        }
        audio_MusicMagnet.Stop();
    }
    public void PlayShieldRunningSound()
    {
        if (DataManager.Instance.soundValue == 0)
        {
            return;
        }
        audio_MusicShield.Play();
    }
    public void StopShieldRunningSound()
    {
        if (DataManager.Instance.soundValue == 0)
        {
            return;
        }
        audio_MusicShield.Stop();
    }

}
