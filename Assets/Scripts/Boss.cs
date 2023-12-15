using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float genSpeed; // tốc độ gen quái vật
    public float spearSpeed; // tốc độ ném lao của quái vật
    public Devil x; // quái vật, lấy ra từ prefab
    float minx = 0, maxx = 0; // giới đoạn màn hình sinh quái
    void Start()
    {
        minx = transform.position.x;
        maxx = -minx;
        InvokeRepeating("GenDevil", 2.0f, genSpeed);
    }
    // sinh quái vật ở vị trí ngẫu nhiên, đứng cùng trục y với boss
    void GenDevil()
    {
        var pos = transform.position;
        pos.x = (Random.value * (maxx - minx)) + minx;
        x.speed = spearSpeed;
        Instantiate(x, pos, Quaternion.identity);
    }
}