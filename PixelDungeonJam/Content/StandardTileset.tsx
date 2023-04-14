<?xml version="1.0" encoding="UTF-8"?>
<tileset version="1.8" tiledversion="1.8.4" name="TiledIcons" tilewidth="16" tileheight="16" tilecount="1024" columns="32">
 <image source="StandardTilesetIcons.png" width="512" height="512"/>
 <tile id="0">
  <objectgroup draworder="index" id="2">
   <object id="2" x="0" y="0">
    <polygon points="0,0 0,16 16,16 16,0"/>
   </object>
  </objectgroup>
 </tile>
 <tile id="101">
  <objectgroup draworder="index" id="2">
   <object id="2" x="0" y="0">
    <polygon points="0,0 0,16 16,16"/>
   </object>
  </objectgroup>
 </tile>
 <tile id="102">
  <objectgroup draworder="index" id="2">
   <object id="6" x="0" y="0">
    <polygon points="0,0 0,16 16,16 16,8 8,8 8,0"/>
   </object>
  </objectgroup>
 </tile>
 <tile id="103">
  <objectgroup draworder="index" id="2">
   <object id="1" x="0" y="0" width="8" height="16"/>
  </objectgroup>
 </tile>
 <tile id="104">
  <objectgroup draworder="index" id="2">
   <object id="1" x="0" y="8" width="8" height="8"/>
  </objectgroup>
 </tile>
 <tile id="133">
  <objectgroup draworder="index" id="2">
   <object id="4" x="0" y="0">
    <polygon points="0,0 0,16 16,16 16,8 8,8"/>
   </object>
  </objectgroup>
 </tile>
 <tile id="134">
  <objectgroup draworder="index" id="2">
   <object id="4" x="0" y="0">
    <polygon points="0,0 0,16 8,16 8,8"/>
   </object>
  </objectgroup>
 </tile>
 <tile id="135">
  <objectgroup draworder="index" id="2">
   <object id="2" x="0" y="4">
    <polygon points="0,0 12,12 5,12 0,7"/>
   </object>
  </objectgroup>
 </tile>
 <tile id="136">
  <objectgroup draworder="index" id="2">
   <object id="1" x="0" y="16">
    <polygon points="0,0 0,-12 12,0"/>
   </object>
  </objectgroup>
 </tile>
 <tile id="256">
  <properties>
   <property name="MatchType" value="Empty"/>
  </properties>
 </tile>
 <tile id="257">
  <properties>
   <property name="MatchType" value="Ignore"/>
  </properties>
 </tile>
 <tile id="258">
  <properties>
   <property name="MatchType" value="NonEmpty"/>
  </properties>
 </tile>
 <tile id="259">
  <properties>
   <property name="MatchType" value="Other"/>
  </properties>
 </tile>
 <tile id="260">
  <properties>
   <property name="MatchType" value="Negate"/>
  </properties>
 </tile>
 <wangsets>
  <wangset name="CollisionSet" type="mixed" tile="-1">
   <wangcolor name="SolidCollision" color="#ff0000" tile="-1" probability="1"/>
   <wangtile tileid="0" wangid="1,1,1,1,1,1,1,1"/>
   <wangtile tileid="73" wangid="1,1,0,0,0,1,1,1"/>
   <wangtile tileid="74" wangid="1,1,1,1,0,0,0,1"/>
   <wangtile tileid="75" wangid="0,0,0,1,0,0,0,0"/>
   <wangtile tileid="76" wangid="0,0,0,1,1,1,0,0"/>
   <wangtile tileid="77" wangid="0,0,0,0,0,1,0,0"/>
   <wangtile tileid="105" wangid="0,0,0,1,1,1,1,1"/>
   <wangtile tileid="106" wangid="0,1,1,1,1,1,0,0"/>
   <wangtile tileid="107" wangid="0,1,1,1,0,0,0,0"/>
   <wangtile tileid="109" wangid="0,0,0,0,0,1,1,1"/>
   <wangtile tileid="139" wangid="0,1,0,0,0,0,0,0"/>
   <wangtile tileid="140" wangid="1,1,0,0,0,0,0,1"/>
   <wangtile tileid="141" wangid="0,0,0,0,0,0,0,1"/>
  </wangset>
 </wangsets>
</tileset>
