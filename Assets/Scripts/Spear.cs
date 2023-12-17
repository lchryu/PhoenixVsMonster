using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{
    [SerializeField] private float speed = 0.1f;
    public LogicScript logic;
    private void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
        if (transform.position.y > 6)
            DestroyImmediate(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("phuonghoang"))
        {
            logic.addScore(-10);
            Destroy(this.gameObject);
            Debug.Log("destroy spear");
        }
    }
}