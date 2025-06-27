
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeliveryCounter : BaseCounter
{
    [SerializeField] private RecipeListSO recipeListSO;

    private List<RecipeSO> _waitingRecipeSOList;
    private float _spawnRecipeTimer;
    private float _spawnRecipeTimerMax = 4f;
    private int _waitingRecipeMax;

    protected override void Start()
    {
        base.Start();
        _waitingRecipeSOList = new List<RecipeSO>();    
    }

    private void Update()
    {
        _spawnRecipeTimer -= Time.deltaTime;
        if (_spawnRecipeTimer <= 0f)
        {
            _spawnRecipeTimer = _spawnRecipeTimerMax;
            if(_waitingRecipeSOList.Count < _waitingRecipeMax)
            {
                var waitingRecipeSO = recipeListSO.recipeSOList[Random.Range(0, recipeListSO.recipeSOList.Count)];
                _waitingRecipeSOList.Add(waitingRecipeSO);
            }
        }
    }

    public override void Interact(Player player)
    {
        base.Interact(player);
    }
}
