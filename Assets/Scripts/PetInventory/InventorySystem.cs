using UnityEngine;
using System.Collections.Generic;

public class InventorySystem : MonoBehaviour
{
    // Egg Inventory
    private Dictionary<int, GameObject> eggList = new Dictionary<int, GameObject>();
    private GameObject activeEgg;
    private int activeEggID;

    // Pet Inventory
    private Dictionary<int, GameObject> petList = new Dictionary<int, GameObject>();
    private GameObject activePet;
    private int activePetID;

    private int currID;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        activeEggID = -1;
        activePetID = -1;
        currID = 0;
    }

    // Update is called once per frame
    // void Update()
    // {
    // }

    private int GetActiveEggID()
    {
        return activeEggID;
    }

    public int GenUniqueID()
    {
        // System.Guid.NewGuid()
        currID++;
        return currID;
    }

    public void AddEggToList(GameObject newEgg)
    {
        int newID = GenUniqueID();
        eggList.Add(newID, newEgg);
    } 

    public void RemoveEggFromList(int eggID)
    {
        eggList.Remove(eggID);
    } 

    public void MakeEggActive(int eggID)
    {
        if(eggList.ContainsKey(eggID))
        {
            if(activeEgg != null)
            {
                eggList[activeEggID] = activeEgg;
            }
            activeEgg = eggList[eggID];
            activeEgg.SetActive(true);

            Debug.Log($"Egg with ID: {eggID} made active");
        } else
        {
            Debug.Log("No egg found with that ID.");
        }
    }

    public void MakeEggInactive()
    {
        if(activeEgg != null)
        {
            activeEgg.SetActive(false);
            eggList[activeEggID] = activeEgg;
            activeEggID = -1;
        } else
        {
            Debug.Log("No Active Egg available.");
        }
    }

    // Hatch Egg will be called after gaining enough exp with the egg in the active spot
    // private void HatchEgg(GameObject currEgg)
    // {
    //     // egg will only hatch from active egg spot????

    //     // Produce Pet from this type of egg
    //     int petType = currEgg.GetPetType();
    //     MakePet(petType);
    //     // AddPetToList();
    //     // possibly set pet as active or send to box

    //     // Destroy/remove egg object
    //     int currEggID = currEgg.GetID();
    //     RemoveEggFromList(currEggID);
    //     // MakeEggInactive();
    // }

    private void MakePet(int petType)
    {
        // newPet = new Pet(petType);
        // return newPet;
    }
}
