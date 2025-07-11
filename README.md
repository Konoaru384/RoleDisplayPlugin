# 🎭 RoleDisplay – RP Plugin for SCP:SL

<p align="center">
  ✨ Enhance the roleplay experience on your SCP: Secret Laboratory servers  
</p>

<p align="center">
  <a href="https://github.com/Konoaru384/RoleDisplayPlugin/releases/latest">
    <img src="https://img.shields.io/github/v/release/Konoaru384/RoleDisplayPlugin?label=Latest%20Release&color=blue&style=for-the-badge"/>
  </a>
  <a href="https://github.com/Konoaru384/RoleDisplayPlugin/releases">
    <img src="https://img.shields.io/github/downloads/Konoaru384/RoleDisplayPlugin/total?label=Downloads&color=success&style=for-the-badge"/>
  </a>
  <a href="https://discord.gg/vxGeGFr5Bc">
    <img src="https://img.shields.io/badge/Join%20Discord-7289da?logo=discord&logoColor=white&style=for-the-badge"/>
  </a>
</p>

---

> ⚠️ **Important Note**  
> This plugin was originally commissioned for a player who ended up scamming me.  
> I'm releasing it publicly so the community can benefit from it.

> ⚠️ **Dependencies**  
> This plugin requires **Hint Service Meow** and **0Harmony** to be in the `dependencies` folder.  
> All required files are included in the release!

---

## 🧩 Features

- 🧬 Displays each role with a **custom description**
- 🎭 Supports **random RP names** per role
- 🏷️ Automatically formats player nicknames as `[Role] Nickname`
- 🌈 Allows **custom hint colors** per role
- ⚙️ Fully configurable using a `.yml` file

---

## 🖼️ RP Display Example

Displayed nickname in-game:  
💡 `[Lead Biologist] Jordan`

---

## 🛠️ Sample Configuration (`.yml`)

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

