using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HitEffect : MonoBehaviour
{
    //Should Check for whats better
    //public List<AudioSource> SoundHit = new List<AudioSource>();
    public AudioClip[] Hit_Sound;
    public GameObject[] Hit_Particle;

    public Texture2D[] Hit_Texture;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
