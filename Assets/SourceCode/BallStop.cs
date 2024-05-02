using UnityEngine;

public class BallStop : MonoBehaviour
{
	public Rigidbody2D _ball;
	public BallController _ballController;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	void OnCollisionEnter2D(Collision2D _other)
	{
		if(_other.gameObject.tag == "Ball")
		{
			//	stop ball ball
			_ball.velocity = Vector2.zero;
			//	rotate the ball
			//	reset the level
			//	set the ball as active
			_ballController._currentBallState = BallController.EBallState.wait;
		}
	}
}