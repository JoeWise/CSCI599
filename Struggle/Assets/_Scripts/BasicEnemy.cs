using UnityEngine;
using System.Collections;
 
public class BasicEnemy : MonoBehaviour
{
        GameObject player;
        Transform target;
        public float speed = 3f;
        public float attack1Range = 1f;
        public int attack1Damage = 1;
        public float timeBetweenAttacks;
        public bool attacking = false;
 
        // Use this for initialization
        void Start()
        {
           player = GameObject.FindGameObjectWithTag ("Player");
           target = player.transform;
           Rest();
        }
     
        // Update is called once per frame
        void Update()
        {
             if(attacking)
             {
                MoveToPlayer();
             }
             else
             {
                Rest();
             }
        }
 
        public void MoveToPlayer()
        {
            //rotate to look at player
            transform.LookAt(target.position);
            transform.Rotate(new Vector3 (0, -90, 0), Space.Self);
         
            //move towards player
            if (Vector3.Distance(target.position, target.position) > attack1Range) 
            {
                    transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
            }
        }
 
        public void Rest()
        {
 
        }
}
