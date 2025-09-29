using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class UI_Controller : MonoSingleton<UI_Controller>
{
    [SerializeField] GameObject[] pages;
    [Header("——————步骤——————")]
    [SerializeField] Image[] Img_steps;
    [SerializeField] Sprite[] nor_steps;
    [SerializeField] Sprite[] sel_steps;
    public RectTransform[] posRectt;
    [SerializeField] Sprite[] tips;
    [SerializeField] Image tipImg;

    [Header("——————预制体——————")]
    [SerializeField] GameObject Player;
    [SerializeField] GameObject Npc;
    Human player;
    Human[] npc;

    int curStepIndex = -1;
    public int[] QueueStatus = new int[12];
    Coroutine displayTipCor;

    private void Start()
    {
        ShowPage(0);
        tipImg.rectTransform.localScale = Vector3.zero;
        // 初始化player
        player = Instantiate(Player, pages[0].transform).GetComponent<Human>();
        player.transform.SetSiblingIndex(pages[0].transform.childCount - 2);
        player.Init();
        player.OnArrive += OnPlayerArrived;
        PlayerWalk(1);

        // 初始化NPC 3个
        for (int i = 1; i < 3; i++) {
            npc[i]=Instantiate(Npc, pages[0].transform).GetComponent<Human>();
            npc[i].transform.SetSiblingIndex(pages[0].transform.childCount - 2);
            npc[i].OnArrive += OnNPCArrived;
        }
    }
    private void OnNPCArrived(int obj)
    {

    }

    void NPCWalk(Human npc, int tarPosIndex)
    {
        // 若前方堵塞，则等待并且延迟回调访问
        if (QueueStatus[tarPosIndex] == 1)
        {
            npc.DelayCallback(() => PlayerWalk(tarPosIndex));
            return;
        }

        switch (tarPosIndex)
        {
            case 1:
                npc.Walk(2, 2);
                break;
            case 2:
                npc.Walk(3, 4);
                break;
            case 3:
                npc.Walk(4, 4);
                break;
            case 4:
                npc.Walk(4, 1);
                break;
            case 5:
                npc.Walk(0, 0, true);
                break;
            case 6:
                npc.Walk(0, 0);
                break;
            case 7:
                npc.Walk(0, 0, true);
                break;
            case 8:
                npc.Walk(4, 0, true);
                break;
            case 9:
                npc.Walk(2, 2);
                break;
            case 10:
                npc.Walk(3, 3, true);
                break;
            case 11:
                npc.Walk(2, 2);
                break;
            default:
                break;
        }
    }

    void PlayerWalk(int tarPosIndex)
    {
        // 若前方堵塞，则等待并且延迟回调访问
        if (QueueStatus[tarPosIndex] == 1)
        {
            player.DelayCallback(()=> PlayerWalk(tarPosIndex));
            return;
        }

        switch (tarPosIndex)
        {
            case 1:
                player.Walk(6, 7);
                break;
            case 2:
                player.Walk(7, 8);
                NextStep();
                break;
            case 3:
                player.Walk(8, 4);
                NextStep();
                break;
            case 4:
                player.Walk(4, 1);
                NextStep();
                break;
            case 5:
                player.Walk(0, 0, true);
                break;
            case 6:
                player.Walk(0, 0);
                NextStep();
                break;
            case 7:
                player.Walk(2, 2);
                NextStep();
                break;
            case 8:
                player.Walk(8, 3);
                NextStep();
                break;
            case 9:
                player.Walk(5, 5);
                InitStep();
                break;
            case 10:
                player.Walk(7, 5, true);
                break;
            case 11:
                player.Walk(5, 5);
                break;
            default:
                break;
        }
    }
    private void OnPlayerArrived(int curPosIndex)
    {
        switch (curPosIndex)
        {
            case 1:
                PlayerWalk(2);
                break;
            case 2:
                ShowTip(0, -356, () => PlayerWalk(3));
                break;
            case 3:
                ShowTip(1, 267, () => PlayerWalk(4));
                break;
            case 4:
                ShowTip(2, 611, () => PlayerWalk(5));
                break;
            case 5:
                PlayerWalk(6);
                break;
            case 6:
                ShowTip(3, 176, () => PlayerWalk(7));
                break;
            case 7:
                ShowTip(4, 970, () => PlayerWalk(8));
                break;
            case 8:
                ShowTip(5, 836, () => PlayerWalk(9));
                break;
            case 9:
                PlayerWalk(10);
                break;
            case 10:
                PlayerWalk(11);
                break;
            case 11:
                PlayerWalk(1);
                break;
            default:
                break;
        }
    }

    void ShowTip(int sprIndex, int x, Action onCompleted)
    {
        tipImg.sprite = tips[sprIndex];
        tipImg.rectTransform.anchoredPosition = new Vector2(x, tipImg.rectTransform.anchoredPosition.y);

        if (displayTipCor != null) StopCoroutine(displayTipCor);
        displayTipCor = StartCoroutine(DisplayTip(onCompleted));
    }

    IEnumerator DisplayTip(Action onCompleted)
    {
        float timer = 0;
        float duration = 0.5f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            tipImg.rectTransform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, timer / duration);
            yield return null;
        }
        yield return new WaitForSeconds(1);
        timer = 0;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            tipImg.rectTransform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, timer / duration);
            yield return null;
        }
        onCompleted?.Invoke();
    }


    void NextStep()
    {
        if (curStepIndex >= 0) Img_steps[curStepIndex].sprite = nor_steps[curStepIndex];
        curStepIndex = curStepIndex + 1 >= nor_steps.Length ? 0 : curStepIndex + 1;
        Img_steps[curStepIndex].sprite = sel_steps[curStepIndex];
    }
    void InitStep()
    {
        Img_steps[curStepIndex].sprite = nor_steps[curStepIndex];
        curStepIndex = -1;
    }



    void ShowPage(int i) { for (int j = 0; j < pages.Length; j++) pages[j].SetActive(j == i); }
    T LoadData<T>(string fileName) where T : new()
    {
        string savePath = Path.Combine(Application.streamingAssetsPath, fileName);
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            T data = JsonUtility.FromJson<T>(json);
            Debug.Log($"Data loaded from {savePath}");
            return data;
        }
        else
        {
            Debug.LogWarning($"Save file not found in {savePath}. Returning default data.");
            return new T(); // 返回默认值
        }
    }
}
