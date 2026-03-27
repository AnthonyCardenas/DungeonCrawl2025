using UnityEngine;

public class RoomHandler : MonoBehaviour
{

    //// Door Instantiation ////
    private const int NorthDoor = 1;  // = 0x0001;
    private const int EastDoor = 2; // = 0x0010;
    private const int SouthDoor = 4; // = 0x0100;
    private const int WestDoor = 8; // 0x1000;
    public const int doorZOffset = 40;
    [SerializeField] private int doorPlacement;

    [SerializeField] private GameObject doorStyle;
    
    private GameObject currNorthDoor;
    private GameObject currEastDoor;
    private GameObject roomSouthDoor;
    private GameObject currWestDoor;
    // private DoorTrigger currNorthDoor;

    //// Wall Instantiation ////
    /// 
    public const int verticalWallOffset = 11; 
    public const int horizontalWallOffset = 8;
    public const int wallZOffset = 30;
    [SerializeField] private GameObject horizontalWallStyle;
    [SerializeField] private GameObject verticalWallStyle;
    private GameObject currNorthWall;
    private GameObject currEastWall;
    private GameObject currSouthWall;
    private GameObject currWestWall;

    //// Floor Instantiation ////
    private const int floorZOffset = 100;
    [SerializeField] private GameObject floorPrefab;
    
    //// Room Location ////  
    public Vector3 roomCenter = new Vector3( 0.0f, 21.5f, 0.0f);
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // roomCenter = new Vector3( 0.0f, 21.5f, 0.0f);
        placeDoors();
        placeWalls();
        placeFloors();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    private void placeDoors()
    {
        Debug.Log($"Door placement{doorPlacement}");
        // Debug.Log($"North Door: {( ( doorPlacement & NorthDoor ) == NorthDoor)}");
        // Debug.Log($"West Door: {(  ( doorPlacement & WestDoor )  == WestDoor)}");
        Vector3 currDoorPos = roomCenter;
        Quaternion currDoorRotation = Quaternion.identity;

        if(doorStyle == null)
        {
            Debug.Log("No Door Style Attached.");
        } else
        {
            Debug.Log("Populating Doors in next room. ");
            if( ( doorPlacement & NorthDoor ) == NorthDoor)
            {
                // Debug.Log($"North Door Placed");
                currDoorPos = roomCenter;
                currDoorPos.y += 7;
                currDoorPos.z += doorZOffset;
                currDoorRotation.eulerAngles = new Vector3(0, 0, 0);
                currNorthDoor = Instantiate(doorStyle, currDoorPos, currDoorRotation);
            }
            if( ( doorPlacement & EastDoor ) == EastDoor)
            {
                currDoorPos = roomCenter;
                currDoorPos.x += 10;
                currDoorPos.z += doorZOffset;
                currDoorRotation.eulerAngles = new Vector3(0, 0, -90);
                currEastDoor = Instantiate(doorStyle, currDoorPos, currDoorRotation);
            }
            if( ( doorPlacement & SouthDoor ) == SouthDoor )
            {
                currDoorPos = roomCenter;
                currDoorPos.y -= 7;
                currDoorPos.z += doorZOffset;
                currDoorRotation.eulerAngles = new Vector3(0, 0, 180);
                roomSouthDoor = Instantiate(doorStyle, currDoorPos, currDoorRotation);
            }
            if( ( doorPlacement & WestDoor ) == WestDoor )
            {
                currDoorPos = roomCenter;
                currDoorPos.x -= 10;
                currDoorPos.z += doorZOffset;
                currDoorRotation.eulerAngles = new Vector3(0, 0, 90);
                currWestDoor = Instantiate(doorStyle, currDoorPos, currDoorRotation);
            }
            unlockDoors();
        }
    }


    // Doors unlocked to start so player can enter the room
    private void unlockDoors() 
    {
        Debug.Log("Working on unlocking doors from Room Handler.");
        // Unlock North door
        // if(currNorthDoor != null)
        // {
        //     Transform tempTriggerObject = currNorthDoor.transform.Find("DoorTriggerCollider");
        //     DoorTrigger tempTriggerScript = tempTriggerObject.gameObject.GetComponent<DoorTrigger>();
        //     tempTriggerScript.UnlockDoor();
        // }

        
        // Unlock South door
        if(roomSouthDoor != null)
        {
            // Debug.Log("Finding south door to unlock.");
            DoorTrigger tempDoorScript;
            Transform tempTriggerObject = roomSouthDoor.transform.Find("DoorTriggerCollider");
            
            if(tempTriggerObject != null)
            {
                tempDoorScript = tempTriggerObject.gameObject.GetComponent<DoorTrigger>();
                if(tempDoorScript != null)
                {
                    // Debug.Log("Unlocking south door from script.");
                    tempDoorScript.UnlockDoor();
                    // Debug.Log($"South Door Status: {tempDoorScript.GetDoorState()}");
                }
                else
                {
                    Debug.Log("Can't find door trigger script");
                }
            }
            else
            {
                Debug.Log("Can't find trigger collider object");
            }
        } 
        else
        {
            Debug.Log("Unable to find south door.");
        }
        

    }
    // private void lockDoors() {}

    private void placeWalls()
    {
        // Debug.Log($"Wall placement{(!doorPlacement)}");
        // Debug.Log("Populating Walls in next room.");

        Vector3 currWallPos = roomCenter;
        Quaternion currWallRotation = Quaternion.identity;
        Transform currCenterWall;

        if(verticalWallStyle == null)
        {
            Debug.Log("No vertical wall style attached.");
        } else
        {
            Debug.Log("Populating vertical Walls in next room. ");
            
            // East Wall Placement //
            currWallPos = roomCenter;
            currWallPos.x += verticalWallOffset;
            currWallPos.z += wallZOffset;
            currWallRotation.eulerAngles = new Vector3(0, 0, -90);
            currEastWall = Instantiate(verticalWallStyle, currWallPos, currWallRotation);
            
            if( ( doorPlacement & EastDoor ) == EastDoor )
            {
                currCenterWall = currEastWall.transform.Find("CenterWallSprite");
                if(currCenterWall != null)
                    currCenterWall.gameObject.SetActive(false);
                
                currCenterWall = currEastWall.transform.Find("CenterWallCollider");
                if(currCenterWall != null)
                    currCenterWall.gameObject.SetActive(false);
            }
            else
            {
                currCenterWall = currEastWall.transform.Find("CenterWallSprite");
                if(currCenterWall != null)
                    currCenterWall.gameObject.SetActive(true);
                
                currCenterWall = currEastWall.transform.Find("CenterWallCollider");
                if(currCenterWall != null)
                    currCenterWall.gameObject.SetActive(true);
            }
            

            // West Wall Placement //
            currWallPos = roomCenter;
            currWallPos.x -= verticalWallOffset;
            currWallPos.z += wallZOffset;
            currWallRotation.eulerAngles = new Vector3(0, 0, 90);
            currWestWall = Instantiate(verticalWallStyle, currWallPos, currWallRotation);

            if( ( doorPlacement & WestDoor ) == WestDoor )
            {
                currCenterWall = currWestWall.transform.Find("CenterWallSprite");
                if(currCenterWall != null)
                    currCenterWall.gameObject.SetActive(false);
                
                currCenterWall = currWestWall.transform.Find("CenterWallCollider");
                if(currCenterWall != null)
                    currCenterWall.gameObject.SetActive(false);
            }
            else
            {
                currCenterWall = currWestWall.transform.Find("CenterWallSprite");
                if(currCenterWall != null)
                    currCenterWall.gameObject.SetActive(true);
                
                currCenterWall = currWestWall.transform.Find("CenterWallCollider");
                if(currCenterWall != null)
                    currCenterWall.gameObject.SetActive(true);
            }
        }

        if(horizontalWallStyle == null)
        {
            Debug.Log("No horizontal wall style attached.");
        } else
        {
            Debug.Log("Populating vertical Walls in next room. ");
            
            // North Wall Placement //
            currWallPos = roomCenter;
            currWallPos.y += horizontalWallOffset;
            currWallPos.z += wallZOffset;
            currWallRotation.eulerAngles = new Vector3(0, 0, 0);
            currNorthWall = Instantiate(horizontalWallStyle, currWallPos, currWallRotation);
            
            if( ( doorPlacement & NorthDoor ) == NorthDoor )
            {
                currCenterWall = currNorthWall.transform.Find("CenterWallSprite");
                if(currCenterWall != null)
                    currCenterWall.gameObject.SetActive(false);
                
                currCenterWall = currNorthWall.transform.Find("CenterWallCollider");
                if(currCenterWall != null)
                    currCenterWall.gameObject.SetActive(false);
            }
            else
            {
                currCenterWall = currNorthWall.transform.Find("CenterWallSprite");
                if(currCenterWall != null)
                    currCenterWall.gameObject.SetActive(true);
                
                currCenterWall = currNorthWall.transform.Find("CenterWallCollider");
                if(currCenterWall != null)
                    currCenterWall.gameObject.SetActive(true);
            }
            
            // South Wall Placement //
            currWallPos = roomCenter;
            currWallPos.y -= horizontalWallOffset;
            currWallPos.z += wallZOffset;
            currWallRotation.eulerAngles = new Vector3(0, 0, 180);
            currSouthWall = Instantiate(horizontalWallStyle, currWallPos, currWallRotation);

            if( ( doorPlacement & SouthDoor ) == SouthDoor )
            {
                currCenterWall = currSouthWall.transform.Find("CenterWallSprite");
                if(currCenterWall != null)
                    currCenterWall.gameObject.SetActive(false);
                
                currCenterWall = currSouthWall.transform.Find("CenterWallCollider");
                if(currCenterWall != null)
                    currCenterWall.gameObject.SetActive(false);
            }
            else
            {
                currCenterWall = currSouthWall.transform.Find("CenterWallSprite");
                if(currCenterWall != null)
                    currCenterWall.gameObject.SetActive(true);
                
                currCenterWall = currSouthWall.transform.Find("CenterWallCollider");
                if(currCenterWall != null)
                    currCenterWall.gameObject.SetActive(true);
            }

        }

    }

    // private void activateWalls() {}
    // private void deactivateWalls() {}

    private void placeFloors()
    {
        Vector3 currFloorPos = roomCenter;
        currFloorPos.z += floorZOffset;
        Quaternion currFloorRotation = Quaternion.identity;
        // Transform currFloorPanel; // use to remove sections of the floor
        if(floorPrefab != null)
            Instantiate(floorPrefab, currFloorPos, currFloorRotation);
        else 
            Debug.Log("No Floor Prefab found in room handler.");
    }
}
