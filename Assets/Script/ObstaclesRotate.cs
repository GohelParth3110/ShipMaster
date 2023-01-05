using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;


public class ObstaclesRotate : MonoBehaviour
{
    [SerializeField] private bool move;
     private int moveSpeed = 10;
    [SerializeField] private float moveMentValue;
    [SerializeField] private bool rotation;
    private int rotationalSpeed = 5;

    void Start()
    {
        RotateGameObject();
        MoveGameObject();
    }
    private void RotateGameObject()
    {
        if (rotation)
        {
            int randomRotationalSpeed = Random.Range(1, rotationalSpeed);
            transform.DORotate(new Vector3(0, 0, 180), randomRotationalSpeed).
                SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);

        }
    }
    private void MoveGameObject()
    {
        if (move)
        {
            int randomMoveSpeed = Random.Range(5,moveSpeed);
            transform.DOMoveY(moveMentValue,randomMoveSpeed , false).
                SetLoops(-1,LoopType.Yoyo).SetEase(Ease.Unset);
        }
        
    }
}
