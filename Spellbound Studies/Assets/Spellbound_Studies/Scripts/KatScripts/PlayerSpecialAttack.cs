using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpecialAttack : MonoBehaviour
{
    public GameObject PlayerBullet;
    public float BulletSpeed = 30f;
    public EnergyPanel energyPanel;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && energyPanel.charges == 5)
        {
            energyPanel.charges = 0;
            Shoot();
            AudioMgr.Instance.PlaySFX("Special Attack");

        }
    }

    void Shoot()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 shootDirection = (mousePosition - transform.position).normalized;

        GameObject bullet = Instantiate(PlayerBullet, transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(shootDirection.x, shootDirection.y) * BulletSpeed;
        Destroy(bullet, 2f);
    }
}
