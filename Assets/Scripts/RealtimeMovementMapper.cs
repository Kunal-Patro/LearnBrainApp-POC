using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealtimeMovementMapper : MonoBehaviour
{
    [SerializeField] List<GameObject> ikObjects;
    [SerializeField] RealtimeMovementCalculator rmc;
    private List<Vector3> updatedPositions = new List<Vector3>(5);

    private void Start()
    {
        Debug.Log(updatedPositions.Capacity + " --- " + ikObjects.Capacity);
    }
    private void Update()
    {
        updatedPositions = rmc.getUpdatedPositions();
        if (updatedPositions.Count != 0)
        {
            for (int i = 0; i < 5; i++)
            {
                ikObjects[i].transform.position = Vector3.Lerp(ikObjects[i].transform.position, ikObjects[i].transform.position + updatedPositions[i].normalized, 0.5f);
            }
        }
    }
}
