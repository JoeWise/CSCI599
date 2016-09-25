using UnityEngine;
using System.Collections;

public class GameManager : Singleton<GameManager> {

    public GameObject lights; 
    //Guarantee this will be always a singleton only
    protected GameManager() { }
    void Awake()
    {

    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public bool isAllLightsCollected()
    {
        for(int i = 0; i < lights.GetComponent<Transform>().childCount; i++)
        {
            if (lights.GetComponent<Transform>().GetChild(i).gameObject.activeSelf)
            {
                return false;
            }
        }
        return true;
    }
}
