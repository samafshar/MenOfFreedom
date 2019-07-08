using UnityEngine;

public enum OverlayBlendMode
{
    AddSub = 0,
    ScreenBlend = 1,
    Multiply = 2,
    Overlay = 3,
    AlphaBlend = 4,
}

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]

public class BrightnessScreenOverlay : PostEffectsBase
{
    public OverlayBlendMode blendMode = OverlayBlendMode.AddSub;
    public float intensity = 1.0f;
    public Texture2D texture;

    public Shader overlayShader;

    private Material overlayMaterial = null;

    bool CheckResources()
    {
        CheckSupport(false);

        overlayMaterial = CheckShaderAndCreateMaterial(overlayShader, overlayMaterial);

        if (!isSupported)
            ReportAutoDisable();

        return isSupported;
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (CheckResources() == false)
        {
            Graphics.Blit(source, destination);
            return;
        }

        overlayMaterial.SetFloat("_Intensity", intensity);
        overlayMaterial.SetTexture("_Overlay", texture);
        Graphics.Blit(source, destination, overlayMaterial, (int)blendMode);
    }
}