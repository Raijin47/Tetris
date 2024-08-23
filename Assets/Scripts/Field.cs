using UnityEngine;

public class Field : MonoBehaviour
{
    #region static
    public static int Height = 9;
    public static int Width = 9;
    public static Transform[,] Grid = new Transform[Height, Width];
    #endregion

    [SerializeField] private GameObject[] _chunks;
    [SerializeField] private Transform _content;
    [SerializeField] private BlockController _blockController;

    private void OnEnable() => GlobalEvent.OnDeliveredChunk += Spawn;
    private void OnDisable() => GlobalEvent.OnDeliveredChunk -= Spawn;

    private void Spawn()
    {
        int random = Random.Range(0, _chunks.Length);

        _blockController.Chunk = Instantiate(_chunks[random], _content.position, Quaternion.identity);
        _blockController.Chunk.transform.parent = _content;
        _blockController.IsRotate = random != 0;
    }
}