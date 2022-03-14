using UnityEngine;
using UnityEngine.UI;


public class Draw : MonoBehaviour
{
    [SerializeField] private Texture2D _texture;
    [Range(2, 512)]
    [SerializeField] private int _resolution = 28;
    [SerializeField] private Slider _sliderResolution;
    [SerializeField] private Color _color;
    [Range(2, 30)]
    [SerializeField] private int _brushSize = 8;
    [SerializeField] private Slider _sliderBrush;
    
    private bool _isMobile = true;


    void OnValidate()
    {
        if (_texture == null)
        {
            _texture = new Texture2D(_resolution, _resolution);
        }
        if (_texture.width != _resolution)
        {
            _texture.Resize(_resolution, _resolution);
        }
        GetComponent<Renderer>().material.mainTexture = _texture;
        _texture.filterMode = FilterMode.Point;
        _texture.Apply();
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && _isMobile == false)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f))
            {
                int rayX = (int)(hit.textureCoord.x * _resolution);
                int rayY = (int)(hit.textureCoord.y * _resolution);
                DrawCircle(rayX, rayY);
                _texture.Apply();
            }
        }

        if (_isMobile == true)
        {
            Ray ray = Camera.main.ScreenPointToRay(DragTracking.dragPosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f))
            {
                int rayX = (int)(hit.textureCoord.x * _resolution);
                int rayY = (int)(hit.textureCoord.y * _resolution);
                DrawCircle(rayX, rayY);
                _texture.Apply();
            }
        }
    }

    void DrawCircle(int rayX, int rayY)
    {
        for (int y = 0; y < _brushSize; y++)
        {
            for (int x = 0; x < _brushSize; x++)
            {
                float x2 = Mathf.Pow(x - _brushSize/2, 2);
                float y2 = Mathf.Pow(y- _brushSize/2, 2);
                float r2 = Mathf.Pow(_brushSize/2, 2);

                if(x2+y2<r2)
                {
                    _texture.SetPixel(rayX + x - _brushSize / 2, rayY + y - _brushSize / 2, _color);
                }
            }
        }
    }

    public void ColorMenu(int ValueColor)
    {
        if( ValueColor == 0 )
        {
            _color = Color.black;
        }
        if (ValueColor == 1)
        {
            _color = Color.blue;
        }
        if (ValueColor == 2)
        {
            _color = Color.green;
        }
    }

    public void SlidreBrushSize()
    {
        _brushSize = (int)_sliderBrush.value;
    }

    public void SlidreReolution()
    {
        _resolution = (int)_sliderResolution.value;
        _texture.Resize(_resolution, _resolution);
        
    }
}