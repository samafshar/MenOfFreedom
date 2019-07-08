using UnityEngine;
using System.Collections;

public class StartPoint : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PlaceCharacterOnIt(GameObject _character)
    {
        GameObject obj = _character;

        if (obj.GetComponent<PlayerCharacterNew>() != null)
        {
            obj.transform.position = transform.position + new Vector3(0, (PlayerCharacterNew.Instance.gameObject.GetComponent<CharacterController>().height / 2) + 0.2f, 0);
        }
        else
        {
            obj.transform.position = transform.position + new Vector3(0, 0.2f, 0);
        }

        Quaternion rot = new Quaternion();
        rot.eulerAngles = new Vector3(0, transform.rotation.eulerAngles.y, 0);

        obj.transform.rotation = rot;
    }
}
