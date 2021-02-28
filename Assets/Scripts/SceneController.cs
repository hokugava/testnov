using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
	public GameObject startPlatform, platform, lastFlatform, mainMenu,gameMenu,endLevelMenu;
	private GameObject _shadowPrefab, _lastShadowPlatform;
	public Camera Camera;
	private float _platformSpeed;
	private List<GameObject> _platforms = new List<GameObject>();
	private float _platformOffset;
    void Start()
    {
	    
	    PlayerPrefs.SetInt("levelFinished", 0);
	    PlayerPrefs.SetInt("go",0);
	    _platformSpeed = 1.5f;
	    if (!PlayerPrefs.HasKey("level"))
	    {
		    PlayerPrefs.SetInt("level",1);
		    mainMenu.SetActive(true);
		    gameMenu.SetActive(false);
		    endLevelMenu.SetActive(false);
		    Camera.transform.position = new Vector3(-0.5f,0.6f,-2.55f);
		    PlayerPrefs.SetInt("isgame",0);
	    }

	    for (int i = 0; i < 3; i++)
	    {
		    if (i >= 1)
		    {
			    _shadowPrefab = Instantiate(platform, new Vector3(0, 0, i * 9.995f), Quaternion.identity);
			    _platforms.Add(_shadowPrefab);
		    }

		    if (i == 2)
		    {
			    _lastShadowPlatform = Instantiate(lastFlatform, new Vector3(0, 0, i * 9.995f), Quaternion.identity);
		    }
	    }

    }
    void Update()
    {
	    if (PlayerPrefs.GetInt("isgame") == 1)
	    {
		    mainMenu.SetActive(false);
		    Quaternion currentRot = Camera.transform.rotation;
		    Quaternion targerRot = Quaternion.Euler(55f,0f,0f);
		    Quaternion smoothRot = Quaternion.Slerp(currentRot, targerRot, Time.deltaTime*4.5f);
		    Camera.transform.position = Vector3.MoveTowards(Camera.transform.position, new Vector3(0f,4f,-4.75f), Time.deltaTime * 3f);
		    Camera.transform.rotation = smoothRot;
		    if (Camera.transform.rotation == targerRot)
		    {
			    gameMenu.SetActive(true);
			    
		    }
	    }
	    if(PlayerPrefs.GetInt("levelFinished") == 1) {
		    Quaternion currentRot = Camera.transform.rotation;
		    Quaternion targerRot = Quaternion.Euler(10f,150f,0f);
		    Quaternion smoothRot = Quaternion.Slerp(currentRot, targerRot, Time.deltaTime*4.5f);
		    Camera.transform.position = Vector3.MoveTowards(Camera.transform.position, new Vector3(-0.5f,0.6f,-2.55f), Time.deltaTime * 3f);
		    Camera.transform.rotation = smoothRot;
		    if (Camera.transform.rotation == targerRot)
		    {
			    endLevelMenu.SetActive(true);
			    
		    }
	    }

	    if (PlayerPrefs.GetInt("go") == 1)
	    {
		    gameMenu.SetActive(false);
		    if (startPlatform != null)
		    {
			    startPlatform.transform.position += new Vector3(0,0,-Time.deltaTime *_platformSpeed);
			    if (startPlatform.transform.position.z <= -20f)
			    {
				    Destroy(startPlatform);
			    }
		    }

		    if (_lastShadowPlatform != null)
		    {
			    _lastShadowPlatform.transform.position += new Vector3(0,0,-Time.deltaTime *_platformSpeed);
		    }

		    if (_platforms.Count != 0)
		    {
			    for (int i = 0; i < _platforms.Count; i++)
			    {
				    _platforms[i].transform.position += new Vector3(0,0,-Time.deltaTime *_platformSpeed);
			    }
		    }

	    }
    }

    public void PlayGame()
    {
	    mainMenu.SetActive(false);
	    PlayerPrefs.SetInt("isgame",1);
    }
    
    public void NextScene()
    {
	    PlayerPrefs.SetInt("level", 2);
	    Scene scene = SceneManager.GetActiveScene();
	    SceneManager.LoadScene(scene.name);
	    PlayerPrefs.SetInt("isgame", 1);
    }
    
    public void Restart()
    {
	    Scene scene = SceneManager.GetActiveScene();
	    SceneManager.LoadScene(scene.name);
	    
	    PlayerPrefs.SetInt("isgame", 1);
	    mainMenu.SetActive(false);
    }

    public void PressToGo()
    {
	    gameMenu.SetActive(false);
	    PlayerPrefs.SetInt("go",1);
    }

    public void ExitGame()
    {
	    Application.Quit();
    }
}
