using UnityEngine;
public class MouseInactivityDetector : MonoBehaviour
{
    [Header("挂机时间阈值（分钟）")]
    [Range(0, 60)] public float InactivityThreshold = 10;
    // 存储上次鼠标位置
    private Vector3 lastMousePosition;
    // 计时器
    private float inactivityTimer;
    void Start()
    {
#if !UNITY_EDITOR
        Cursor.visible = false;
#endif
        // 初始化鼠标位置和计时器
        lastMousePosition = Input.mousePosition;
        inactivityTimer = 0f;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(1)) Cursor.visible = !Cursor.visible;
        // 检查鼠标位置是否变化
        if (Input.mousePosition != lastMousePosition)
        {
            // 鼠标移动了，重置计时器
            inactivityTimer = 0f;
            lastMousePosition = Input.mousePosition;
        }
        else
        {
            // 鼠标没有移动，增加计时器
            inactivityTimer += Time.deltaTime;
            // 检查是否达到阈值
            if (inactivityTimer >= InactivityThreshold * 60)
            {
                // 触发事件
                // UI_Controller.Instance.OnGoHome();
                // 重置计时器
                inactivityTimer = 0f;
            }
        }
    }
}