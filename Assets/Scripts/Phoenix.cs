using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pheonix : MonoBehaviour
{
    [SerializeField] private int speed;
    public Fire fire;

    public Text cooldownText; // Tham chiếu đến UI Text

    private float fireCooldown = 1.0f; // Thời gian giữa các lần phun lửa
    private float fireTimer = 0.0f;

    void Update()
    {
        control();

        // Kiểm tra thời gian để phun lửa
        fireTimer += Time.deltaTime;
        if (Input.GetKey(KeyCode.Space) && fireTimer >= fireCooldown)
        {
            Instantiate(fire, transform.position, Quaternion.identity);
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
            cooldownText.text = "Cooldown: " + remainingCooldown.ToString("F1") + "s";
            cooldownText.text = "Đang hồi chiêu! " + remainingCooldown.ToString() + " s";
        }
        else
        {
            cooldownText.text = "Sẵn sàng";
        }
    }
}
