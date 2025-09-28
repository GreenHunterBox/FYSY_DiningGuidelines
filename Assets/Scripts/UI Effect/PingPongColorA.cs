using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Image))]
public class PingPongColorA : MonoBehaviour
{
    [Range(0, 1)][SerializeField] float colorAlpha = 0.5f;
    [SerializeField] AnimationCurve alphaCurve; // ��Inspector���������ߴ�0��1
    [Range(0, 2)][SerializeField] float duration = 2.0f;
    Image img;
    Color aColor;
    private void Awake()
    {
        img = GetComponent<Image>();
        aColor = img.color;
    }
    void FixedUpdate()
    {
        // ʹ��PingPong��ȡ0��1֮���ֵ
        float pingPongValue = Mathf.PingPong(Time.time, duration) / duration;

        // ͨ������ӳ�䵽0.5f-1��Χ
        aColor.a = alphaCurve.Evaluate(pingPongValue) * colorAlpha + colorAlpha;
        img.color = aColor;
    }
}
