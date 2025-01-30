using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    [Header("Settings")] 
    [SerializeField] private GameObject[] chunks;
    [SerializeField] private int chunkCount = 1;
    [SerializeField] private float speedMove = 1f;
    [Space(10)]
    [SerializeField] private bool _move;

    [Header("References")]
    [SerializeField] RSO_CurrentLocation rsoCurrentLoc;

    [Header("Input")]
    [SerializeField] private RSE_MoveChunk rseMoveChunk;
    [SerializeField] private RSE_StopChunk rseStopChunk;
    
    private Chunk[] _activeChunks;
    private int indexChunks;
    
    
    private void Awake()
    {
        InitChunks();
    }

    private void OnEnable()
    {
        rseMoveChunk.action += StartChunkMove;
        rseStopChunk.action += StopChunkMove;
    }

    private void OnDisable()
    {
        rseMoveChunk.action -= StartChunkMove;
        rseStopChunk.action -= StopChunkMove;
    }

    private void InitChunks()
    {
        GameObject chunkGenerated;
        GameObject chunkParent = new GameObject("Chunks");
        chunkParent.transform.position = Vector3.zero;
        int index = 0;
        _activeChunks = new Chunk[chunkCount*chunks.Length];

        for (int i = 0; i < chunkCount; i++)
        {
            foreach (var chunk in chunks)
            {
                chunkGenerated = Instantiate(chunk, chunkParent.transform);
                _activeChunks[index] = chunkGenerated.GetComponent<Chunk>();
                if (index > 0)
                {
                    _activeChunks[index].transform.position =
                        _activeChunks[(index - 1) % _activeChunks.Length].endChunk.position;
                }
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
        MoveChunks();
    }

    private void MoveChunks()
    {
        for (int i = 0; i < _activeChunks.Length; i++)
        {
            _activeChunks[(indexChunks + i) % _activeChunks.Length].transform.position += Vector3.back * (speedMove * Time.fixedDeltaTime);
        }
    }

    private void CheckChunksExit()
    {
        if (_activeChunks[indexChunks].endChunk.transform.position.z <= -0.05f)
        {
            _activeChunks[indexChunks].transform.position = _activeChunks[indexChunks == 0 ? _activeChunks.Length -1 : 
                (indexChunks -1)  % _activeChunks.Length].endChunk.transform.position;
            indexChunks = (indexChunks+1) % _activeChunks.Length;
            _activeChunks[indexChunks].gameObject.SetActive(true);
            rsoCurrentLoc.Value = _activeChunks[indexChunks].locationType;
        }
    }
    
}