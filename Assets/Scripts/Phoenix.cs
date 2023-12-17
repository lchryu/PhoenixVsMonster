using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pheonix : MonoBehaviour
{
    [SerializeField] private int speed;
    [SerializeField] private Fire fire;
    public UltimateScript ultimate;

    public Text cooldownText; // Tham chiếu đến UI Text

    private float fireCooldown = 1.0f; // Thời gian giữa các lần phun lửa
    private float fireTimer = 0.0f;

    private Animator anim;
    [SerializeField] private AudioSource deathSoundEffect;


    private void Start()
    {
        anim = GetComponent<Animator>();

    }

    private void Update()
    {
        control();

        // Kiểm tra thời gian để phun lửa
        fireTimer += Time.deltaTime;
        if (Input.GetKey(KeyCode.Space) && fireTimer >= fireCooldown)
        {
            Instantiate(fire, transform.position, Quaternion.identity);
            fireTimer = 0.0f; // Đặt lại thời gian để phun lửa
        }
        if (Input.GetKey(KeyCode.U) && fireTimer >= fireCooldown)
        {
            Instantiate(ultimate, transform.position, Quaternion.identity);
            fireTimer = 0.0f; // Đặt lại thời gian để phun lửa
        }

        // Hiển thị thời gian hồi chiêu trên UI Text
        DisplayCooldown();
    }

    private void control()
    {
        if (Input.GetKey(KeyCode.W))
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.S))
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.A))
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.D))
            transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    private void DisplayCooldown()
    {
        if (fireTimer < fireCooldown)
        {
            float remainingCooldown = fireCooldown - fireTimer;
            //Debug.Log($"remain cooldown: {remainingCooldown.ToString("F2")}");
            cooldownText.text = "Cooldown: " + (int)(remainingCooldown * 1000) + "ms";
        }
        else
        {
            cooldownText.text = "Ready!";
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Spear"))
        {
            deathSoundEffect.Play();
            anim.SetTrigger("phoenixDeathTrigger");

            if (LogicScript.playerScore < 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        // chuyển sang màn hình game over
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
