
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeliveryCounter : BaseCounter
{
    
    public override void Interact(Player player)
    {
        base.Interact(player);
        if (player.HasKitchenObject())
        {
            if (player.GetKitchenObject() is PlateKitchenObject plateKitchenObject)
            {
                DeliveryManager.Instance.DeliverRecipe(plateKitchenObject);
                player.GetKitchenObject().DestroySelf();
                SoundManagerScript.PlaySound(SoundManagerScript.GetAudioClipRefesSO().objectDrop, this.transform.position);
            }
        }
    }
}
