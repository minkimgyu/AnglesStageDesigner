using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewSpawner : Pool
{
    public Previewer Create(Sprite sprite, float size)
    {
        IPoolable poolable = _pool.Get();
        Previewer previewer = poolable.ReturnObject().GetComponent<Previewer>();
        previewer.ResetData(sprite, size);
        return previewer;
    }

    public void Clear()
    {
        for (int i = 0; i < activatedPreviewerParent.transform.childCount; i++)
        {
            Previewer previewer = activatedPreviewerParent.transform.GetChild(i).GetComponent<Previewer>();
            previewer.Deactivate();
            i--;
        }
    }
}
