using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    public int health;
    public bool hasJumpPowerUp;
    public bool hasDashPowerUp;
    public bool hasShoutPowerUp;
    public Vector2 respawnPoint;
}

