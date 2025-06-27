using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct KitchenObjectSO_GameObject
    {
        public KitchenObjectSO KitchenObject;
        public GameObject GameObject;
    }

    [SerializeField] private List<KitchenObjectSO_GameObject> kitchenObjectSoGameObjects;
    
    private void Start()
    {
        foreach (var x in kitchenObjectSoGameObjects)
            x.GameObject.SetActive(false);
    }

    public void AddKitchenObjectVisual(KitchenObjectSO kitchenObjectSO)
    {
        foreach (var x in kitchenObjectSoGameObjects)
        {
            if (x.KitchenObject == kitchenObjectSO)
            {
                x.GameObject.SetActive(true);
            }
        }
    }
    
}
