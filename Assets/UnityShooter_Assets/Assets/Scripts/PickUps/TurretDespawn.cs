using System.Collections;
using UnityEngine;

public class TurretDespawn : MonoBehaviour
{

    /// <summary>
    /// To Despawn turret after 10 seconds
    /// </summary>
    private IEnumerator DespawnTurret()
    {
        yield return new WaitForSeconds(10);
        
        Destroy(gameObject);
    }

    
    private void Awake()
    {
        //Despawing Turret
        StartCoroutine(DespawnTurret());
    }
}
