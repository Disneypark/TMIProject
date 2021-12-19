using UnityEngine;

public class Create_Metirial : MonoBehaviour
{
    // Creates a material from shader and texture references.
    Shader shader;
    Texture texture;
    Color color;
    Material mat;
    Renderer rend;
    void Awake()
    {
        rend = GetComponent<Renderer>();
        rend.material.SetInt("_Mode", 2);
        rend.material.DisableKeyword("_EMISSION");
    }

    private void Update()
    {
        if (Time.time > 3.0f)
        {
            rend.material.EnableKeyword("_EMISSION");
            rend.material.SetColor("_EmissionColor", rend.material.GetColor("_EmissionColor"));
        }
    }
}