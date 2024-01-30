using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform projectilParent, barrelPos;
    [SerializeField] float fireRate;
    float fireRateTimer;
    [SerializeField] Vector2 shootdir;
    public void Shoot()
    {
        if(fireRateTimer <0)
        {
            
            GameObject projectile = Instantiate(projectilePrefab, barrelPos.position, Quaternion.identity);
            projectile.GetComponent<Projectile>().Dir = shootdir;
            fireRateTimer = fireRate;
        }
    }

    void Update()
    {
        fireRateTimer -= Time.deltaTime;

        switch (PlayerInputControl.Instance.MoveDir)
        {
            case Vector2 vector when vector.Equals(Vector2.right):
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                shootdir = Vector2.right;
                break;
            case Vector2 vector when vector.Equals(Vector2.left):
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0,  -180,0));
                shootdir = Vector2.left;
                break;
            case Vector2 vector when vector.Equals(Vector2.up):
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
                shootdir = Vector2.up;
                break;
            case Vector2 vector when vector.Equals(Vector2.down):
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
                shootdir = Vector2.down;
                break;
            case Vector2 vector when vector.Equals(new Vector2(.707107f,.707107f)):
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 45));
                shootdir = new Vector2(.707107f, .707107f);
                break;
            case Vector2 vector when vector.Equals(new Vector2(-.707107f, .707107f)):
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, -180, 45));
                shootdir = new Vector2(-.707107f, .707107f);
                break;

        }

    }
}
