using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Puzzle
{
    public class PuzzleManager : MonoBehaviour
    {
        [SerializeField] private Transform grid;
        [SerializeField] private Transform piecePrefab;

        [SerializeField] private List<Transform> puzzles;
        [SerializeField] private Sprite[] sprites;

        public int width;
        public int height;
        
        
        
        private void Start()
        {
            CreatePuzzles();
        }

        public void Setup(int index)
        {
            puzzles[index].GetComponent<Puzzle>().SetupPuzzle(sprites[index]);
        }

        private void CreatePuzzles()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Transform puzzle = Instantiate(piecePrefab, grid);
                    puzzle.gameObject.SetActive(true);
                    puzzles.Add(puzzle);
                    Setup(i);
                }
            }
        }
    }
}