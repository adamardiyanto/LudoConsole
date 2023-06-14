namespace LudoApp;
public class Display
{
    private string[,] _plainBoard = new string[,]
    {  //    '1' '2' '3' '4' '5' '6' '7' '8' '9' "[ ]" '1' '2' '3' '4' '5'  
        {" | ","---","---","---","---","---","---","---","---","---","---","---","---","---","---","---"," | "}, 
        {" | "," + "," + "," + "," + "," + "," + ","[ ]","[ ]","[ ]"," + "," + "," + "," + "," + "," + "," | "}, // 1
        {" | "," + ","---","---","---","---"," + ","[ ]","[ ]","[S]"," + ","---","---","---","---"," + "," | "}, // 2
        {" | "," + ","---","[ ]","[ ]","---"," + ","[*]","[ ]","[ ]"," + ","---","[ ]","[ ]","---"," + "," | "}, // 3
        {" | "," + ","---","[ ]","[ ]","---"," + ","[ ]","[ ]","[ ]"," + ","---","[ ]","[ ]","---"," + "," | "}, // 4
        {" | "," + ","---","---","---","---"," + ","[ ]","[ ]","[ ]"," + ","---","---","---","---"," + "," | "}, // 5
        {" | "," + "," + "," + "," + "," + "," + ","[ ]","[ ]","[ ]"," + "," + "," + "," + "," + "," + "," | "}, // 6
        {" | ","[ ]","[S]","[ ]","[ ]","[ ]","[ ]","---","---","---","[ ]","[ ]","[ ]","[*]","[ ]","[ ]"," | "}, // 7
        {" | ","[ ]","[ ]","[ ]","[ ]","[ ]","[ ]","---","---","---","[ ]","[ ]","[ ]","[ ]","[ ]","[ ]"," | "}, // 8
        {" | ","[ ]","[ ]","[*]","[ ]","[ ]","[ ]","---","---","---","[ ]","[ ]","[ ]","[ ]","[S]","[ ]"," | "}, // 9
        {" | "," + "," + "," + "," + "," + "," + ","[ ]","[ ]","[ ]"," + "," + "," + "," + "," + "," + "," | "}, // 10    
        {" | "," + ","---","---","---","---"," + ","[ ]","[ ]","[ ]"," + ","---","---","---","---"," + "," | "}, // 11
        {" | "," + ","---","[ ]","[ ]","---"," + ","[ ]","[ ]","[ ]"," + ","---","[ ]","[ ]","---"," + "," | "}, // 12
        {" | "," + ","---","[ ]","[ ]","---"," + ","[ ]","[ ]","[*]"," + ","---","[ ]","[ ]","---"," + "," | "}, // 13
        {" | "," + ","---","---","---","---"," + ","[S]","[ ]","[ ]"," + ","---","---","---","---"," + "," | "}, // 14
        {" | "," + "," + "," + "," + "," + "," + ","[ ]","[ ]","[ ]"," + "," + "," + "," + "," + "," + "," | "}, // 15
        {" | ","---","---","---","---","---","---","---","---","---","---","---","---","---","---","---"," | "},
    };
    private string[,] _currentBoard = new string[17,17];
    public void ClearBoard()
    {
        _currentBoard = _plainBoard;
    }
    public void ShowBoard()
    {
        for(int i = 0; i < _currentBoard.GetLength(0); i++)
        {
            for(int j = 0; j < _currentBoard.GetLength(1); j++)
            {
                Console.Write(_currentBoard.GetValue(i, j));
            }
            Console.WriteLine();
        }
    }
    public void UpdateBoard(Dictionary<IPlayer,List<IPawn>> pawns, Dictionary<IPlayer, string> players)
    {
        foreach (var p in players)
        {
            //cek per player
            List<IPawn> listPawn = pawns[p.Key];
            string color = p.Value;
            int pawnBase = listPawn.Count(x => x.GetPosition()==0);
            foreach(var pawn in listPawn)
            {
                switch(pawn.GetPosition())
                {
                    case 0:
                        if (pawnBase == 4)
                        {
                            if (color == Color.Red.ToString())
                            {
                                
                            }
                        }
                        break;
                    case 1:
                        _currentBoard[7,14] = GetPawnColor(color);
                        break;
                    case 2:
                        _currentBoard[7,13] = GetPawnColor(color);
                        break;
                    case 3:
                        _currentBoard[7,12] = GetPawnColor(color);
                        break;
                    case 4:
                        _currentBoard[7,11] = GetPawnColor(color);
                        break;
                    case 5:
                        _currentBoard[7,10] = GetPawnColor(color);
                        break;
                    case 6:
                        _currentBoard[6,9] = GetPawnColor(color);
                        break;
                    case 7:
                        _currentBoard[5,9] = GetPawnColor(color);
                        break;
                    case 8:
                        _currentBoard[4,9] = GetPawnColor(color);
                        break;
                    case 9:
                        _currentBoard[3,9] = GetPawnColor(color);
                        break;
                    case 10:
                        _currentBoard[2,9] = GetPawnColor(color);
                        break;
                    case 11:
                        _currentBoard[1,9] = GetPawnColor(color);
                        break;
                    case 12:
                        _currentBoard[1,8] = GetPawnColor(color);
                        break;
                    case 13:
                        _currentBoard[1,7] = GetPawnColor(color);
                        break;
                    case 14:
                        _currentBoard[2,7] = GetPawnColor(color);
                        break;
                    case 15:
                        _currentBoard[3,7] = GetPawnColor(color);
                        break;
                    case 16:
                        _currentBoard[4,7] = GetPawnColor(color);
                        break;
                    case 17:
                        _currentBoard[5,7] = GetPawnColor(color);
                        break;
                    case 18:
                        _currentBoard[6,7] = GetPawnColor(color);
                        break;
                    case 19:
                        _currentBoard[7,6] = GetPawnColor(color);
                        break;
                    case 20:
                        _currentBoard[7,5] = GetPawnColor(color);
                        break;
                    case 21:
                        _currentBoard[7,4] = GetPawnColor(color);
                        break;
                    case 22:
                        _currentBoard[7,3] = GetPawnColor(color);
                        break;
                    case 23:
                        _currentBoard[7,2] = GetPawnColor(color);
                        break;
                    case 24:
                        _currentBoard[7,1] = GetPawnColor(color);
                        break;
                    case 25:
                        _currentBoard[8,1] = GetPawnColor(color);
                        break;
                    case 26:
                        _currentBoard[9,1] = GetPawnColor(color);
                        break;
                    case 27:
                        _currentBoard[9,2] = GetPawnColor(color);
                        break;
                    case 28:
                        _currentBoard[9,3] = GetPawnColor(color);
                        break;
                    case 29:
                        _currentBoard[9,4] = GetPawnColor(color);
                        break;
                    case 30:
                        _currentBoard[9,5] = GetPawnColor(color);
                        break;
                    case 31:
                        _currentBoard[9,6] = GetPawnColor(color);
                        break;
                    case 32:
                        _currentBoard[10,7] = GetPawnColor(color);
                        break;
                    case 33:
                        _currentBoard[11,7] = GetPawnColor(color);
                        break;
                    case 34:
                        _currentBoard[12,7] = GetPawnColor(color);
                        break;
                    case 35:
                        _currentBoard[13,7] = GetPawnColor(color);
                        break;
                    case 36:
                        _currentBoard[14,7] = GetPawnColor(color);
                        break;
                    case 37:
                        _currentBoard[15,7] = GetPawnColor(color);
                        break;
                    case 38:
                        _currentBoard[15,8] = GetPawnColor(color);
                        break;
                    case 39:
                        _currentBoard[15,9] = GetPawnColor(color);
                        break;
                    case 40:
                        _currentBoard[14,9] = GetPawnColor(color);
                        break;
                    case 41:
                        _currentBoard[13,9] = GetPawnColor(color);
                        break;
                    case 42:
                        _currentBoard[12,9] = GetPawnColor(color);
                        break;
                    case 43:
                        _currentBoard[11,9] = GetPawnColor(color);
                        break;
                    case 44:
                        _currentBoard[10,9] = GetPawnColor(color);
                        break;
                    case 45:
                        _currentBoard[9,10] = GetPawnColor(color);
                        break;
                    case 46:
                        _currentBoard[9,11] = GetPawnColor(color);
                        break;
                    case 47:
                        _currentBoard[9,12] = GetPawnColor(color);
                        break;
                    case 48:
                        _currentBoard[9,13] = GetPawnColor(color);
                        break;
                    case 49:
                        _currentBoard[9,14] = GetPawnColor(color);
                        break;
                    case 50:
                        _currentBoard[9,15] = GetPawnColor(color);
                        break;
                    case 51:
                        _currentBoard[8,15] = GetPawnColor(color);
                        break;
                    case 52:
                        _currentBoard[7,15] = GetPawnColor(color);
                        break;
                    

                }
            }
        }
    }
    public string GetPawnColor(string color)
    {
        if (color == Color.Blue.ToString()) return "[B]";
        else if (color == Color.Yellow.ToString()) return "[Y]";
        else if (color == Color.Red.ToString()) return "[R]";
        else return "[G]"; //green
    }
}