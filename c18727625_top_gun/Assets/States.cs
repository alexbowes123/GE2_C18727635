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


class FlightAttackState : State
{
    public override void Enter()
    {
        Debug.Log("Baddie chasing");
        owner.GetComponent<Pursue>().target = owner.GetComponent<Fighter>().enemy.GetComponent<Boid>();
        owner.GetComponent<Pursue>().enabled = true;
    }

    public override void Think()
    {
        Vector3 toEnemy = owner.GetComponent<Fighter>().enemy.transform.position - owner.transform.position; 
        if (Vector3.Angle(owner.transform.forward, toEnemy) < 45 && toEnemy.magnitude < 60 && owner.GetComponent<Fighter>().ammo > 0)
        {
          

            GameObject bullet = GameObject.Instantiate(owner.GetComponent<Fighter>().bullet, owner.transform.position + owner.transform.forward * 2, owner.transform.rotation);
            bullet.transform.parent = owner.transform;
            bullet.GetComponent<Missile>().target = owner.GetComponent<Fighter>().enemy;
            // bullet.transform.Rotate(0,90,90);
            bullet.transform.Rotate(90,0,0);

            Debug.Log("ammo is" +owner.GetComponent<Fighter>().ammo);


            owner.GetComponent<Fighter>().ammo --;
        }        
        if (Vector3.Distance(
            owner.GetComponent<Fighter>().enemy.transform.position,
            owner.transform.position) < 10)
        {
            // owner.ChangeState(new FleeState());
        }

    }

    public override void Exit()
    {
        owner.GetComponent<Pursue>().enabled = false;
    }
}


class PatrolState : State
{
    public override void Enter()
    {
        owner.GetComponent<FollowPath>().enabled = true;
    }

    public override void Think()
    {
        if (Vector3.Distance(
            owner.GetComponent<Fighter>().enemy.transform.position,
            owner.transform.position) < 10)
        {
            owner.ChangeState(new DefendState());
        }
    }

    public override void Exit()
    {
        owner.GetComponent<FollowPath>().enabled = false;
    }
}

public class DefendState : State
{
    public override void Enter()
    {
        Debug.Log("In Defending");
        owner.GetComponent<Pursue>().target = owner.GetComponent<Fighter>().enemy.GetComponent<Boid>();
        owner.GetComponent<Pursue>().enabled = true;
    }

    public override void Think()
    {
        Vector3 toEnemy = owner.GetComponent<Fighter>().enemy.transform.position - owner.transform.position; 
        if (Vector3.Angle(owner.transform.forward, toEnemy) < 45 && toEnemy.magnitude < 20)
        {
            GameObject bullet = GameObject.Instantiate(owner.GetComponent<Fighter>().bullet, owner.transform.position + owner.transform.forward * 2, owner.transform.rotation);
            owner.GetComponent<Fighter>().ammo --;        
        }
        if (Vector3.Distance(
            owner.GetComponent<Fighter>().enemy.transform.position,
            owner.transform.position) > 30)
        {
            owner.ChangeState(new PatrolState());
        }
    }

    public override void Exit()
    {
        owner.GetComponent<Pursue>().enabled = false;
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

// public class AttackState : State
// {
//     public override void Enter()
//     {
//         owner.GetComponent<Pursue>().target = owner.GetComponent<Fighter>().enemy.GetComponent<Boid>();
//         owner.GetComponent<Pursue>().enabled = true;
//     }

//     public override void Think()
//     {
//         Vector3 toEnemy = owner.GetComponent<Fighter>().enemy.transform.position - owner.transform.position; 
//         if (Vector3.Angle(owner.transform.forward, toEnemy) < 45 && toEnemy.magnitude < 30)
//         {
//             GameObject bullet = GameObject.Instantiate(owner.GetComponent<Fighter>().bullet, owner.transform.position + owner.transform.forward * 2, owner.transform.rotation);
//             owner.GetComponent<Fighter>().ammo --;
//         }        
//         if (Vector3.Distance(
//             owner.GetComponent<Fighter>().enemy.transform.position,
//             owner.transform.position) < 10)
//         {
//             owner.ChangeState(new FleeState());
//         }

//     }

//     public override void Exit()
//     {
//         owner.GetComponent<Pursue>().enabled = false;
//     }
// }

// public class FleeState : State
// {
//     public override void Enter()
//     {
//         owner.GetComponent<Flee>().targetGameObject = owner.GetComponent<Fighter>().enemy;
//         owner.GetComponent<Flee>().enabled = true;
//     }

//     public override void Think()
//     {
//         if (Vector3.Distance(
//             owner.GetComponent<Fighter>().enemy.transform.position,
//             owner.transform.position) > 30)
//         {
//             owner.ChangeState(new AttackState());
//         }
//     }
//     public override void Exit()
//     {
//         owner.GetComponent<Flee>().enabled = false;
//     }
// }

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
            owner.ChangeState(new FindHealth());
            return;
        }
        
        if (owner.GetComponent<Fighter>().ammo <= 0)
        {
            owner.ChangeState(new FindAmmo());
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

public class FindAmmo:State
{
    Transform ammo;
    public override void Enter()
    {
        GameObject[] ammos = GameObject.FindGameObjectsWithTag("Ammo");
        // Find the closest ammo;
        Transform closest = ammos[0].transform;
        foreach(GameObject go in ammos)
        {
            if (Vector3.Distance(go.transform.position, owner.transform.position) <
                Vector3.Distance(closest.position, owner.transform.position))
                {
                    closest = go.transform;
                }
        }
        ammo = closest;
        owner.GetComponent<Seek>().targetGameObject = ammo.gameObject;
        owner.GetComponent<Seek>().enabled = true;
    }

    public override void Think()
    {
        // If the other guy already took tghe ammo
        if (ammo == null)
        {
            owner.ChangeState(new FindAmmo());
            return;
        }
        if (Vector3.Distance(owner.transform.position, ammo.position) < 1)
        {
            owner.GetComponent<Fighter>().ammo += 10;
            owner.RevertToPreviousState();
            GameObject.Destroy (ammo.gameObject);
        }
    }

    public override void Exit()
    {
        owner.GetComponent<Seek>().enabled = false;
    }
}

public class FindHealth:State
{
    Transform health;
    public override void Enter()
    {
        GameObject[] healths = GameObject.FindGameObjectsWithTag("Health");
        // Find the closest ammo;
        Transform closest = healths[0].transform;
        foreach(GameObject go in healths)
        {
            if (Vector3.Distance(go.transform.position, owner.transform.position) <
                Vector3.Distance(closest.position, owner.transform.position))
                {
                    closest = go.transform;
                }
        }
        health = closest;
        owner.GetComponent<Seek>().targetGameObject = health.gameObject;
        owner.GetComponent<Seek>().enabled = true;
    }

    public override void Think()
    {
        // If the other guy already took the health
        if (health == null)
        {
            owner.ChangeState(new FindHealth());
            return;
        }
        if (Vector3.Distance(owner.transform.position, health.transform.position) < 2)
        {
            owner.GetComponent<Fighter>().health += 10;
            owner.RevertToPreviousState();
            GameObject.Destroy (health.gameObject);
        }
    }

    public override void Exit()
    {
        owner.GetComponent<Seek>().enabled = false;
    }
}