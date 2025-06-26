using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchen;
    
    protected IKitchenObjectParent KitchenObjectParent;

    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
    {
        if (kitchenObjectParent.HasKitchenObject())
        {
            Debug.Log("KitchenObject has been set");
            return;
        }
        if (this.KitchenObjectParent != null) KitchenObjectParent.ClearKitchenObject();
        
        this.KitchenObjectParent = kitchenObjectParent;
        kitchenObjectParent.SetKitchenObject(this);
        transform.parent = kitchenObjectParent.GetKitchenObjectToTransform();
        transform.localPosition = Vector3.zero;
    }

    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return this.KitchenObjectParent;
    }

    public void DestroySelf()
    {
        KitchenObjectParent.ClearKitchenObject();
        Destroy(this.gameObject);
    }

    public static void SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent)
    {
        Transform newKitchenObject = Instantiate(kitchenObjectSO.prefab);
        newKitchenObject.TryGetComponent(out KitchenObject kitchenObject);
        kitchenObject.SetKitchenObjectParent(kitchenObjectParent);
       // return kitchenObject;
    }
    
    public KitchenObjectSO GetKitchenObjectSO => kitchen;
    
}