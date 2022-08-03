using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Pool<T> where T  : MonoBehaviour
{
    [SerializeField] private List<T> _pool = new List<T>();
    private int _nextIndex;
    /// <summary>
    /// This method initializes the pool that has a length of 'count'.
    /// It instantiates the pool at the (0, 0, 0) in the scene.
    /// The 'prefab' gameObject has to have the pool component.
    /// </summary>
    /// <param name="count"> Pool length. </param>
    /// <param name="prefab"> The prefab. </param>

    public void Initialize(int count, GameObject prefab)
    {
        GameObject parent = new GameObject("Pool Parent");
        
        for (int i = 0; i < count; i++)
        {
            var poolObj = GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity, parent.transform);
            _pool.Add(poolObj.GetComponent<T>());
        }

    }

    /// <summary>
    /// Returns the determined indexed pool object. If the index is bigger than the pool length, it returns the first element of the pool.
    /// </summary>
    /// <param name="index"></param>
    /// <returns>The pool object which is on the determined index. </returns>
    public T GetIndexOf(int index)
    {
        if (index >= _pool.Count)
        {
            Debug.LogWarning($"There is no index: {index} in this pool. Returned the first element.");
            return _pool[0];
        }
        else
        {
            return _pool[index];
        }
    }
    
    /// <summary>
    /// It increases the pool length by 'plusNum'.
    /// </summary>
    /// <param name="plusNum">How many new pool objects will be added to the pool?</param>
    public void ExpandPool(int plusNum)
    {
        for (int i = 0; i < plusNum; i++)
        {
            var poolObj = GameObject.Instantiate(_pool[0], Vector3.zero, Quaternion.identity, _pool[0].transform.parent);
            _pool.Add(poolObj);
            poolObj.gameObject.SetActive(true);
        }
    }
    
    /// <summary>
    /// Next object is determined by the private parameter that is set to 0 at the beginning of the game.
    /// </summary>
    /// <returns> Next Pool Object</returns>
    public T GetNext()
    {
        var r= _pool[_nextIndex % _pool.Count];
        _nextIndex++;
        return r;
    }
    
}
