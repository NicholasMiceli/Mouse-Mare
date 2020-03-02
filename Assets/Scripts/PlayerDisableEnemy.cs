using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDisableEnemy : MonoBehaviour
{
    public static bool cease;
    public static float vision = 3f;
    Transform target;

    void Start()
    {
        target = PlayerManager.instance.player.transform;
    }

    
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        cease = PlayerMove.enemyStop;
     
        
        if (cease == true && distance <= vision)
        {
            StartCoroutine("DisableEnemy");
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, vision);
    }

    IEnumerator DisableEnemy()
    {
        GetComponent<EnemyController>().enabled = false;
        //GetComponent<EnemyRotate>().enabled = false;
        yield return new WaitForSeconds(5f);
        //GetComponent<EnemyRotate>().enabled = true;
        GetComponent<EnemyController>().enabled = true;
        
        Debug.Log("Yes!");

    }

}


