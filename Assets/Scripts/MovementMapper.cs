using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementMapper : MonoBehaviour
{
    [SerializeField] List<GameObject> bones;
    [SerializeField] List<GameObject> brainParts;
    [SerializeField] Animator brainAnim;
    [SerializeField] Material highlightMat;
    [SerializeField] Material originalMat;
    [SerializeField] BoneTracker bt;
    bool isCoroutineRunning = false, isReset;
    List<GameObject> movingBones = new List<GameObject>();
    private Dictionary<GameObject, GameObject> mapping = new Dictionary<GameObject, GameObject>();
    
    void Start()
    {
        for(int i = 0; i < bones.Capacity; i++)
        {
            mapping.Add(bones[i], brainParts[i]);
        }
    }

   
    void Update()
    {
        movingBones = bt.getBonesMoving();
        foreach (GameObject bone in movingBones)
        {
            if (mapping.ContainsKey(bone))
            {
                HighlightBrainPart(mapping[bone]);
            }
        }
        if (!isCoroutineRunning)
        {
            StartCoroutine(ResetThings());
            isCoroutineRunning = true;
        }
        if (AnimationController.isMovementChanged)
        {
            brainAnim.SetTrigger("Ideal");
            AnimationController.isMovementChanged = false;
            foreach (GameObject bone in movingBones)
            {
                if (mapping.ContainsKey(bone))
                {
                    Debug.Log(mapping[bone].name);
                }
            }
        }
    }

    public void HighlightBrainPart(GameObject obj)
    {
        obj.GetComponent<MeshRenderer>().material = highlightMat;
        brainAnim.SetTrigger(obj.name);
    }
    
    IEnumerator ResetThings()
    {
        yield return new WaitForEndOfFrame();
        foreach (GameObject brainPart in brainParts)
        {
            brainPart.GetComponent<MeshRenderer>().material = originalMat;
        }
        yield return null;
        isCoroutineRunning = false;
    }


}
