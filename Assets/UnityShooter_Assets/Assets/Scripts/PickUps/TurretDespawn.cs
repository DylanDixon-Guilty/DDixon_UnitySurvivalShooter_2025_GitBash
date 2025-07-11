using System.Collections;
using UnityEngine;

public class TurretDespawn : MonoBehaviour
{

    //To Despawn turret after 10 seconds//
    private IEnumerator DespawnTurret()
    {
        yield return new WaitForSeconds(10);
        
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Awake()
    {
        //Despawing Turret
        StartCoroutine(DespawnTurret());
    }
}
