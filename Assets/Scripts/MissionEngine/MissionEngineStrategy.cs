using System;

namespace MissionEngine
{
    public class DeprecatedMissionStrategy
    {
        public static DeprecatedCaMMission Create(MissionType mission)
        {
            switch (mission)
            {
                case MissionType.CatAndMouse:
                    return new DeprecatedCaMMission();
                default:
                    throw new ArgumentOutOfRangeException("mission");
            }
        }
    }
}
