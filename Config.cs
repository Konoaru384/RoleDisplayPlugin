using Exiled.API.Interfaces;
using System.Collections.Generic;
using YamlDotNet.Serialization;

public class Config : IConfig
{
    public bool IsEnabled { get; set; } = true;
    public bool Debug { get; set; } = false;
    public bool EnableTpsDisplay { get; set; } = true;

    [YamlMember(Alias = "role_display")]
    public RoleDisplayConfig RoleDisplay { get; set; } = new();
}

public class RoleDisplayConfig
{
    public bool Enabled { get; set; } = true;
    public string Format { get; set; } = "%role_name%\\n%description%";

    [YamlMember(Alias = "labels")]
    public LabelDisplay Labels { get; set; } = new();

    public Dictionary<string, RoleInfo> Roles { get; set; } = new()
    {
        { "ClassD", new RoleInfo() },
        { "Scientist", new RoleInfo() },
        { "FacilityGuard", new RoleInfo() },
        { "NtfPrivate", new RoleInfo() },
        { "NtfSergeant", new RoleInfo() },
        { "NtfSpecialist", new RoleInfo() },
        { "NtfCaptain", new RoleInfo() },
        { "ChaosConscript", new RoleInfo() },
        { "ChaosMarauder", new RoleInfo() },
        { "ChaosRepressor", new RoleInfo() },
        { "ChaosRifleman", new RoleInfo() },
        { "Scp173", new RoleInfo() },
        { "Scp106", new RoleInfo() },
        { "Scp096", new RoleInfo() },
        { "Scp049", new RoleInfo() },
        { "Scp0492", new RoleInfo() },
        { "Scp939", new RoleInfo() },
        { "Tutorial", new RoleInfo() }
    };
}

public class LabelDisplay
{
    public string RoleLabel { get; set; } = "Role name:";
    public string DescriptionLabel { get; set; } = "Description:";
}

public class RoleInfo
{
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public bool RandomNameEnable { get; set; } = false;
    public List<string> RandomName { get; set; } = new();
    public string HintColor { get; set; } = "#996633";
}