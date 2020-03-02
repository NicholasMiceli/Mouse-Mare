using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRotate: MonoBehaviour {
    public int speedRotate = 10;
      void Update()
     {
        transform.Rotate (new Vector3 (0,70, 0) * speedRotate * Time.deltaTime);
     }
}
