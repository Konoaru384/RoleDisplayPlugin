<h1 align="center">🎭 RoleDisplay – RP Plugin for SCP:SL</h1>

<p align="center">
  ✨ An immersive plugin to enhance Roleplay experience on your SCP: Secret Laboratory servers  
</p>

<p align="center">
  <a href="https://github.com/Konoaru384/RoleDisplayPlugin/releases/latest">
    <img src="https://img.shields.io/github/v/release/Konoaru384/RoleDisplayPlugin?label=Latest%20Release&color=blue&style=for-the-badge"/>
  </a>
  <a href="https://github.com/Konoaru384/RoleDisplayPlugin/releases">
    <img src="https://img.shields.io/github/downloads/Konoaru384/RoleDisplayPlugin/total?label=Downloads&color=success&style=for-the-badge"/>
  </a>
  [![Join Discord](https://img.shields.io/badge/Join_Discord-7289da?logo=discord&logoColor=white&style=for-the-badge)](https://discord.gg/vxGeGFr5Bc)
  </a>
</p>

---

> ⚠️ This plugin was originally commissioned for a player who later scammed me.  
> I'm now releasing it publicly for the community to benefit from.

## 🧩 Features

- 🧬 Dynamic display of each role and its **custom description**.
- 🎭 Option to assign **random names per role** — great for immersive RP.
- 🏷️ Auto-renames players using the format `[Role] Nickname`.
- 🌈 Custom hint colors per role.
- 📄 Fully customizable `.yml` config file.
- 🧪 Extra options like TPS display, debug mode, and more.

---

## 🖼️ RP Visual Example

Displayed nickname in game:  
💡 `[Lead Biologist] Jordan`

---

## ⚙️ Requirements

| Component        | Minimum Version     |
|------------------|---------------------|
| 🧩 **Exiled**     | `v9.6.2`             |
| 🎮 **SCP:SL**     | Latest stable build |

---

## 🛠️ Example Configuration (`.yml`)

```yaml
role_display:
  enabled: true
  format: "%role_name%\n%description%"
  labels:
    role_label: "Role:"
    description_label: "Description:"
  roles:
    Scientist:
      name: "Lead Biologist"
      description: "Studies SCPs and supports medical staff"
      random_name_enable: true
      random_name:
        - "Dr. Lambert"
        - "Dr. Vassiliev"
        - "Dr. Kessler"
      hint_color: "#3FAFBD"

