using UnityEngine;
using System;

public class CuttingCounter : BaseCounter, IHasProgressBar
{

    public event EventHandler<IHasProgressBar.OnProgressChangedEventArgs> OnProgressChanged;

    public static EventHandler OnAnyCut;
    new public static void ResetStaticData()
    { OnAnyCut = null; }
    public event EventHandler OnCutAction;


    private int cuttingProgress;
    

    [SerializeField] private CuttingRecipeSO[] cuttingRecipeArray;

    
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //empty counter
            if (player.HasKitchenObject())
            {
                //player has kitchenObject

                if (HasCuttingRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    //object can be cutted

                    cuttingProgress = 0;
                    OnProgressChanged?.Invoke(this, new IHasProgressBar.OnProgressChangedEventArgs
                    {
                        progressNormalized = 0f
                    }) ;


                    player.GetKitchenObject().SetKitchenObjectParent(this);

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
                    }
                }

            }
            else
            {
                //player has nothing
                this.GetKitchenObject().SetKitchenObjectParent(player);
            }
        }

    }

    public override void IneractAlterante(Player player)
    {
       if(HasKitchenObject() && HasCuttingRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            CuttingRecipeSO cuttingRecipe = GetCuttingRecipeSOFromInput(GetKitchenObject().GetKitchenObjectSO());

            cuttingProgress++;
            OnAnyCut?.Invoke(this, EventArgs.Empty);

            OnProgressChanged?.Invoke(this, new IHasProgressBar.OnProgressChangedEventArgs
            {
                progressNormalized = (float)cuttingProgress / cuttingRecipe.NoOfCutsNeeded
            }) ;

            OnCutAction?.Invoke(this, EventArgs.Empty);



            if(cuttingProgress >= cuttingRecipe.NoOfCutsNeeded)
            {
                KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());

                GetKitchenObject().DestroySelf();

                KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
            }

        }
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipe = GetCuttingRecipeSOFromInput(inputKitchenObjectSO);

        if(cuttingRecipe != null)
        {
            return cuttingRecipe.output;
        }

        return null;
    }

    private bool HasCuttingRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipe = GetCuttingRecipeSOFromInput(inputKitchenObjectSO);

        return cuttingRecipe != null;
    }

    private CuttingRecipeSO GetCuttingRecipeSOFromInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach(CuttingRecipeSO cuttingRecipe in cuttingRecipeArray)
        {
            if(cuttingRecipe.input == inputKitchenObjectSO)
            {
                return cuttingRecipe;
            }

        }
        return null;
    }

}

