using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Previewer : MonoBehaviour, IPoolable
{
    [SerializeField] SpriteRenderer spriteRenderer;
    Action ReturnToPool;

    public void ResetData(Sprite sprite, float size)
    {
        spriteRenderer.sprite = sprite;
        transform.localScale = Vector3.one * size;
    }

    public void SetReturnToPoolEvent(Action ReturnToPool)
    {
        this.ReturnToPool = ReturnToPool;
    }

    public void Deactivate()
    {
        ReturnToPool?.Invoke();
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    public GameObject ReturnObject()
    {
        return gameObject;
    }

    public void SetParent(Transform parent)
    {
        transform.SetParent(parent);
    }
}
