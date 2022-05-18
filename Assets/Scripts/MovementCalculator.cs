using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementCalculator : MonoBehaviour
{
    [SerializeField] List<string> trackedJointsName;
    private List<RectTransform> trackedJoints = new List<RectTransform>();
    public string humanBodyname;
    [SerializeField] List<GameObject> bones;
    private List<Vector2> baseVectors = new List<Vector2>();
    private List<Vector2> trackedJointsBones = new List<Vector2>();
    private List<float> angles = new List<float>();
    bool flag = false;
    private List<float> adjFactor = new List<float>();
    

    // Start is called before the first frame update
    void Start()
    {
        int count = 0;
        Debug.Log("------>Bones Detected<-------" + bones.Count);
        for(int i = 0; i< bones.Count; i++)
        {
            trackedJointsBones.Add(Vector2.zero);
            angles.Add(0f);
            adjFactor.Add(bones[i].transform.eulerAngles.z);
            if (count > 1)
            {
                count = 0;
                baseVectors.Add(Vector2.zero);
                continue;
            }
            baseVectors.Add(getDirVector(bones[i + 1].transform.position, bones[i].transform.position));
            count++;

            //trackedJoints[i] = null;
        }
        Debug.Log("------>Tracked Joints<-------" + trackedJointsBones.Count);

    }

    // Update is called once per frame
    void Update()
    {
        if (!flag && GameObject.Find(humanBodyname) != null)
        {
            for(int i = 0; i < trackedJointsName.Count; i++)
            {
                trackedJoints.Add(GameObject.Find(trackedJointsName[i]).GetComponent<RectTransform>());
                //Debug.Log(trackedJoints[i].gameObject.name);
            }
            flag = true;
        }
        else if(flag)
        {
            int count = 0;
            for (int i = 0; i < trackedJoints.Count; i++)
            {
                if (count > 1)
                {
                    count = 0;
                    trackedJointsBones[i] = Vector2.zero;
                    continue;
                }
                trackedJointsBones[i] = getDirVector(trackedJoints[i + 1].transform.position, trackedJoints[i].transform.position);
                count++;
            }
            //Debug.Log("---->Tracked Joints Bone<-----" + trackedJointsBones.Count);
            for (int i = 0; i < trackedJointsBones.Count; i++)
            {
                angles[i] = getRotation(baseVectors[i], trackedJointsBones[i]);
            }
            updateBones(bones, angles);
        }
        else
        { return; }
    }

    private void updateBones(List<GameObject> bones, List<float> angles)
    {
        int count = 0;
        for(int i = 0; i < bones.Count; i++)
        {
            if(count > 1)
            {
                count = 0;
                //bones[i].transform.eulerAngles = bones[i-1].transform.eulerAngles;
                continue;
            }
            //Debug.Log(bones[1].name + " is to be rotated by " + angles[1]);
            if (i > 2)
            {
                bones[i].transform.eulerAngles = new Vector3(bones[i].transform.eulerAngles.x, bones[i].transform.eulerAngles.y, adjFactor[i] + angles[i]);
            }
            else
            {
                bones[i].transform.eulerAngles = new Vector3(bones[i].transform.eulerAngles.x, bones[i].transform.eulerAngles.y, adjFactor[i] + angles[i]);
                //Debug.Log("IS AT " + bones[1].transform.eulerAngles);
            }
            count++;
        }
    }

    private Vector2 getDirVector(Vector2 vec1, Vector2 vec2)
    {
        return new Vector2(vec1.x - vec2.x, vec1.y - vec2.y);
    }

    private float getRotation(Vector2 to, Vector2 from)
    {
        /*float magTo = to.magnitude;
        float magFrom = from.magnitude;
        float dot = Vector2.Dot(to, from);
        float angle = Mathf.Rad2Deg * Mathf.Acos(dot / (magTo * magFrom));
        if (angle > 180)
            Debug.Log("ANgle is " + angle);*/
        return Vector3.SignedAngle(from, to, Vector3.forward);
    }
}
