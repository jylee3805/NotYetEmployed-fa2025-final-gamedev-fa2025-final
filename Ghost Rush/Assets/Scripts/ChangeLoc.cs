using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ChangeLoc : MonoBehaviour
{
    private List<GameObject> playerPoints = new List<GameObject>();
    public Transform spawnP;
    public GameManager gmScript;
    public Transform mainPlayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

     void Start()
    {
        foreach (Transform child in spawnP)
        {
            playerPoints.Add(child.gameObject);
        }
    }
   public void DoAttackHit()
    {
        int randomMap = Random.Range(0,4);
        mainPlayer.position = playerPoints[randomMap].transform.position;  
        StartCoroutine(gmScript.newRound(randomMap));
    }
}
