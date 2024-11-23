using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Pool : MonoBehaviour
{
    IPoolable _poolObject;
    protected IObjectPool<IPoolable> _pool;

    protected Transform activatedPreviewerParent;
    protected Transform deactivatedPreviewerParent;

    bool _nowInitialized = false;
    public bool NowInitialized { get { return _nowInitialized; } }

    public void Initialize(IPoolable poolObject, int startCount, Transform activatedPreviewerParent, Transform deactivatedPreviewerParent)
    {
        // �ʱ�ȭ ����
        if(_nowInitialized == false) _nowInitialized = true;

        this.activatedPreviewerParent = activatedPreviewerParent;
        this.deactivatedPreviewerParent = deactivatedPreviewerParent;

        _poolObject = poolObject;
        _pool = new ObjectPool<IPoolable>(
            CreateObject,
            OnGetObject,
            OnReleaseObject,
            OnDestroyObject);

        for (int i = 0; i < startCount; i++)
        {
            CreateObject();
        }
    }

    // ������Ʈ�� ������ �� ����
    private IPoolable CreateObject()
    {
        GameObject poolObject = Object.Instantiate(_poolObject.ReturnObject());
        IPoolable poolable = poolObject.GetComponent<IPoolable>();

        poolable.SetReturnToPoolEvent(() => _pool.Release(poolable)); // ���� Ǯ�� ������ �� �ְ� �ѱ�.
        poolable.SetParent(deactivatedPreviewerParent);
        poolable.SetActive(false);
        return poolable;
    }

    // Ǯ���� ������Ʈ�� ���� �� ����
    private void OnGetObject(IPoolable poolable)
    {
        poolable.SetActive(true);
        poolable.SetParent(activatedPreviewerParent);
    }

    // ������Ʈ�� Ǯ�� ȸ���� �� ����
    private void OnReleaseObject(IPoolable poolable)
    {
        poolable.SetActive(false);
        poolable.SetParent(deactivatedPreviewerParent);
    }

    // ������Ʈ�� Ǯ���� ������ �� ����
    private void OnDestroyObject(IPoolable poolable)
    {
        Object.DestroyImmediate(poolable.ReturnObject());
    }
}