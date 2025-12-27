<H1>Mount and Blade - My Little Warband Ex</H1>
This is a recovered version of one of my favorite Bannerlord mods.  So far it is pretty unchanged from the original - outside of all the coding fixes required to make the mod compatible with Mount and Blade 2, v1.3+ - and a few minor tweaks.

I plan on providing additional improvements.

Not sure if I'll ever be able to release officially because the original author is no longer supporting the Mod, and would likely need permission.

**Additional Tweaks**
<ul>
<li>Added the ability to turn the NavalSoldier flag on</li>
<li>Added a UI button for this</li>
<li>Added filters, and sorting to the equipment menus</li>
<li>Added the ability to see if a weapon is capable of Spear Brace or Couchable from the equipment menu</li>
<li>Made the equipment view, slightly easier to read using</li> 
</ul>

**Primary Fixes So Far**
Adding this for those who are curious what needed to change in order to make the mod run with 1.3+.
<ul>
<li>Updated the **ImageIdentiferVM** to work with latest SDK</li>
  <ul>
<li>Changed the **ImageIdentifierVM** references, which in previous iterations of the SDK were ambigious, to the now required specific **ImageIdentifierVM** types **(CharacterImageIdentifierVM, ItemImageIdentifierVM)**, as ImageIdentifierVM is now abstract.</li>  
<li>Fixed the UI xml to mirror the new **ImageIdentifierVM** classes as the UI Xml referenced attributes no longer accessible.</li>  
</ul>
  <li>Created a custom EquipmentTooltipInnerContent type in order to provide formattability to the individual Equipment Cards.</li>
<li>Fixed the equipment cards based on the recommendations from the community to prevent confusing references between EquipmentCard, EquipmentSelector and EquipmentCard.</li>
<li>Fixed the Patches for IsMounted and IsRanged</li>
<li>Used Harmony to fix several references to character fields that now have private setters</li>
  <ul>
    <li>Culture</li>
    <li>The Default Formation type</li>
    <li>Culture</li>
  </ul>
<li></li>
</ul>
