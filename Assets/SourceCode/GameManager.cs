using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public Transform[] _spawPoints;
	public GameObject _squareBrick;
	public GameObject _triangleBrick;
	public int _numberOfBrickToStart;
	public int _level;
	public List<GameObject> _brickInScene;
	private ObjectPool _objectPool;

	// Start is called before the first frame update
	void Start()
	{
		_level = 1;
		int _numberOfBricksCreated = 0;
		_objectPool = FindObjectOfType<ObjectPool>();
		for (int i = 0; i < _spawPoints.Length; i++)
		{
			int _brickRandom = Random.Range(0, 3);
			if (_brickRandom == 0)
			{
				_brickInScene.Add(Instantiate(_squareBrick, _spawPoints[i].position, Quaternion.identity)); 
			} 
			else if (_brickRandom == 1) 
			{
				_brickInScene.Add(Instantiate(_triangleBrick,_spawPoints[i].position, Quaternion.identity));
			}

		}
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void PlaceBrick()
	{
		_level++;
		foreach (Transform position in _spawPoints)
		{
			int _brickRandom = Random.Range(0, 3);
			if (_brickRandom == 0)
			{
				GameObject brick = _objectPool.GetPooledObject("SquareBrick");
				_brickInScene.Add(brick);
				if(brick != null) 
				{
					brick.transform.position = position.position;
					brick.transform.rotation = Quaternion.identity;
					brick.SetActive(true);
				}
			} 
			else if (_brickRandom == 1) 
			{
				GameObject brick = _objectPool.GetPooledObject("TriangleBrick");
				_brickInScene.Add(brick);
				if(brick != null) 
				{
					brick.transform.position = position.position;
					brick.transform.rotation = Quaternion.identity;
					brick.SetActive(true);
				}
			}

		}
	}
}