using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour
{
    void Update()
    {
        var pos = transform.position;

        if (pos.x > 100 || pos.y > 100 || pos.z > 100)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        Destroy(gameObject);
    }
}
