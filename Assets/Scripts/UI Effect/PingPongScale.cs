using UnityEngine;
public class PingPongScale : MonoBehaviour
{
    [Range(0, 5)][SerializeField] float minScale = 0.9f;  // ��С����ֵ
    [Range(0, 5)][SerializeField] float maxScale = 1.1f;  // �������ֵ
    [Range(0, 1)][SerializeField] float speed = 0.1f;       // �����ٶ�
    void FixedUpdate()
    {
        transform.localScale = Vector3.one * (Mathf.PingPong(Time.fixedTime * speed, maxScale - minScale) + minScale);
    }
}