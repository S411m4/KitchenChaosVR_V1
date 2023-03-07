using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StoveCounter : BaseCounter, IHasProgressBar
{
    [SerializeField] FryingRecipeSO[] fryingRecipeArray;
    [SerializeField] BurningRecipeSO[] burningRecipeArray;

    private float fryingTimer;
    private float burningTimer;

    private FryingRecipeSO fryingRecipeSO;
    private BurningRecipeSO burningRecipeSO;


    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public event EventHandler<IHasProgressBar.OnProgressChangedEventArgs> OnProgressChanged;


    public class OnStateChangedEventArgs : EventArgs
    { public State state; }


    public enum State
    {Idle, Frying, Fried, Burned, }

    private State state;

    private void Start()
    {
        state = State.Idle;

        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = State.Idle });

    }


    private void Update()
    {

        if (HasKitchenObject())
        {
            switch (state)
            {
                case State.Idle:
                    OnProgressChanged?.Invoke(this, new IHasProgressBar.OnProgressChangedEventArgs
                    {
                        progressNormalized = 0f
                    }) ;
                    break;

                case State.Frying:
                    fryingTimer += Time.deltaTime;

                    if (fryingTimer >= fryingRecipeSO.FryingTime)
                    {
                        //Fried
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);

                        

                        burningRecipeSO = GetBurningRecipeSOFromInput(GetKitchenObject().GetKitchenObjectSO());

                        state = State.Fried;
                        burningTimer = 0f;

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = State.Fried });

                    }
                    OnProgressChanged?.Invoke(this, new IHasProgressBar.OnProgressChangedEventArgs
                    {
                        progressNormalized = fryingTimer / fryingRecipeSO.FryingTime
                    }) ;


                    break;

                case State.Fried:
                    burningTimer += Time.deltaTime;

                    if(burningTimer >= burningRecipeSO.BurningTime)
                    {
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);

                        state = State.Burned;
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = State.Burned });


                    }

                    OnProgressChanged?.Invoke(this, new IHasProgressBar.OnProgressChangedEventArgs
                    {
                        progressNormalized = burningTimer / burningRecipeSO.BurningTime
                    }) ;
                    break;

                case State.Burned:
                    OnProgressChanged?.Invoke(this, new IHasProgressBar.OnProgressChangedEventArgs
                    {
                        progressNormalized = 1f
                    });
                    break;
            }
        
        }
    }
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //empty counter
            if (player.HasKitchenObject())
            {
                //player has kitchenObject

                if (HasFryingRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    //object can be Cooked

                    player.GetKitchenObject().SetKitchenObjectParent(this);

                    fryingRecipeSO = GetFryingRecipeSOFromInput(GetKitchenObject().GetKitchenObjectSO());

                    state = State.Frying;
                    fryingTimer = 0f;

                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = State.Frying });

                }

            }
            else
            {
                //player has nothing
            }
        }
        else
        {
            //counter has kitchenObject
            if (player.HasKitchenObject())
            {
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                { //player is holding a plate

                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();

                        state = State.Idle;
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = State.Idle });

                        OnProgressChanged?.Invoke(this, new IHasProgressBar.OnProgressChangedEventArgs
                        {
                            progressNormalized = 0f
                        });

                    }
                }

            }
            else
            {
                //player has nothing
                this.GetKitchenObject().SetKitchenObjectParent(player);

                state = State.Idle;
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = State.Idle });

                OnProgressChanged?.Invoke(this, new IHasProgressBar.OnProgressChangedEventArgs
                {
                    progressNormalized = 0f
                });

            }
        }

    }



  

    private bool HasFryingRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOFromInput(inputKitchenObjectSO);

        return fryingRecipeSO != null;
    }

    private FryingRecipeSO GetFryingRecipeSOFromInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (FryingRecipeSO fryingRecipe in fryingRecipeArray)
        {
            if (fryingRecipe.input == inputKitchenObjectSO)
            {
                return fryingRecipe;
            }

        }
        return null;
    }


    private BurningRecipeSO GetBurningRecipeSOFromInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (BurningRecipeSO burningRecipeSO in burningRecipeArray)
        {
            if (burningRecipeSO.input == inputKitchenObjectSO)
            {
                return burningRecipeSO;
            }

        }
        return null;
    }

    public bool IsFried()
    { return state == State.Fried; }
}
