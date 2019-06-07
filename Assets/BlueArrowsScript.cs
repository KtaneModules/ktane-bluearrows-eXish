using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KModkit;
using System;

public class BlueArrowsScript : MonoBehaviour {

    public KMAudio audio;
    public KMBombInfo bomb;

    public KMSelectable[] buttons;
    public GameObject numDisplay;

    private string[] moves = new string[4];
    private int current;

    private string up;
    private string down;
    private string left;
    private string right;

    private string priority;
    private string prevpriority;

    private string[] coord1 = { "C","3","G","7","D","5","H","2" };
    private string[] coord2 = { "A","1","B","8","F","4","E","6" };
    private string coord;

    static int moduleIdCounter = 1;
    int moduleId;
    private bool moduleSolved;

    void Awake()
    {
        current = 0;
        moduleId = moduleIdCounter++;
        moduleSolved = false;
        foreach(KMSelectable obj in buttons){
            KMSelectable pressed = obj;
            pressed.OnInteract += delegate () { PressButton(pressed); return false; };
        }
    }

    void Start () {
        current = 0;
        priority = "";
        prevpriority = "";
        up = "UP";
        down = "DO";
        left = "LE";
        right = "RI";
        numDisplay.GetComponent<TextMesh>().text = " ";
        StartCoroutine(generateNewCoord());
    }

    void PressButton(KMSelectable pressed)
    {
        if(moduleSolved != true)
        {
            pressed.AddInteractionPunch(0.25f);
            audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
            if(pressed == buttons[0] && !moves[current].Equals("UP"))
            {
                GetComponent<KMBombModule>().HandleStrike();
                Debug.LogFormat("[Blue Arrows #{0}] The button 'UP' was incorrect, expected '{1}'! Resetting module...", moduleId, moves[current]);
                Start();
            }
            else if (pressed == buttons[1] && !moves[current].Equals("DOWN"))
            {
                GetComponent<KMBombModule>().HandleStrike();
                Debug.LogFormat("[Blue Arrows #{0}] The button 'DOWN' was incorrect, expected '{1}'! Resetting module...", moduleId, moves[current]);
                Start();
            }
            else if (pressed == buttons[2] && !moves[current].Equals("LEFT"))
            {
                GetComponent<KMBombModule>().HandleStrike();
                Debug.LogFormat("[Blue Arrows #{0}] The button 'LEFT' was incorrect, expected '{1}'! Resetting module...", moduleId, moves[current]);
                Start();
            }
            else if (pressed == buttons[3] && !moves[current].Equals("RIGHT"))
            {
                GetComponent<KMBombModule>().HandleStrike();
                Debug.LogFormat("[Blue Arrows #{0}] The button 'RIGHT' was incorrect, expected '{1}'! Resetting module...", moduleId, moves[current]);
                Start();
            }
            else
            {
                current++;
                if(current == 4)
                {
                    StartCoroutine(victory());
                }
            }
        }
    }

    private void getArrowLetters()
    {
        if (coord.Equals("CA"))
        {
            up += "D";
            down += "K";
            left += "B";
            right += "G";
        }else if (coord.Equals("C1"))
        {
            up += "Z";
            down += "E";
            left += "A";
            right += "Y";
        }
        else if (coord.Equals("CB"))
        {
            up += "X";
            down += "I";
            left += "G";
            right += "F";
        }
        else if (coord.Equals("C8"))
        {
            up += "I";
            down += "T";
            left += "Y";
            right += "J";
        }
        else if (coord.Equals("CF"))
        {
            up += "M";
            down += "S";
            left += "F";
            right += "D";
        }
        else if (coord.Equals("C4"))
        {
            up += "C";
            down += "R";
            left += "J";
            right += "K";
        }
        else if (coord.Equals("CE"))
        {
            up += "S";
            down += "P";
            left += "D";
            right += "B";
        }
        else if (coord.Equals("C6"))
        {
            up += "Q";
            down += "P";
            left += "K";
            right += "A";
        }else if (coord.Equals("3A"))
        {
            up += "A";
            down += "J";
            left += "P";
            right += "E";
        }
        else if (coord.Equals("31"))
        {
            up += "G";
            down += "O";
            left += "K";
            right += "I";
        }
        else if (coord.Equals("3B"))
        {
            up += "Y";
            down += "N";
            left += "E";
            right += "T";
        }
        else if (coord.Equals("38"))
        {
            up += "F";
            down += "D";
            left += "I";
            right += "S";
        }
        else if (coord.Equals("3F"))
        {
            up += "J";
            down += "X";
            left += "T";
            right += "R";
        }
        else if (coord.Equals("34"))
        {
            up += "D";
            down += "W";
            left += "S";
            right += "P";
        }
        else if (coord.Equals("3E"))
        {
            up += "K";
            down += "I";
            left += "R";
            right += "P";
        }
        else if (coord.Equals("36"))
        {
            up += "B";
            down += "T";
            left += "P";
            right += "K";
        }
        else if (coord.Equals("GA"))
        {
            up += "K";
            down += "B";
            left += "T";
            right += "O";
        }
        else if (coord.Equals("G1"))
        {
            up += "E";
            down += "Z";
            left += "J";
            right += "N";
        }
        else if (coord.Equals("GB"))
        {
            up += "I";
            down += "Q";
            left += "O";
            right += "D";
        }
        else if (coord.Equals("G8"))
        {
            up += "T";
            down += "K";
            left += "N";
            right += "X";
        }
        else if (coord.Equals("GF"))
        {
            up += "S";
            down += "A";
            left += "D";
            right += "W";
        }
        else if (coord.Equals("G4"))
        {
            up += "R";
            down += "U";
            left += "X";
            right += "I";
        }
        else if (coord.Equals("GE"))
        {
            up += "P";
            down += "L";
            left += "W";
            right += "T";
        }
        else if (coord.Equals("G6"))
        {
            up += "P";
            down += "N";
            left += "I";
            right += "J";
        }
        else if (coord.Equals("7A"))
        {
            up += "J";
            down += "V";
            left += "N";
            right += "Z";
        }
        else if (coord.Equals("71"))
        {
            up += "O";
            down += "S";
            left += "B";
            right += "Q";
        }
        else if (coord.Equals("7B"))
        {
            up += "N";
            down += "G";
            left += "Z";
            right += "K";
        }
        else if (coord.Equals("78"))
        {
            up += "D";
            down += "C";
            left += "Q";
            right += "A";
        }
        else if (coord.Equals("7F"))
        {
            up += "X";
            down += "O";
            left += "K";
            right += "U";
        }
        else if (coord.Equals("74"))
        {
            up += "W";
            down += "H";
            left += "A";
            right += "L";
        }
        else if (coord.Equals("7E"))
        {
            up += "I";
            down += "H";
            left += "U";
            right += "N";
        }
        else if (coord.Equals("76"))
        {
            up += "T";
            down += "Y";
            left += "L";
            right += "B";
        }
        else if (coord.Equals("DA"))
        {
            up += "B";
            down += "F";
            left += "Y";
            right += "S";
        }
        else if (coord.Equals("D1"))
        {
            up += "Z";
            down += "N";
            left += "V";
            right += "G";
        }
        else if (coord.Equals("DB"))
        {
            up += "Q";
            down += "M";
            left += "S";
            right += "C";
        }
        else if (coord.Equals("D8"))
        {
            up += "K";
            down += "P";
            left += "G";
            right += "O";
        }
        else if (coord.Equals("DF"))
        {
            up += "A";
            down += "L";
            left += "C";
            right += "H";
        }
        else if (coord.Equals("D4"))
        {
            up += "U";
            down += "R";
            left += "O";
            right += "H";
        }
        else if (coord.Equals("DE"))
        {
            up += "L";
            down += "T";
            left += "H";
            right += "Y";
        }
        else if (coord.Equals("D6"))
        {
            up += "N";
            down += "B";
            left += "H";
            right += "V";
        }
        else if (coord.Equals("5A"))
        {
            up += "V";
            down += "W";
            left += "B";
            right += "N";
        }
        else if (coord.Equals("51"))
        {
            up += "S";
            down += "R";
            left += "F";
            right += "M";
        }
        else if (coord.Equals("5B"))
        {
            up += "G";
            down += "E";
            left += "N";
            right += "P";
        }
        else if (coord.Equals("58"))
        {
            up += "C";
            down += "U";
            left += "M";
            right += "L";
        }
        else if (coord.Equals("5F"))
        {
            up += "O";
            down += "F";
            left += "P";
            right += "R";
        }
        else if (coord.Equals("54"))
        {
            up += "H";
            down += "Z";
            left += "L";
            right += "T";
        }
        else if (coord.Equals("5E"))
        {
            up += "H";
            down += "V";
            left += "R";
            right += "B";
        }
        else if (coord.Equals("56"))
        {
            up += "Y";
            down += "O";
            left += "T";
            right += "F";
        }
        else if (coord.Equals("HA"))
        {
            up += "F";
            down += "D";
            left += "O";
            right += "R";
        }
        else if (coord.Equals("H1"))
        {
            up += "N";
            down += "A";
            left += "W";
            right += "E";
        }
        else if (coord.Equals("HB"))
        {
            up += "M";
            down += "X";
            left += "R";
            right += "U";
        }
        else if (coord.Equals("H8"))
        {
            up += "P";
            down += "I";
            left += "E";
            right += "F";
        }
        else if (coord.Equals("HF"))
        {
            up += "L";
            down += "M";
            left += "U";
            right += "Z";
        }
        else if (coord.Equals("H4"))
        {
            up += "R";
            down += "C";
            left += "F";
            right += "V";
        }
        else if (coord.Equals("HE"))
        {
            up += "T";
            down += "S";
            left += "Z";
            right += "O";
        }
        else if (coord.Equals("H6"))
        {
            up += "B";
            down += "Q";
            left += "V";
            right += "W";
        }
        else if (coord.Equals("2A"))
        {
            up += "W";
            down += "A";
            left += "Q";
            right += "Z";
        }
        else if (coord.Equals("21"))
        {
            up += "R";
            down += "G";
            left += "D";
            right += "X";
        }
        else if (coord.Equals("2B"))
        {
            up += "E";
            down += "Y";
            left += "A";
            right += "I";
        }
        else if (coord.Equals("28"))
        {
            up += "U";
            down += "F";
            left += "X";
            right += "M";
        }
        else if (coord.Equals("2F"))
        {
            up += "F";
            down += "J";
            left += "I";
            right += "C";
        }
        else if (coord.Equals("24"))
        {
            up += "Z";
            down += "D";
            left += "M";
            right += "S";
        }
        else if (coord.Equals("2E"))
        {
            up += "V";
            down += "K";
            left += "C";
            right += "Q";
        }
        else if (coord.Equals("26"))
        {
            up += "O";
            down += "B";
            left += "S";
            right += "D";
        }
        Debug.LogFormat("[Blue Arrows #{0}] Up Arrow is assigned to '{1}'! Down Arrow is assigned to '{2}'! Left Arrow is assigned to '{3}'! Right Arrow is assigned to '{4}'!", moduleId, up.ElementAt(2), down.ElementAt(2), left.ElementAt(2), right.ElementAt(2));
        makePriorityString();
    }

    private IEnumerator generateNewCoord()
    {
        yield return null;
        int rando = UnityEngine.Random.RandomRange(0, 8);
        int rando2 = UnityEngine.Random.RandomRange(0, 8);
        coord = coord1[rando] + "" + coord2[rando2];
        Debug.LogFormat("[Blue Arrows #{0}] The initial Coordinate is '{1}'!", moduleId, coord);
        yield return new WaitForSeconds(0.5f);
        numDisplay.GetComponent<TextMesh>().text = "" + coord1[rando];
        yield return new WaitForSeconds(0.5f);
        numDisplay.GetComponent<TextMesh>().text += "" + coord2[rando2];
        StopCoroutine("generateNewCoord");
        getArrowLetters();
    }

    private void makePriorityString()
    {
        priority = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        int temp;
        int.TryParse(""+bomb.GetSerialNumber().ElementAt(5), out temp);
        for (int i = 0; i < temp; i++)
        {
            priority = priority.ElementAt(25) + "" + priority.Substring(0,25);
        }
        Debug.LogFormat("[Blue Arrows #{0}] Caesar Shift: "+priority, moduleId);
        for (int i = 0; i < 6; i++)
        {
            if (priority.Contains(("" + bomb.GetSerialNumber().ElementAt(i))))
            {
                int te = priority.IndexOf(("" + bomb.GetSerialNumber().ElementAt(i)));
                if (te == 0)
                {
                    string tem = "" + priority.ElementAt(0);
                    priority = priority.Remove(0, 1);
                    priority += tem;
                }
                else
                {
                    string tem = ""+priority.ElementAt(te);
                    priority = priority.Remove(te,1);
                    priority = tem + "" + priority.Substring(0, 25);
                }
                break;
            }
        }
        Debug.LogFormat("[Blue Arrows #{0}] Move First Letter: " + priority, moduleId);
        if (bomb.IsIndicatorOn("BOB") && (bomb.GetBatteryCount() == 0) && (bomb.GetPortPlateCount() == 0) && noUnlitIndicators() && serialContainsVowel())
        {
            priority = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            Debug.LogFormat("[Blue Arrows #{0}] Condition 1: " + priority, moduleId);
        }
        else
        {
            if (bomb.IsIndicatorOn("BOB"))
            {
                string tempprior = priority;
                priority = "";
                for (int i = 25; i >= 0; i--)
                {
                    priority += (""+tempprior.ElementAt(i));
                }
                Debug.LogFormat("[Blue Arrows #{0}] Condition 2: " + priority, moduleId);
            }
            if ((bomb.GetBatteryCount() % 2) == 0)
            {
                string vowels = "";
                int counter = 26;
                for (int i = 0; i < counter; i++)
                {
                    if (charIsVowel(priority.ElementAt(i)))
                    {
                        vowels += (""+priority.ElementAt(i));
                        priority = priority.Remove(i, 1);
                        counter--;
                        i--;
                    }
                }
                priority += vowels;
                Debug.LogFormat("[Blue Arrows #{0}] Condition 3: " + priority, moduleId);
            }
            if (!bomb.IsPortPresent("DVI"))
            {
                string tempprior = priority;
                priority = priority.Substring(0, 13);
                for (int i = 25; i >= 13; i--)
                {
                    priority += (""+tempprior.ElementAt(i));
                }
                Debug.LogFormat("[Blue Arrows #{0}] Condition 4: " + priority, moduleId);
            }
            prevpriority = priority;
            bool doneaction = false;
            if (bomb.IsPortPresent("StereoRCA"))
            {
                int rind = priority.IndexOf('R');
                priority = priority.Remove(rind, 1);
                int cind = priority.IndexOf('C');
                priority = priority.Remove(cind, 1);
                int aind = priority.IndexOf('A');
                priority = priority.Remove(aind, 1);
                priority = "RCA" + priority;
                doneaction = true;
                Debug.LogFormat("[Blue Arrows #{0}] Condition 5: " + priority, moduleId);
            }
            if (isDisplayNumbers() && doneaction == true)
            {
                priority = prevpriority;
                Debug.LogFormat("[Blue Arrows #{0}] Condition 6: " + priority, moduleId);
            }
            if (!((bomb.GetBatteryHolderCount() % 2) == 0))
            {
                string primes = "";
                primes += ("" + priority.ElementAt(1));
                primes += ("" + priority.ElementAt(2));
                primes += ("" + priority.ElementAt(4));
                primes += ("" + priority.ElementAt(6));
                primes += ("" + priority.ElementAt(10));
                primes += ("" + priority.ElementAt(12));
                primes += ("" + priority.ElementAt(16));
                primes += ("" + priority.ElementAt(18));
                primes += ("" + priority.ElementAt(22));
                for(int i = 0; i < 9; i++)
                {
                    for(int j = 0; j < priority.Length; j++)
                    {
                        if (priority.ElementAt(j).Equals(primes.ElementAt(i)))
                        {
                            priority = priority.Remove(j,1);
                        }
                    }
                }
                string reverse = "";
                for (int i = primes.Length - 1; i >= 0; i--)
                {
                    reverse = reverse + primes.ElementAt(i);
                }
                priority = reverse + "" + priority;
                Debug.LogFormat("[Blue Arrows #{0}] Condition 7: " + priority, moduleId);
            }
            if (bomb.GetSolvableModuleNames().Contains("Yellow Arrows") || bomb.GetSolvableModuleNames().Contains("Green Arrows") || bomb.GetSolvableModuleNames().Contains("The Sphere"))
            {
                int index1 = priority.IndexOf('A');
                int index2 = priority.IndexOf('E');
                string front = "";
                if(index1 < index2)
                {
                    front = priority.Substring((index1+1),(index2-index1-1));
                    priority = priority.Remove((index1+1),(index2-index1-1));
                }
                else
                {
                    front = priority.Substring((index2+1), (index1 - index2 - 1));
                    priority = priority.Remove((index2+1), (index1 - index2 - 1));
                }
                priority = front + "" + priority;
                Debug.LogFormat("[Blue Arrows #{0}] Condition 8: " + priority, moduleId);
            }
            if (charIsVowel(up.ElementAt(2)))
            {
                int temp2 = ((int)left.ElementAt(2));
                int temp_integer = 64;
                for (int i = 0; i < (temp2-temp_integer); i++)
                {
                    priority = priority.ElementAt(25) + "" + priority.Substring(0, 25);
                }
                Debug.LogFormat("[Blue Arrows #{0}] Condition 9: " + priority, moduleId);
            }
            if (charIsVowel(down.ElementAt(2)))
            {
                string reverse = priority.Substring(0,13);
                string reversebuild = "";
                for (int i = reverse.Length - 1; i >= 0; i--)
                {
                    reversebuild = reversebuild + reverse.ElementAt(i);
                }
                priority = priority.Remove(0,13);
                priority = reversebuild + "" + priority;
                Debug.LogFormat("[Blue Arrows #{0}] Condition 10: " + priority, moduleId);
            }
            if (isAllLetters(numDisplay.GetComponent<TextMesh>().text))
            {
                int indexX = priority.IndexOf('X');
                priority = priority.Remove(indexX, 1);
                priority += "X";
                Debug.LogFormat("[Blue Arrows #{0}] Condition 11: " + priority, moduleId);
            }
        }
        getMoves();
    }

    private bool isAllLetters(String name)
    {
        char[] chars = name.ToCharArray();

        foreach (char c in chars)
        {
            if (!Char.IsLetter(c))
            {
                return false;
            }
        }

        return true;
    }

    private bool noUnlitIndicators()
    {
        if(!bomb.IsIndicatorOff("BOB") && !bomb.IsIndicatorOff("SND") && !bomb.IsIndicatorOff("SIG") && !bomb.IsIndicatorOff("CAR") && !bomb.IsIndicatorOff("CLR") && !bomb.IsIndicatorOff("FRK") && !bomb.IsIndicatorOff("FRQ") && !bomb.IsIndicatorOff("IND") && !bomb.IsIndicatorOff("MSA") && !bomb.IsIndicatorOff("NSA") && !bomb.IsIndicatorOff("TRN"))
        {
            return true;
        }
        return false;
    }

    private bool serialContainsVowel()
    {
        if (bomb.GetSerialNumber().Contains("A") || bomb.GetSerialNumber().Contains("E") || bomb.GetSerialNumber().Contains("O") || bomb.GetSerialNumber().Contains("U") || bomb.GetSerialNumber().Contains("I"))
        {
            return true;
        }
        return false;
    }

    private bool charIsVowel(char c)
    {
        string temp = "" + c;
        if(temp.Equals("A") || temp.Equals("E") || temp.Equals("O") || temp.Equals("U") || temp.Equals("I"))
        {
            return true;
        }
        return false;
    }

    private bool isDisplayNumbers()
    {
        string num = numDisplay.GetComponent<TextMesh>().text;
        char part1 = num.ElementAt(0);
        char part2 = num.ElementAt(1);
        if (part1.Equals('0') || part1.Equals('1') || part1.Equals('2') || part1.Equals('3') || part1.Equals('4') || part1.Equals('5') || part1.Equals('6') || part1.Equals('7') || part1.Equals('8') || part1.Equals('9'))
        {
            if (part2.Equals('0') || part2.Equals('1') || part2.Equals('2') || part2.Equals('3') || part2.Equals('4') || part2.Equals('5') || part2.Equals('6') || part2.Equals('7') || part2.Equals('8') || part2.Equals('9'))
            {
                return true;
            }
        }
        return false;
    }

    private IEnumerator victory()
    {
        yield return null;
        for (int i = 0; i < 100; i++)
        {
            int rand1 = UnityEngine.Random.RandomRange(0, 10);
            int rand2 = UnityEngine.Random.RandomRange(0, 10);
            if (i < 50)
            {
                numDisplay.GetComponent<TextMesh>().text = rand1 + "" + rand2;
            }
            else
            {
                numDisplay.GetComponent<TextMesh>().text = "G" + rand2;
            }
            yield return new WaitForSeconds(0.025f);
        }
        numDisplay.GetComponent<TextMesh>().text = "GG";
        StopCoroutine("victory");
        GetComponent<KMBombModule>().HandlePass();
        moduleSolved = true;
    }

    private void getMoves()
    {
        int counter = 0;
        for(int i = 0; i < 26; i++)
        {
            if (priority.ElementAt(i).Equals(up.ElementAt(2))){
                moves[counter] = "UP";
                counter++;
            }else if (priority.ElementAt(i).Equals(down.ElementAt(2)))
            {
                moves[counter] = "DOWN";
                counter++;
            }else if (priority.ElementAt(i).Equals(left.ElementAt(2)))
            {
                moves[counter] = "LEFT";
                counter++;
            }else if (priority.ElementAt(i).Equals(right.ElementAt(2)))
            {
                moves[counter] = "RIGHT";
                counter++;
            }
        }
        Debug.LogFormat("[Blue Arrows #{0}] The correct order of presses is: '{1}', '{2}', '{3}', and '{4}'.", moduleId, moves[0], moves[1], moves[2], moves[3]);
    }

    //twitch plays
    #pragma warning disable 414
    private readonly string TwitchHelpMessage = @"!{0} up [Presses the up arrow button] | !{0} right [Presses the right arrow button] | !{0} down [Presses the down arrow button once] | !{0} left [Presses the left arrow button once] | !{0} left right down up [Chain button presses] | !{0} reset [Resets the module back to the start] | Direction words can be substituted as one letter (Ex. right as r)";
    #pragma warning restore 414

    IEnumerator ProcessTwitchCommand(string command)
    {
        string[] parameters = command.Split(' ');
        foreach (string param in parameters)
        {
            yield return null;
            if (param.EqualsIgnoreCase("up") || param.EqualsIgnoreCase("u"))
            {
                buttons[0].OnInteract();
                yield return new WaitForSeconds(0.1f);
            }
            else if (param.EqualsIgnoreCase("down") || param.EqualsIgnoreCase("d"))
            {
                buttons[1].OnInteract();
                yield return new WaitForSeconds(0.1f);
            }
            else if (param.EqualsIgnoreCase("left") || param.EqualsIgnoreCase("l"))
            {
                buttons[2].OnInteract();
                yield return new WaitForSeconds(0.1f);
            }
            else if (param.EqualsIgnoreCase("right") || param.EqualsIgnoreCase("r"))
            {
                buttons[3].OnInteract();
                yield return new WaitForSeconds(0.1f);
            }
            else if (param.EqualsIgnoreCase("reset"))
            {
                numDisplay.GetComponent<TextMesh>().text = " ";
                yield return new WaitForSeconds(0.5f);
                current = 0;
                numDisplay.GetComponent<TextMesh>().text = "" + coord;
            }
            else
            {
                break;
            }
        }
    }
}
