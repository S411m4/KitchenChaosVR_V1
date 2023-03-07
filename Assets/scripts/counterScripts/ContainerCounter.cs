using System;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    [SerializeField] KitchenObjectSO kitchenObjectSO;

    public event EventHandler OnPlayerGrabbedObject;




    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            //Player is not carrying anything

            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);


            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }

    }
}