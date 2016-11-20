using UnityEngine;
using System.Collections;

public class SpriteToFullScreen : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
    {
	    SpriteRenderer sprite = transform.GetComponent<SpriteRenderer>();

        if (sprite == null)
        {
            return;
        }

        transform.localScale = new Vector3(1f, 1f, 1f);

        float width = sprite.sprite.bounds.size.x;
        float height = sprite.sprite.bounds.size.y;

        double worldScreenHeight = Camera.main.orthographicSize * 2.0;
        double worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        transform.localScale = new Vector3((float)(worldScreenWidth / width), (float)(worldScreenHeight / height), transform.localScale.z);

	}
	
	
}
