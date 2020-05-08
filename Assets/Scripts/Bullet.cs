using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 2f;
    public Vector2 direction;
    public float livingTime = 3f;
    public Color initialColor = Color.white;
    public Color finalColor;

    private SpriteRenderer _rendere;
    private float _startingTime;

    private void Awake()
    {
        _rendere = this.GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _startingTime = Time.time;
        Destroy(this.gameObject,this.livingTime);
    }

    // Update is called once per frame
    void Update()
    {
        //Move object
        Vector2 movement = this.direction.normalized * this.speed * Time.deltaTime;
        //this.transform.position = new Vector2(this.transform.position.x + movement.x, this.transform.position.y + movement.y);
        this.transform.Translate(movement);

        //Change bullet's color over time
        float _timeSinceStarted = Time.time - _startingTime;
        float _percentageCompleted = _timeSinceStarted / livingTime;
        _rendere.color = Color.Lerp(initialColor, finalColor, _percentageCompleted);
    }
}
