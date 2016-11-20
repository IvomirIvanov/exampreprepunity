using UnityEngine;
using System.Collections;

public class FloatingLandScript : MonoBehaviour
{
    public float endOfScreenCoordinate = -16;
    public float movingSpeed = -1f;
    public bool isMoving;
    public GameObject bananaPrefab;

    void OnEnable()
    {
        int bananasCount = Random.Range(1, 4);
        for (int i = 0; i < bananasCount; i++)
        {
            GameObject newBanana = Instantiate<GameObject>(bananaPrefab);
            newBanana.transform.SetParent(this.transform);
            newBanana.transform.localPosition = new Vector3(-0.7f + (i * 0.45f), 0.5f, 0f);
            newBanana.transform.localScale = Vector3.one * 0.5f;
        }

        isMoving = true;
    }

    void Update()
    {
        if (isMoving)
        {
            transform.Translate(movingSpeed * Time.deltaTime, 0f, 0f);

            if (transform.position.x <= endOfScreenCoordinate)
            {
                foreach (Transform child in transform)
                {
                    Destroy(child.gameObject);
                }

                Destroy(gameObject);
            }
        }
    }
}
