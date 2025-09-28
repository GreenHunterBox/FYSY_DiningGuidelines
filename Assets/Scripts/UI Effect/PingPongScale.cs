using UnityEngine;
public class PingPongScale : MonoBehaviour
{
    [Range(0, 5)][SerializeField] float minScale = 0.9f;  // 最小缩放值
    [Range(0, 5)][SerializeField] float maxScale = 1.1f;  // 最大缩放值
    [Range(0, 1)][SerializeField] float speed = 0.1f;       // 缩放速度
    void FixedUpdate()
    {
        transform.localScale = Vector3.one * (Mathf.PingPong(Time.fixedTime * speed, maxScale - minScale) + minScale);
    }
}