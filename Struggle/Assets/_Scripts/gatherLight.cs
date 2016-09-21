using UnityEngine;
using System.Collections;

public class gatherLight : MonoBehaviour {

	public GameObject lantern;
	public GameObject lightSource;

	private Light lanternLight = null;
	private Renderer currentRend = null;

	// Use this for initialization
	void Start () {
		lanternLight = lantern.GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {

	}

	private void OnMouseDown()
	{
		if (lightSource != null)
		{

			if(currentRend != null)
			{
				//InvokeRepeating("diminishLightSource", 1.0f, 0.1f);
			}
			/* When animated will need to switch line */
			/*Invoke("removeLightSource", 2);*/ lightSource.SetActive(false);

			for(int i = 0; i < 50; i++)
            {
                lanternLight.color += Color.green / 100;
                //addToLantern();
            }

            currentRend = null;
		}
	}

	// Looping function
	private void diminishLightSource()
	{
		// Ok so it turns out animating emissive lighting is hard.
		// I'll come back to this.
		/*
		currentRend = lightSource.GetComponent<Renderer>();
		Material mat = currentRend.material;
		mat.EnableKeyword("_EMISSION");
		mat.SetColor("_EmissionColor", Color.red);
		*/
	}

    // Looping function
    private void addToLantern()
	{
		lanternLight.color += Color.green / 10;
        // Add some time delay here
    }



	private void removeLightSource()
	{
		lightSource.SetActive(false);
	}

}
