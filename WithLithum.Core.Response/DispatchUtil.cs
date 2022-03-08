namespace WithLithum.Core.Response;

using Rage;
using Rage.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class DispatchUtil
{
    /// <summary>Displays an notification showing the speaker speaking the text.</summary>
    /// <param name="speaker">The speaker.</param>
    /// <param name="text">The text.</param>
    /// <returns>The handle of the notification.</returns>
    /// <remarks>
    /// <c>{player}</c> in the text will be replaced to the player's character name
    /// (e.g. Jenny Sandusky).
    /// </remarks>
    public static uint DisplayRadioQuote(string speaker, string text)
    {
        return Game.DisplayNotification($"~b~{speaker}~w~: {text.Replace("{player}", Functions.GetPersonaForPed(Game.LocalPlayer.Character).FullName)}");
    }

    /// <summary>Displays the radio quote using position.</summary>
    /// <param name="speaker">The speaker.</param>
    /// <param name="text">The text.</param>
    /// <param name="position">The position.</param>
    /// <param name="street">if set to <c>true</c>, displays the street name.</param>
    /// <returns>
    ///   The handle of the notification.
    /// </returns>
    /// <remarks>
    /// <c>{position}</c> in the text will be replaced to the position display name, and <c>{player}</c> in the text will be replaced to the player's character name
    /// (e.g. Jenny Sandusky).
    /// </remarks>
    public static uint DisplayRadioQuoteUsingPosition(string speaker, string text, Vector3 position, bool street = false)
    {
        string zoneName = NativeFunction.Natives.GET_NAME_OF_ZONE<string>(position.X, position.Y, position.Z);
        var streetName = World.GetStreetName(World.GetStreetHash(position));

        var result = Game.GetLocalizedString(zoneName);
        if (street)
        {
            result = $"{streetName}, {result}";
        }

        return DisplayRadioQuote(speaker, text.Replace("{position}", result));
    }
}
