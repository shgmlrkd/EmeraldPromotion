using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotClickHandler : MonoBehaviour, IPointerClickHandler
{
    private int index;
    private Action<int> onRightClick;

    // 매니저에서 슬롯을 초기화할 때 인덱스와 이벤트 받음
    public void Setup(int slotIndex, Action<int> rightClickCallback)
    {
        index = slotIndex;
        onRightClick = rightClickCallback;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // 우클릭을 여기서 처리
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            onRightClick?.Invoke(index);
        }
    }
}
