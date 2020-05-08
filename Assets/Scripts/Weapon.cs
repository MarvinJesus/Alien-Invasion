using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject shooter;
    public GameObject explosionEffect;
    public LineRenderer lineRenderer;

    private Transform _firePoint;

    private void Awake()
    {
        _firePoint = this.transform.Find("Firepoint");
    }

    // Start is called before the first frame update
    void Start() 
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
        if(bulletPrefab != null && _firePoint != null && shooter != null)
        {
            GameObject myBullet = Instantiate(this.bulletPrefab, _firePoint.position, Quaternion.identity) as GameObject;
            Bullet bulletComponent = myBullet.GetComponent<Bullet>();
            if (shooter.transform.localScale.x < 0f)
            {
                bulletComponent.direction = Vector2.left;
            }
            else 
            {
                bulletComponent.direction = Vector2.right;
            }
        }
    }
    public IEnumerator ShootWithRaycast()
    {
        if (this.explosionEffect != null && this.lineRenderer != null)
        {
            RaycastHit2D hitInfo = Physics2D.Raycast(this._firePoint.position, this._firePoint.right);
            if (hitInfo)
            {
                /*if (hitInfo.collider.tag == "Player")
                {
                    Transform player = hitInfo.transform;
                    player.GetComponent<PlayerHealth>().ApllyDamage(5);
                }*/
                Instantiate(this.explosionEffect, hitInfo.point, Quaternion.identity);
                this.lineRenderer.SetPosition(0, this._firePoint.position);
                this.lineRenderer.SetPosition(1, hitInfo.point);
            }
            else 
            {
                this.lineRenderer.SetPosition(0, this._firePoint.position);
                this.lineRenderer.SetPosition(1, hitInfo.point + Vector2.right * 100);
            }
            this.lineRenderer.enabled = true;
            yield return null;
            this.lineRenderer.enabled = false;
        }
    }
}
