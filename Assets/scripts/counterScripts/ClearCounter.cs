using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    
    public override void Interact(Player player)
    {
        if(!HasKitchenObject())
        {
            //empty counter
            if(player.HasKitchenObject())
            {

                //player has kitchenObject
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                //player has nothing
            }
        }
        else
        {
            //counter has kitchenObject
            if(player.HasKitchenObject())
            {
                //player has kitchenObject

                    
                    if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                    { //player is holding a plate

                        if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                        {
                            GetKitchenObject().DestroySelf();
                        }
                    }
                    else
                    {//player is not holding a plate but smth else
                        if(GetKitchenObject().TryGetPlate(out plateKitchenObject))
                        {
                        //counter is holding a plate
                        if(plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            player.GetKitchenObject().DestroySelf();
                        }
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

}

