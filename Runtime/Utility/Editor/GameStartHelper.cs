#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;

namespace Boshphelm.Utility.Editor
{
    public static class GameStartHelper
    {
        [MenuItem("Boshphelm/Game/StartGame")]
        public static void StartGame()
        {
            EditorSceneManager.SaveOpenScenes();

            EditorSceneManager.OpenScene("Assets/Project/Runtime/Scenes/Initializer.unity");
            EditorApplication.EnterPlaymode();
        }

        [MenuItem("Boshphelm/Game/DeleteSaveAndStartGame")]
        public static void DeleteSaveAndStartGame()
        {
            // TODO: Open Save System Scene.
            // TODO: Find Save System.
            // TODO: Delete Save.

            StartGame();
        }
    }
}
#endif