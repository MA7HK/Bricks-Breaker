using UnityEngine;

public class BrickMovementController : MonoBehaviour
{
	public enum EBrickState { stop, move }
	public EBrickState _currentState;
	private bool _hasMove;

	// Start is called before the first frame update
	void Start()
	{
		_hasMove = false;
		_currentState = EBrickState.stop;
	}

	// Update is called once per frame
	void Update()
	{
		if(_currentState == EBrickState.stop) _hasMove = false;
		if(_currentState == EBrickState.move) 
		{
			if(_hasMove == false)
			{
				transform.position = new Vector2(transform.position.x, transform.position.y - 1);
				_currentState = EBrickState.stop;
				_hasMove = true;
			}
		}
	}
}