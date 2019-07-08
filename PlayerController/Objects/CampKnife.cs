using UnityEngine;
using System.Collections;

public class CampKnife : MonoBehaviour {

    [HideInInspector]
    public Vector3 posBoneInitialLocalPosition;

    [HideInInspector]
    public Quaternion rotBoneInitialLocalRotation;

	// Use this for initialization
	void Start ()
    {
        posBoneInitialLocalPosition = gameObject.GetComponent<GunBonesForPosAndRot>().BonePosition.localPosition;
        rotBoneInitialLocalRotation = gameObject.GetComponent<GunBonesForPosAndRot>().BoneRotation.localRotation;

        PlayerCharacterNew.Instance.campKnife_PosBoneInitialLocalPosition = posBoneInitialLocalPosition;
        PlayerCharacterNew.Instance.campKnife_RotBoneInitialLocalRotation = rotBoneInitialLocalRotation;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
