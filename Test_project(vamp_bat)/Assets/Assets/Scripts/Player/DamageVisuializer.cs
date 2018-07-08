using UnityEngine;
using System.Collections;
 
[ExecuteInEditMode]
public class DamageVisuializer : MonoBehaviour
{
    public float intensity;
    private Material material;
    private float timer;

    // Creates a private material used to the effect
    void Awake()
    {
        material = new Material(Shader.Find("Hidden/ScreenShader"));
    }

    // Postprocess the image
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (intensity == 0)
        {
            Graphics.Blit(source, destination);
            return;
        }

        material.SetFloat("_bwBlend", intensity);
        Graphics.Blit(source, destination, material);
    }

    void Update()
    {
        timer += Time.fixedDeltaTime;

        if (timer >= .016)
        {
            if (intensity > 0)
            {
                intensity -= 0.01f;
            }
        }
    }
}
