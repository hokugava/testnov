using UnityEngine;

public class ObstacleCollManager : MonoBehaviour
{
	private void OnCollisionEnter(Collision other)
	{
		if (other.collider.CompareTag("Wall"))
		{
			Physics.IgnoreCollision(other.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
		}
	}
}
