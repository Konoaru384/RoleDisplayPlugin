using System;
using System.Collections.Generic;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using MEC;
using UnityEngine;
using HintServiceMeow.Core.Enum;
using HintServiceMeow.Core.Models.Hints;
using HintServiceMeow.Core.Utilities;
using HSMHint = HintServiceMeow.Core.Models.Hints.Hint;

namespace RoleDisplayPlugin
{
    public class RoleDisplayPlugin : Plugin<Config>
    {
        public override string Name => "RoleDisplayPlugin";
        public override Version Version => new Version(2, 0, 0);
        public override Version RequiredExiledVersion => new Version(9, 6, 2);

        private readonly Dictionary<Player, CoroutineHandle> activeHints = new Dictionary<Player, CoroutineHandle>();
        private readonly Dictionary<Player, string> originalNames = new Dictionary<Player, string>();

        public override void OnEnabled()
        {
            Exiled.Events.Handlers.Player.Spawned += OnSpawned;
            Exiled.Events.Handlers.Player.ChangingRole += OnChangingRole;
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Player.Spawned -= OnSpawned;
            Exiled.Events.Handlers.Player.ChangingRole -= OnChangingRole;

            foreach (var handle in activeHints.Values)
                Timing.KillCoroutines(handle);

            activeHints.Clear();
            originalNames.Clear();
            base.OnDisabled();
        }

        private void OnSpawned(SpawnedEventArgs ev)
        {
            var player = ev.Player;
            var roleKey = player.Role.Type.ToString();

            if (Config.RoleDisplay.Roles.TryGetValue(roleKey, out var roleInfo)
                && roleInfo.RandomNameEnable
                && roleInfo.RandomName.Count > 0)
            {
                if (!originalNames.ContainsKey(player))
                    originalNames[player] = player.Nickname;

                var randomName = roleInfo.RandomName[UnityEngine.Random.Range(0, roleInfo.RandomName.Count)];
                player.DisplayNickname = $"{randomName} {originalNames[player]}";
            }

            var handle = Timing.RunCoroutine(DisplayHintLoop(player));
            activeHints[player] = handle;
        }

        private void OnChangingRole(ChangingRoleEventArgs ev)
        {
            var player = ev.Player;

            if (activeHints.TryGetValue(player, out var handle))
            {
                Timing.KillCoroutines(handle);
                activeHints.Remove(player);
            }

            if (originalNames.TryGetValue(player, out var original))
            {
                player.DisplayNickname = original;
                originalNames.Remove(player);
            }

            PlayerDisplay.Get(player).ClearHint();
        }

        private IEnumerator<float> DisplayHintLoop(Player player)
        {
            var roleKey = player.Role.Type.ToString();
            if (!Config.RoleDisplay.Roles.TryGetValue(roleKey, out var roleInfo))
                yield break;

            var roleName = roleInfo.Name;
            if (roleInfo.RandomNameEnable && roleInfo.RandomName.Count > 0)
                roleName = player.DisplayNickname.Split(' ')[0];

            var labelRole = Config.RoleDisplay.Labels.RoleLabel;
            var labelDesc = Config.RoleDisplay.Labels.DescriptionLabel;
            var color = roleInfo.HintColor ?? "#996633";
            var desc = roleInfo.Description;

            if (desc.Length > 100)
                desc = desc.Insert(100, "\n");

            var display = PlayerDisplay.Get(player);
            const int yBase = 1030;
            const int spacing = 30;

            while (player.Role.Type.ToString() == roleKey)
            {
                display.ClearHint();

                var hintRole = new HSMHint
                {
                    Text = $"<b><size=26><color=white>{labelRole}</color></size></b> <size=25><color={color}><b>{roleName}</b></color></size>",
                    FontSize = 25,
                    YCoordinate = yBase,
                    YCoordinateAlign = HintVerticalAlign.Bottom,
                    Alignment = HintAlignment.Center
                };

                var hintDesc = new HSMHint
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
}
