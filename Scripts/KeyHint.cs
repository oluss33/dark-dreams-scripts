using UnityEngine;
using UnityEngine.UI;

namespace Scripts
{
    public class KeyHint : MonoBehaviour
    {
        public Text action;
        public Text keybind;
        public Image keyIcon;
        
        public void SetKeyboardHint(string actionText, string keyText)
        {
            action.text = actionText;
            keybind.text = keyText;
            keyIcon.gameObject.SetActive(false);
        }
    }
}