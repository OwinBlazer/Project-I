using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNotification : MonoBehaviour {

    public float relativePointL;//-
    public float relativePointR;//+
    public GameObject leftIcon;
    public GameObject rightIcon;
    public GameObject enemy;

    public float radius;
    public Collider[] hitColliders;

    // Use this for initialization
    void Start()
    {

    }

    void OverlapSphere()
    {
        hitColliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider col in hitColliders)
        {
            if (col.gameObject.tag == "Enemy")
            {
                enemy = col.gameObject;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        OverlapSphere();

        if (enemy != null)
        {
            var relativePoint = transform.InverseTransformPoint(enemy.transform.position);

            if (relativePoint.x < relativePointL) // if the object is on your left
            {
                leftIcon.SetActive(true);
                rightIcon.SetActive(false);
            }
            else if (relativePoint.x > relativePointR)// if the object is on your right
            {
                rightIcon.SetActive(true);
                leftIcon.SetActive(false);
            }
            else // if the object is on your mid
            {
                rightIcon.SetActive(false);
                leftIcon.SetActive(false);
            }
        }
        else
        {
            rightIcon.SetActive(false);
            leftIcon.SetActive(false);
        }
    }
}
