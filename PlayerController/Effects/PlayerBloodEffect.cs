using UnityEngine;

// This class implements simple ghosting type Player Blood Image Effect.
// STFU and just use it.

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]

public class PlayerBloodEffect : PostEffectsBase {

    public OverlayBlendMode blendMode = OverlayBlendMode.AddSub;
	public float intensity = 1.0f;
	public Texture2D mainTexture;
    public Texture2D bloodAlphaTexture;
			
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
        overlayMaterial.SetTexture("_Alpha", bloodAlphaTexture);

        Graphics.Blit(source, overlayMaterial, (int)blendMode);
        Graphics.Blit(source, destination);

        //Graphics.Blit(source, destination, overlayMaterial, (int)blendMode);
	}

    public void SetBloodAlpha(float _value)
    {
        intensity = _value;
    }
}