using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BaseCounter : MonoBehaviour,  IkitchenObjectParent
{
    [SerializeField] Transform centerTopPoint;
    private KitchenObject kitchenObject;

    public static event EventHandler OnDropAnything;
    public static void ResetStaticData()
    { OnDropAnything = null; }

    public virtual void Interact(Player player)
    {

    }

    public virtual void IneractAlterante(Player player)
    {

    }
    public Transform GetKitchenObjectFollowTransform()
    {
        return centerTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;

        if(kitchenObject != null)
            OnDropAnything?.Invoke(this, EventArgs.Empty);
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;

    }
}
