using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance { get; private set; }
    public class RecipeSpawnedEventArgs : EventArgs
    {
        public WaitingRecipe waitingRecipe;

    }
    public event EventHandler<RecipeSpawnedEventArgs> OnRecipeSpawned;
    public event EventHandler<RecipeSpawnedEventArgs> OnRecipeDestroy;
    
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;

    public class WaitingRecipe
    {
        public RecipeSO recipeSO;
        public float timer;
        public GameObject uiObject; 

        public WaitingRecipe(RecipeSO recipeSO)
        {
            this.recipeSO = recipeSO;
            this.timer = recipeSO.countDownTimer;
        }
    }
    
    [SerializeField] private RecipeListSO recipeListSO;

    private List<WaitingRecipe> _waitingRecipeList;


    private float _spawnRecipeTimer;
    private float _spawnRecipeTimerMax = 4f;
    private int _waitingRecipeMax = 4;
    
    private SoundManager _soundManager;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    private void Start()
    {
        _waitingRecipeList = new List<WaitingRecipe>();
        _soundManager = SoundManager.Instance;
    }

    private void Update()
    {
        _spawnRecipeTimer -= Time.deltaTime;
        if (_spawnRecipeTimer <= 0f)
        {
            _spawnRecipeTimer = _spawnRecipeTimerMax;
            if(_waitingRecipeList.Count < _waitingRecipeMax)
            {
                var recipeSO = recipeListSO.recipeSOList[Random.Range(0, recipeListSO.recipeSOList.Count)];
                var waitingRecipe = new WaitingRecipe(recipeSO);
                _waitingRecipeList.Add(waitingRecipe);

                OnRecipeSpawned?.Invoke(this, new RecipeSpawnedEventArgs
                {
                    waitingRecipe = waitingRecipe
                });

            }
        }
        
        for (int i = _waitingRecipeList.Count - 1; i >= 0; i--)
        {
            _waitingRecipeList[i].timer -= Time.deltaTime;

            if (_waitingRecipeList[i].timer <= 0f)
            {
                var expiredRecipe = _waitingRecipeList[i];
                Debug.Log("Hết thời gian: " + expiredRecipe.recipeSO.name);

                Destroy(expiredRecipe.uiObject); 
                _waitingRecipeList.RemoveAt(i);
                
                OnRecipeFailed?.Invoke(this,EventArgs.Empty);
                
                OnRecipeDestroy?.Invoke(this, new RecipeSpawnedEventArgs
                {
                    waitingRecipe = expiredRecipe
                });
            }

        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < _waitingRecipeList.Count; i++)
        {
            RecipeSO waitingRecipeSO = _waitingRecipeList[i].recipeSO;
            var waitingRecipeList = waitingRecipeSO.kitchenObjectSOList;
            var kitchenPlateList = plateKitchenObject.GetKitchenObjectSOs();

            if (waitingRecipeList.Count == kitchenPlateList.Count &&
                !waitingRecipeList.Except(kitchenPlateList).Any())
            {
                Debug.Log("!!!!Player delivered the correct Recipe");

                Destroy(_waitingRecipeList[i].uiObject);

                var deliveredRecipe = _waitingRecipeList[i];
                _waitingRecipeList.RemoveAt(i);
                
                OnRecipeSuccess?.Invoke(this,EventArgs.Empty);
                
                OnRecipeDestroy?.Invoke(this, new RecipeSpawnedEventArgs
                {
                    waitingRecipe = deliveredRecipe
                });

                _soundManager.PlaySound(_soundManager.GetAudioClipRefesSO().deliverySuccess, this.transform.position);
                return;
            }
        }

        _soundManager.PlaySound(_soundManager.GetAudioClipRefesSO().deliveryFail, this.transform.position);
        Debug.Log("!!!!Player did not deliver a correct Recipe");
    }


    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return _waitingRecipeList.Select(r => r.recipeSO).ToList();
    }

}
