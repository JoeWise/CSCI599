using UnityEngine;
using System.Collections;

public class playerCollider : MonoBehaviour
{

    public GameObject lantern;
    public GameObject victoryText;
    public float pushPower = 2.0F; // Push force of player
    public GameObject[] carpetHair;
    public float distTriggerCarpetHair = 2.0f; // Distance to start carpet hair animation
    public float maxAngle = 80; // Max angle of carpet hair tilt


    private Light lanternLight = null;
    private Vector3[] carpetHairAngles = null;


    // Use this for initialization
    void Start()
    {
        lanternLight = lantern.GetComponent<Light>();
        carpetHair = GameObject.FindGameObjectsWithTag("CarpetHair");
        if (carpetHair != null)
        {
            for (int i = 0; i < carpetHair.Length; i++)
            {
                //carpetHairAngles[i] = new Vector3(0, 0, 0);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (carpetHair != null)
        {
            for (int i = 0; i < carpetHair.Length; i++)
            {
                Vector3 distance = transform.position - carpetHair[i].transform.position;
                distance.y = 0; // Only look on x,z
                float distanceMag = distance.magnitude;
                if (distanceMag < distTriggerCarpetHair)
                {
                    adjustCarpetHair(carpetHair[i], distanceMag);
                }
                else carpetHair[i].transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
    }

    // Animate carpet hairs
    void adjustCarpetHair(GameObject carpetHair, float distance)
    {
        //carpetHair.SetActive(false); // Testing
        float magnitude = ((distTriggerCarpetHair - distance) / distTriggerCarpetHair);
        // Non-linear
        magnitude *= magnitude;


        float xRatio, zRatio;
        xRatio = (carpetHair.transform.position.x - transform.position.x) / distance * maxAngle;
        zRatio = (carpetHair.transform.position.z - transform.position.z) / distance * maxAngle;

        Debug.Log("Distance: " + distance + "Magnitude: " + magnitude + " Ratios " + xRatio + " " + zRatio);
        carpetHair/*[i]*/.transform.eulerAngles = new Vector3(zRatio * magnitude, 0, -xRatio * magnitude);// carpetHairAngles[i];

    }

    // Physics for collision - may be needed later
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        if (body == null || body.isKinematic)
            return;

        if (hit.moveDirection.y < -0.3F)
            return;

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        body.velocity = pushDir * pushPower;
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

        lanternLight.color += colorObject / 3;
        lanternLight.range *= 1.1f;
        //lanternLight.intensity *= 1.1f;
    }
}
