using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class GameManager : MonoBehaviour
{
    public Transform mainPlayer;

    public Animator transition;
    public int numGhosts;
    public bool floorCleared = false;
    public Transform spawnF;

    private List<List<GameObject>> SpawnPoints = new List<List<GameObject>>();

    private int level = 1;
    public int baseGhosts = 5;
    public GameObject ghostAsset;


    



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (Transform child in spawnF)
        {
            List<GameObject> current = new List<GameObject>();
            foreach (Transform c in child)
            {
                current.Add(c.gameObject);
            }
            SpawnPoints.Add(current);
        }
        StartCoroutine(newRound(0));
    }

    public IEnumerator newRound(int pSpawn)
    {   
        yield return new WaitForSeconds(3f);
        numGhosts = baseGhosts*level;
        for(int i =0; i<baseGhosts*level; i++){
            int randomNumber = Random.Range(0,SpawnPoints[pSpawn].Count);
            GameObject ghost = Instantiate(ghostAsset, SpawnPoints[pSpawn][randomNumber].transform.position, Quaternion.identity);
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
            transition.SetTrigger("Start");
            //StartCoroutine(newRound());
        }
    }




    
}
