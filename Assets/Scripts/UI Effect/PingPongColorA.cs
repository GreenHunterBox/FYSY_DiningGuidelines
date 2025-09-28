using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Image))]
public class PingPongColorA : MonoBehaviour
{
    [Range(0, 1)][SerializeField] float colorAlpha = 0.5f;
    [SerializeField] AnimationCurve alphaCurve; // 在Inspector中设置曲线从0到1
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
        // 使用PingPong获取0到1之间的值
        float pingPongValue = Mathf.PingPong(Time.time, duration) / duration;

        // 通过曲线映射到0.5f-1范围
        aColor.a = alphaCurve.Evaluate(pingPongValue) * colorAlpha + colorAlpha;
        img.color = aColor;
    }
}
