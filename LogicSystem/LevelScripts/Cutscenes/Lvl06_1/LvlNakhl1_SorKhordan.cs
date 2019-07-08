using UnityEngine;
using System.Collections;

public class LvlNakhl1_SorKhordan : MonoBehaviour {

    public ParticleSystem particleA;
    public ParticleSystem particleB;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void ShowParticleA()
    {
        particleA.active = true;
    }

    void StopParticleA()
    {
        particleA.Stop();
    }

    void ShowParticleB()
    {
        particleB.active = true;
    }

    void StopParticleB()
    {
        particleB.Stop();
    }
}
