using UnityEngine;
using System.Collections;

public class _OptionsRelatedCamera : MonoBehaviour
{

    public bool follow_Brightness = false;
    public bool follow_SSAO = false;
    public bool follow_Bloom = false;

    Camera cam;
    SSAOEffect ssao;
    BloomAndLensFlares bloom;
    BrightnessScreenOverlay brightness;

    MapLogic mapLogic;

    void Start()
    {
        mapLogic = MapLogic.Instance;

        if (follow_SSAO)
        {
            if (ssao != null)
            {
                ssao.m_Blur = mapLogic.mapSSAO.m_Blur;
                ssao.m_Downsampling = mapLogic.mapSSAO.m_Downsampling;
                ssao.m_MinZ = mapLogic.mapSSAO.m_MinZ;
                ssao.m_OcclusionAttenuation = mapLogic.mapSSAO.m_OcclusionAttenuation;
                ssao.m_OcclusionIntensity = mapLogic.mapSSAO.m_OcclusionIntensity;
                ssao.m_Radius = mapLogic.mapSSAO.m_Radius;
                ssao.m_RandomTexture = mapLogic.mapSSAO.m_RandomTexture;
                ssao.m_SampleCount = mapLogic.mapSSAO.m_SampleCount;
                ssao.m_SSAOShader = mapLogic.mapSSAO.m_SSAOShader;
            }
        }

        if (follow_Bloom)
        {
            if (bloom != null)
            {
                bloom.tweakMode = mapLogic.mapBloom.tweakMode;
                bloom.screenBlendMode = mapLogic.mapBloom.screenBlendMode;
                bloom.hdr = mapLogic.mapBloom.hdr;
                bloom.lensflares = mapLogic.mapBloom.lensflares;
                bloom.bloomIntensity = mapLogic.mapBloom.bloomIntensity;
                bloom.bloomThreshhold = mapLogic.mapBloom.bloomThreshhold;
                bloom.bloomBlurIterations = mapLogic.mapBloom.bloomBlurIterations;
                bloom.sepBlurSpread = mapLogic.mapBloom.sepBlurSpread;
                bloom.useSrcAlphaAsMask = mapLogic.mapBloom.useSrcAlphaAsMask;
            }
        }
    }

    // Use this for initialization
    void Awake()
    {
        cam = GetComponent<Camera>();
        ssao = GetComponent<SSAOEffect>();
        bloom = GetComponent<BloomAndLensFlares>();
        brightness = GetComponent<BrightnessScreenOverlay>();
    }

    // Update is called once per frame
    void Update()
    {
        if (follow_Brightness)
        {
            if (brightness == null)
                Debug.LogError("Brightness component has NOT been found in an options relative camera!");
            else
                brightness.intensity = VideoSettingsController.curBrightness;
        }

        if (follow_SSAO)
        {
            if (ssao == null)
                Debug.LogError("SSAO component has NOT been found in an options relative camera!");
            else
            {
                ssao.enabled = VideoSettingsController.curUseSSAO;
            }
        }

        if (follow_Bloom)
        {
            if (bloom == null)
                Debug.LogError("Bloom component has NOT been found in an options relative camera!");
            else
            {
                bloom.enabled = VideoSettingsController.curUseBloom;
            }
        }
    }
}
