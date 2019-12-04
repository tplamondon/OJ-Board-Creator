using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Tile IDs:
00		Void
01		Blank
02		Home
03		Battle
04		Draw
05		Bonus
06		Drop
07		Warp
08		Draw x2
09		Bonus x2
0A		Drop x2
0B		"Flat Wall" (Unusued?)
0C		Void (Unused)
0D		Void (Unused)
0E		Void (Unused)
0F		Void (Unused)
10		Void (Unused)
11		Void (Unused)
12		Deck
13		Void (Unused)
14		Battle x2
15		Move
16		Move x2
17		Warp Move
18		Warp Move x2
19		Ice
1A		"Snow" (Not Winter event snow)
1B		Heal
1C		Heal x2
1D		Event (Playground)
1E		"???"
1F		Co-op Boss

Movement Flags
Bit 0: Exit west
Bit 1: Exit north
Bit 2: Exit east
Bit 3: Exit south
Bit 4: Enter west
Bit 5: Enter north
Bit 6: Enter east
Bit 7: Enter south

*/
public class TileID
{
    public static byte getTileID(int id)
    {
        byte retVal = 0;
        switch (id)
        {
            case (int)TileEnum.BLANK:
                retVal = 0x01;
                break;
            case (int)TileEnum.HOME:
                retVal = 0x02;
                break;
            case (int)TileEnum.BATTLE:
                retVal = 0x03;
                break;
            case (int)TileEnum.DRAW:
                retVal = 0x04;
                break;
            case (int)TileEnum.STAR:
                retVal = 0x05;
                break;
            case (int)TileEnum.DROP:
                retVal = 0x06;
                break;
            case (int)TileEnum.WARP:
                retVal = 0x07;
                break;
            case (int)TileEnum.DRAW2:
                retVal = 0x08;
                break;
            case (int)TileEnum.STAR2:
                retVal = 0x09;
                break;
            case (int)TileEnum.DROP2:
                retVal = 0x0A;
                break;
            //unused tiles then...
            case (int)TileEnum.DECK:
                retVal = 0x12;
                break;
            case (int)TileEnum.BATTLE2:
                retVal = 0x14;
                break;
            case (int)TileEnum.MOVE:
                retVal = 0x15;
                break;
            case (int)TileEnum.MOVE2:
                retVal = 0x16;
                break;
            case (int)TileEnum.WARPMOVE:
                retVal = 0x17;
                break;
            case (int)TileEnum.WARPMOVE2:
                retVal = 0x18;
                break;
            case (int)TileEnum.ICE:
                retVal = 0x19;
                break;
            case (int)TileEnum.HEAL:
                retVal = 0x1B;
                break;
            case (int)TileEnum.HEAL2:
                retVal = 0x1C;
                break;
            case (int)TileEnum.COOPBOSS:
                retVal = 0x1F;
                break;
        }
        return retVal;
    }
}
