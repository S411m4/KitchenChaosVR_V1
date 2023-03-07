using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInputLegacy : MonoBehaviour
{



    public Vector2 GetGameInputVector2Normalizied(KeyCode upKey, KeyCode downKey,KeyCode rightKey, KeyCode leftKey)
    {
        Vector2 inputVector = new Vector2(0, 0);


        if (Input.GetKey(upKey))
        {
            inputVector.y = +1;
        }
        if (Input.GetKey(downKey))
        {
            inputVector.y = -1;
        }
        if (Input.GetKey(rightKey))
        {
            inputVector.x = +1;
        }
        if (Input.GetKey(leftKey))
        {
            inputVector.x = -1;
        }

        inputVector = inputVector.normalized;

        return inputVector;
    }
        
    

}
