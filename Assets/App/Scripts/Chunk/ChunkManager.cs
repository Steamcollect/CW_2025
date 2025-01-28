using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    [Header("Settings")] 
    [SerializeField] private GameObject[] chunks;
    [SerializeField] private int chunkCount = 1;
    [SerializeField] private float speedMove = 1f;
    
    private GameObject[] _activeChunks;
    private bool _move;
    
    private void Awake()
    {
        InitChunks();
    }

    private void InitChunks()
    {
        GameObject chunkGenerated;
        int index = 0;
        _activeChunks = new GameObject[chunkCount*chunks.Length];

        for (int i = 0; i < chunkCount; i++)
        {
            foreach (var chunk in chunks)
            {
                chunkGenerated = Instantiate(chunk);
                chunkGenerated.SetActive(false);
                _activeChunks[index] = chunkGenerated;
                index++;
            }
        }
    }
    
    private void StartChunkMove()
    {
        _move = true;
    }

    private void StopChunkMove()
    {
        _move = false;
    }

    private void Update()
    {
        if (!_move) return;
        CheckChunksExit();
    }
    
    private void FixedUpdate()
    {
        if (!_move) return;
    }

    private void MoveChunks()
    {
        
    }

    private void CheckChunksExit()
    {
        
    }
    
}