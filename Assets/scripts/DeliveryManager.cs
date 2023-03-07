using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class DeliveryManager : MonoBehaviour
{
    [SerializeField] private RecipeListSO recipeListSO;
    private List<RecipeSO> waitingRecipeListSO;
    private float spawnRecipeTimer;
    private const float SPAWN_RECIPE_TIMER_MAX = 4f;
    private const int RECIPE_MAX = 3;

    private int successfulRecipes = 0;
    public event EventHandler<RecipeSpawnedCompleted> OnRecipeSpawned;

    public event EventHandler<RecipeSpawnedCompleted> OnRecipeCompleted;

    public class RecipeSpawnedCompleted : EventArgs { public RecipeSO recipeSO; }

    public event EventHandler OnDeliverySuccess;
    public event EventHandler onDeliveryFail;
    public static DeliveryManager Instance
       {  get; private set;}

private void Awake()
    {
        waitingRecipeListSO = new List<RecipeSO>();
        Instance = this;
    }

    private void Update()
    {
        spawnRecipeTimer += Time.deltaTime;
        if(spawnRecipeTimer > SPAWN_RECIPE_TIMER_MAX)
        {
            spawnRecipeTimer = 0f;
            if(KitchenGameManager.Instance.IsGamePlaying() && waitingRecipeListSO.Count < RECIPE_MAX)
            {
                RecipeSO waitingRecipeSO = recipeListSO.allRecipes[UnityEngine.Random.Range(0, recipeListSO.allRecipes.Count)];

                waitingRecipeListSO.Add(waitingRecipeSO);
                OnRecipeSpawned?.Invoke(this, new RecipeSpawnedCompleted {recipeSO = waitingRecipeSO });
            }
        }
    }


    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for(int i = 0; i < waitingRecipeListSO.Count; i++)
        {
            RecipeSO waitingRecipeSO = waitingRecipeListSO[i];

            if(waitingRecipeSO.IngredientSO.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                //has same number of ingredients
                bool plateContentsMatchRecipe = true;
                
                foreach(KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.IngredientSO)
                {
                    //cycling through all ingredients in Recipe
                    bool ingredientFound = false;
                    foreach(KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        //cycling through all ingreients in plate
                        if(plateKitchenObjectSO == recipeKitchenObjectSO)
                        {
                            //Ingredient matches!
                            ingredientFound = true;
                            break;
                        }
                    }

                    if(!ingredientFound)
                    {
                        //this recipe ingredient was not found
                        plateContentsMatchRecipe = false;break;
                    }
                }

                if(plateContentsMatchRecipe)
                {
                    //player delivered recipe 
                    OnDeliverySuccess?.Invoke(this, EventArgs.Empty);
                    waitingRecipeListSO.RemoveAt(i);
                    OnRecipeCompleted?.Invoke(this, new RecipeSpawnedCompleted { recipeSO=waitingRecipeSO});
                    successfulRecipes++;
                    return;

                }
               
            }
        }
        onDeliveryFail?.Invoke(this, EventArgs.Empty);

    }

    public List<RecipeSO> GetWaitingRecipeListSO()
    {
        return waitingRecipeListSO;
    }

    public int GetSuccessfulRecipesDelivered()
    {
        return successfulRecipes;
    }
}
