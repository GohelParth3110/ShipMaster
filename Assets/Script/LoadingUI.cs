using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadingUI : MonoBehaviour
{
    [SerializeField] private float loadingTime;
    [SerializeField] private float startPosition;
    [SerializeField] private float endPosition;
    [SerializeField] private GameObject player;
    [SerializeField] private Slider loadingSlider;
    [SerializeField] private UiHomeScreen uiHOmeScreen;
    [SerializeField] private TextMeshProUGUI txt_LoadingPanel;
    [SerializeField] private TextMeshProUGUI txt_PlayerPanel;
    private void Start()
    {
        bool isLoadingFirstTime = FindObjectOfType<AdsManager>().isFirstTime;

        if (isLoadingFirstTime)
        {
            StartCoroutine(LoadingPanel());
            FindObjectOfType<AdsManager>().isFirstTime = false;
        }
        else
        {
            EndOfLoadinfSystem();
        }
    }

    IEnumerator LoadingPanel()
    {
        float startTime = 0;
        while (startTime < loadingTime)
        {
            loadingSlider.value = Mathf.Lerp(0, 100, startTime / loadingTime);
            txt_LoadingPanel.text = loadingSlider.value.ToString("F0") + "%";
            txt_PlayerPanel.text = loadingSlider.value.ToString("F0") + "%";
            float playerPosition = Mathf.Lerp(-30, 100, startTime / loadingTime);
            player.transform.position = new Vector3(playerPosition, player.transform.position.y, player.transform.position.z);
            startTime += Time.deltaTime;

            yield return null;
        }
        EndOfLoadinfSystem();
    }

  private void EndOfLoadinfSystem()
    {
        Destroy(player);
        GameManager.InstanceOfGameManager.PlayerSelection(DataManager.Instance.CurrentPlayerIndex);
        uiHOmeScreen.gameObject.SetActive(true);
        uiHOmeScreen.StartUiHomeScreen();
        this.gameObject.SetActive(false);
        
    }
}

