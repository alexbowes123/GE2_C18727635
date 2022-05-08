using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class TakeOffState: State
{
   
    public override void Enter()
    {
        Debug.Log("Hi I'm in takeoff!");
        Debug.Log("state is "+owner.GetComponent<StateMachine>().currentState);
        owner.GetComponent<FollowPath>().enabled = true;
     
    }
    public override void Exit()
    {   
        //leave follow path behaviour after taking off

        owner.GetComponent<FollowPath>().enabled = false;
        Debug.Log("BYE TAKEOFF!");
       
    }

    public override void Think()
    {
        //Has their take off path reached the end

        // change to wander behaviour
       
       

      
       
    }
}

class WanderState: State
{
   
    public override void Enter()
    {
        owner.GetComponent<NoiseWander>().enabled = true;
        owner.GetComponent<Constrain>().enabled = true;
        Debug.Log("Hi I'm in wander!");
     
    }
    public override void Exit()
    {   
        //leave take off state
       
    }

    public override void Think()
    {
        //Has their take off path reached the end

        // change to wander behaviour
       
    }
}

public class HeroController : MonoBehaviour
{ 
    // Start is called before the first frame update


    public GameObject c1;
    public GameObject c2;


    

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.name == "change_path")
        {
            c1.GetComponent<Camera>().enabled = false;
            c2.GetComponent<Camera>().enabled = true;
            
            GetComponent<StateMachine>().ChangeState(new WanderState());

        
            // GetComponent<TakeOffCam>().enabled = false;
            // GetComponent<FollowHero>().enabled = true;
        }
    
        else if (col.tag == "Missile")
        {
            // if (GetComponent<Fighter>().health > 0)
            // {            
            //     GetComponent<Fighter>().health --;
            // }
            // Destroy(c.gameObject);
            // if (GetComponent<StateMachine>().currentState.GetType() != typeof(Dead))
            // {
            //     GetComponent<StateMachine>().ChangeState(new DefendState());    
            // }
            Debug.Log("Damage taken!");
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

        c1 = GameObject.Find("TakeOffCam");
        c2 = GameObject.Find("FollowHero");
 

        c1.GetComponent<Camera>().enabled =true;
        c2.GetComponent<Camera>().enabled = false;

        

        GetComponent<StateMachine>().ChangeState(new TakeOffState());
        StartCoroutine(LandingGear());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
