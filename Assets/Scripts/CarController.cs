using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private GameObject pickUpEffectDia;
    [SerializeField] private GameObject pickUpEffectStar;

    public float moveSpeed;
    private bool movingLeft = true;
    private bool firstInput = true;

    //private bool gameOver;
  
    void Update()
    {
        if(GameManager.instance.gameStarted){
            Move();
            CheckInput();
        }

        if(transform.position.y <= -1/* && !gameOver*/){
            //gameOver=true;
            GameManager.instance.GameOver();
        }        
    }

    void Move(){
        transform.position += moveSpeed * Time.deltaTime * transform.forward;
    }

    void CheckInput(){
        //if first input ignore
        if(firstInput){
            firstInput=false;
            return;
        }

        if(Input.GetMouseButtonDown(0)){
            ChangeDirection();
        }
    }

    void ChangeDirection(){
        if(movingLeft){
            transform.rotation = Quaternion.Euler(0,90,0);            
            movingLeft=false;
        }
        else{
            transform.rotation = Quaternion.Euler(0,0,0);
            movingLeft=true;
        }
        //movingLeft = !movingLeft; cambiar el booleano        
    }

    private void OnTriggerEnter(Collider collider) {
        Vector3 posicionDia = collider.transform.position;
        Vector3 posicionStar = collider.transform.position;
        posicionDia.y -= 0.3f;
        posicionStar.y -= 0.7f;

        if(collider.CompareTag("Diamond"))
        {
            Instantiate(pickUpEffectDia,posicionDia,pickUpEffectDia.transform.rotation);            
            GameManager.instance.IncrementScore(2);            
        }
        else if(collider.CompareTag("Star"))
        {
            Instantiate(pickUpEffectStar,posicionStar,pickUpEffectStar.transform.rotation);            
            GameManager.instance.IncrementScore(5);            
        }
        collider.gameObject.SetActive(false);
    }
}
