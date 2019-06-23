using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UnityChan
{
    public class FaceUpdate2 : MonoBehaviour
    {
        public SkinnedMeshRenderer BlwDef;
        public SkinnedMeshRenderer EyeDef;
        public SkinnedMeshRenderer ElDef;
        public SkinnedMeshRenderer MthDef;

        //各スライダーの現在値を保持するリスト
        private List<float> _sliderValues;

        //BlendShape名の表示スタイル
        private GUIStyle _labelTitleStyle;

        //スライダー値の表示スタイル
        private GUIStyle _labelValueStyle;

        private int _blendShapeCount;

        private void Start()
        {
            if (this.BlwDef == null || this.EyeDef == null || this.ElDef == null || this.MthDef == null) return;

            //blendShapeのカウント ※EL_DEFはEYE_DEFと同時に処理するためノーカウント
            this._blendShapeCount = this.BlwDef.sharedMesh.blendShapeCount + this.EyeDef.sharedMesh.blendShapeCount + this.MthDef.sharedMesh.blendShapeCount;
            this._sliderValues = Enumerable.Repeat<float>(0f, this._blendShapeCount).ToList();

            this._labelTitleStyle = new GUIStyle { fixedWidth = 80f, alignment = TextAnchor.MiddleRight, normal = new GUIStyleState { textColor = Color.white } };
            this._labelValueStyle = new GUIStyle { fixedWidth = 20f, alignment = TextAnchor.MiddleRight, normal = new GUIStyleState { textColor = Color.white } };
        }

        private void OnGUI()
        {
            if (this.BlwDef == null || this.EyeDef == null || this.ElDef == null || this.MthDef == null) return;

            GUILayout.Box("", GUILayout.Width(220), GUILayout.Height(15 * (this._blendShapeCount + 1)));
            Rect screenRect = new Rect(10, 10, 190, 15 * (this._blendShapeCount + 1));
            GUILayout.BeginArea(screenRect);

            var sliderIndex = 0;
            //BLW_DEF
            for (var defIndex = 0; defIndex < this.BlwDef.sharedMesh.blendShapeCount; defIndex++)
            {
                sliderIndex = defIndex;
                GUILayout.BeginHorizontal();

                //BlendShape名
                var labelName = this.BlwDef.sharedMesh.GetBlendShapeName(defIndex);
                labelName = labelName.Substring(labelName.IndexOf('.') + 1);
                GUILayout.Label(labelName, this._labelTitleStyle);

                //スライダー
                this._sliderValues[sliderIndex] = GUILayout.HorizontalSlider(this._sliderValues[sliderIndex], 0f, 100f);

                //スライダーの値
                GUILayout.Label(((int)this._sliderValues[sliderIndex]).ToString("#0"), this._labelValueStyle);

                GUILayout.EndHorizontal();

                //スライダーの値をBlendShapeに反映
                this.BlwDef.SetBlendShapeWeight(defIndex, this._sliderValues[sliderIndex]);
            }

            //EYE_DEF, EL_DEF
            var adjustCount = this.BlwDef.sharedMesh.blendShapeCount;
            for (var defIndex = 0; defIndex < this.EyeDef.sharedMesh.blendShapeCount; defIndex++)
            {
                sliderIndex = adjustCount + defIndex;
                GUILayout.BeginHorizontal();

                //BlendShape名
                var labelName = this.EyeDef.sharedMesh.GetBlendShapeName(defIndex);
                labelName = labelName.Substring(labelName.IndexOf('.') + 1);
                GUILayout.Label(labelName, this._labelTitleStyle);

                //スライダー
                this._sliderValues[sliderIndex] = GUILayout.HorizontalSlider(this._sliderValues[sliderIndex], 0f, 100f);

                //スライダーの値
                GUILayout.Label(((int)this._sliderValues[sliderIndex]).ToString("#0"), this._labelValueStyle);

                GUILayout.EndHorizontal();

                //スライダーの値をBlendShapeに反映
                this.EyeDef.SetBlendShapeWeight(defIndex, this._sliderValues[sliderIndex]);
                this.ElDef.SetBlendShapeWeight(defIndex, this._sliderValues[sliderIndex]);
            }

            //MTH_DEF
            adjustCount += this.EyeDef.sharedMesh.blendShapeCount;
            for (int defIndex = 0; defIndex < this.MthDef.sharedMesh.blendShapeCount; defIndex++)
            {
                sliderIndex = adjustCount + defIndex;
                GUILayout.BeginHorizontal();

                //BlendShape名
                var labelName = this.MthDef.sharedMesh.GetBlendShapeName(defIndex);
                labelName = labelName.Substring(labelName.IndexOf('.') + 1);
                GUILayout.Label(labelName, this._labelTitleStyle);

                //スライダー
                this._sliderValues[sliderIndex] = GUILayout.HorizontalSlider(this._sliderValues[sliderIndex], 0f, 100f);

                //スライダーの値
                GUILayout.Label(((int)this._sliderValues[sliderIndex]).ToString("#0"), this._labelValueStyle);

                GUILayout.EndHorizontal();

                //スライダーの値をBlendShapeに反映
                this.MthDef.SetBlendShapeWeight(defIndex, this._sliderValues[sliderIndex]);
            }

            GUILayout.EndArea();
        }

        private void Update()
        {
        }

        private void OnCollisionStay(Collision collision)
        {
            
        }
    }
}
