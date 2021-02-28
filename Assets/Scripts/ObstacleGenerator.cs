using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
	public GameObject sphere,block;
	private GameObject _shadowSphere1,_shadowSphere2, _shadowBlock;
	private int width;
	private int height;
  
	void Start()
	{
		width = Random.Range(5, 8);
		height = Random.Range(5, 8);
		for (int z=0; z<height; ++z)
		{
			for (int x=0; x<width; ++x)
			{
				_shadowSphere1 = Instantiate(sphere, new Vector3(
					transform.position.x + x/4f,
					transform.position.y+0.15f,
					transform.position.z + z/4f), Quaternion.identity);
				
				
				_shadowSphere2 = Instantiate(sphere, new Vector3(
					transform.position.x - x/4f,
					transform.position.y+0.15f,
					transform.position.z - z/6f), Quaternion.identity);
				
				_shadowSphere1.transform.parent = _shadowSphere2.transform.parent = gameObject.transform;
			}
		}

		if (PlayerPrefs.GetInt("level") == 2)
		{
			_shadowBlock = Instantiate(block, new Vector3(
				transform.position.x,
				transform.position.y+0.45f,
				transform.position.z + 3f), Quaternion.identity);
			_shadowBlock.transform.parent = gameObject.transform;
		}
	}
}
