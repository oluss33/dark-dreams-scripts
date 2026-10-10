using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Scenes
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] int loadDistance;
        private Player.Player player;
        [SerializeField] string sceneName;
        bool isLoaded;

        private void Start()
        {
            player = Player.Player.main;
        }

        private void Update()
        {
            CheckDistance();
        }

        void CheckDistance()
        {
            if (Vector2.Distance(player.transform.position, transform.position) < loadDistance)
            {
                LoadScene();
            }
            else UnloadScene();
        }
        void LoadScene()
        {
            if (!isLoaded)
            {
                SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
                isLoaded = true;
            }
        }

        void UnloadScene()
        {
            if (isLoaded)
            {
                SceneManager.UnloadSceneAsync(sceneName);
                isLoaded = false;
            }

        }
    }
}