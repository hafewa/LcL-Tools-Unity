﻿using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(AnimationToFrames))]
public class AnimationToFramesEditor : Editor
{
    string[] displayTexts;
    int selectDisplayIndex = 0;

    private void OnEnable()
    {
        GameViewUtils.UpdateDisplaySizes();
        selectDisplayIndex = GameViewUtils.FindSize(Screen.width, Screen.height);
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        AnimationToFrames tools = (AnimationToFrames)target;

        EditorGUI.BeginChangeCheck();
        {
            selectDisplayIndex = EditorGUILayout.Popup("分辨率", selectDisplayIndex, GameViewUtils.DisplayTexts);
        }
        if (EditorGUI.EndChangeCheck())
        {
            GameViewUtils.OpenWindow();
            GameViewUtils.SetSize(selectDisplayIndex);
            GameViewUtils.SetMinScale();
        }

        var list = tools.GetAnimatorClip();
        tools.selectIndex = EditorGUILayout.Popup("选择动画", tools.selectIndex, list.ToArray());

        if (GUILayout.Button("预览动画"))
        {
            tools.PlayAnimationByClipName(tools.AnimationName);
            {
            }
        }

        if (GUILayout.Button("渲染序列帧动画"))
        {
            GameViewUtils.OpenWindow();
            tools.Capture();
        }
    }
}
