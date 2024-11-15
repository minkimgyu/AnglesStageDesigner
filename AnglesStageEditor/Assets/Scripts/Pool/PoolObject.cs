//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PoolObject : MonoBehaviour, IPoolable
//{
//    Action ReturnToPool;

//    protected virtual void OnDisable()
//    {
//        transform.rotation = Quaternion.identity;
//        transform.position = Vector3.zero;
//        _timer.Reset();
//        ReturnToPool?.Invoke();
//    }

//    public GameObject ReturnObject()
//    {
//        return gameObject;
//    }

//    public void SetReturnToPoolEvent(Action ReturnToPool)
//    {
//        this.ReturnToPool = ReturnToPool;
//    }

//    public void SetActive(bool active)
//    {
//        gameObject.SetActive(active);
//    }
//}
