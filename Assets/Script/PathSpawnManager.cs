using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSpawnManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> list_Path;
    private List<GameObject> list_OfPathActiveInScene = new List<GameObject>();
    private int noOfActivepathInScene = 4;
    private int distanceBetweentwoPath = 100;
    private float maxDistanceWhenPathDestroy = 100;
    [SerializeField] Transform startingGroundTransform;
    [SerializeField] private Transform pathparent;
    public Transform player;
    


    private void Start()
    {
       
        FindPathFromlist_path();
        SpawnPathWhenStartGame();

    }
    private void Update()
    {
        if (!GameManager.InstanceOfGameManager.isPlayerAlive) { return; }
       
        DestroyAndSpawnPath();
    }

    private void DestroyAndSpawnPath()
    {  // When playerposition>pathposition destroy that path.after destroying new 1 path 
        //genrate

        if (player.transform.position.x-pathparent.GetChild(0).transform.position.x>
            maxDistanceWhenPathDestroy)
        {
           Destroy(pathparent.GetChild(0).gameObject); // destroy Path
           list_OfPathActiveInScene.RemoveAt(0);   // remove gameobject in active list

           int list_pathIndex = Random.Range(0, list_Path.Count); //find rendom path
        

           list_OfPathActiveInScene.Add(list_Path[list_pathIndex]); // add new path in list
            
            Transform lastpath = pathparent.GetChild(3);    // get property of last path
            Vector3 lastPathPosition = new Vector3(lastpath.position.x +
                distanceBetweentwoPath, 
                lastpath.position.y, lastpath.position.z);  // find position of new path
            Instantiate(list_OfPathActiveInScene[list_OfPathActiveInScene.Count-1],
                lastPathPosition, lastpath.rotation, pathparent);   // Spawn new path
                                                               
        }
    }
    private void SpawnPathWhenStartGame()
    {
        // spwn path in Game
        for (int i = 0; i < list_OfPathActiveInScene.Count; i++)
        {
            
            Instantiate(list_OfPathActiveInScene[i], new Vector3
          (startingGroundTransform.position.x + distanceBetweentwoPath * (i + 1),
            startingGroundTransform.position.y,
            0),
             startingGroundTransform.rotation, pathparent);
           
        }
    }
    private void FindPathFromlist_path()
    {   // find rendom path in list_path 

        for (int i = 0; i < noOfActivepathInScene; i++)
        {
            int list_pathIndex = Random.Range(0, list_Path.Count);
            list_OfPathActiveInScene.Add(list_Path[list_pathIndex]);
        }
    }
    

}
