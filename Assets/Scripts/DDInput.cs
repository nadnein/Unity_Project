using UnityEngine;

public class DDInput : MonoBehaviour
    // Contains code referring to the drag & drop inputs 
{
    // added in the Unity Inspector 
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _pickClip;

    private bool _dragging, _matched; 

    private Vector2 _offset, _originalPosition;

    private DDTarget _target;


    void Start()
    {
        _originalPosition = transform.position;
    }

    void Update()
    {
        if (_matched) return;
        if (!_dragging) return;

        var mousePosition = GetMousePos();

        // move object you want to move smoothly 
        transform.position = mousePosition - _offset;
    }


    void OnMouseDown()
    {
        _dragging = true;
        _audioSource.PlayOneShot(_pickClip);
        _offset = GetMousePos() - (Vector2)transform.position;
    }

    void OnMouseUp()
    {
        if (_target != null)
        {
            // if input and target have a low distance it's a match 
            if (Vector2.Distance(transform.position, this._target.transform.position) < 2)
            {
                _matched = true;
            }
            else
            {
                transform.position = _originalPosition;
                _dragging = false;
            }
        }
        else
        {
            transform.position = _originalPosition;
            _dragging = false;
        }
    }

    public bool IsMatched()
    {
        return _matched;
    }

    public void SetMatched(bool status)
    {
        _matched = status;
    }

    // Initialise the "correct" target to the input (that meand correct sound/picture to animal at the bottom)
    public void Init(DDTarget target)
    {
        _target = target;
    }
   

    Vector2 GetMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
