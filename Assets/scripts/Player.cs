using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class Player : MonoBehaviour, IkitchenObjectParent
{
    //[SerializeField] GameInputLegacy gameInput; //legacyInputManager
    [SerializeField] GameInput gameInput;

    private BaseCounter selectedCounter;

    private KitchenObject kitchenObject;

    [SerializeField] Transform kitchenObjectHoldPoint;

    public event EventHandler OnPickAnything;

    public static Player Instance { get; private set; }

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;

    [SerializeField] XRRayInteractor rightHandRay;
    

    public class OnSelectedCounterChangedEventArgs : EventArgs {
        public BaseCounter selectedCounter;
    }


    private void Awake()
    {
        if(Instance != null)
        {
           

        }
        else
        {
            Instance = this;
        }
    }
    private void Start()
    {
        gameInput.OnInteractionAction += GameInput_OnInteractionAction;
        gameInput.OnAlternateAction += GameInput_OnAltenrateAction;

    }

    private void GameInput_OnAltenrateAction(object sender, EventArgs e)
    {
        if (!KitchenGameManager.Instance.IsGamePlaying()) return;
        
        if (selectedCounter != null)
        {
            selectedCounter.IneractAlterante(this);
        }
        
    }

    private void GameInput_OnInteractionAction(object sender, System.EventArgs e)
    {

        if (!KitchenGameManager.Instance.IsGamePlaying()) return;
        
        if (selectedCounter != null)
        { selectedCounter.Interact(this); }
        
    }

    private void Update()
    {
        HandleInteractions();

    }

    private GameObject collidedObjectWithHand;
    private void HandleInteractions()
    {

        RaycastHit raycastHit;


        if (rightHandRay.TryGetCurrent3DRaycastHit(out raycastHit))
        {
            if (raycastHit.transform.gameObject.TryGetComponent(out BaseCounter baseCounter))
            {

                if (baseCounter != selectedCounter)
                {
                    SetSelectedCounter(baseCounter);
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }

        else
        {  //not hitting anything
            SetSelectedCounter(null);
        }

    }



    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = selectedCounter
        });

    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        OnPickAnything?.Invoke(this, EventArgs.Empty);
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


