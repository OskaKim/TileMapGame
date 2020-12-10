using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class OverrideSprite : MonoBehaviour
{
    [SerializeField] private Texture overrideTexture = null;

    private SpriteRenderer spriteRenderer;
    private static int idMainTex = Shader.PropertyToID("_MainTex");
    private MaterialPropertyBlock materialPropertyBlock;

    void Awake()
    {
        materialPropertyBlock = new MaterialPropertyBlock();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.GetPropertyBlock(materialPropertyBlock);
        ChangeTexture();
    }

    void LateUpdate()
    {
        spriteRenderer.SetPropertyBlock(materialPropertyBlock);
    }

    void OnValidate()
    {
        if (materialPropertyBlock != null)
        {
            ChangeTexture();
        }
    }

    void ChangeTexture()
    {
        materialPropertyBlock.SetTexture(idMainTex, overrideTexture);
    }
}