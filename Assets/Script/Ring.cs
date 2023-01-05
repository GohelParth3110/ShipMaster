using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Ring : MonoBehaviour
{   
    [Header("Collider Information")]
    [SerializeField]private MeshCollider meshCol;
    [SerializeField]private SphereCollider sphereCol;
    [Header("Score")]
    [SerializeField]private int meshCollScore;
    [SerializeField] private int sphereCollScore;
    [Header("Animation Information")]
    [SerializeField] private float meshColliderScale;
    [SerializeField] private float sphereColliderScale;
    [SerializeField] private float playerRotation;
    [SerializeField] private float animationTime;
    [SerializeField] private Material Material;
    



    public void PlayerCollidedWithRing(int index, Collider playerCol)
    {   
        meshCol.enabled = false;
        sphereCol.enabled = false;
        
        if (index == 0)
        {         
            transform.DOScale(meshColliderScale, animationTime);
            Invoke("OnDisable", animationTime + 0.1f);
            GameManager.InstanceOfGameManager.UpdateScore(meshCollScore);
            AudioManager.instance.RingTouchSFX();
        }
        else
        {

            AudioManager.instance.RingPassSFX();
            playerCol.GetComponentInParent<PlayerMoveMent>().PlayRocketAnimation();
            Material ringMaterial = GetComponentInChildren<MeshRenderer>().material;
            GameManager.InstanceOfGameManager.UpdateScore(sphereCollScore);
            ringMaterial.DOColor(Color.white, animationTime);
            transform.DOScale(sphereColliderScale, animationTime);
           Invoke("OnDisable", animationTime + 0.1f);
            
        }
    }
    private void OnDisable()
    {
        meshCol.enabled = true;
        sphereCol.enabled = true;
        gameObject.SetActive(false);
        transform.localScale = new Vector3(2, 2, 2);
        GetComponentInChildren<MeshRenderer>().material.color = Material.color;
        
    }



}
