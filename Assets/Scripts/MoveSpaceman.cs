using UnityEngine;

public class MoveSpaceman : MonoBehaviour
{
	private Touch _touch;
	private float speedM;
	private Rigidbody _rigidbody;
	private Animator _animator;
	public ParticleSystem ps;
	void Start()
	{
		_animator = GetComponent<Animator>();
		speedM = .005f;
		_rigidbody = GetComponent<Rigidbody>();
		_rigidbody.freezeRotation = true;
		if (ps != null)
		{
			ps.Stop();
		}
	}
	void Update()
	{
		if (PlayerPrefs.GetInt("isgame") == 1 && PlayerPrefs.GetInt("go") == 1)
		{
			_animator.SetBool("isRun",true);
			if (Input.touchCount > 0)
			{
				_touch = Input.GetTouch(0);
				if (_touch.phase == TouchPhase.Moved)
				{
					transform.position = new Vector3(
						transform.position.x+_touch.deltaPosition.x*speedM,
						transform.position.y,transform.position.z + _touch.deltaPosition.y * speedM);	
				}
			}
		}
	}

	private void OnCollisionEnter(Collision other)
	{
		if (other.collider.CompareTag("StartLine"))
		{
			Destroy(other.gameObject);
			if (ps != null)
			{
				ps.Play();
			}
		}
		
		if (other.collider.CompareTag("FinishLine"))
		{
			Destroy(other.gameObject);
			_animator.SetBool("isRun",false);
			PlayerPrefs.SetInt("levelFinished", 1);
			PlayerPrefs.SetInt("isgame",0);
			PlayerPrefs.SetInt("go",0);
			_rigidbody.velocity = Vector3.zero;
		}
	}
}
