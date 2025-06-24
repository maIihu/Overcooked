using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    public override void Interact(PlayerController player)
    {
        if (player.HasKitchenObject())
        {
            Debug.Log("Player has kitchen");
            return;
        }
        var newKitchenObject = Instantiate(kitchenObjectSO.prefab);
        newKitchenObject.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
    }
    
    
}
