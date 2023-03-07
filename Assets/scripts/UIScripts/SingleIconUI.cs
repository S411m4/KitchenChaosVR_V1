using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SingleIconUI : MonoBehaviour
{
    [SerializeField] Image img;
    
    public void SetKitchenObjectSO(KitchenObjectSO kitchenObjectSO)
    {
        img.sprite = kitchenObjectSO.sprite;
    }
}
