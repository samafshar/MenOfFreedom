using UnityEngine;
using System.Collections;

public class BurningManInfo : MonoBehaviour
{
    [HideInInspector]
    public GameObject animObject;

    public AudioInfo burningManAudio;

    public AudioInfo fireOfBurningAudio;

    public void SetAnimObj()
    {
        animObject = this.gameObject;
    }
}
