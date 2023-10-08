using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// 可变摇杆类，继承自摇杆类
public class VariableJoystickMove : Joystick
{
    // 移动阈值，当摇杆移动距离大于该值时才会触发移动事件
    public float MoveThreshold { get { return moveThreshold; } set { moveThreshold = Mathf.Abs(value); } }

    // 移动阈值
    [SerializeField] private float moveThreshold = 1;
    // 摇杆类型
    [SerializeField] private JoystickType joystickType = JoystickType.Fixed;

    // 固定摇杆位置
    private Vector2 fixedPosition = Vector2.zero;

    // 设置摇杆类型
    public void SetMode(JoystickType joystickType)
    {
        this.joystickType = joystickType;
        if(joystickType == JoystickType.Fixed)
        {
            background.anchoredPosition = fixedPosition;
            background.gameObject.SetActive(true);
        }
        else
            background.gameObject.SetActive(false);
    }

    // 重写 Start 方法
    protected override void Start()
    {
        base.Start();
        fixedPosition = background.anchoredPosition;
        SetMode(joystickType);
    }

    // 重写 OnPointerDown 方法
    public override void OnPointerDown(PointerEventData eventData)
    {
        if(joystickType != JoystickType.Fixed)
        {
            background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
            background.gameObject.SetActive(true);
        }
        base.OnPointerDown(eventData);
    }

    // 重写 OnPointerUp 方法
    public override void OnPointerUp(PointerEventData eventData)
    {
        if(joystickType != JoystickType.Fixed)
            background.gameObject.SetActive(false);

        base.OnPointerUp(eventData);
    }

    // 重写 HandleInput 方法
    protected override void HandleInput(float magnitude, Vector2 normalised, Vector2 radius, Camera cam)
    {
        if (joystickType == JoystickType.Dynamic && magnitude > moveThreshold)
        {
            Vector2 difference = normalised * (magnitude - moveThreshold) * radius;
            background.anchoredPosition += difference;
        }
        base.HandleInput(magnitude, normalised, radius, cam);
    }
}

// 摇杆类型枚举
public enum JoystickType { Fixed, Floating, Dynamic }

