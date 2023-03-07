using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class TrashCan : BaseCounter
{
    public static event EventHandler OnTrashAnything;
    new public static void ResetStaticData()
    { OnTrashAnything = null; }
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            OnTrashAnything?.Invoke(this, EventArgs.Empty);
            player.GetKitchenObject().DestroySelf();
        }

    }
}
