using UnityEngine;

public class DimensionSwitcher : MonoBehaviour
{
    public Sprite WhiteDimensionSprite;
    public Sprite BlackDimensionSprite;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        DimensionManager.Instance.OnDimensionChange += UpdateSprite;
        UpdateSprite();
    }

    void OnDestroy()
    {
        DimensionManager.Instance.OnDimensionChange -= UpdateSprite;
    }

    void UpdateSprite()
    {
        if (DimensionManager.Instance.IsInWhiteDimension)
            spriteRenderer.sprite = WhiteDimensionSprite;
        else
            spriteRenderer.sprite = BlackDimensionSprite;
    }
}
