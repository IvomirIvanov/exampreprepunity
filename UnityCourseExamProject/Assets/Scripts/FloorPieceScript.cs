using UnityEngine;
using System.Collections;

public class FloorPieceScript : MonoBehaviour
{
    public float endOfScreenCoordinate = -16;
    public float startOfScreeenCoordinate = 16f;
    public float movingSpeed = -1f;
    public bool isMoving;
    
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (isMoving)
        {
            transform.Translate(movingSpeed * Time.deltaTime, 0f, 0f);

            if (transform.position.x <= endOfScreenCoordinate)
            {
                transform.position = new Vector3(startOfScreeenCoordinate, transform.position.y, transform.position.z);
            }
        }
	}
}
