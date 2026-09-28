# Patient Medical Report Management System - Design Guidelines

## Visual Language
The application adopts a **Modern Flat UI Design** built entirely in Windows Forms, departing from the classic 3D gray UI in favor of solid colors, crisp typography, and borderless elements.

## Color Palette
The color scheme is inspired by professional medical and enterprise dashboards, heavily relying on the Flat UI color palette:

- **Sidebar Background:** `#2c3e50` (Midnight Blue) - Grounding the navigation.
- **Main Background:** `#ffffff` (White) - Keeping the data area clean and legible.
- **Primary Action / Active Tabs:** `#2980b9` (Belize Hole / Bright Elegant Blue) - Used for primary actions, headers, and active tab highlights.
- **Success / Add:** `#2ecc71` (Emerald) - Used for creating patients and saving records.
- **Destructive / Logout:** `#c0392b` / `#e74c3c` (Alizarin) - Used for logout and deletion operations.
- **Inactive / Disabled:** `#f0f0f0` (Light Gray) - Used for inactive tabs or disabled fields.
- **Text (Primary):** `#404040` (Dark Gray) for readability over pure black.
- **PDF Documents:** `#ffffcc` (Light Yellow) - Used for the signature "Sticky Note" aesthetic when generating PDF exports.

## Typography
- **Primary Font:** `Segoe UI`
- **Sizes:** 
  - Standard text / form fields: `10pt` or `11pt` Regular
  - Headers / Buttons: `10pt` or `11pt` Bold
  - Major Titles: `16pt` Bold

## Component Styling (UITheme.cs)
- **Buttons:** `FlatStyle.Flat` with `BorderSize = 0`. White foreground text.
- **Data Grids:** `EnableHeadersVisualStyles = false`, customized header backgrounds to match the primary theme, no cell borders, alternating row colors for readability.
- **Dropdowns (ComboBoxes):** `FlatStyle.Standard` to retain a visible border against the flat background, ensuring users recognize them as interactive.
- **Tabs:** `OwnerDrawFixed` custom rendering. Active tabs are highlighted in bright blue (`#2980b9`) with white text, inactive tabs are grayed out. Tab content is given a `20, 6` padding to avoid text cramping.

## Iconography
The application relies heavily on standard Unicode emojis (e.g., 🚪, ℹ️) for lightweight, universally supported iconography without external dependencies.
