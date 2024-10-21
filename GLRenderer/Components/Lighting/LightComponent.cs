using System;
using System.Collections.Generic;
using GLComponentSystem;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;
using GLRenderer.Rendering;

namespace GLRenderer.Components
{
    public enum LightType
    {

    }
    public class LightComponent : Component
    {
        public Entity Entity { get; set; }

        public float brightness { get; set; } = 1.0f;
        public float diffuseBrightness { get; set; } = 0.1f;
        public float specularStrength { get; set;} = 0.0f;

        public Vector4 color { get; set; } = new Vector4(1.0f);
        public LightType lightType { get; set; }

        public float lightColorStrength { get; set; } = 1.0f;

        private LightData lightData;
        private UBO<LightData> ubo;

        public override void OnStart(Entity entity)
        {
            base.OnStart(entity);

            lightData = new LightData();
            ubo = new UBO<LightData>(ref lightData, "LightData", 1);
            lightData.UpdateLightData(entity.position, brightness, diffuseBrightness, Vector4.Lerp(new Vector4(1.0f), color, lightColorStrength), specularStrength);
        }
        public override void OnUpdate(Entity entity)
        {
            base.OnUpdate(entity);

            lightData.UpdateLightData(entity.position, brightness, diffuseBrightness, Vector4.Lerp(new Vector4(1.0f), color, lightColorStrength), specularStrength);
            ubo.UpdateUBO(ref lightData);
        }

        public override void OnUnload(Entity entity)
        {
            base.OnUnload(entity);
            lightData.UpdateLightData(new Vector3(0), 0, 0, new Vector4(0), 0);
            ubo.UpdateUBO(ref lightData);
        }
    }
}
