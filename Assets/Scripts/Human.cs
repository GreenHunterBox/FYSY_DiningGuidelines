using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Human : MonoBehaviour
{
    [Range(0, 5)][SerializeField] float speed = 1;
    [SerializeField] Sprite[] spr;

    public int CurPosIndex { get; set; }
    Animator animator;
    Image humanImg;
    Coroutine WalkingCor;
    Coroutine delayCallbackCor;
    bool isWalking;
    public Action<Human> OnArrive;
    public void Init()
    {
        humanImg = GetComponentInChildren<Image>();
        animator = GetComponent<Animator>();
        transform.position = UI_Controller.Instance.posRectt[0].position;
    }
    public void Walk(int walkSprId, int onArriveSprId, bool reverseWalkSpr = false)
    {
        if(isWalking)return;
        if (WalkingCor!=null)StopCoroutine(WalkingCor);
        WalkingCor = StartCoroutine(Walking(walkSprId, onArriveSprId, reverseWalkSpr));
    }
    IEnumerator Walking(int walkSprId,int onArriveSprId,bool reverseWalkSpr)
    {
        isWalking = true;
        // 当处于最后1个点位时，重置
        if (CurPosIndex + 1 >= UI_Controller.Instance.posRectt.Length)
        {
            UI_Controller.Instance.QueueStatus[CurPosIndex]--;
            CurPosIndex = 0;
        }

        animator.SetBool("walking",true);
        isWalking = true;
        if(CurPosIndex!=0) UI_Controller.Instance.QueueStatus[CurPosIndex]--;
        UI_Controller.Instance.QueueStatus[CurPosIndex+1]++;
        Vector3 curPos = UI_Controller.Instance.posRectt[CurPosIndex].position;
        Vector3 tarPos = UI_Controller.Instance.posRectt[CurPosIndex+1].position;

        humanImg.rectTransform.parent.localScale = reverseWalkSpr? new Vector3(-1,1,1):Vector3.one;
        humanImg.sprite = spr[walkSprId];

        while (transform.position!= tarPos) {
            transform.position=Vector3.MoveTowards(transform.position, tarPos, speed);
            yield return null;
        }
        humanImg.sprite = spr[onArriveSprId];
        animator.SetBool("walking", false);
        CurPosIndex++;
        isWalking = false;

        OnArrive?.Invoke(this);

    }

    public void DelayCallback(Action onWaitOver)
    {
        if (delayCallbackCor != null) StopCoroutine(delayCallbackCor);
        delayCallbackCor = StartCoroutine(DelayCallbackCor(onWaitOver));
        
    }
    IEnumerator DelayCallbackCor(Action onWaitOver)
    {
        yield return new WaitForSeconds(1f);
        onWaitOver?.Invoke();
    }
}
