using System.IO;
using UnityEngine;

public class SaveGameManager : MonoBehaviour
{
    public static SaveGameManager Instance { get; private set; }

    private const string SAVE_FILE_NAME = "/Save.json";

    private SerializedSaveGame serializedSaveGame;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    [ContextMenu("Save!")]
    public void SaveGame()
    {
        PlayerCharacterController player = GameManager.Instance.playerCharacterController;
        Transform playerTransform = player.transform;

        serializedSaveGame = new SerializedSaveGame
        {
            playerPositionX = playerTransform.position.x,
            playerPositionY = playerTransform.position.y,
            playerPositionZ = playerTransform.position.z,
            playerRotationX = playerTransform.eulerAngles.x,
            playerRotationY = playerTransform.eulerAngles.y,
            playerRotationZ = playerTransform.eulerAngles.z,
            playerHPNew = player.Hp,
            currentWaypointIndex = player.CurrentWaypointIndex
        };

        SaveToJson();
    }

    [ContextMenu("Load!")]
    public void LoadGame()
    {
        LoadFromJson();
        if (serializedSaveGame == null) return;

        PlayerCharacterController player = GameManager.Instance.playerCharacterController;
        player.transform.position = new Vector3(
            serializedSaveGame.playerPositionX,
            serializedSaveGame.playerPositionY,
            serializedSaveGame.playerPositionZ);
        player.transform.eulerAngles = new Vector3(
            serializedSaveGame.playerRotationX,
            serializedSaveGame.playerRotationY,
            serializedSaveGame.playerRotationZ);
        player.Hp = serializedSaveGame.playerHPNew;
        player.CurrentWaypointIndex = serializedSaveGame.currentWaypointIndex;

        UIManager.Instance.RefreshHPText(player.Hp);
        player.SetDestination(player.CurrentWaypointIndex);
    }

    private void SaveToJson()
    {
        string json = JsonUtility.ToJson(serializedSaveGame, true);
        File.WriteAllText(Application.persistentDataPath + SAVE_FILE_NAME, json);
    }

    private void LoadFromJson()
    {
        string path = Application.persistentDataPath + SAVE_FILE_NAME;
        if (!File.Exists(path))
        {
            Debug.LogWarning("No save file found at: " + path);
            return;
        }
        serializedSaveGame = JsonUtility.FromJson<SerializedSaveGame>(File.ReadAllText(path));
    }
}