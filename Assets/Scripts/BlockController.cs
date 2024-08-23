using System.Collections;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    [SerializeField] private float _fallTime = .7f;

    private Timer _timer;
    private Coroutine _updateProcessCoroutine;

    public GameObject Chunk { get; set; }
    public bool IsRotate { get; set; }

    private void Start() => _timer = new Timer(_fallTime);

    private void OnEnable()
    {
        GlobalEvent.Left += MoveLeft;
        GlobalEvent.Right += MoveRight;
        GlobalEvent.Rotate += Rotate;
        GlobalEvent.StartGame += StartGame;
    }

    private void OnDisable()
    {
        GlobalEvent.Left -= MoveLeft;
        GlobalEvent.Right -= MoveRight;
        GlobalEvent.Rotate -= Rotate;
        GlobalEvent.StartGame -= StartGame;
    }

    private void StartGame()
    {
        if (_updateProcessCoroutine != null)
        {
            StopCoroutine(_updateProcessCoroutine);
            _updateProcessCoroutine = null;
        }
        _updateProcessCoroutine = StartCoroutine(UpdateProcessCoroutine());
    }

    private IEnumerator UpdateProcessCoroutine()
    {
        while(true)
        {
            _timer.Update();

            if(_timer.IsCompleted)
            {
                Chunk.transform.position += Vector3.down;
                if (!CanMove())
                {
                    Chunk.transform.position += Vector3.up;
                    AddToGrid();
                    CheckForLine();
                    Release();
                }
                _timer.RestartTimer();
            }
            yield return null;
        }
    }

    private void MoveLeft()
    {
        Chunk.transform.position += Vector3.left;
        if (!CanMove()) Chunk.transform.position += Vector3.right;
    }

    private void MoveRight()
    {
        Chunk.transform.position += Vector3.right;
        if (!CanMove()) Chunk.transform.position += Vector3.left;
    }

    private void Rotate()
    {
        if (!IsRotate) return;
        Chunk.transform.Rotate(0, 0, 90);
        if (!CanMove()) Chunk.transform.Rotate(0, 0, -90);
    }

    private void Release()
    {
        GlobalEvent.OnDeliveredChunk?.Invoke();
    }

    private void CheckForLine()
    {
        for(int i = Field.Height-1; i >= 0; i--)
        {
            if(HasLine(i))
            {
                DeleteLine(i);
                RowDown(i);
            }
        }
    }

    private bool HasLine(int i)
    {
        for(int j =0; j < Field.Width; j++)
        {
            if (Field.Grid[j, i] == null)
                return false;
        }
        return true;
    }

    private void DeleteLine(int i)
    {
        for (int j = 0; j < Field.Width; j++)
        {
            Destroy(Field.Grid[j, i].gameObject);
            Field.Grid[j, i] = null;
        }
    }

    private void RowDown(int i)
    {
        for(int y = i; y < Field.Height; y++)
        {
            for(int j = 0; j < Field.Width; j++)
            {
                if(Field.Grid[j, y] != null)
                {
                    Field.Grid[j, y - 1] = Field.Grid[j, y];
                    Field.Grid[j, y] = null;
                    Field.Grid[j, y - 1].transform.position -= Vector3.up;
                }
            }
        }
    }

    private void AddToGrid()
    {
        foreach (Transform children in Chunk.transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            Field.Grid[roundedX, roundedY] = children;
        }
    }

    private bool CanMove()
    {
        foreach (Transform children in Chunk.transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            if(roundedX < 0 || roundedX >= Field.Width || roundedY < 0 || roundedY >= Field.Height)
            {
                return false;
            }

            if (Field.Grid[roundedX, roundedY] != null)
            {
                return false;
            }
        }
        return true;
    }
}