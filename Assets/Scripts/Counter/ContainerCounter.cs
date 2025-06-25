using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    private Animator _ani;

    protected override void Awake()
    {
        base.Awake();
        _ani = GetComponentInChildren<Animator>();

    }
    
    
    public override void Interact(PlayerController player)
    {
        if (!player.HasKitchenObject())
        {
            _ani.SetTrigger(ContainString.OpenClose);
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
        }
    }
    
    
}
