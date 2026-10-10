using UnityEngine;
using UnityEngine.Serialization;

namespace Scripts
{
    [CreateAssetMenu(menuName = "CubeLand Studio/Sentences/New Sentence", fileName = "Sentence")]
    public class Sentence : ScriptableObject
    {
        [FormerlySerializedAs("sentenceText")] public string content;
        public AudioClip sentenceClip;

    }
}