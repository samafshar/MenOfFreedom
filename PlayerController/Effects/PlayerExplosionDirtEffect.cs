using UnityEngine;

// This class implements simple ghosting type Player Blood Image Effect.
// STFU and just use it.

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]

public class PlayerExplosionDirtEffect : PostEffectsBase {
    public OverlayBlendMode blendMode = OverlayBlendMode.AddSub;
	public float intensity = 1.0f;
	public Texture2D mainTexture;
    public Texture2D alphaTexture;
			
	public Shader playerBloodShader;
	
	private Material overlayMaterial = null;
	
	bool CheckResources()
    {
		CheckSupport (false);
		
		overlayMaterial = CheckShaderAndCreateMaterial (playerBloodShader, overlayMaterial);
		
		if(!isSupported)
			ReportAutoDisable ();

		return isSupported;
	}

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {		
		if(CheckResources()==false)
        {
			Graphics.Blit (source, destination);
			return;
		}

		overlayMaterial.SetFloat ("_AlphaRange", intensity);
        overlayMaterial.SetTexture("_Diffuse", mainTexture);
        overlayMaterial.SetTexture("_Alpha", alphaTexture);

        //Graphics.Blit(source, overlayMaterial);
        //Graphics.Blit (source, destination);

        Graphics.Blit(source, overlayMaterial, (int)blendMode);
        Graphics.Blit(source, destination);
	}

    public void SetAlpha(float _value)
    {
        intensity = _value;
    }
}