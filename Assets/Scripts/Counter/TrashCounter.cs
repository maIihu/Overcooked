using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public override void Interact(Player player)
    {
        base.Interact(player);
        if(player.HasKitchenObject())
        {
            player.GetKitchenObject().DestroySelf();
            SoundManagerScript.PlaySound(SoundManagerScript.GetAudioClipRefesSO().objectDrop, this.transform.position);
            SoundManagerScript.PlaySound(SoundManagerScript.GetAudioClipRefesSO().trash, this.transform.position);
        }
    }
}
