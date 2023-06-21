namespace LudoApp;
public partial class Program
{
    static public void StartGame()
    {
        _runner.clearBoard();
        _runner.updateBoard(_runner.GetPawnList(), _runner.GetPlayerList());
        _runner.showBoard();
        while (!_runner.CheckEndGame())
        {
            foreach (var player in _runner.GetPlayerList()) // looping each player
            {
                _runner.SetCurrentPlayer(player.Key);
                int diceValue = 0;
                do // looping if player get 6
                {
                    Console.WriteLine(player.Key.Name.ToUpper() + " press enter to roll dice");
                    Console.ReadLine();
                    diceValue = _runner.RollDice();
                    Console.WriteLine(diceValue);
                    Console.ReadLine();
                    if (_runner.CountPawnOutOfBase(_runner.GetCurrentPlayer()) is 0) // there are no pawn out of base
                    {
                        if (_runner.CheckIsSix(diceValue))
                        {
                            _runner.PawnToStart(_runner.GetPawnList()[_runner.GetCurrentPlayer()][0], _runner.GetPlayerList()[_runner.GetCurrentPlayer()]);
                        }
                    }
                    else if (_runner.CountPawnOutOfBase(_runner.GetCurrentPlayer()) is 1) // if there is a pawn out of base
                    {
                        if (_runner.CheckIsSix(diceValue))
                        {
                            MoveOrOut(diceValue, _runner.CountPawnOutOfBase(_runner.GetCurrentPlayer()));
                        }
                        else
                        {
                            _runner.MovePawn(_runner.GetPawnList()[_runner.GetCurrentPlayer()].Find(x => x.GetPosition() > (int)Cell.Base && x.GetPosition() < (int)Cell.Triangle), diceValue);
                        }
                    }
                    else if (_runner.CountPawnOutOfBase(_runner.GetCurrentPlayer()) < 4)
                    {
                        if (_runner.CheckIsSix(diceValue))
                        {
                            MoveOrOut(diceValue, _runner.CountPawnOutOfBase(_runner.GetCurrentPlayer()));
                        }
                        else
                        {
                            SelectPawnToMove(diceValue);
                        }

                    }
                    else
                    {
                        SelectPawnToMove(diceValue);

                    }
                    _runner.clearBoard();
                    _runner.updateBoard(_runner.GetPawnList(), _runner.GetPlayerList());
                    _runner.showBoard();
                } while (diceValue == 6);

            }
        }
    }

    static private void MoveOrOut(int diceValue, int pawnOutbase)
    {
        Console.WriteLine("choose pawn out of base or move pawn");
        Console.WriteLine("press M to move pawn and press O to release pawn");

        while (true)
        {
            string input = Console.ReadLine();
            if (input == "m" || input == "M")
            {
                if (pawnOutbase == 1)
                {
                    _runner.MovePawn(_runner.GetPawnList()[_runner.GetCurrentPlayer()].Find(x => x.GetPosition() > (int)Cell.Base && x.GetPosition() < (int)Cell.Triangle), diceValue);
                    break;
                }
                else
                {
                    SelectPawnToMove(diceValue);
                    break;
                }
            }
            else if (input == "o" || input == "O")
            {
                IPawn p = _runner.GetPawnList()[_runner.GetCurrentPlayer()].Find(x => x.GetPosition() is (int)Cell.Base);
                _runner.PawnToStart(p, _runner.GetPlayerList()[_runner.GetCurrentPlayer()]);
                break;
            }
            else
            {
                Console.WriteLine("Invalid input");
            }
        }
    }

    static private void SelectPawnToMove(int diceValue)
    {
        // select pawn to move
        List<IPawn> listPawns = _runner.GetPawnList()[_runner.GetCurrentPlayer()].FindAll(x => x.GetPosition() > (int)Cell.Base && x.GetPosition() < (int)Cell.Triangle);
        foreach (IPawn p in listPawns)
        {
            Console.WriteLine((listPawns.IndexOf(p) + 1) + ". pawn with position " + p.GetPosition());
        }
        Console.WriteLine("select pawn to move");
        Console.WriteLine("enter number based on pawn order");
        while (!ValidatePawn(listPawns.Count))
        {
            Console.WriteLine("Invalid input or number out of range");
        }
        // next should add condition if out of bound
        _runner.MovePawn(listPawns[_numPawn - 1], diceValue);
    }
    static private bool ValidatePawn(int totalPawn)
    {
        if (int.TryParse(Console.ReadLine(), out _numPawn))
        {
            return _numPawn > 0 && _numPawn <= totalPawn;
        }
        else
        {
            return false;
        }
    }
}