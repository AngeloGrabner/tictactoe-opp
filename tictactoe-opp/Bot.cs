using System;
internal class Bot
{
    private int[] f = new int[0]; // is the tic tac toe field 
    private float _errorRate;
    private int counterMovePosition; // -1 = no counter move 

    public Bot(int difficulty = 1) // difficulty 1 to 5 (easy to hard) 
    {
        _errorRate = (5 - difficulty)/5;
        counterMovePosition = -1;
    }
    public void setState(int[] state)
    {
        f = state;
    }
    public int getTurn()
    {
        analyseEnemy();
        analyseSelf();
        return 0;
    }
    private void analyseEnemy() // trys to stop the oponent form winning 
    {
        int enemyTurns = 0;
        for (int i = 0; i < f.Length; i++)
        {
            if (f[i] == 2)
            {
                enemyTurns++;
            }
        }
        if (enemyTurns == 0)
        {
            counterMovePosition = -1;
            return;
        }
        else if (enemyTurns == 1) // look for most valueable move 
        {
            if (f[4] == 0) // take mide if it is not taken
            {
                counterMovePosition = 4;
            }
        }
        


    }
    private void analyseSelf() // trys to find a way to win 
    {

    }
}