using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharRaycastResult{
    public float rating = 0;
    public GameObject character = null;
    public bool isCharacterHitted = false;
    public bool isHaloHitted = false;
    public List<Transform> characterHittedPoses = new List<Transform>();
    public List<Transform> haloHittedPoses = new List<Transform>();
}
