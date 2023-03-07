using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerVisual : MonoBehaviour
{
    [SerializeField] private GameObject customerBody;
    [SerializeField] private GameObject customerHead;

    private RecipeSO customerRecipe;

    public void SetCustomerRecipe(RecipeSO recipeSO)
    { customerRecipe = recipeSO; }

    public RecipeSO GetCustomerRecipe()
    { return customerRecipe;
    }

    public void SetCustomerMaterial(Material customerMaterial)
    {
        customerBody.GetComponent<Renderer>().material = customerMaterial;
        customerHead.GetComponent<Renderer>().material = customerMaterial;
    }
}
