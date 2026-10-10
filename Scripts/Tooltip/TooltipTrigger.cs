using UnityEngine;
using UnityEngine.EventSystems;

namespace Scripts.Tooltip
{
    public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private static LTDescr delay;
        public string header;

        [Multiline()] public string content;


        public void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log("show");
            delay = LeanTween.delayedCall(0.5f, () =>
            {
                Debug.Log("show 2");
                TooltipSystem.Show(content, header);
            });
        }


        public void OnPointerExit(PointerEventData eventData)
        {
            Debug.Log("hide");
            LeanTween.cancel(delay.id);
            TooltipSystem.Hide();
        }
    }
}