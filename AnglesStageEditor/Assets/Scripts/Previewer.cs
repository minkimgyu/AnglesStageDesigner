using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Previewer : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;

    public void ResetSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }

    public void ResetScale(float size)
    {
        transform.localScale = Vector3.one * size;
    }
}
