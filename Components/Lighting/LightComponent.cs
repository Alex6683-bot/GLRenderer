using System;
using System.Collections.Generic;
using GLEntitySystem;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;
using GLRenderer.Rendering;

namespace GLRenderer.Components
{
    enum LightType
    {

    }
    class LightComponent : Component
    {
        public Entity Entity { get; set; }
        public float ambientBrightness { get; set; } = 0.2f;
        public float specularStrength { get; set;} = 0.1f;
        public Vector4 color { get; set; } = new Vector4(1.0f);
        public LightType lightType { get; set; }

        private LightData lightData;
        private UBO<LightData> ubo;

        public override void OnStart(Entity entity)
        {
            base.OnStart(entity);

            lightData = new LightData();
            ubo = new UBO<LightData>(ref lightData, "LightData", 1);
            lightData.UpdateLightData(entity.position, ambientBrightness, color, specularStrength);
        }
        public override void OnUpdate(Entity entity)
        {
            base.OnUpdate(entity);
            lightData.UpdateLightData(entity.position, ambientBrightness, color, specularStrength);
            ubo.UpdateUBO(ref lightData);
        }
    }
}
