using UnityEngine;

public class BrickColorController : MonoBehaviour
{
	public Gradient _gradient;
	private SpriteRenderer _spriteRenderer;

    private void Start() 
	{
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_spriteRenderer.color = _gradient.Evaluate(Random.Range(0f, 1f));
	} 

    // Update is called once per frame
    void Update()
	{

	}
}