﻿#region Header
/**
 * 名称：CompareFloat
 
 * 日期：2016.6.2
 * 描述：
 **/
#endregion
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Simple.BehaviorTree
{
    public enum enCompare
    {
        equal,
        greater,
        greaterAndEqual,
        less,
        lessAndEqual,
        unEqual
    }


    public class CompareFloatCfg : ConditionalCfg
    {
        public static string[] CompareTypeNames = new string[] { "等于", "大于", "大于等于", "小于", "小于等于","不等于" };
        public Value<float> v1 = new Value<float>(0);
        public Value<float> v2 = new Value<float>(0);
        public enCompare comp = enCompare.equal;
        public Value<bool> ret = new Value<bool>(false);
        public bool alwaysSuccess = false;
#if UNITY_EDITOR
        public override void DrawAreaInfo(Node n)
        {
            v1.Draw("值1",this,n);
            v2.Draw("值2",this,n);
            comp = (enCompare)EditorGUILayout.Popup("比较类型",(int)comp, CompareTypeNames);
            ret.Draw("结果",this,n);
            using (new AutoEditorTipButton("如果不勾选，那么比较结果为真才返回成功，相当于当成条件用；勾选的话那么始终为真，相当于当成行为用了"))
                alwaysSuccess = EditorGUILayout.Toggle("始终为真", alwaysSuccess);
        }
#endif

    }


    public class CompareFloat : Conditional
    {
        CompareFloatCfg CfgEx { get { return (CompareFloatCfg)m_cfg; } }

        //执行。遍历到这个节点的时候就会在OnPush()后执行，如果返回running的话就会一直执行，直到返回success或者fail，然后OnPop()
        protected override enNodeState OnExecute(enExecute executeType)
        {
            var v1 = GetValue(CfgEx.v1);
            var v2 = GetValue(CfgEx.v2);
            bool ret=false;
            switch(CfgEx.comp)
            {
                case enCompare.equal: ret= Mathf.Abs(v1 -v2) < 0.00001f; break;
                case enCompare.greater: ret = v1 > v2; break;
                case enCompare.greaterAndEqual: ret = v1 >= v2; break;
                case enCompare.less: ret = v1 < v2; break;
                case enCompare.lessAndEqual: ret = v1 <= v2; break;
                case enCompare.unEqual: ret = Mathf.Abs(v1 - v2) > 0.00001f; break;
                default:
                    {
                        Debuger.LogError("未知的类型:{0}", CfgEx.comp);
                        ret = false;
                    };break;
            }

            if(CfgEx.ret.region!= enValueRegion.constant)
                SetValue(CfgEx.ret, ret);
            return CfgEx.alwaysSuccess || ret ? enNodeState.success:enNodeState.failure;

        }
    }
}