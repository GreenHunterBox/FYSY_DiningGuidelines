using UnityEngine;
using System.Diagnostics;

public class ForceQuit : MonoBehaviour
{
    private float escHoldTime = 0f;
    [Header("长按ESC关闭程序（秒）")]
    [Range(10, 20)][SerializeField] float requiredHoldTime = 10f; // 需要按住10秒
    private bool isQuitting = false;
    private Stopwatch doubleClickTimer = new Stopwatch();
    private const double doubleClickThreshold = 0.5; // 0.5秒内视为双开

    void Start()
    {
        // 初始化计时器
        doubleClickTimer.Start();
    }

    void Update()
    {
        // 检测ESC键按下
        if (Input.GetKey(KeyCode.Escape))
        {
            escHoldTime += Time.deltaTime;

            // 检测是否在短时间内再次按下ESC（双开）
            if (Input.GetKeyDown(KeyCode.Escape) && doubleClickTimer.Elapsed.TotalSeconds < doubleClickThreshold)
            {
                escHoldTime = 0f; // 重新计时
            }

            // 重置双开计时器
            doubleClickTimer.Restart();

            // 如果按住时间达到要求且未在退出过程中
            if (escHoldTime >= requiredHoldTime && !isQuitting)
            {
                isQuitting = true;
                QuitApplication();
            }
        }
        else
        {
            escHoldTime = 0f; // 松开ESC键时重置计时
        }
    }

    void QuitApplication()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
    void OnApplicationQuit()
    {
        isQuitting = true;
    }
}