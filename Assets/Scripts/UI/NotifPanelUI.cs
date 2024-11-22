using System.Collections;
using System.Collections.Generic;
using TvZ.Interfaces;
using UnityEngine;
using UnityEngine.Pool;

namespace TvZ.UI
{
    public class NotifPanelUI : MonoBehaviour, IPoolAble
    {

        ObjectPool<GameObject> notifPool;

        public void ReleaseObject()
        {
            notifPool.Release(this.gameObject);
        }

        public void SetPool(ObjectPool<GameObject> pool)
        {
            notifPool = pool;
        }


    }
}
