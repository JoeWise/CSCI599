using UnityEngine;
using System.Collections;

public class playerCollider : MonoBehaviour {

    public GameObject lantern;
    public GameObject victoryText;
    private Light lanternLight = null;


    // Use this for initialization
    void Start () {
        lanternLight = lantern.GetComponent<Light>();
    }

    // Update is called once per frame
    void Update () {
	
	}

    // Collect light source. Should probably not be here.
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            Debug.Log("Collision Detected");
            GameObject otherObject = other.transform.gameObject;
            updateLantern(otherObject);
            other.gameObject.SetActive(false);

            if (GameObject.FindGameObjectsWithTag("PickUp").Length == 0)
            {
                Debug.Log("All lights collected");
                victoryText.SetActive(true);
            }
            // change lantern colour
        }
    }

    void updateLantern(GameObject otherObject)
    {
        Light colorLight = otherObject.GetComponent<Light>();
        Color colorObject = colorLight.color;
        for (int i = 0; i < 3; i++)
        {
            Debug.Log("Colour: " + i + ": " + colorObject[i]);
            if (colorObject[i] < 0.8) colorObject[i] = 0; // Remove secondary colours
        }

        lanternLight.color += colorObject / 2;
        lanternLight.range *= 1.1f;
        lanternLight.intensity *= 1.1f;
    }
}
