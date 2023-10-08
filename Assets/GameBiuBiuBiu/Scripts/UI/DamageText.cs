using UnityEngine;
using TMPro;
public class DamageText : MonoBehaviour
{
    //position:生成位置
    public static void Create(Vector3 position, int damageAmount, bool isCriticalHit)
    {

    }
    private TextMeshPro _textMeshPro;
    private Color _textColor;

    [Header("上飘")]
    public Vector3 moveUpVector = new Vector3(0.5f, 1, 0);
    public float moveUpSpeed = 2.0f;

    [Header("下落")]
    public Vector3 moveDownVector = new Vector3(-0.7f, 1, 0);

    [Header("计时器")]
    public float disappearSpeed = 3.0f;
    private const float DisappearTimeMax = 0.2f;
    private float _disappearTimer;

    [Header("颜色")]
    private Color normalColor = Color.white;
    private Color criticalHitColor = Color.red;
    private Color addHealthColor = Color.green;

    //damageAmount：数值 isCriticalHit:是否暴击
    public void SetUp(int damageAmount, bool isCriticalHit)
    {
        // 设置文本内容
        _textMeshPro.SetText(damageAmount.ToString());
        if (isCriticalHit)
        {
            // 如果是暴击，设置字体大小和颜色
            _textMeshPro.fontSize = 4;
            _textColor = criticalHitColor;
        }
        else
        {
            // 如果不是暴击，设置字体大小和颜色
            _textMeshPro.fontSize = 3;
            _textColor = normalColor;
        }
        _textMeshPro.color = _textColor;
        _disappearTimer = DisappearTimeMax;
    }

    public void SetAddHealth(int damageAmount)
    {
        // 设置文本内容
        _textMeshPro.SetText("+"+damageAmount.ToString());
        _textMeshPro.fontSize = 4;
        _textColor = addHealthColor;
        _textMeshPro.color = _textColor;
        _disappearTimer = 0.4f;
    }

    public void SetGoodsText(string damageAmount)
    {
        // 设置文本内容
        _textMeshPro.SetText(damageAmount.ToString());
        _textMeshPro.fontSize = 4;
        _textColor = criticalHitColor;
        _textMeshPro.color = _textColor;
        _disappearTimer = 0.4f;
    }
    
    private void Awake()
    {
        // 获取 TextMeshPro 组件
        _textMeshPro = GetComponent<TextMeshPro>();
    }

    private void Update()
    {

        if (_disappearTimer > DisappearTimeMax * 0.5f)
        {
            // 上飘
            transform.position += moveUpVector * Time.deltaTime;
            moveUpVector += moveUpVector * (Time.deltaTime * moveUpSpeed);
            transform.localScale += Vector3.one * Time.deltaTime * 1;
        }
        else
        {
            // 下落
            transform.position -= moveDownVector * Time.deltaTime;
            transform.localScale -= Vector3.one * Time.deltaTime * 1;
        }

        _disappearTimer -= Time.deltaTime;
        if (_disappearTimer < 0)
        {
            // 渐隐
            _textColor.a -= disappearSpeed * Time.deltaTime;
            _textMeshPro.color = _textColor;
            if (_textColor.a < 0)
            {
                // 销毁文本
                Destroy(gameObject);
            }
        }
    }
}
