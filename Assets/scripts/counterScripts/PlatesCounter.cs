using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class PlatesCounter : BaseCounter
{
    [SerializeField] KitchenObjectSO plateSO;
    private float plateSpawnTimer = 0;
    private float plateSpawnTimerMax = 4f;
    private int noOfPlatesSpawned = 0;
    private int maxNoOfPlates = 4;


    public event EventHandler OnPlateAdded;
    public event EventHandler OnPlateRemoved;



    private void Update()
    {
        if (KitchenGameManager.Instance.IsGamePlaying() && noOfPlatesSpawned < maxNoOfPlates)
        {
            plateSpawnTimer += Time.deltaTime;


            if(plateSpawnTimer >= plateSpawnTimerMax)
            {
                plateSpawnTimer = 0f;

                noOfPlatesSpawned++;

                OnPlateAdded?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player)
    {
        if(!player.HasKitchenObject() && noOfPlatesSpawned > 0)
        {
            //pickup plate
            KitchenObject.SpawnKitchenObject(plateSO, player);
            noOfPlatesSpawned--;

            OnPlateRemoved?.Invoke(this, EventArgs.Empty);
        }
    }
}
