using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CustomerDeliveryManager : MonoBehaviour
{
    [SerializeField] private Transform[] customersTransform;
    [SerializeField] private Material[] customerMaterials;
    [SerializeField] private GameObject customerPrefab;

    private List<GameObject> customersWaiting = new List<GameObject>();


    private int NoOfCustomers = 0;

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSpawned += DeliveryManager_OnRecipeSpawned;
        DeliveryManager.Instance.OnRecipeCompleted += DeliveryManager_OnRecipeCompleted;
    }

    private void DeliveryManager_OnRecipeCompleted(object sender, DeliveryManager.RecipeSpawnedCompleted e)
    {
        foreach (GameObject customer in customersWaiting)
        { 
            if (e.recipeSO == customer.GetComponent<CustomerVisual>().GetCustomerRecipe())
            {
                customersWaiting.Remove(customer);
                Destroy(customer);
                NoOfCustomers--;
                break;
            }
        }

        //move customers in line
        for (int i = 0; i < customersWaiting.Count; i++)
        {
            customersWaiting[i].transform.position = customersTransform[i].position;
        }
    
        
    }

    private void DeliveryManager_OnRecipeSpawned(object sender, DeliveryManager.RecipeSpawnedCompleted e)
    {
        GameObject InstantiatedCustomer = Instantiate(customerPrefab, customersTransform[NoOfCustomers].position,customersTransform[NoOfCustomers].rotation);
        Material customerMaterial = customerMaterials[UnityEngine.Random.Range(0,customerMaterials.Count())];

        InstantiatedCustomer.GetComponent<CustomerVisual>().SetCustomerMaterial(customerMaterial);
        InstantiatedCustomer.GetComponent<CustomerVisual>().SetCustomerRecipe(e.recipeSO);

        customersWaiting.Add(InstantiatedCustomer);
        NoOfCustomers++;

    }
}
