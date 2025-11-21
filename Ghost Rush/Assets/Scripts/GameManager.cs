using UnityEngine;
using System.Collections.Generic;
public class GameManager : MonoBehaviour
{
    public Transform mainPlayer;
    public int numGhosts;
    public bool floorCleared = false;
    public Transform spawnF;
    private List<GameObject> SpawnPoints = new List<GameObject>();
    private int level = 1;
    public int baseGhosts = 5;
    public GameObject ghostAsset;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (Transform child in spawnF)
        {
            SpawnPoints.Add(child.gameObject);
        }

        for(int i =0; i<baseGhosts*level; i++){
            int randomNumber = Random.Range(0,SpawnPoints.Count);
            GameObject ghost = Instantiate(ghostAsset, SpawnPoints[randomNumber].transform.position, Quaternion.identity);
            ghost.GetComponent<GhostMovement>().player = mainPlayer;
            Debug.Log(i);
        }


    }



    
}
