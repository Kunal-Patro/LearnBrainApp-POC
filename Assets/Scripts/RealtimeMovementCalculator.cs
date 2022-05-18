using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealtimeMovementCalculator : MonoBehaviour
{
    [SerializeField] List<string> stickBonesNames;
    private List<RectTransform> stickBones = new List<RectTransform>(5);
    public string humanbodyName;
    private bool flag = false;
    private List<Vector3> oldPos = new List<Vector3>(5);
    private List<Vector3> newPos = new List<Vector3>(5);
    void Start()
    {
        
    }

    void Update()
    {
        if(!flag && GameObject.Find(humanbodyName)!=null)
        {
            for(int i = 0; i < 5; i++)
            {
                stickBones.Add(GameObject.Find(stickBonesNames[i]).GetComponent<RectTransform>());
                oldPos.Add(stickBones[i].transform.position);
                newPos.Add(Vector3.zero);
            }
            flag = true;
        }
        else if(flag)
        {
            for(int i = 0; i < 5; i++)
            {
                Vector3 positionUpdate = (stickBones[i].transform.position - oldPos[i]);
                newPos[i] = positionUpdate;
                oldPos[i] = stickBones[i].transform.position;
            }
        }
        else
        {
            return;
        }
    }
    public List<Vector3> getUpdatedPositions()
    {
        return this.newPos;
    }
}
