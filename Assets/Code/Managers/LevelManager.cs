using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Ensures the 
public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [SerializeField] List<FighterMovement> fighters;

    private void Awake()
    {
        if (Instance != null)
            Destroy(this);
        else
            Instance = this;
        //TODO: generate necessary fighters
    }
    
    //Removes the fighter in question from this script's list, and also destroys the fighter object 
    public void RemoveFighter(FighterMovement f)
    {
        fighters.Remove(f);
        Destroy(f.gameObject);
        //TODO: handle errors that will occur from input listener
        if (fighters.Count == 1)
            Debug.Log(fighters[0].name + " is the winner!");
    }

    public List<FighterMovement> GetFighters()
    {
        return fighters;
    }
}
