using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerData playerData;

    private void Awake()
    {
        if (playerData == null)
        {
            Debug.LogError("PlayerData scriptable object is not assigned!");
            return;
        }

        // Ensure the GameManager persists between scenes
        DontDestroyOnLoad(gameObject);

        // If the player data is not initialized, create a new instance
        if (playerData == null)
        {
            playerData = ScriptableObject.CreateInstance<PlayerData>();
        }
    }
}
