using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter, IKitchenObjectParent
{
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;
    private KitchenObject _kitchenObject;

    public override void Interact(PlayerController player)
    { 
        base.Interact(player);

        if (HasKitchenObject())
        {
            if(!player.HasKitchenObject())
                _kitchenObject.SetKitchenObjectParent(player);
        }
    }
    
    public override void InteractAlternate(PlayerController player)
    {
        base.InteractAlternate(player);
        if (player.HasKitchenObject())
        {
            KitchenObjectSO kitchenObjectSO = GetOutputForInput(player.GetKitchenObject().GetKitchenObjectSO);
            if(kitchenObjectSO && kitchenObjectSO != player.GetKitchenObject().GetKitchenObjectSO)
            {
                player.GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(kitchenObjectSO, this);
            }
        }
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO kitchenObjectSO)
    {
        foreach (var cuttingRecipeSo in cuttingRecipeSOArray)
        {
            if(cuttingRecipeSo.input == kitchenObjectSO) 
                return cuttingRecipeSo.output;
        }
        return null;
    }
    
    public Transform GetKitchenObjectToTransform()
    {
        return CounterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this._kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return this._kitchenObject;
    }

    public void ClearKitchenObject()
    {
        this._kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return this._kitchenObject != null;
    }
}
