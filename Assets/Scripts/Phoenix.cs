﻿using System;
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
        Control();

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

    private void Control()
    {
        // Di chuyển theo các phím được nhấn và giới hạn toạ độ
        float horizontalInput = 0f;
        float verticalInput = 0f;

        if (Input.GetKey(KeyCode.A))
        {
            horizontalInput = -1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            horizontalInput = 1f;
        }

        if (Input.GetKey(KeyCode.S))
        {
            verticalInput = -1f;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            verticalInput = 1f;
        }

        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0) * speed * Time.deltaTime;
        transform.Translate(movement);

        // Giới hạn toạ độ của chim
        float clampedX = Mathf.Clamp(transform.position.x, -8.9f, 8.9f);
        float clampedY = Mathf.Clamp(transform.position.y, -5f, 5f);

        // Áp dụng giới hạn vào vị trí của chim
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }

    private void DisplayCooldown()
    {
        if (fireTimer < fireCooldown)
        {
            float remainingCooldown = fireCooldown - fireTimer;
            //Debug.Log($"remain cooldown: {remainingCooldown.ToString("F2")}");
            cooldownText.text = "Cooldown: " + (int)(remainingCooldown * 1000);
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
        SceneManager.LoadScene(2);
    }
}
