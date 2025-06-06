/*
 * Copyright (c) Meta Platforms, Inc. and affiliates.
 * All rights reserved.
 *
 * Licensed under the Oculus SDK License Agreement (the "License");
 * you may not use the Oculus SDK except in compliance with the License,
 * which is provided at the time of installation or download, or which
 * otherwise accompanies this software in either electronic or hard copy form.
 *
 * You may obtain a copy of the License at
 *
 * https://developer.oculus.com/licenses/oculussdk/
 *
 * Unless required by applicable law or agreed to in writing, the Oculus SDK
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using UnityEngine;

namespace Oculus.Interaction
{
    /// <summary>
    /// Helps determine the position, orientation, and size of curved components such as the CanvasCylinder, CylinderSurface, and ClippedCylinderSurface. In a curved UI with multiple canvases, a single cylinder can be used to drive all panels, removing the need to manually align each panel in a cylinder orientation.
    /// </summary>
    public class Cylinder : MonoBehaviour
    {
        /// <summary>
        /// The radius of the cylinder.
        /// </summary>
        [Tooltip("The radius of the cylinder.")]
        [SerializeField]
        private float _radius = 1f;

        public float Radius
        {
            get => _radius;
            set => _radius = value;
        }
    }
}
