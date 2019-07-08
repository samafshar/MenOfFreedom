using UnityEngine;
using System.Collections;

public class _OptionsRelatedLight : MonoBehaviour
{

    Light datLight;

    float shadowBias = 0.05f;
    float shadowSoftness = 2.3f;
    float shadowSoftnessFade = 0.2f;

    // Use this for initialization
    void Awake()
    {
        datLight = GetComponent<Light>();

        if (datLight == null)
            Debug.LogError("Light component has NOT been found in an options relative light!");
        else
        {
            datLight.shadowBias = shadowBias;
            datLight.shadowSoftness = shadowSoftness;
            datLight.shadowSoftnessFade = shadowSoftnessFade;
        }
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
