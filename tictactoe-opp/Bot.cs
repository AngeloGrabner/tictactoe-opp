using System;

internal enum prioraty
{
    NaN = -1,
    unImportant = 0,
    important = 1,
    veryImportant = 2,
    mostImportant = 3
}
internal class Bot
{
    private int[] input = new int[9]; // is the tic tac toe field and ist start at index 0 and ends at index 8 
    private prioraty[] enemy = new prioraty[9];
    private prioraty[] self = new prioraty[9];
    private float _errorRate;
    private int _enemy;
    private int _self;
    private int _empty;
    private int finalMove;
    public Bot(int difficulty = 1, int ownPlayerNumber = 2, int enemyPlayerNumber = 1, int emptyFieldNumber = 0) // difficulty 1 to 5 (easy to hard) 
    {
        _errorRate = (5 - difficulty) / 5;
        _self = ownPlayerNumber;
        _enemy = enemyPlayerNumber;
        _empty = emptyFieldNumber;
        finalMove = 0;
    }
    public void setState(int[] state)
    {
        input = state;
    }
    public int getTurn()
    {
        Console.WriteLine("preanalyse");
        Tuple<bool, int> isLastMove = preAnalyse();
        if (isLastMove.Item1)
        {
            return isLastMove.Item2;
        }
        //Console.WriteLine("analysEnemy"); //just for debuging
        analyseEnemy();
        //Console.WriteLine("analyseSelf");
        analyseSelf();
        //Console.WriteLine("evaluate");
        evaluate();
        //Console.WriteLine($"bot is done, value: {finalMove}");
        return finalMove;
    }
    private Tuple<bool, int> preAnalyse() // in case only one field is left 
    {
        int totalTurnCounter = 0;
        int lastPossibleMove = 0;
        for (int i = 0; i < input.Length; i++)
        {
            if (input[i] != _empty)
            {
                totalTurnCounter++;
            }
            if (input[i] == _empty)
            {
                lastPossibleMove = i;
            }

        }
        if (totalTurnCounter == 8)
        {
            return new(true, lastPossibleMove);
        }
        else
        {
            return new(false, -1);
        }
    }
    private void analyseEnemy() // trys to stop the oponent form winning 
    {
        int enemyTurnCount = 0;
        for (int i = 0; i < input.Length; i++)
        {
            enemy[i] = (int)prioraty.unImportant; // clearing old data
            if (input[i] == _enemy)
            {
                enemyTurnCount++;
            }
        }
        if (enemyTurnCount == 0) //all moves have equal value from the perspective of countering the enemy 
        {
            return;
        }
        else if (enemyTurnCount == 1)
        {
            int pos = Array.IndexOf(input, _enemy);
            if (pos == 4) // in the middle 
            {
                enemy[0] = prioraty.important;
                enemy[2] = prioraty.important;
                enemy[6] = prioraty.important;
                enemy[8] = prioraty.important;
            }
            else
            {
                enemy[4] = prioraty.important;
            }
        }
        else
        {
            //trying to find 2  in a row
            for (int i = 0; i < 3; i++) // vertiacl lines
            {
                if (input[i] == _enemy && input[i + 3] == _enemy && input[i + 6] == _empty) // bottom field empty 
                {
                    enemy[i + 6] = prioraty.veryImportant;
                    return; // if there is a situation with 2 diffent 2 in a rows it is a lose anyways
                }
                else if (input[i] == _enemy && input[i + 3] == _empty && input[i + 6] == _enemy) //mid
                {
                    enemy[i + 3] = prioraty.veryImportant;
                    return;
                }
                else if (input[i] == _empty && input[i + 3] == _enemy && input[i + 6] == _enemy) // top
                {
                    enemy[i] = prioraty.veryImportant;
                    return;
                }
            }
            for (int i = 0; i < 9; i += 3) // horizontal lines 
            {
                if (input[i] == _enemy && input[i + 1] == _enemy && input[i + 2] == _empty) // right side 
                {
                    enemy[i + 2] = prioraty.veryImportant;
                    return;
                }
                else if (input[i] == _enemy && input[i + 1] == _empty && input[i + 2] == _enemy) // mid
                {
                    enemy[i + 1] = prioraty.veryImportant;
                    return;
                }
                else if (input[i] == _empty && input[i + 1] == _enemy && input[i + 2] == _enemy) // left side 
                {
                    enemy[i] = prioraty.veryImportant;
                    return;
                }
            }
            if (input[0] == _enemy && input[4] == _enemy && input[8] == _empty) // left top to right bottom 
            {
                enemy[8] = prioraty.veryImportant;
            }
            else if (input[0] == _enemy && input[4] == _empty && input[8] == _enemy)
            {
                enemy[4] = prioraty.veryImportant;
            }
            else if (input[0] == _empty && input[4] == _enemy && input[8] == _enemy)
            {
                enemy[0] = prioraty.veryImportant;
            }
            else if (input[6] == _enemy && input[4] == _enemy && input[2] == _empty) // left bottom to right top
            {
                enemy[2] = prioraty.veryImportant;
            }
            else if (input[6] == _enemy && input[4] == _empty && input[2] == _enemy)
            {
                enemy[4] = prioraty.veryImportant;
            }
            else if (input[6] == _empty && input[4] == _enemy && input[2] == _enemy)
            {
                enemy[6] = prioraty.veryImportant;
            }
        }
    }
    private void analyseSelf() // trys to find a way to win 
    {
        int ownMoves = 0;
        for (int i = 0; i < input.Length; i++)
        {
            if (input[i] == _self)
            {
                ownMoves++;
            }
        }
        if (ownMoves == 0)
        {
            return;
        }
        else if (ownMoves == 1)
        {
            int pos = Array.IndexOf(input, _self);
            if (pos == 4)
            {
                self[0] = prioraty.important;
                self[2] = prioraty.important;
                self[6] = prioraty.important;
                self[8] = prioraty.important;
            }
            else
            {
                self[4] = prioraty.important;
            }
        }
        else
        {
            //looking for 2 in a row 
            for (int i = 0; i < 3; i++) // vertiacl lines
            {
                if (input[i] == _self && input[i + 3] == _self && input[i + 6] == _empty) // bottom field empty 
                {
                    self[i + 6] = prioraty.veryImportant;
                    return; // if there is a situation with 2 diffent 2 in a rows it is a lose anyways
                }
                else if (input[i] == _self && input[i + 3] == _empty && input[i + 6] == _self) //mid
                {
                    self[i + 3] = prioraty.veryImportant;
                    return;
                }
                else if (input[i] == _empty && input[i + 3] == _self && input[i + 6] == _self) // top
                {
                    self[i] = prioraty.veryImportant;
                    return;
                }
            }
            for (int i = 0; i < 9; i += 3) // horizontal lines 
            {
                if (input[i] == _self && input[i + 1] == _self && input[i + 2] == _empty) // right side 
                {
                    self[i + 2] = prioraty.veryImportant;
                }
                else if (input[i] == _self && input[i + 1] == _empty && input[i + 2] == _self) // mid
                {
                    self[i + 1] = prioraty.veryImportant;
                }
                else if (input[i] == _empty && input[i + 1] == _self && input[i + 2] == _self) // left side 
                {
                    self[i] = prioraty.veryImportant;
                }
            }
            if (input[0] == _self && input[4] == _self && input[8] == _empty) // left top to right bottom 
            {
                self[8] = prioraty.veryImportant;
            }
            if (input[0] == _self && input[4] == _empty && input[8] == _self)
            {
                self[4] = prioraty.veryImportant;
            }
            if (input[0] == _empty && input[4] == _self && input[8] == _self)
            {
                self[0] = prioraty.veryImportant;
            }
            if (input[6] == _self && input[4] == _self && input[2] == _empty) // left bottom to right top
            {
                self[2] = prioraty.veryImportant;
            }
            if (input[6] == _self && input[4] == _empty && input[2] == _self)
            {
                self[4] = prioraty.veryImportant;
            }
            if (input[6] == _empty && input[4] == _self && input[2] == _self)
            {
                self[6] = prioraty.veryImportant;
            }
        }
    }
    private void evaluate() // evaluates the final move 
    {
        float[] possibleMoves = new float[9];
        for (int i = 0; i < 9; i++)
        {
            possibleMoves[i] = 0;
        }
        for (int i = 0; i < 9; i++)
        {
            possibleMoves[i] = ((float)enemy[i] + (float)self[i]) / 2;
            if (input[i] != 0)
            {
                possibleMoves[i] = (float)prioraty.NaN;
            }
        }
        finalMove = indexOfHighst(possibleMoves);
    }
    private int indexOfHighst(float[] array)
    {
        int heighst = 0;
        for (int i = 0; i < array.Length; i++)
        {
            if (array[heighst] < array[i])
            {
                heighst = i;
            }
        }
        return heighst;
    }
}