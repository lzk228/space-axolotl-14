ent-CoreSubstation = { "" }
    .desc = { "" }
ent-BaseSubstation = substation
    .desc = Reduces the voltage of electricity put into it.
ent-BaseSubstationWall = wallmount substation
    .desc = A substation designed for compact shuttles and spaces.
ent-SubstationBasic = { ent-BaseSubstation }
    .suffix = Basic, 2.5MJ
    .desc = { ent-BaseSubstation.desc }
ent-SubstationBasicEmpty = { ent-SubstationBasic }
    .suffix = Empty
    .desc = { ent-SubstationBasic.desc }
ent-SubstationWallBasic = { ent-BaseSubstationWall }
    .suffix = Basic, 2MJ
    .desc = { ent-BaseSubstationWall.desc }
ent-BaseSubstationWallFrame = wallmount substation frame
    .desc = A substation frame for construction.
