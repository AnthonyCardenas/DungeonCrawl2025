using UnityEngine;

public class EnemyHandler : MonoBehaviour
{

    public GameObject currPrefab;
    [SerializeField] private DoorTrigger currNorthDoor;
    // [SerializeField] private DoorTrigger currEastDoor;
    [SerializeField] private DoorTrigger currSouthDoor;
    // [SerializeField] private DoorTrigger currWestDoor;
    

    [SerializeField] private int maxNumEnemies;
    [SerializeField] private int currNumEnemies;

    // public List<Enemy> enemyArray = new List<Enemy>();
    // Enemy mainEnemy = new Enemy();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Started Enemy Handler/Spawner");
        maxNumEnemies = 2;
        currNumEnemies = 2;
        if(currPrefab == null)
        {
            Debug.Log("No enemy prefab found in Handler");
        } else
        {
            Vector3 currPos = new Vector3(-3.0f, 5f, 0f);
            Instantiate(currPrefab, currPos, Quaternion.identity);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetCurrNumEnemies()
    {
        return currNumEnemies;
    }

    public void SetCurrNumEnemies(int newNumEnem)
    {
        currNumEnemies = newNumEnem;
    }

    public void RemoveEnemy()
    {
        currNumEnemies--;
        if(currNumEnemies <= 0)
        {
            Debug.Log("All enemies killed");
            // SetDoorState(DoorState.Unlocked);
            currNorthDoor.UnlockDoor();
            // currEastDoor.UnlockDoor();
            currSouthDoor.UnlockDoor();
            // currWestDoor.UnlockDoor();

            // Populate adjacent room
        }
    }
}
