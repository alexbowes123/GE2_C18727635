using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class TakeOffState: State
{
   
    public override void Enter()
    {
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


public class Alive:State
{
    public override void Think()
    {

        if (owner.GetComponent<Fighter>().health <= 0)
        {
            Dead dead = new Dead();
            owner.ChangeState(dead);
            owner.SetGlobalState(dead);
            return;
        }

        if (owner.GetComponent<Fighter>().health <= 2)
        {
            // owner.ChangeState(new FindHealth());
            Debug.Log("Need health");
            return;
        }
        
        if (owner.GetComponent<Fighter>().ammo <= 0)
        {
            // owner.ChangeState(new FindAmmo());
            Debug.Log("Need Ammo");
            return;
        }
    }
}

public class Dead:State
{
    public override void Enter()
    {
        SteeringBehaviour[] sbs = owner.GetComponent<Boid>().GetComponents<SteeringBehaviour>();
        foreach(SteeringBehaviour sb in sbs)
        {
            sb.enabled = false;
        }
        owner.GetComponent<StateMachine>().enabled = false;        
    }         
}


public class HeroController : MonoBehaviour
{ 
    // Start is called before the first frame update


    public GameObject cam1;
    public GameObject cam2;


    

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

             if (GetComponent<Fighter>().health > 0)
            {            
                GetComponent<Fighter>().health --;
            }
          
            if (GetComponent<StateMachine>().currentState.GetType() != typeof(Dead))
            {
                // GetComponent<StateMachine>().ChangeState(new DefendState());   
                Debug.Log("Stand up for yourself!");
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
