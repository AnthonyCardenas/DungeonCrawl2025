using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;

namespace HomeMadeInteractable.UI
{
    public class AdvancedButton : Selectable, IPointerClickHandler
    {
        [Header("Click Events")]
        public UnityEvent OnLeftClick;
        public UnityEvent OnMiddleClick;
        public UnityEvent OnRightClick;

        private Coroutine _resetCoroutine;

        // protected override void Reset()
        // {
            
        // }

        public void OnPointerClick(PointerEventData eventData)
        {
            
        }

        // private IEnumerator OnFinishedSubmit()
        // {
            
        // }

    }

}

