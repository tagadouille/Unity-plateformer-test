using UnityEngine;

// L'enum doit être défini AVANT de l'utiliser
public enum GameState
{
    MainMenu,
    Playing,
    Paused,
    GameOver
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [SerializeField] private PlayerController playerPrefab;
    [SerializeField] private Transform spawnPointTransform;
    
    public PlayerController Player { get; private set; }
    public GameState CurrentState { get; private set; } // Maintenant GameState est connu
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeGame();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void InitializeGame()
    {
        CurrentState = GameState.MainMenu;
        SpawnPlayer();
    }
    
    private void SpawnPlayer()
    {
        Vector3 spawnPosition;
        
        if (spawnPointTransform != null)
        {
            spawnPosition = spawnPointTransform.position;
            Debug.Log($"Spawn point assigné utilisé : {spawnPosition}");
        }
        else
        {
            GameObject spawnPointObject = GameObject.FindGameObjectWithTag("SpawnPoint");
            if (spawnPointObject != null)
            {
                spawnPosition = spawnPointObject.transform.position;
                Debug.Log($"Spawn point trouvé avec le tag : {spawnPosition}");
            }
            else
            {
                spawnPosition = new Vector3(0, 1, 0);
                Debug.LogWarning("Aucun point de spawn trouvé ! Utilisation de (0,1,0)");
            }
        }
        
        if (playerPrefab != null)
        {
            Player = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
            Debug.Log($"Joueur instancié à la position : {spawnPosition}");
        }
        else
        {
            Debug.LogError("PlayerPrefab n'est pas assigné dans l'inspecteur !");
        }
    }
}