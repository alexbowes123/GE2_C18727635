using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Tile
{
    public GameObject theTile;
    public float creationTime;

    public Tile(GameObject t, float ct)
    {
        theTile = t;
        creationTime = ct;
    }
}

public class GenerateInfinite : MonoBehaviour
{
    
    public GameObject plane; //game object smart plane
    public GameObject cylinder; // player character




    public GameObject objectToSpawn; //cactus game object
    public int numCacti = 10;
    public float theta = (2.0f * Mathf.PI) /10;  //angle between each cactus
   

    [SerializeField] private Vector3 _Rotation;
    private float radius = 15f;


    private List<Vector3> tilePositions = new List<Vector3>();

    int planeSize = 10; // each tile is 10 away from the previous tile

    int halfTilesX = 5; //radius of how many tiles around the player
    int halfTilesZ = 5;

    Vector3 startPos; //keep track of where player is/was

    private Hashtable tiles = new Hashtable(); // used for storing key+value pairs of tiles that are created
     

    void Start()
    {
        this.gameObject.transform.position = Vector3.zero; //set position of tile to  0,0,0 of world

        startPos = Vector3.zero; // start position is also 0,0,0

        float updateTime = Time.realtimeSinceStartup; //time since game started

    
        //Generate initial tiles around the starting tile 
        for(int x = - halfTilesX; x < halfTilesX; x++)
        {
            for(int z = -halfTilesZ; z < halfTilesZ; z++)
            {
                //position vector that is relative to the starting position 
                Vector3 pos = new Vector3((x * planeSize+startPos.x),
                        0,(z * planeSize+startPos.z));

                //instantiate a plane at the position
                GameObject t = (GameObject) Instantiate(plane, pos, Quaternion.identity);

                string tilename = "Tile_" + ((int)(pos.x)).ToString() + "_" + ((int)(pos.z)).ToString();
                t.name = tilename;

                //new tile object is created and added to the hashtable with its name for keeping track of existing tiles
                Tile tile = new Tile(t, updateTime);
                tiles.Add(tilename, tile); 

                tilePositions.Add(t.transform.position);
                
            }
        }  
        SpawnObject(); //spawn cacti when the game starts
       
    }

    private void SpawnObject(){


        for(int c =0; c < numCacti; c++){

            //get the angle between each cacti
            float radians = 2 * Mathf.PI/numCacti * c;

            //instnatiate the cacti at the position
            GameObject toPlaceObject = Instantiate(objectToSpawn, 
            ObjectSpawnLocation(c, radians),
            Quaternion.identity);
         

            //each object will point to the center of the spawn circle
            Vector3 pointTo = new Vector3(0,3.6f,0);
            toPlaceObject.transform.LookAt(pointTo);

            // toPlaceObject.AddComponent<Rigidbody>();
            // toPlaceObject.constraints = RigidbodyConstraints.FreezePosition;

            Quaternion target = Quaternion.Euler(90, 0, 0);  
        }
    }

     private Vector3 ObjectSpawnLocation (int c, float radians) {                             //spawn objects in a circle

        float angle = theta * c; //get angle between 2 objs

        float circleX = Mathf.Sin(radians);
        float circleZ = Mathf.Cos(radians);
    
  
        //get position of the cactus 
        Vector3 newPos = new Vector3 (
            circleX,
            0,
            circleZ
        );

        Debug.Log("Angle: "+angle);

        Vector3 spawnCenter = new Vector3 (0, 3.6f, 0); //the center of ring

        var spawnPos = spawnCenter + newPos * radius; 
        //each cacti will point to the starting position (the player when the game starts)
        this.transform.LookAt(spawnCenter);

        return spawnPos;

    }

    // Update is called once per frame
    void Update()
    {
        //Determine how far player has moved
        int xMove = (int)(cylinder.transform.position.x - startPos.x);
        int zMove = (int)(cylinder.transform.position.z - startPos.z);

        Debug.Log("xMove: "+xMove);
        Debug.Log("zMove: "+zMove);

        //if move further than the planeSize
        if(Mathf.Abs(xMove) >= planeSize || Mathf.Abs(zMove) >= planeSize)
        {
            //timestamp for new tiles created
            float updateTime = Time.realtimeSinceStartup;

            //retrieve integer value for x and z position and round off to nearest 10 (tilesize)
            int playerX = (int)(Mathf.Floor(cylinder.transform.position.x/planeSize)*planeSize);
            int playerZ = (int)(Mathf.Floor(cylinder.transform.position.z/planeSize)*planeSize);


            //generating new tile
            for(int x = - halfTilesX; x < halfTilesX; x++)
            {
                for(int z = -halfTilesZ; z < halfTilesZ; z++)
                {
                    
                    Vector3 pos = new Vector3((x * planeSize + playerX),
                                    0,
                                        (z * planeSize + playerZ)); //offset based on players position
                   
                    string tilename = "Tile_" + ((int)(pos.x)).ToString() + "_" + ((int)(pos.z)).ToString();

                    Debug.Log(tilename);
                    if(!tiles.ContainsKey(tilename))//if a tile has not already been created that has the same name.. (check if there is not already a tile on this spot)
                    {
                        GameObject t = (GameObject) Instantiate(plane, pos,
                                Quaternion.identity);
                                                                                    //create and add the new tile to hashtable
                        t.name = tilename;
                        Tile tile = new Tile(t, updateTime);
                        tiles.Add(tilename, tile);
                         
                    }
                    else
                    {
                        (tiles[tilename] as Tile).creationTime = updateTime;
                    }
                }
            }

            //destroy all tiles not just created or with time updated
            //and put new tiles and tile to be kept in a new hashtable
            Hashtable newTerrain = new Hashtable();

            //loop around old hashtable
            foreach(Tile tls in tiles.Values)
            {
                if(tls.creationTime != updateTime)
                {
                    //destroy tile gameObject
                    Destroy(tls.theTile);
                }
                else
                {
                    //if it is a tile worth that needs to be kept
                    newTerrain.Add(tls.theTile.name, tls);
                }
            }
            //copy new hashtable contents to the working hashtable
            tiles = newTerrain;

            startPos = cylinder.transform.position;
        }
        
    }
}