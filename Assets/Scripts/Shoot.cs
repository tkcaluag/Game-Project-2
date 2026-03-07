using System.Numerics;
using UnityEngine.InputSystem;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    public GameObject bulletObject;
    public Transform firePoint;
    public float bulletSpeed = 20f;
    public void ShootBullet()
    {
        if(Time.timeScale == 1) //check if game is paused
        {
            UnityEngine.Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            mousePos.z = 0f;

            UnityEngine.Vector2 direction = (mousePos - firePoint.position).normalized;

            GameObject bullet = Instantiate(bulletObject, firePoint.position, UnityEngine.Quaternion.identity);

            Rigidbody2D rb =  bullet.GetComponent<Rigidbody2D>();
            rb.linearVelocity = direction * bulletSpeed;
        } else
        
        {
            return;
        }
       
    }
}
