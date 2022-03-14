using UnityEngine;

public class Draw : MonoBehaviour
{
    [SerializeField] private Texture2D _texture;
    [Range(2, 512)]
    [SerializeField] private int _resolution = 28;
    [SerializeField] private Color _color;
    [Range(2, 30)]
    [SerializeField] private int _brushSize = 8;

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
        if (Input.GetMouseButton(0))
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
}