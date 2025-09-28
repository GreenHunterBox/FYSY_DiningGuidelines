using UnityEngine;
using System.Diagnostics;

public class ForceQuit : MonoBehaviour
{
    private float escHoldTime = 0f;
    [Header("����ESC�رճ����룩")]
    [Range(10, 20)][SerializeField] float requiredHoldTime = 10f; // ��Ҫ��ס10��
    private bool isQuitting = false;
    private Stopwatch doubleClickTimer = new Stopwatch();
    private const double doubleClickThreshold = 0.5; // 0.5������Ϊ˫��

    void Start()
    {
        // ��ʼ����ʱ��
        doubleClickTimer.Start();
    }

    void Update()
    {
        // ���ESC������
        if (Input.GetKey(KeyCode.Escape))
        {
            escHoldTime += Time.deltaTime;

            // ����Ƿ��ڶ�ʱ�����ٴΰ���ESC��˫����
            if (Input.GetKeyDown(KeyCode.Escape) && doubleClickTimer.Elapsed.TotalSeconds < doubleClickThreshold)
            {
                escHoldTime = 0f; // ���¼�ʱ
            }

            // ����˫����ʱ��
            doubleClickTimer.Restart();

            // �����סʱ��ﵽҪ����δ���˳�������
            if (escHoldTime >= requiredHoldTime && !isQuitting)
            {
                isQuitting = true;
                QuitApplication();
            }
        }
        else
        {
            escHoldTime = 0f; // �ɿ�ESC��ʱ���ü�ʱ
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