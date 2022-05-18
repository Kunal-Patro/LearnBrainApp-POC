using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneTracker : MonoBehaviour
{
    [SerializeField] List<GameObject> bonesToBeTracked;
    private List<GameObject> bonesMoving = new List<GameObject>();
    private List<Vector3> bonesBasicPosition = new List<Vector3>();
    private List<Vector3> bonesBasicRotation = new List<Vector3>();
    bool flag1 = false, flag2 = true, isCoroutineRunning = false;
    void Start()
    {
        foreach(GameObject obj in bonesToBeTracked)
        {
            bonesBasicPosition.Add(obj.transform.position);
            bonesBasicRotation.Add(obj.transform.rotation.eulerAngles);
        }
    }

    
    void FixedUpdate()
    {
        bonesMoving.Clear();
        for (int i = 0; i < bonesToBeTracked.Capacity; i++)
        {
            if(isPositionChanged(bonesToBeTracked[i].transform.position, bonesBasicPosition[i]) || isRotationChanged(bonesToBeTracked[i].transform.rotation.eulerAngles, bonesBasicRotation[i]))
            {
                bonesMoving.Add(bonesToBeTracked[i]);
                flag1 = true;
                if(!isCoroutineRunning)
                {
                    isCoroutineRunning = true;
                    StartCoroutine(UpdateBaseVectors(bonesToBeTracked, bonesBasicPosition, bonesBasicRotation));
                }
            }
        }
        if(flag1 && flag2)
        {
            foreach(GameObject obj in bonesMoving)
            {
                Debug.Log(obj.name);
            }
            flag2 = false;
        }
    }

    IEnumerator UpdateBaseVectors(List<GameObject> bonesToBeTracked, List<Vector3> bonesBasicPosition, List<Vector3> bonesBasicRotation)
    {
        yield return new WaitForSeconds(3);
        for(int i = 0; i<bonesToBeTracked.Count; i++)
        {
            bonesBasicPosition[i] = bonesToBeTracked[i].transform.position;
            bonesBasicRotation[i] = bonesToBeTracked[i].transform.rotation.eulerAngles;
        }
        isCoroutineRunning = false;
        yield return null;
    }

    public List<GameObject> getBonesMoving()
    {
        return this.bonesMoving;
    }
    public bool isPositionChanged(Vector3 pos1 , Vector3 pos2)
    {
        if ((pos1 - pos2).sqrMagnitude > 100)
            return true;
        else
            return false;
    }

    public bool isRotationChanged(Vector3 rot1, Vector3 rot2)
    {
        if ((rot1 - rot2).sqrMagnitude > 100)
            return true;
        else
            return false;
    }
}
