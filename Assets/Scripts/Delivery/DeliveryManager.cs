using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance { get; private set; }

    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    
    [SerializeField] private RecipeListSO recipeListSO;

    private List<RecipeSO> _waitingRecipeSOList;
    private float _spawnRecipeTimer;
    private float _spawnRecipeTimerMax = 4f;
    private int _waitingRecipeMax = 4;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    public void Start()
    {
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
                Debug.Log("Wait for " + waitingRecipeSO.name);
                _waitingRecipeSOList.Add(waitingRecipeSO);
                
                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < _waitingRecipeSOList.Count; i++)
        {
            RecipeSO waitingRecipeSO = _waitingRecipeSOList[i];
            var waitingRecipeList = waitingRecipeSO.kitchenObjectSOList;
            var kitchenPlateList = plateKitchenObject.GetKitchenObjectSOs();
            if (waitingRecipeList.Count == kitchenPlateList.Count && !waitingRecipeList.Except(kitchenPlateList).Any())
            {
                Debug.Log("!!!!Player delivered the correct Recipe");
                _waitingRecipeSOList.RemoveAt(i);
                OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                return;
            }
        }
        Debug.Log("!!!!Player did not delivered a correct Recipe");
    }

    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return _waitingRecipeSOList;
    }
}
