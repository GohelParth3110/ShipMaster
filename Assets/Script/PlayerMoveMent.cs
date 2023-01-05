using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;
using Cinemachine;

public class PlayerMoveMent : MonoBehaviour
{

    

    // Rotation Properties
    [Header("ShipRotation Properties")]
    public float rotationAngle;
    private float targetAngle;
    private float currentAngle;
    public float changeAngleTime;


    [Header("Animation Properties")]
    private bool isPlayerRotating = false;
    public int animationRotation;
    public float animationTime;
    [Header("StartAnimation")]
    [SerializeField] private Vector3 AnimationStartPosition;
    [SerializeField] private Vector3 AnimationEndPosition;
    public float animationStartRotation;
    public float animationStartTime;

    [Header("Particle system")]
    [SerializeField] private ParticleSystem dieEffect;
    [SerializeField] private GameObject magneticEffect;

    [SerializeField] int delayofparticleSystem;
    private string groundTag = "Ground";
    // motion 

    [Header("Ship properties")]
    public float forceChangeTime;
    [SerializeField] private float maxHorizontalSpeed;
    private Vector3 horizontalMotion;
    private Vector3 verticalMotion;
    private float currentVerticalForce;
    private float targetVerticalForce;
    private float maxVerticalForce;
    [SerializeField] private int minSecondChangeSpeed;
    [SerializeField] private int maxSecondChangeSpeed;
    private float maxWaitTimeToChangeSpeed = 0f;
    float startTime = 0;



    [Header("Ship Components")]
    public Transform body;

    [Header("ShieldEffect")]
    public bool isShieldEffect = false;
    private float timeStart = 0;
    [SerializeField] private ParticleSystem shieldEffect;
    private bool Trigger = false;


    [SerializeField] private float minvalue;
    [SerializeField] private float maxvalue;
    private float magnetTime;
    private float magnetStartTime = 0;
    private float shieldTime;
    private bool health = true;

    private void Start()
    {
        isShieldEffect = false;
        shieldEffect.gameObject.SetActive(false);
        //maxVerticalForce = shipProperties.verticalForce;
        //maxHorizontalSpeed = shipProperties.horizontalSpeed;
        //magnetTime = shipProperties.magnet;
        //shieldTime = shipProperties.shieldDuration;

        maxVerticalForce = ShipManager.Instance.all_ShipProperties[ShipManager.Instance.currentShipSelectedIndex].verticalForce;
        maxHorizontalSpeed = ShipManager.Instance.all_ShipProperties[ShipManager.Instance.currentShipSelectedIndex].horizontalSpeed;
        magnetTime = ShipManager.Instance.all_ShipProperties[ShipManager.Instance.currentShipSelectedIndex].magnet;
        shieldTime = ShipManager.Instance.all_ShipProperties[ShipManager.Instance.currentShipSelectedIndex].shieldDuration;
        targetVerticalForce = -maxVerticalForce;
        
        GenerateRandomMaxTime();

    }

    private void GenerateRandomMaxTime()
    {
        maxWaitTimeToChangeSpeed = Random.Range(8f, 12f);
    }

    void Update()
    {
        if (GameManager.InstanceOfGameManager.isPlayerAlive)
        {
            UpdateSpeed();
            PlayerMotion();
            PlayerRotation();
            PlayerInput();

            if (isShieldEffect)
            {
               
                transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y,
                  minvalue, maxvalue), transform.position.z);

                CompleteShieldEffect();
            }
            if (GameManager.InstanceOfGameManager.isMagnetEffectPlay)
            {
               
                CompleteMagnetTime();
            }
        }


    }
    private void UpdateSpeed()
    {
        if (maxHorizontalSpeed < 40)
        {
            startTime += Time.deltaTime;

            if (startTime > maxWaitTimeToChangeSpeed)
            {
                maxHorizontalSpeed++;
                startTime = 0;
                GenerateRandomMaxTime();
            }
        }
    }
    private void PlayerInput()
    {


        if (Input.GetMouseButton(0))
        {

            targetVerticalForce = maxVerticalForce;
            targetAngle = rotationAngle;
        }
        else
        {
            targetVerticalForce = -maxVerticalForce;
            targetAngle = -rotationAngle;
        }

    }
    private void PlayerRotation()
    {
        currentAngle = Mathf.Lerp(currentAngle, targetAngle,
                            changeAngleTime * Time.deltaTime);

        transform.rotation = Quaternion.Euler(0, 0, currentAngle);
    }

    private void PlayerMotion()
    {

        currentVerticalForce = Mathf.Lerp(currentVerticalForce, targetVerticalForce,
                       forceChangeTime * Time.deltaTime);
        horizontalMotion = Vector3.right * maxHorizontalSpeed;
        verticalMotion = Vector3.up * currentVerticalForce;
        Vector3 direction = horizontalMotion + verticalMotion;
        transform.position += (direction * Time.deltaTime);

    }


    private void OnTriggerEnter(Collider other)
    {
        if (isShieldEffect)
        {
            if (other.gameObject.CompareTag(groundTag))
            {
                shieldEffect.gameObject.SetActive(false);
                AudioManager.instance.StopShieldRunningSound();
                Trigger = true;
                timeStart = 0;
            }
        }
        else
        {
            if (other.gameObject.CompareTag(groundTag))
            {
                GameManager.InstanceOfGameManager.isPlayerAlive = false;
                body.gameObject.SetActive(false);
                dieEffect.Play();
                AudioManager.instance.DieSFX();
                GameManager.InstanceOfGameManager.cineMachineCamera.GetComponent<CameraShake>().CamreShakingProcess();
                

                if (health )
                {
                   
                        GameManager.InstanceOfGameManager.ShowReviveScreen();
                        

                        health = false;
                   
                   


                }
                else
                {
                    GameManager.InstanceOfGameManager.PlayerGameOver();
                }
               
            }
        }
    }
    public void StartAnimation()
    {
        transform.position = AnimationStartPosition;

        transform.DOMove(AnimationEndPosition, animationStartTime);
        transform.DORotate(new Vector3(animationStartRotation, 0, 0),
         animationStartTime, RotateMode.FastBeyond360);
    }


    public void PlayRocketAnimation()
    {
        if (!isPlayerRotating)
        {
            isPlayerRotating = true;
            body.DOLocalRotate(new Vector3(animationRotation, 0, 0), animationTime, RotateMode.FastBeyond360)
                    .SetEase(Ease.Linear);
            Invoke("IsStartRotatingAniamation", animationTime + 0.2f);
        }

    }
    private void IsStartRotatingAniamation()
    {
        isPlayerRotating = false;
    }

    public void ShieldEffect()
    {

        isShieldEffect = true;
        timeStart = 0;
        shieldEffect.gameObject.SetActive(true);
        AudioManager.instance.PlayShieldRunningSound();

    }
    private void CompleteShieldEffect()
    {
        if (isShieldEffect)
        {
            timeStart += Time.deltaTime;


            if (timeStart > shieldTime)
            {
                shieldEffect.gameObject.SetActive(false);
                isShieldEffect = false;
                AudioManager.instance.StopShieldRunningSound();
                timeStart = 0;
            }
            if (Trigger)
            {
                if (timeStart > 0.5f)
                {
                    
                    isShieldEffect = false;
                    Trigger = false;
                    timeStart = 0;
                }
            }

        }

    }

    public void MagnetEffect()
    {
        GameManager.InstanceOfGameManager.isMagnetEffectPlay = true;
        AudioManager.instance.PlayMagnetRunningSound();
        magneticEffect.SetActive(true);
        magnetStartTime = 0;
    }
    private void CompleteMagnetTime()
    {
        magnetStartTime += Time.deltaTime;



        if (magnetStartTime > magnetTime)
        {
            AudioManager.instance.StopMagnetRunningSound();
            GameManager.InstanceOfGameManager.isMagnetEffectPlay = false;
            magneticEffect.SetActive(false);
        }
    }

}
