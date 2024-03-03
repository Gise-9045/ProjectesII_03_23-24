using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneArguments : MonoBehaviour
{
    public static class SceneManager
    {
        private static string sceneArguments;

        public static void LoadScene(string sceneName, string argument)
        {
            sceneArguments = argument;
            Application.LoadLevel(sceneName);
        }

        public static string GetSceneArguments()
        {
            string argument = sceneArguments;
            sceneArguments = "";
            return argument;
        }
    }
}
