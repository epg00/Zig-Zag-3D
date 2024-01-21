using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    [SerializeField] private GameObject diamond, star;
    
    public Vector3 lastPositionTouched;
    public static Platform instance;

    public Vector3 ultimoEnCrear;

    //public float lastPositionX;
    //public float lastPositionZ;

    public void Awake(){
        if(instance==null){
            instance=this;
        }
    }

    void Start(){
        int randDiamond = Random.Range(0,10);
        int randStar = Random.Range(0,30);
        Vector3 objectPos = transform.position;
        objectPos.y += 1f;

        if(randDiamond<1){
            GameObject diamondInstance = Instantiate(diamond,objectPos,diamond.transform.rotation);
            diamondInstance.transform.SetParent(gameObject.transform);
        }
        else if(randStar<1){
            GameObject starInstance = Instantiate(star,objectPos,star.transform.rotation);
            starInstance.transform.SetParent(gameObject.transform);
        }
    }
    private void OnCollisionExit(Collision collision) {
        if(collision.gameObject.CompareTag("Player"))
        {
            Invoke(nameof(Fall), 0.2f);
        }
    }

    private void Fall(){
        /*lastPositionTouched = gameObject.transform.position;
        lastPositionX = gameObject.transform.position.x;
        lastPositionZ = gameObject.transform.position.z;
        Debug.Log("Ultima posicion tocada: " + lastPositionTouched);*/
        GetComponent<Rigidbody>().isKinematic=false;
        Destroy(gameObject,1f);
    }

    public Vector3 UltimoPisado(){
        for(int i=0; i < PlatformSpawner.instance.platforms.Length; i++){
            if(PlatformSpawner.instance.platforms[i] != null){
                /*Debug.Log("Posicion correcta: " + PlatformSpawner.instance.platforms[i].transform.position);
                Debug.Log("LAST POSITION TOUCHED en x: " + lastPositionX);
                if((lastPositionX == PlatformSpawner.instance.platforms[i].transform.position.x) && (lastPositionZ == PlatformSpawner.instance.platforms[i].transform.position.z)){*/
                    lastPositionTouched = PlatformSpawner.instance.platforms[i].transform.position;
                //}
                
                break;
            }            
        }

        ultimoEnCrear = PlatformSpawner.instance.platforms[PlatformSpawner.instance.platforms.Length-1].transform.position;
        
        return lastPositionTouched;
    }
}
