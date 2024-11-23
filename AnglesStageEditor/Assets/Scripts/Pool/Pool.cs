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
        // 초기화 적용
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

    // 오브젝트가 생성될 때 실행
    private IPoolable CreateObject()
    {
        GameObject poolObject = Object.Instantiate(_poolObject.ReturnObject());
        IPoolable poolable = poolObject.GetComponent<IPoolable>();

        poolable.SetReturnToPoolEvent(() => _pool.Release(poolable)); // 관리 풀을 참조할 수 있게 넘김.
        poolable.SetParent(deactivatedPreviewerParent);
        poolable.SetActive(false);
        return poolable;
    }

    // 풀에서 오브젝트를 꺼낼 때 실행
    private void OnGetObject(IPoolable poolable)
    {
        poolable.SetActive(true);
        poolable.SetParent(activatedPreviewerParent);
    }

    // 오브젝트를 풀에 회수할 때 실행
    private void OnReleaseObject(IPoolable poolable)
    {
        poolable.SetActive(false);
        poolable.SetParent(deactivatedPreviewerParent);
    }

    // 오브젝트를 풀에서 제거할 때 실행
    private void OnDestroyObject(IPoolable poolable)
    {
        Object.DestroyImmediate(poolable.ReturnObject());
    }
}