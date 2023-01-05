using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingCollisionDetector : MonoBehaviour
{
    [SerializeField] private Ring ring;
    [SerializeField] private int index;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ring.PlayerCollidedWithRing(index, other);
            AudioManager.instance.RingTouchSFX();
        }
    }
}
