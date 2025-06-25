using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter, IKitchenObjectParent
{
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;
    private KitchenObject _kitchenObject;
    private int _cuttingProgress;
    
    public override void Interact(PlayerController player)
    { 
        base.Interact(player);

        if (HasKitchenObject())
        {
            if(!player.HasKitchenObject())
                _kitchenObject.SetKitchenObjectParent(player);
        }
        else
        {
            if (player.HasKitchenObject())
            {
                _cuttingProgress = 0;
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
        }
    }
    
    public override void InteractAlternate(PlayerController player)
    {
        base.InteractAlternate(player);
        if (HasKitchenObject())
        {
            var cuttingRecipe = GetCuttingRecipeSOForInput(GetKitchenObject().GetKitchenObjectSO);
            if (cuttingRecipe)
            {
                _cuttingProgress++;
                if (_cuttingProgress >= 3)
                {
                    GetKitchenObject().DestroySelf();
                    KitchenObject.SpawnKitchenObject(cuttingRecipe.output, this);
                }
            }
        }
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO kitchenObjectSO)
    {
        var cuttingRecipeSO = GetCuttingRecipeSOForInput(kitchenObjectSO);
        return cuttingRecipeSO ? cuttingRecipeSO.output : null;
    }
    
    private CuttingRecipeSO GetCuttingRecipeSOForInput(KitchenObjectSO kitchenObjectSO)
    {
        foreach (var cuttingRecipeSo in cuttingRecipeSOArray)
        {
            if(cuttingRecipeSo.input == kitchenObjectSO) 
                return cuttingRecipeSo;
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
