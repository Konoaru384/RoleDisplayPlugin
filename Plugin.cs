using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using MEC;
using PlayerRoles;
using System.Collections.Generic;
using UnityEngine;
using HintServiceMeow.Core.Enum;
using HintServiceMeow.Core.Models.Hints;
using HintServiceMeow.Core.Utilities;

public class RoleInfoPlugin : Plugin<Config>
{
    public override string Name => "RoleDisplayPlugin";

    private readonly Dictionary<Player, CoroutineHandle> activeHints = new();
    private readonly Dictionary<Player, string> originalNames = new();

    public override void OnEnabled()
    {
        Exiled.Events.Handlers.Player.Spawned += OnSpawned;
        Exiled.Events.Handlers.Player.ChangingRole += OnChangingRole;
    }

    public override void OnDisabled()
    {
        Exiled.Events.Handlers.Player.Spawned -= OnSpawned;
        Exiled.Events.Handlers.Player.ChangingRole -= OnChangingRole;

        foreach (var handle in activeHints.Values)
            Timing.KillCoroutines(handle);

        activeHints.Clear();
        originalNames.Clear();
    }

    private void OnSpawned(SpawnedEventArgs ev)
    {
        string roleKey = ev.Player.Role.Type.ToString();

        if (Config.RoleDisplay.Roles.TryGetValue(roleKey, out var roleInfo)
            && roleInfo.RandomNameEnable && roleInfo.RandomName.Count > 0)
        {
            if (!originalNames.ContainsKey(ev.Player))
                originalNames[ev.Player] = ev.Player.Nickname;

            string random = roleInfo.RandomName[Random.Range(0, roleInfo.RandomName.Count)];
            ev.Player.DisplayNickname = $"{random} {originalNames[ev.Player]}";
        }

        CoroutineHandle handle = Timing.RunCoroutine(DisplayHintLoop(ev.Player));
        activeHints[ev.Player] = handle;
    }

    private void OnChangingRole(ChangingRoleEventArgs ev)
    {
        if (activeHints.TryGetValue(ev.Player, out var handle))
        {
            Timing.KillCoroutines(handle);
            activeHints.Remove(ev.Player);
        }

        if (originalNames.TryGetValue(ev.Player, out var original))
        {
            ev.Player.DisplayNickname = original;
            originalNames.Remove(ev.Player);
        }

        PlayerDisplay.Get(ev.Player).ClearHint();
    }

    private IEnumerator<float> DisplayHintLoop(Player player)
    {
        string roleKey = player.Role.Type.ToString();
        if (!Config.RoleDisplay.Roles.TryGetValue(roleKey, out var roleInfo)) yield break;

        string roleName = roleInfo.Name;
        if (roleInfo.RandomNameEnable && roleInfo.RandomName.Count > 0)
            roleName = player.DisplayNickname.Split(' ')[0];

        string labelRole = Config.RoleDisplay.Labels.RoleLabel;
        string labelDesc = Config.RoleDisplay.Labels.DescriptionLabel;
        string color = roleInfo.HintColor ?? "#996633";
        string desc = roleInfo.Description;

        if (desc.Length > 100)
            desc = desc.Insert(100, "\n");

        PlayerDisplay display = PlayerDisplay.Get(player);

        int yBase = 1030;
        int spacing = 30;

        while (player.Role.Type.ToString() == roleKey)
        {
            display.ClearHint();

            HintServiceMeow.Core.Models.Hints.Hint hintRole = new()
            {
                Text = $"<b><size=26><color=white>{labelRole}</color></size></b> <size=25><color={color}><b>{roleName}</b></color></size>",
                FontSize = 25,
                YCoordinate = yBase,
                YCoordinateAlign = HintVerticalAlign.Bottom,
                Alignment = HintAlignment.Center
            };

            HintServiceMeow.Core.Models.Hints.Hint hintDesc = new()
            {
                Text = $"<color=white><b>{labelDesc} </b></color><color={color}><i>{desc}</i></color>",
                FontSize = 23,
                YCoordinate = yBase + spacing,
                YCoordinateAlign = HintVerticalAlign.Bottom,
                Alignment = HintAlignment.Center
            };

            display.AddHint(hintRole);
            display.AddHint(hintDesc);

            yield return Timing.WaitForSeconds(1f);
        }
    }
}