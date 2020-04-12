using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorMousePos : MonoBehaviour {

    public float camRayLength;
    int floorMask;
    static FloorMousePos instance;

    static Vector3 mousePosOnFloor;

    public static Vector3 MousePosition()
    {
        return mousePosOnFloor;
    } 

    // Use this for initialization
    void Start () {
        if (instance == null)
        {
            floorMask = LayerMask.GetMask("Water");
            instance = this;
        }
        else
            Destroy(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Create a RaycastHit variable to store information about what was hit by the ray.
        RaycastHit floorHit;
        // Perform the raycast and if it hits something on the floor layer...
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            mousePosOnFloor = floorHit.point;
        }
    }
}
