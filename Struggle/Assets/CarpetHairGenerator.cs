using UnityEngine;
using System.Collections;

//[ExecuteInEditMode]
public class CarpetHairGenerator : MonoBehaviour {

    public GameObject mainPlane;
    public GameObject hairPrefab;
    //public GameObject player;

    public float density = 5;
    public float radius = 20; //gap btwn hairs and objects

	// Use this for initialization
	void Start ()
    {
        if (density <= 2)
            density = 2f;
        if (radius <= 1f)
            radius = 1f;
        Debug.Log(mainPlane.GetComponent<Transform>().localScale);
        Debug.Log(mainPlane.GetComponent<MeshFilter>().sharedMesh.bounds.size);
        Vector3 localScale = mainPlane.GetComponent<Transform>().localScale;
        Vector3 boundSize = mainPlane.GetComponent<MeshFilter>().sharedMesh.bounds.size;
        Vector3 pos = mainPlane.GetComponent<Transform>().position;
        float xLength = localScale.x * boundSize.x;
        float zLength = localScale.z * boundSize.z;
        for (float i = pos.x - xLength / 2; i <= pos.x + xLength / 2; i += density)
            for (float j = pos.z - zLength / 2; j <= pos.z + zLength / 2; j += density)
            {
                Vector3 currentPosition = new Vector3(i, pos.y, j);
                Collider[] hitColliders = Physics.OverlapSphere(currentPosition, radius);
                if(hitColliders.Length <= 1)
                {
                    Instantiate(hairPrefab, currentPosition, Quaternion.identity);
                }
            }
        Debug.Log("Generation completed");
    }
	
	// Update is called once per frame
	void Update () {
	    
	}
}
