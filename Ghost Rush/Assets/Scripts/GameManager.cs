using UnityEngine;
using System.Collections.Generic;
using System.Collections;
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
        StartCoroutine(newRound());
    }

    private IEnumerator newRound()
    {
        yield return new WaitForSeconds(3f);
        numGhosts = baseGhosts*level;
        for(int i =0; i<baseGhosts*level; i++){
            int randomNumber = Random.Range(0,SpawnPoints.Count);
            GameObject ghost = Instantiate(ghostAsset, SpawnPoints[randomNumber].transform.position, Quaternion.identity);
            GhostMovement gm = ghost.GetComponent<GhostMovement>();
            gm.player = mainPlayer;
            gm.gmScript = this;
            yield return new WaitForSeconds(.35f);
        }

    }   

    public void ghostDeath()
    {
        numGhosts-=1;
        if (numGhosts == 0)
        {
            level+=1;
            StartCoroutine(newRound());
        }
    }




    
}
