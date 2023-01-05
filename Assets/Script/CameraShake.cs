using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    [SerializeField]private CinemachineVirtualCamera  cinemachine;
    [SerializeField] private float intensity;
    public float ShakeTime;

    CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;


    public void CamreShakingProcess()
    {
        cinemachineBasicMultiChannelPerlin = cinemachine.
          GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        Invoke("ShakingTimer", ShakeTime);
       
    }
    private void ShakingTimer()
    {
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;
    }


}
