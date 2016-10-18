using UnityEngine;
using System.Collections;
 
public class BasicEnemy : MonoBehaviour
{
        GameObject player;
        Transform player_target;
        Transform center_target;
        float damping = 1f;
        public GameObject territory;
        public float speed = 3f;
        public float attack1Range = 5f;
        public int attack1Damage = 1;
        public float timeBetweenAttacks;
        public bool attacking = false;
        public bool resting = true;
 
        // Use this for initialization
        void Start()
        {
           player = GameObject.FindGameObjectWithTag("Player");
           player_target = player.transform;
           center_target = territory.GetComponent<Collider>().transform;
           Rest();
        }
     
        // Update is called once per frame
        void Update()
        {
             if(attacking)
             {
                MoveToPlayer();
             }
             else if(resting)
             {
                Rest();
             }
             else
             {
                MoveToCenter();
             }
        }
 
        public void MoveToPlayer()
        {
            MoveToTarget(player_target.position);
            // //rotate to look at player
            // Look(player_target.position);
            // // transform.Rotate(new Vector3 (0, 0, 0), Space.Self);
         
            // //move towards player
            // Debug.Log(Vector3.Distance(targetPostition, this.transform.position));

            // if (Vector3.Distance(targetPostition, this.transform.position) > attack1Range) 
            // {
            //     // Debug.Log("Moving towards player");
            //     transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
            //     resting = false;
            // }
        }

        public void MoveToCenter()
        {
            MoveToTarget(center_target.position);

            // //rotate to look at center
            // Look(center.position);
            // // transform.Rotate(new Vector3 (0, 0, 0), Space.Self);
         
            // //move towards center
            // Debug.Log(Vector3.Distance(targetPostition, this.transform.position));
            // if (Vector3.Distance(targetPostition, this.transform.position) > 1) 
            // {
            //     // Debug.Log("Moving towards center");
            //     transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
            //     resting = false;
            // }
            // else
            // {
            //     resting = true;
            // }
        }

        public void MoveToTarget(Vector3 target)
        {
            //rotate to look at center
            Look(target);
            // transform.Rotate(new Vector3 (0, 0, 0), Space.Self);
         
            //move towards center
            Vector3 targetPostition = new Vector3(target.x, this.transform.position.y, target.z);

            if (Vector3.Distance(targetPostition, this.transform.position) > attack1Range) 
            {
                // Debug.Log("Moving towards center");
                transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
                resting = false;
            }
            else
            {
                resting = true;
            }
        }
 
 
        public void Look(Vector3 target)
        {
            // Vector3 targetPostition = new Vector3(target.x, this.transform.position.y, target.z);
            // this.transform.LookAt(targetPostition);

            Vector3 lookPos = target - transform.position;
            lookPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
        }

        public void Rest()
        {
            Look(player_target.position);
        }
}
