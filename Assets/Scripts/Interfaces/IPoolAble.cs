using UnityEngine;
using UnityEngine.Pool;

namespace TvZ.Interfaces
{
    public interface IPoolAble
    {
        
       void SetPool(ObjectPool<GameObject> pool);
       void ReleaseObject();
    }
}
