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

    private int level = 0;
    public int baseGhosts = 5;
    public GameObject ghostAsset;
    public GameObject ghostAsset2;


    



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
        int numPoints = baseGhosts + (3*level);
        if (level > 3)
        {
            int max = (int)((level-3) * 1.5);
            int current = 0;
            while (true)
            {
                if (current == max) break;
                if (numPoints > 2)
                {
                    numPoints -= 2;
                    current++;
                    numGhosts++;
                    int randomNumber = Random.Range(0,SpawnPoints[pSpawn].Count);
                    GameObject ghost = Instantiate(ghostAsset2, SpawnPoints[pSpawn][randomNumber].transform.position, Quaternion.identity);
                    GhostMovement gm = ghost.GetComponent<GhostMovement>();
                    gm.player = mainPlayer;
                    gm.gmScript = this;
                    gm.maxHealth = 200f;
                    gm.health = 200f;
                    gm.speed = 1.5f;
                    gm.dmg = 20f;
                    yield return new WaitForSeconds(.35f);
                }else
                {
                    break;
                }
            }
        
        }
        int healthLVL = level / 3;
        float dmg = 4f;
        float health = 20f;
        if (healthLVL > 3)
        {
            health = 80f;
            dmg = 10f;
        }
        else
        {
            health += 20*healthLVL;
            dmg += 2 * healthLVL;
        }
        
        
        for(int i =0; i<numPoints; i++){
            numGhosts++;
            int randomNumber = Random.Range(0,SpawnPoints[pSpawn].Count);
            GameObject ghost = Instantiate(ghostAsset, SpawnPoints[pSpawn][randomNumber].transform.position, Quaternion.identity);
            GhostMovement gm = ghost.GetComponent<GhostMovement>();
            gm.player = mainPlayer;
            gm.gmScript = this;
            gm.maxHealth = health;
            gm.health = health;
            gm.dmg = dmg;
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
