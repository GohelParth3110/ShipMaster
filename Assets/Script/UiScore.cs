using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txt_Score;


    private void Update()
    {
        txt_Score.text = GameManager.InstanceOfGameManager.currentScore.ToString();
    }
}
