using UnityEngine;
public class MouseInactivityDetector : MonoBehaviour
{
    [Header("�һ�ʱ����ֵ�����ӣ�")]
    [Range(0, 60)] public float InactivityThreshold = 10;
    // �洢�ϴ����λ��
    private Vector3 lastMousePosition;
    // ��ʱ��
    private float inactivityTimer;
    void Start()
    {
#if !UNITY_EDITOR
        Cursor.visible = false;
#endif
        // ��ʼ�����λ�úͼ�ʱ��
        lastMousePosition = Input.mousePosition;
        inactivityTimer = 0f;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(1)) Cursor.visible = !Cursor.visible;
        // ������λ���Ƿ�仯
        if (Input.mousePosition != lastMousePosition)
        {
            // ����ƶ��ˣ����ü�ʱ��
            inactivityTimer = 0f;
            lastMousePosition = Input.mousePosition;
        }
        else
        {
            // ���û���ƶ������Ӽ�ʱ��
            inactivityTimer += Time.deltaTime;
            // ����Ƿ�ﵽ��ֵ
            if (inactivityTimer >= InactivityThreshold * 60)
            {
                // �����¼�
                // UI_Controller.Instance.OnGoHome();
                // ���ü�ʱ��
                inactivityTimer = 0f;
            }
        }
    }
}