using UnityEngine;

namespace Scripts {
    public class SentenceTrigger : MonoBehaviour
    {
        [SerializeField] private Sentence sentence;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                PlayerSentences.instance.SaySentence(sentence);
                Destroy(gameObject);
            }
        }
    }
}
