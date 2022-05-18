using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BoneMovementMap")]
public class BoneMovementMap : ScriptableObject
{
    [System.Serializable]
    public class BoneMovementPair : ScriptableObject
    {
        public GameObject bone;
        public GameObject brainPart;
    }

    public BoneMovementPair[] map;
}
