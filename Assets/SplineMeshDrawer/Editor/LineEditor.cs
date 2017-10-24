﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace geniikw.UIMeshLab.Editors {
        
    public class LineEditor : Editor
    {
        private PointHandler _pointHandler;
        //private LineInpectorHandler _inspector;
        private MonoBehaviour _owner;
        
        protected void OnEnable()
        {
            _owner = target as MonoBehaviour;
                  
            _pointHandler = new PointHandler(_owner,serializedObject);
            //_inspector = new LineInpectorHandler(serializedObject);
        }
        protected void OnSceneGUI()
        {
            if (Application.isPlaying)
                return;
            _pointHandler.OnSceneGUI();
        }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            base.OnInspectorGUI();
            if (EditorGUI.EndChangeCheck())
            {
                SyncPoint();
            }
        }

        void SyncPoint()
        {
            if (serializedObject.FindProperty("line").FindPropertyRelative("useListPoints").boolValue)
                return;

            var p0 = serializedObject
                .FindProperty("line")
                .FindPropertyRelative("points")
                .GetArrayElementAtIndex(0);

            var p1 = serializedObject
                .FindProperty("line")
                .FindPropertyRelative("points")
                .GetArrayElementAtIndex(1);

            var p00 = serializedObject
               .FindProperty("line")
               .FindPropertyRelative("p0");

            var p11 = serializedObject
                     .FindProperty("line")
                     .FindPropertyRelative("p1");

            p00.Position().vector3Value = p0.Position().vector3Value;
            p00.NextOffset().vector3Value = p0.NextOffset().vector3Value;
            p00.PrevOffset().vector3Value = p0.PrevOffset().vector3Value;

            p11.Position().vector3Value = p1.Position().vector3Value;
            p11.NextOffset().vector3Value = p1.NextOffset().vector3Value;
            p00.PrevOffset().vector3Value = p1.PrevOffset().vector3Value;
        }

    }
    
    [CustomEditor(typeof(UILine))]
    public class UILineEditor : LineEditor {}

    [CustomEditor(typeof(WorldLine))]
    public class WorldLineEditor : LineEditor { }

    [CustomEditor(typeof(GizmoLine))]
    public class NoRenderLineEditor : LineEditor { }

}


