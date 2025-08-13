using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneOnlineDemo : MonoBehaviour
{
    public Shader OnlineShader;
    Material material;
    [ColorUsage(true, true)]
    public Color ColorLine;
    public Vector2 vector;
    public float LineWide;
    // Start is called before the first frame update
    void Start()
    {
        material = new Material(OnlineShader);

        GetComponent<Camera>().depthTextureMode |= DepthTextureMode.DepthNormals;
    }

    void Update()
    {

    }
    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (material != null)
        {
            material.SetVector("_ColorLine", ColorLine);
            material.SetVector("_Sensitivity", vector);
            material.SetFloat("_SampleDistance", LineWide);
            Graphics.Blit(src, dest, material);
        }
        else
        {
            Graphics.Blit(src, dest);
        }
    }
}

