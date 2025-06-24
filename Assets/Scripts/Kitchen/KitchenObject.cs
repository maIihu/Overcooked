using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchen;
    
    private IKitchenObjectParent _kitchenObjectParent;

    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
    {
        if (kitchenObjectParent.HasKitchenObject())
        {
            Debug.Log("KitchenObject has been set");
            return;
        }
        if (this._kitchenObjectParent != null) _kitchenObjectParent.ClearKitchenObject();
        
        this._kitchenObjectParent = kitchenObjectParent;
        kitchenObjectParent.SetKitchenObject(this);
        transform.parent = kitchenObjectParent.GetKitchenObjectToTransform();
        transform.localPosition = Vector3.zero;
    }

    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return this._kitchenObjectParent;
    }
    
    public KitchenObjectSO GetKitchenObject => kitchen;
    
}
