using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
   // [SerializeField] private KitchenOjbectSO kitchen;
    [SerializeField] private Transform counterTopPoint;
    
    public void Interact()
    {
        Debug.Log("Interacting...");   
       //    Instantiate(kitchen.prefab, counterTopPoint.position, Quaternion.identity);
    }
}
