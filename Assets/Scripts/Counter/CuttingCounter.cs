using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter, IKitchenObjectParent
{
    [SerializeField] private KitchenObjectSO cutKitchenObjectSO;
    private KitchenObject _kitchenObject;

    public override void Interact(PlayerController player)
    { 
        base.Interact(player);

        if (!player.HasKitchenObject() && HasKitchenObject())
        {
            _kitchenObject.SetKitchenObjectParent(player);
        }
    }
    
    public override void InteractAlternate(PlayerController player)
    {
        base.InteractAlternate(player);
        if (player.HasKitchenObject())
        {
            player.GetKitchenObject().DestroySelf();
            KitchenObject.SpawnKitchenObject(cutKitchenObjectSO, this);
        }
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
