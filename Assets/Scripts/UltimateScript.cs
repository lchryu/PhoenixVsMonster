////using System.Collections;
////using System.Collections.Generic;
////using UnityEngine;

////public class UltimateScript : MonoBehaviour
////{
////    private Animator anim;
////    private bool hasCollided = false;
////    Rigidbody2D rb;

////    [SerializeField] private float speed;
////    [SerializeField] private float activeTime = 1.0f; // Thời gian lửa được phun
////    [SerializeField] private float cooldownTime = 0.5f; // Thời gian nghỉ giữa các lần phun

////    public LogicScript logic;

////    private float timer;

////    void Start()
////    {
////        timer = activeTime;
////        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
////        anim = GetComponent<Animator>();
////        rb = GetComponent<Rigidbody2D>();
////    }

////    void Update()
////    {
////        if (!hasCollided)
////        {
////            if (timer > 0)
////            {
////                transform.Translate(Vector3.down * speed * Time.deltaTime);
////                timer -= Time.deltaTime;
////            }
////            else
////            {
////                timer = cooldownTime;
////                Destroy(this.gameObject);
////            }
////        }
////    }

////    void OnTriggerEnter2D(Collider2D obj)
////    {
////        if (obj.gameObject.CompareTag("quaivat") && !hasCollided)
////        {
////            hasCollided = true;
////            logic.addScore(3);
////            Destroy(obj.gameObject);
////            anim.SetTrigger("ExplosionTrigger");
////            rb.bodyType = RigidbodyType2D.Static;
////            Destroy(this.gameObject, anim.GetCurrentAnimatorStateInfo(0).length);
////        }
////    }
////}
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class UltimateScript : MonoBehaviour
//{
//    private Animator anim;
//    private bool hasCollided = false;
//    private Rigidbody2D rb;

//    [SerializeField] private float speed;
//    [SerializeField] private float activeTime = 1.0f; // Thời gian lửa được phun
//    [SerializeField] private float cooldownTime = 0.5f; // Thời gian nghỉ giữa các lần phun

//    public LogicScript logic;

//    private float timer;

//    void Start()
//    {
//        timer = activeTime;
//        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
//        anim = GetComponent<Animator>();
//        rb = GetComponent<Rigidbody2D>();
//    }

//    void Update()
//    {
//        if (!hasCollided)
//        {
//            if (timer > 0)
//            {
//                transform.Translate(Vector3.down * speed * Time.deltaTime);
//                timer -= Time.deltaTime;
//            }
//            else
//            {
//                timer = cooldownTime;
//                Destroy(this.gameObject);
//            }
//        }
//    }

//    void OnTriggerEnter2D(Collider2D obj)
//    {
//        if (obj.gameObject.CompareTag("quaivat") && !hasCollided)
//        {
//            hasCollided = true;

//            // Lấy danh sách tất cả các Devil(clone)
//            GameObject[] devils = GameObject.FindGameObjectsWithTag("quaivat");

//            // Xóa tất cả Devil(clone)
//            foreach (var devil in devils)
//            {
//                logic.PlayAddScroreSoundEffect();
//                Destroy(devil);
//            }

//            // Tăng điểm
//            int numberOfDevils = devils.Length;
//            int scoreToAdd = numberOfDevils * 3; // 3 điểm cho mỗi Devil(clone)
//            logic.addScore(scoreToAdd);

//            Destroy(obj.gameObject);
//            anim.SetTrigger("ExplosionTrigger");
//            rb.bodyType = RigidbodyType2D.Static;
//            Destroy(this.gameObject, anim.GetCurrentAnimatorStateInfo(0).length);
//        }
//    }
//}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateScript : MonoBehaviour
{
    private Animator anim;
    private bool hasCollided = false;
    private Rigidbody2D rb;

    [SerializeField] private float speed;
    [SerializeField] private float activeTime = 1.0f; // Thời gian lửa được phun
    [SerializeField] private float cooldownTime = 0.5f; // Thời gian nghỉ giữa các lần phun

    public LogicScript logic;

    private float timer;

    // Thêm biến cho Coroutine
    private Coroutine soundCoroutine;

    void Start()
    {
        timer = activeTime;
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!hasCollided)
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
    }

    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject.CompareTag("quaivat") && !hasCollided)
        {
            hasCollided = true;

            // Lấy danh sách tất cả các Devil(clone)
            GameObject[] devils = GameObject.FindGameObjectsWithTag("quaivat");

            // Tăng điểm và phát âm thanh cho mỗi Devil(clone)
            int numberOfDevils = devils.Length;
            int scoreToAdd = numberOfDevils * 3; // 3 điểm cho mỗi Devil(clone)
            logic.addScore(scoreToAdd);

            // Chạy Coroutine để phát âm thanh liên tục
            soundCoroutine = StartCoroutine(PlayAddScroreSoundEffectCoroutine(numberOfDevils));

            foreach (var devil in devils)
            {
                Destroy(devil);
            }

            Destroy(obj.gameObject);
            anim.SetTrigger("ExplosionTrigger");
            rb.bodyType = RigidbodyType2D.Static;
            Destroy(this.gameObject, anim.GetCurrentAnimatorStateInfo(0).length);
        }
    }

    // Coroutine để phát âm thanh liên tục
    IEnumerator PlayAddScroreSoundEffectCoroutine(int numberOfDevils)
    {
        for (int i = 0; i < numberOfDevils; i++)
        {
            logic.PlayAddScroreSoundEffect();
            yield return new WaitForSeconds(0.1f); // Chờ 0.1 giây giữa mỗi âm thanh
        }
    }


    // Hủy Coroutine khi đối tượng bị hủy
    private void OnDestroy()
    {
        if (soundCoroutine != null)
        {
            StopCoroutine(soundCoroutine);
        }
    }
}
