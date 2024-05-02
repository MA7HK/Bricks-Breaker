
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

[System.Serializable]
public class ObjectPoolItem
{
	public int _amountToPool;
	public GameObject _objectToPool;
}


public class ObjectPool : MonoBehaviour
{
	public List<GameObject> _pooledObjects;
	public List<ObjectPoolItem> _itemsToPool;

	// Start is called before the first frame update
	void Start()
	{
		_pooledObjects = new List<GameObject>();
		foreach (ObjectPoolItem item in _itemsToPool)
		{
			for (int i = 0; i < item._amountToPool; i++)
			{
				GameObject obj = Instantiate(item._objectToPool) as GameObject;
				obj.SetActive(false);
				_pooledObjects.Add(obj);
			}
		}
	}

	// Update is called once per frame
	void Update()
	{

	}

	public GameObject GetPooledObject(string tag)
	{
		for (int i = 0; i < _pooledObjects.Count; i++)
		{
			if(_pooledObjects[i].activeInHierarchy == false && _pooledObjects[i].tag == tag)
			{
				return _pooledObjects[i];
			}
		}
		return null;
	}
}