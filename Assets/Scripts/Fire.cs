using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Fire : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float activeTime = 1.0f; // Thời gian lửa được phun
    [SerializeField] private float cooldownTime = 0.5f; // Thời gian nghỉ giữa các lần phun
    //[SerializeField] private AudioSource addScoreSoundEffect;

    public LogicScript logic;
    

    private float timer;

    void Start()
    {
        timer = activeTime;
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    void Update()
    {
        if (timer > 0)
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
            timer -= Time.deltaTime;
        }
        else
        {
            timer = cooldownTime;
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject.CompareTag("quaivat"))
        {
            logic.PlayAddScroreSoundEffect();
            logic.addScore(3);
            Destroy(obj.gameObject);
            Destroy(this.gameObject);
        }
    }
}
