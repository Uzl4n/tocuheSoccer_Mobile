using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shoot : MonoBehaviour
{
    public Image medidor;
    public Text golUI;
    private float str;
    private bool isStrDefined = false;

    private bool shooting = false;
    private bool startCounter = false;
    private bool canShoot = false;

    private int increase = 1;
    private float strCounter = 0;
    private float timerCounter = 0;
    private float shootingCounter = 0;
    private static int golCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(Random.Range(-3f,3.15f),transform.position.y,transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {

      if(Input.GetMouseButtonUp(0) || Input.touchCount>0 && Input.touches[0].phase == TouchPhase.Began){  

            if(isStrDefined){
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if(Physics.Raycast(ray, out hit, 100f)){

                    Debug.DrawLine(transform.position, hit.point, Color.red);
                    Vector3 dir = (hit.point - transform.position).normalized;
                    GetComponent<Rigidbody>().AddForce(new Vector3(dir.x*str,dir.y*str,dir.z*str), ForceMode.Impulse);
                    shooting = true;
                }
            }
            else{
               startCounter = true;
            }   
         }

         if(startCounter){

              timerCounter += Time.deltaTime;
              
              if(timerCounter >= 0.3f){
                 timerCounter = 0;
                 canShoot = true;
              }

              if(increase == 1){
                strCounter++;
                  if(strCounter >= 20){
                      increase = -1;
                  }
              }

              else{
                 strCounter--;
                 if(strCounter<0){
                    increase = 1;
                 }
              }
         } 

          if(Input.GetMouseButtonUp(0) && canShoot || Input.touchCount>0 && Input.touches[0].phase == TouchPhase.Began && canShoot){
              startCounter = false;
              isStrDefined = true;
              str = strCounter;
          }

          medidor.GetComponent<RectTransform>().sizeDelta = new Vector2((strCounter/10)*205.64f,22.70f);

          if(shooting){
            shootingCounter += Time.deltaTime;
            if(shootingCounter >= 2f){
              Application.LoadLevel(Application.loadedLevel);
             

            }
          }
    }

    void OnCollisionEnter(Collision col){

        if(col.gameObject.tag == "gol"){
            GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
            GetComponent<Rigidbody>().angularVelocity = new Vector3(0,0,0);
            golCounter++;
            golUI.text = golCounter.ToString()+ " Gol!";
            
        }
    }
}
