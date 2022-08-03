using System;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class Test : MonoBehaviour
{
    private Pool<poolObj> _poolObjPool = new Pool<poolObj>();
    [SerializeField] private GameObject _poolPrefab;
    private float time;
    private void Start()
    {
        _poolObjPool.Initialize(10, _poolPrefab);
    }
    private void Update()
    {
        time += Time.deltaTime;
        if (time> 2)
        {
            time = 0;
            var obj = _poolObjPool.GetNext();
            obj.gameObject.SetActive(!obj.gameObject.activeSelf);
            _poolObjPool.ExpandPool(1);
        }
    }
}