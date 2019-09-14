using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesOnCollision : MonoBehaviour
{
    public ParticleSystem particles;
    public float damage;
    List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();


    private void OnParticleCollision(GameObject other)
    {
        //Debug.Log("collision");
        ParticlePhysicsExtensions.GetCollisionEvents(particles, other, collisionEvents); //this method fills the coliisionEvents list

        for (int i = 0; i < collisionEvents.Count; i++)
        {
            Health health = collisionEvents[i].colliderComponent.gameObject.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damage);


            }
        }
    }

}
