namespace WithLithum.Core.Response;

using Rage;
using Rage.Native;

public static class SpawnerUtil
{
    /// <summary>Gets a position around the player (usually on motorized roads) which is relatively safer for <see cref="Vehicle"/>s to cruise.</summary>
    /// <param name="minimumDistance">The minimum distance.</param>
    /// <param name="maximumDistance">The maximum distance.</param>
    /// <returns>
    ///   <para>A <see cref="Vector3" /> indicating the generated spawn point.</para>
    /// </returns>
    public static Vector3 GetRandomPositionOnStreet(float minimumDistance, float maximumDistance)
    {
        return World.GetNextPositionOnStreet(Game.LocalPlayer.Character.Position.Around(minimumDistance, maximumDistance));
    }

    /// <summary>Gets a position around the player (usually on sidewalk) which is relatively safer for <see cref="Ped" />s to walk.</summary>
    /// <param name="minimumDistance">The minimum distance.</param>
    /// <param name="maximumDistance">The maximum distance.</param>
    /// <returns>If success, the generated position; otherwise, returns <see cref="Vector3.Zero" />.<br /></returns>
    public static Vector3 GetRandomPositionOnSidewalk(float minimumDistance, float maximumDistance)
    {
        var vector = GetRandomPositionOnStreet(minimumDistance, maximumDistance);
        var result = Vector3.Zero;

        if (NativeFunction.Natives.GET_SAFE_COORD_FOR_PED<bool>(vector.X, vector.Y, vector.Z, false, ref result, 28))
        {
            return result;
        }
        else
        {
            return Vector3.Zero;
        }
    }
}
