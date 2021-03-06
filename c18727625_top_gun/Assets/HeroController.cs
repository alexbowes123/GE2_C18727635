using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// class TakeOffState: State
// {
   
//     public override void Enter()
//     {
//         owner.GetComponent<FollowPath>().enabled = true;
//     }
//     public override void Exit()
//     {   
//         //leave follow path behaviour after taking off

//         owner.GetComponent<FollowPath>().enabled = false;
//         Debug.Log("BYE TAKEOFF!");
       
//     }

//     public override void Think()
//     {
//         //Has their take off path reached the end

//         // change to wander behaviour
       
       

      
       
//     }
// }

// class WanderState: State
// {
   
//     public override void Enter()
//     {
//         owner.GetComponent<NoiseWander>().enabled = true;
//         owner.GetComponent<Constrain>().enabled = true;
//         Debug.Log("Hi I'm in wander!");
     
//     }
//     public override void Exit()
//     {   
//         //leave take off state
       
//     }

//     public override void Think()
//     {
//         //Has their take off path reached the end

//         // change to wander behaviour
       
//     }
// }


// p
// }

// public class Dead:State
// {
//     float angles;
//     float radiuss;
//     float angleSpeed;
//     float rSpeed;

//     public override void Enter()
//     {

//         angles = 0;
//         radiuss = 10;
//         angleSpeed = 150;
//         rSpeed = 0.7f;
//         angles = Mathf.Max(0, Mathf.PI);
//         radiuss = Mathf.Max(0, radiuss);

//         SteeringBehaviour[] sbs = owner.GetComponent<Boid>().GetComponents<SteeringBehaviour>();
//         foreach(SteeringBehaviour sb in sbs)
//         {
//             sb.enabled = false;
//         }
//         owner.GetComponent<StateMachine>().enabled = false;   
//         // owner.GetComponent<Rigidbody>().useGravity = true;     
//     } 


//     // When a plane is shot down, nose dive 

//     public override void Think()
//     {
//         angles += Time.deltaTime * angleSpeed;
//         radiuss -= Time.deltaTime * rSpeed;

//         //  if (radiuss <= 0)
//         // {
//         //     float x = 0;
//         //     float y = 0;
//         //     float z = 0;

//         //     owner.transform.position = new Vector3(x, y, z);
//         // }

//         // else
//         // {
//             float x = radiuss * Mathf.Cos(Mathf.Deg2Rad * angles);
//             float z = radiuss * Mathf.Sin(Mathf.Deg2Rad * angles);
//             float y = 0;

//             owner.transform.position = new Vector3(x, y, z);
//         // }

//     }
      
// }


public class HeroController : MonoBehaviour
{ 
    // Start is called before the first frame update


    public GameObject cam1;
    public GameObject cam2;

    public Carrier myCarrier;
    public GameObject enemy;

    public Vector3 jetScale = new Vector3(10.0f,10.0f,10.0f);
    // public Vector3 jetPos = new Vector3(0.0f,5.0f,-116f);


    

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.name == "change_path")
        {
            cam1.GetComponent<Camera>().enabled = false;
            cam2.GetComponent<Camera>().enabled = true;
            
            GetComponent<StateMachine>().ChangeState(new WanderState());

        
            // GetComponent<TakeOffCam>().enabled = false;
            // GetComponent<FollowHero>().enabled = true;
        }
    
        else if (col.tag == "Missile")
        {
            Debug.Log("Damage taken!");

            enemy = col.transform.parent.gameObject;
            Debug.Log("Enemy is " + enemy.name);

            if(GetComponent<Fighter>().health > 0)
            {            
                GetComponent<Fighter>().health --;
            }
            else
            {
                GetComponent<StateMachine>().SetGlobalState(new Dead());
            }
          
            if (GetComponent<StateMachine>().currentState.GetType() != typeof(Dead))
            {
                GetComponent<StateMachine>().ChangeState(new DefendState());   
                Debug.Log("Stand up for yourself!");
                
            }
            else
            {
                Debug.Log("Dead now!");
            }
        }
    }

    IEnumerator LandingGear()
    {
        GameObject leftBreak = transform.Find("Rafale 7").gameObject;
        GameObject leftWheel = transform.Find("Rafale 18").gameObject;

        GameObject RightBreak = transform.Find("Rafale 6").gameObject;
        GameObject RightWheel = transform.Find("Rafale 15").gameObject;
        
        while(true)
        {
            if((GetComponent<StateMachine>().currentState).ToString() == "TakeOffState")
            {
                // Debug.Log("Retracting..");


               
                //move the landing gears realtive to hero parent until they are horizontal
            }
            else if((GetComponent<StateMachine>().currentState).ToString() == "WanderState")
            {
                // Debug.Log("Done retracting!");

                Destroy(leftWheel);
                Destroy(leftBreak);
                yield break;
            }

            yield return new WaitForSeconds(1.0f);


        }
       
    }


    


    void Start()
    {

        transform.localScale = jetScale;
        // transform.position = jetPos;

        cam1 = GameObject.Find("TakeOffCam");
        cam2 = GameObject.Find("FollowHero");
 

        cam1.GetComponent<Camera>().enabled =true;
        cam2.GetComponent<Camera>().enabled = false;

        

        GetComponent<StateMachine>().ChangeState(new TakeOffState());
        GetComponent<StateMachine>().SetGlobalState(new Alive());  
        StartCoroutine(LandingGear());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
