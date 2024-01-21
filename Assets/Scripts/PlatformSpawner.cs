using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platform;
    public Transform lastPlatform;
    public Vector3 lastPosition;
    Vector3 newPosition;
    bool stop;    
    
    public GameObject[] platforms;


    public int i =0;
    public static PlatformSpawner instance;
    public void Awake(){
        if(instance==null){
            instance=this;
        }
    }
    void Start()
    {
        lastPosition = lastPlatform.position;

        platforms = new GameObject[100000];

        StartCoroutine(SpawnPlatforms());
    }
        
    IEnumerator SpawnPlatforms(){
        while (!stop){
            GeneratePosition();

            platforms[i] = Instantiate(platform,newPosition,Quaternion.identity,platform.transform.parent); 

            lastPosition = newPosition;             

            i++;

            yield return new WaitForSeconds(0.1f);      
        }        
        
    }

    void GeneratePosition(){
        newPosition = lastPosition;

        int random = Random.Range(0,2);

        if(random>0){
            newPosition.x += 2f;            
        }
        else{
            newPosition.z += 2f;
        }              
    }
}
