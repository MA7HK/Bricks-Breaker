using System.Diagnostics;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public enum EBallState { aim, fire, wait, endShot };
    public EBallState _currentBallState;

    [SerializeField] private Rigidbody2D _ballRigidbody;                                             // The Rigidbody2D component of the ball
    [SerializeField] private GameObject _arrow;                                                      // The arrow object used for indicating direction
    [SerializeField] private float _constantSpeed;                                                   // The constant speed at which the ball moves
    private Vector2 _mouseStartPosition;                                                             // The starting position of the mouse when clicked
    private Vector2 _mouseEndPosition;                                                               // The ending position of the mouse when released
    private GameManager _gameManager;

    private void Start()
    {
        _ballRigidbody = GetComponent<Rigidbody2D>();                                                // Get the Rigidbody2D component of the ball
        _gameManager = FindObjectOfType<GameManager>();                                              // Get the GameManager component from the scene
        _currentBallState = EBallState.aim;                                                          // Set the initial state of the ball to aim
    }

    private void Update()
    {
        switch (_currentBallState)
        {
            case EBallState.aim:
                if (Input.GetMouseButtonDown(0)) HandleMouseDown();                                 // Check the mouse button down pressed or not
                if (Input.GetMouseButton(0)) HandleMouseDrag();                                     // Check the left click and handle mouse dragging if it occurred
                if (Input.GetMouseButtonUp(0)) HandleMouseUp();                                     // Check if the left mouse button has been released and then call the function
                break;
            case EBallState.fire:
                // Add any logic needed for the fire state here
                break;
            case EBallState.wait:
                // Add any logic needed for the wait state here
                _currentBallState = EBallState.endShot;
                break;
            case EBallState.endShot:
                // Add any logic needed for the endShot state here
                for (int i = 0; i < _gameManager._brickInScene.Count; i++)
                {
                    _gameManager._brickInScene[i].GetComponent<BrickMovementController>()._currentState = BrickMovementController.EBrickState.move;
                }
                _gameManager.PlaceBrick();
                _currentBallState = EBallState.aim;
                break;
            default:
                break;
        }
    }

    private void HandleMouseDown()
    {
        _mouseStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);                   // Set the starting position of the mouse
    }

    private void HandleMouseDrag()
    {
        _arrow.SetActive(true);                                                                      // Activate the arrow object
        Vector2 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);         // Store the world space coordinates for the current mouse position
        SetArrowDirection(_mouseStartPosition, currentMousePosition);                                // Call the function that sets the mouseStartposition and currentMousePosition
    }

    private void HandleMouseUp()
    {
        _arrow.SetActive(false);                                                                     // Deactivate the arrow object
        _mouseEndPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);                     // Set the end mouse position and convert it from screen coordinates
        Vector2 velocity = CalculateVelocity(_mouseStartPosition, _mouseEndPosition);                 // Calculate the velocity
        _ballRigidbody.velocity = velocity * _constantSpeed;                                          // Set the ball's velocity
        if (_ballRigidbody.velocity != Vector2.zero)                                                 // Check if the velocity is not zero
            _currentBallState = EBallState.fire;                                                     // If not, change the state to fire
    }

    private void SetArrowDirection(Vector2 start, Vector2 end)
    {
        Vector2 direction = start - end;                                                            // Calculate the direction vector as the difference between the start and end positions
        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;                        // Calculate the angle between the start and end positions
        _arrow.transform.rotation = Quaternion.Euler(0f, 0f, -angle);                                // Set the rotation of the arrow based on the angle
    }

    private Vector2 CalculateVelocity(Vector2 start, Vector2 end)
    {
        Vector2 direction = start - end;                                                            // Set the direction vector as the difference between the end mouse position and start mouse position
        return direction.normalized;                                                                // Return a normalized vector representing the direction
    }

}
