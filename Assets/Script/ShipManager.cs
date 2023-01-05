using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManager : MonoBehaviour
{
    public static ShipManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public ShipProperties[] all_ShipProperties;
    public int[] all_ShipPrices;
    public bool[] all_UnlockStatus;

    public int currentShipSelectedIndex;

   
}
