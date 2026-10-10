using UnityEngine;
using UnityEngine.UI;

namespace Scripts {
    public class LocationElement : MonoBehaviour
    {
        public Text firstDesc;
        public Text secondDesc;

        public void AddTextToFirstDesc(string t)
        {
            firstDesc.text += "\n";
            firstDesc.text += t;
        } 
        public void AddTextToSecondDesc(string t)
        {
            secondDesc.text += "\n";
            secondDesc.text += t;
        } 
    }
}