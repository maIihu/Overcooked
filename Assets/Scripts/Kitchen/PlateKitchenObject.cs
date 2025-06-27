
using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOs;
    
    private List<KitchenObjectSO> _kitchenObjectSOs;
    private PlateCompleteVisual _plateVisual;
    private PlateIconUI _plateIconUI;

    private void Awake()
    {
        _plateVisual = GetComponentInChildren<PlateCompleteVisual>();
        _plateIconUI = GetComponentInChildren<PlateIconUI>();

    }

    private void Start()
    {
        _kitchenObjectSOs = new List<KitchenObjectSO>();
    }

    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        if (!validKitchenObjectSOs.Contains(kitchenObjectSO) || _kitchenObjectSOs.Contains(kitchenObjectSO))
            return false;
        
        _kitchenObjectSOs.Add(kitchenObjectSO);
        _plateVisual.AddKitchenObjectVisual(kitchenObjectSO);
        _plateIconUI.UpdateVisualIcon();
        return true;
    }

    public List<KitchenObjectSO> GetKitchenObjectSOs()
    {
        return _kitchenObjectSOs;
    }
}
