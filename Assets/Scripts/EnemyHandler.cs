using UnityEngine;

public class EnemyHandler : MonoBehaviour
{

<<<<<<< Updated upstream
    // Enemy[] enemyArray;
    Enemy mainEnemy;


=======
    // public List<Enemy> enemyArray = new List<Enemy>();
    // Enemy mainEnemy = new Enemy();
>>>>>>> Stashed changes
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Started Enemy Handler");
        Vector3 pos = new Vector3(0f, 0f, 0f);
        Color color = Color.blue;
        Vector3 size = new Vector3(0f, 0f, 0f);
        // mainEnemy = Enemy.Create(pos, color, size);
        // GameObject enemyGameObject = new GameObject("Enemy");
        // enemyGameObject.tranform.position = new Vector3(0, 0);

    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
