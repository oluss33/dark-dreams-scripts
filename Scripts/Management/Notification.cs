using UnityEngine;

namespace Management
{
    public class Notification : MonoBehaviour
    {
        public void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}