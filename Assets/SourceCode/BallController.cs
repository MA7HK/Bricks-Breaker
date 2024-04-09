using System.Diagnostics;
using UnityEngine;

public class BallController : MonoBehaviour
{
	public enum BallState { aim, fire, wait, endShot };
	public BallState currentBallState;

    // SerializeField allows private fields to be visible in the Unity inspector
    [SerializeField] private Rigidbody2D ballRigidbody; 										// 	The Rigidbody2D component of the ball;
    [SerializeField] private GameObject arrow; 													// 	The arrow object used for indicating direction;
    [SerializeField] private float constantSpeed;												// 	The constant speed at which the ball moves;
    private Vector2 mouseStartPosition; 														// 	The starting position of the mouse when clicked;
    private Vector2 mouseEndPosition; 															// 	The ending position of the mouse when released;

    private void Start()
    {
        ballRigidbody = GetComponent<Rigidbody2D>();						 					// 	Get the Rigidbody2D component of the ball;
		currentBallState = BallState.aim; 														//	Set the initial state of the ball to aim;
    }

    private void Update()
    {
		switch (currentBallState)
		{
			case BallState.aim: if (Input.GetMouseButtonDown(0)) HandleMouseDown();         	// 	Check the mouse button down pressed or not;
								if (Input.GetMouseButton(0) ) HandleMouseDrag();				//	Check the left click and handle mouse dragging if it occurred;
							 	if (Input.GetMouseButtonUp(0) ) HandleMouseUp();				//	Check if the left mouse button has been released and then call the function;
							break;	 
			case BallState.fire : break;
			case BallState.wait : break;
			case BallState.endShot : break;	
			default:
				break;
		}


    }

    private void HandleMouseDown()
    {
        mouseStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);				// Set the starting position of the mouse and set the didClick flag to true;
       
    }

    private void HandleMouseDrag()
    {
        
        arrow.SetActive(true);																	//	Activate the arrow object;

        Vector2 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);		//	Store the world space coordinates for the current mouse position;
        SetArrowDirection(mouseStartPosition, currentMousePosition);							//	call the function that sets the mouseStartposition and currentMousePosition;
    }

    private void HandleMouseUp()
    {
        
        arrow.SetActive(false);																	// 	Deactivate the arrow object,
        mouseEndPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);					//	Set the end mouse position and convert it from screen coordinates;

        Vector2 velocity = CalculateVelocity(mouseStartPosition, mouseEndPosition);				//	Calculate the velocity;
        ballRigidbody.velocity = velocity * constantSpeed;										//	Set the ball's velocity;
		if(ballRigidbody.velocity == Vector2.zero) return;
		currentBallState = BallState.fire;
    }

    private void SetArrowDirection(Vector2 start, Vector2 end)
    {
        Vector2 direction;
		direction.x = start.x - end.x;
		direction.y = start.y - end.y;
		if(direction.y <= 0) direction.y = 0.01f;
        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;					// 	Calculate the angle between the mouse position and the ball position;
        arrow.transform.rotation = Quaternion.Euler(0f, 0f, -angle);							//	Set the rotation of the arrow based on the angle between the the mouse position and the ball position;
    }

    private Vector2 CalculateVelocity(Vector2 start, Vector2 end)
    {
        Vector2 direction = start - end;														//	Set the direction vector as the difference between the end mouse position to start mosue position; 
        return direction.normalized;															//  Return a normalised vector representing the direction;
    }
}