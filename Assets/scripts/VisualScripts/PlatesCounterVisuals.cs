using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisuals : MonoBehaviour
{
    [SerializeField] Transform platePrefab;
    [SerializeField] Transform CounterTopPoint;
    [SerializeField] PlatesCounter platesCounter;

    private List<GameObject> spawnedPlatesObjectList;

    
    private void Awake()
    {
        spawnedPlatesObjectList = new List<GameObject>();
    }

    private void Start()
    {
        platesCounter.OnPlateAdded += PlatesCounter_OnPlateAdded;
        platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
    }

    private void PlatesCounter_OnPlateRemoved(object sender, System.EventArgs e)
    {
        GameObject plate = spawnedPlatesObjectList[spawnedPlatesObjectList.Count - 1];
        spawnedPlatesObjectList.Remove(plate);
        Destroy(plate);
    }

    private void PlatesCounter_OnPlateAdded(object sender, System.EventArgs e)
    {
        const float  PLATE_Y_OFFSET  = 0.1f;

        Transform plate = Instantiate(platePrefab,CounterTopPoint);
        plate.localPosition = new Vector3(0, PLATE_Y_OFFSET * spawnedPlatesObjectList.Count, 0);

        spawnedPlatesObjectList.Add(plate.gameObject);
       
    }
}
