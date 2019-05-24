using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellCollision : MonoBehaviour
{
    public GameObject explosionParticlesPrefab;
    public GameObject terrainExplosionPrefab;

    private void OnCollisionEnter(Collision collision)
        // Destroy the shell if it hits something (e.g. rock, ground, enemytank, etc.). 
        // If the shell hits enemy tank, the enemy tank will also be destroyed.
        {
            Destroy(gameObject);
            //Debug.Log(collision.gameObject.name);
            
            if (collision.gameObject.name == "enemytank(Clone)")
            {
                TankExplode();
                Destroy(collision.gameObject);
            }
            else
            {
                TerrainExplode();
            }
        }
      void TankExplode () 
      {
          GameObject tankExplosion = Instantiate(explosionParticlesPrefab, gameObject.transform.position, Quaternion.identity);
          //tankExplosion.GetComponent<ParticleSystem>().Play();
      }

      void TerrainExplode () 
      {
          GameObject terrainExplosion = Instantiate(terrainExplosionPrefab, gameObject.transform.position, Quaternion.identity);
          //tankExplosion.GetComponent<ParticleSystem>().Play();
      }
}



