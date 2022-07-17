using UnityEngine;
using System.Collections;

namespace DigitalRuby.AnimatedLineRenderer
{
    public class DemoScriptDraw : MonoBehaviour
    {
        public AnimatedLineRenderer AnimatedLine;

        private void Start()
        {

        }

        private void Update()
        {
            if (AnimatedLine == null)
            {
                return;
            }
            else if (Input.GetMouseButton(0))
            {
                Vector3 pos = Input.mousePosition;

                pos = Camera.main.ScreenToWorldPoint(new Vector3(pos.x, pos.y, AnimatedLine.transform.position.z));

                AnimatedLine.Enqueue(pos);
            }
            else if (Input.GetKey(KeyCode.R))
            {
                AnimatedLine.ResetAfterSeconds(0.5f, null);
            }
        }
    }
}