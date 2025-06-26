using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter, IKitchenObjectParent
{
    public event EventHandler<OnProgressBarChangedEventArgs> OnProgressBarChanged;
    public class OnProgressBarChangedEventArgs : EventArgs
    {
        public float progressNormalized;
    }
    
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;
    private KitchenObject _kitchenObject;
    private float _cuttingProgress;
    private Animator _ani;

    protected override void Awake()
    {
        base.Awake();
        _ani = GetComponentInChildren<Animator>();
    }

    public override void Interact(Player player)
    { 
        base.Interact(player);

        if (HasKitchenObject())
        {
            if(!player.HasKitchenObject())
            {
                _kitchenObject.SetKitchenObjectParent(player);
            }
        }
        else
        {
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
                _cuttingProgress = 0;
                OnProgressBarChanged?.Invoke(this, 
                    new OnProgressBarChangedEventArgs() { progressNormalized = 0 });
            }
        }
    }
    
    public override void InteractAlternate(Player player)
    {
        base.InteractAlternate(player);
        if (HasKitchenObject())
        {
            var cuttingRecipe = GetCuttingRecipeSOForInput(GetKitchenObject().GetKitchenObjectSO);
            if (cuttingRecipe)
            {
                _cuttingProgress += Time.deltaTime;
                _ani.SetBool(ContainString.Cut, true);
                OnProgressBarChanged?.Invoke(this, 
                    new OnProgressBarChangedEventArgs() 
                        { progressNormalized = _cuttingProgress/cuttingRecipe.cuttingProgressMax });
                if (_cuttingProgress >= cuttingRecipe.cuttingProgressMax)
                {
                    _ani.SetBool(ContainString.Cut, false);
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

    #region IKitchenObjectParent

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

    #endregion
    

}
