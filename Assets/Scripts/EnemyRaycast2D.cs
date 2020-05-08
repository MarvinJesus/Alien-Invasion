using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRaycast2D : MonoBehaviour
{
    private Animator _animator;
    private Weapon _weapon;

    private void Awake()
    {
        this._animator = GetComponent<Animator>();
        this._weapon = GetComponentInChildren<Weapon>();
    }
    // Start is called before the first frame update
    void Start()
    {
        this._animator.SetBool("Idle",true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            this._animator.SetTrigger("Shoot");
        }
    }
    private void CanShoot()
    {
        if (this._weapon != null)
        {
            StartCoroutine(this._weapon.ShootWithRaycast());
        }
    }

}
