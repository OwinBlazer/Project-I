using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderBody : MonoBehaviour
{
    public Enemy en;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (en.dead)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Bullet _bullet = collision.gameObject.GetComponent<Bullet>();
            en.curHP -= _bullet.damage;

            StartCoroutine(en.ActivateCanvasHP());
            // bisa cek bulletID dan level yang mengenai enemy disini
        }
    }
}
