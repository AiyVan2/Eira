using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public float shakeIntensity;  
    public float shakeDuration; 

    private CinemachineBasicMultiChannelPerlin perlin;
    private float shakeTime;

    void Start()
    {
        perlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }
    void Update()
    {
        if (shakeTime > 0)
        {
            perlin.m_AmplitudeGain = Mathf.Lerp(shakeIntensity, 0f, 1 - (shakeTime / shakeDuration));
            perlin.m_FrequencyGain = Mathf.Lerp(shakeIntensity, 0f, 1 - (shakeTime / shakeDuration));
            shakeTime -= Time.deltaTime;
        }
        else
        {
            perlin.m_AmplitudeGain = 0f;
            perlin.m_FrequencyGain = 0f;
        }
    }
    public void TriggerShake()
    {
        shakeTime = shakeDuration;
        perlin.m_AmplitudeGain = shakeIntensity;
        perlin.m_FrequencyGain = shakeIntensity;
    }
}
