using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed = 1f;
    public float minX;
    public float maX;
    public float waitingTime = 2f;

    private GameObject _target;
    private Animator _animator;
    private Weapon _weapon;

    private void Awake()
    {
        this._animator = this.GetComponent<Animator>();
        this._weapon =  this.GetComponentInChildren<Weapon>();
    }
    // Start is called before the first frame update
    void Start()
    {
        this.UpdateTarget();
        StartCoroutine("PatrolToTarget");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateTarget()
    {
        if (this._target == null)
        {
            this._target = new GameObject("Target");
            this._target.transform.position = new Vector2(this.minX, this.transform.position.y);
            this.transform.localScale = new Vector3(-1, 1, 1);
            return;
        }
        if (this._target.transform.position.x == this.minX)
        {
            this._target.transform.position = new Vector2(this.maX, this.transform.position.y);
            this.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (this._target.transform.position.x == this.maX)
        {
            this._target.transform.position = new Vector2(this.minX, this.transform.position.y);
            this.transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private IEnumerator PatrolToTarget()
    {
        //Coroutine  to move the enemy
        while (Vector2.Distance(this.transform.position, this._target.transform.position) > 0.05)
        {
            this._animator.SetBool("Idle", false);
            Vector2 direction = this._target.transform.position - this.transform.position;
            float xDirection = direction.x;
            this.transform.Translate(direction.normalized * this.speed * Time.deltaTime);
            yield return null;
        }
        this.transform.position = new Vector2(this._target.transform.position.x, this.transform.position.y);
        this.UpdateTarget();
        this._animator.SetBool("Idle", true);
        this._animator.SetTrigger("Shoot");
        yield return new WaitForSeconds(waitingTime);
        StartCoroutine("PatrolToTarget");
    }
    private void CanShoot()
    {
        if (this._weapon != null)
        {
            this._weapon.Shoot();
        }
    }
}
