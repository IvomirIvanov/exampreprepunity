using UnityEngine;
using System.Collections;

public class LevelManagerScript : MonoBehaviour
{
    public PlayerControllerScript playerScript;
    public GameObject UIMenu;
    public Transform[] spawnPositions;
    public GameObject floatingLandPrefab;
    FloorPieceScript[] floorPieces;
    float nextSpawnTime;
    float minTimeBetweenLandSpawn = 1.5f;
    float maxTimeBetweenLandSpawn = 3f;
    public GameObject PauseButton;

    void Awake()
    {
        floorPieces = transform.GetComponentsInChildren<FloorPieceScript>();
        nextSpawnTime = Random.Range(minTimeBetweenLandSpawn, maxTimeBetweenLandSpawn);
        PauseGame();
    }

    void Update()
    {
        if (!playerScript.isMoving)
        {
            return;
        }

        nextSpawnTime -= Time.deltaTime;

        if (nextSpawnTime <= 0f)
        {
            nextSpawnTime = Random.Range(minTimeBetweenLandSpawn, maxTimeBetweenLandSpawn);
            SpawnNewLand();
        }
    }
    
    

    void SpawnNewLand()
    {
        Transform newLand = GetFreeFloatingLand();

        if (newLand == null)
        {
            return;
        }

        newLand.position = spawnPositions[Random.Range(0, spawnPositions.Length)].position;
        newLand.gameObject.SetActive(true);
    }

    Transform GetFreeFloatingLand()
    {
        return Instantiate<GameObject>(floatingLandPrefab).transform;
    }

    public void PauseGame()
    {
        int count = floorPieces.Length;

        for (int i = 0; i < count; i++)
        {
            floorPieces[i].isMoving = false;
        }
        Time.timeScale = 0;

        playerScript.isMoving = false;
        playerScript.attachedRigidbody.Sleep();
        UIMenu.SetActive(true);
        PauseButton.SetActive(false);
    }

    public void UnPauseGame()
    {
        int count = floorPieces.Length;

        for (int i = 0; i < count; i++)
        {
            floorPieces[i].isMoving = true;
        }
        Time.timeScale = 1;
        playerScript.isMoving = true;
        playerScript.attachedRigidbody.WakeUp();
        UIMenu.SetActive(false);
        PauseButton.SetActive(true);
    }
}
