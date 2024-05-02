using TMPro;
using UnityEngine;

public class BrickHealthManger : MonoBehaviour
{
	public int _brickHealth;
	private TMP_Text _brickHealthText;
	private GameManager _gameManager;


    // Start is called before the first frame update
    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
		_brickHealth = _gameManager._level;
		_brickHealthText = GetComponentInChildren<TMP_Text>();
    }

    // Update is called once per frame

    private void Update() 
	{
		_brickHealthText.text = _brickHealth.ToString();
		if(_brickHealth <= 0) { this.gameObject.active = false; } //	Destroy	brick
	}

    void TakeDamage(int _damage) => _brickHealth -= _damage;

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Ball")) TakeDamage(1);
	}
}