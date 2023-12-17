namespace VUDK.Features.More.PuzzleSystem.Saving
{
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using VUDK.Features.Main.SaveSystem;
    using VUDK.Features.More.PuzzleSystem.Saving.SaveData;

    public static class PuzzlesSaver
    {
        private static SavedPuzzleBase[] s_puzzles;
        private static int s_puzzlesSceneIndex;

        private static string PuzzlesSaveFileName => $"Puzzles_{s_puzzlesSceneIndex}";

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Init()
        {
            SceneManager.sceneLoaded += InitPuzzles;
        }

        private static void InitPuzzles(Scene scene, LoadSceneMode mode)
        {
            s_puzzlesSceneIndex = scene.buildIndex;
            s_puzzles = Object.FindObjectsOfType<SavedPuzzleBase>();
            LoadPuzzles();
        }

        public static void SavePuzzles()
        {
            PuzzlesSaveData puzzlesSaveData = new PuzzlesSaveData(s_puzzles);
            BinarySave.Save(puzzlesSaveData, PuzzlesSaveFileName);
        }

        public static void LoadPuzzles()
        {
            if (BinarySave.TryLoad(out PuzzlesSaveData puzzlesSaveData, PuzzlesSaveFileName))
            {
                for(int i = 0; i < puzzlesSaveData.PuzzleDatas.Count; i++)
                    s_puzzles[i].Init(puzzlesSaveData.PuzzleDatas[i]);
            }
        }
    }
}