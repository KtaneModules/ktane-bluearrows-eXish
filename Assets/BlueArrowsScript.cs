using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KModkit;
using System;
using System.Text.RegularExpressions;

public class BlueArrowsScript : MonoBehaviour {

    public KMAudio audio;
    public KMBombInfo bomb;

    public KMSelectable[] buttons;
    public GameObject numDisplay;

    public KMColorblindMode Colorblind;
    public GameObject colorblindText;
    private bool colorblindMode;

    private string[] moves = new string[4];
    private int current;

    private string up;
    private string down;
    private string left;
    private string right;
    private string center;

    private string priority;
    private string prevpriority;
    private List<int> usedOperations = new List<int>();

    private string[] coord1 = { "C","3","G","7","D","5","H","2" };
    private string[] coord2 = { "A","1","B","8","F","4","E","6" };
    private string coord;

    private bool animate = true;
    private bool solveAnim = false;
    private Coroutine co;

    static int moduleIdCounter = 1;
    int moduleId;
    private bool moduleSolved;

    void Awake()
    {
        current = 0;
        moduleId = moduleIdCounter++;
        moduleSolved = false;
        colorblindMode = Colorblind.ColorblindModeActive;
        foreach (KMSelectable obj in buttons){
            KMSelectable pressed = obj;
            pressed.OnInteract += delegate () { PressButton(pressed); return false; };
        }
        GetComponent<KMBombModule>().OnActivate += OnActivate;
    }

    void Start () {
        current = 0;
        priority = "";
        prevpriority = "";
        coord = "";
        up = "UP";
        down = "DO";
        left = "LE";
        right = "RI";
        center = "";
        usedOperations.Clear();
        numDisplay.GetComponent<TextMesh>().text = " ";
        generateNewCoord();
    }

    void OnActivate()
    {
        co = StartCoroutine(showText());
        if (colorblindMode)
            colorblindText.SetActive(true);
    }

    void PressButton(KMSelectable pressed)
    {
        if (moduleSolved != true && !animate)
        {
            pressed.AddInteractionPunch(0.25f);
            audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, pressed.transform);
            if (pressed == buttons[0] && !moves[current].Equals("UP"))
            {
                GetComponent<KMBombModule>().HandleStrike();
                Debug.LogFormat("[Blue Arrows #{0}] The button 'UP' was incorrect, expected '{1}'! Resetting module...", moduleId, moves[current]);
                StopCoroutine(co);
                Start();
                co = StartCoroutine(showText());
            }
            else if (pressed == buttons[1] && !moves[current].Equals("DOWN"))
            {
                GetComponent<KMBombModule>().HandleStrike();
                Debug.LogFormat("[Blue Arrows #{0}] The button 'DOWN' was incorrect, expected '{1}'! Resetting module...", moduleId, moves[current]);
                StopCoroutine(co);
                Start();
                co = StartCoroutine(showText());
            }
            else if (pressed == buttons[2] && !moves[current].Equals("LEFT"))
            {
                GetComponent<KMBombModule>().HandleStrike();
                Debug.LogFormat("[Blue Arrows #{0}] The button 'LEFT' was incorrect, expected '{1}'! Resetting module...", moduleId, moves[current]);
                StopCoroutine(co);
                Start();
                co = StartCoroutine(showText());
            }
            else if (pressed == buttons[3] && !moves[current].Equals("RIGHT"))
            {
                GetComponent<KMBombModule>().HandleStrike();
                Debug.LogFormat("[Blue Arrows #{0}] The button 'RIGHT' was incorrect, expected '{1}'! Resetting module...", moduleId, moves[current]);
                StopCoroutine(co);
                Start();
                co = StartCoroutine(showText());
            }
            else
            {
                current++;
                if (current == 4)
                {
                    moduleSolved = true;
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
            center += "A";
        }
        else if (coord.Equals("C1"))
        {
            up += "Z";
            down += "E";
            left += "A";
            right += "Y";
            center += "G";
        }
        else if (coord.Equals("CB"))
        {
            up += "X";
            down += "I";
            left += "G";
            right += "F";
            center += "Y";
        }
        else if (coord.Equals("C8"))
        {
            up += "I";
            down += "T";
            left += "Y";
            right += "J";
            center += "F";
        }
        else if (coord.Equals("CF"))
        {
            up += "M";
            down += "S";
            left += "F";
            right += "D";
            center += "J";
        }
        else if (coord.Equals("C4"))
        {
            up += "C";
            down += "R";
            left += "J";
            right += "K";
            center += "D";
        }
        else if (coord.Equals("CE"))
        {
            up += "S";
            down += "P";
            left += "D";
            right += "B";
            center += "K";
        }
        else if (coord.Equals("C6"))
        {
            up += "Q";
            down += "P";
            left += "K";
            right += "A";
            center += "B";
        }
        else if (coord.Equals("3A"))
        {
            up += "A";
            down += "J";
            left += "P";
            right += "E";
            center += "K";
        }
        else if (coord.Equals("31"))
        {
            up += "G";
            down += "O";
            left += "K";
            right += "I";
            center += "E";
        }
        else if (coord.Equals("3B"))
        {
            up += "Y";
            down += "N";
            left += "E";
            right += "T";
            center += "I";
        }
        else if (coord.Equals("38"))
        {
            up += "F";
            down += "D";
            left += "I";
            right += "S";
            center += "T";
        }
        else if (coord.Equals("3F"))
        {
            up += "J";
            down += "X";
            left += "T";
            right += "R";
            center += "S";
        }
        else if (coord.Equals("34"))
        {
            up += "D";
            down += "W";
            left += "S";
            right += "P";
            center += "R";
        }
        else if (coord.Equals("3E"))
        {
            up += "K";
            down += "I";
            left += "R";
            right += "P";
            center += "P";
        }
        else if (coord.Equals("36"))
        {
            up += "B";
            down += "T";
            left += "P";
            right += "K";
            center += "P";
        }
        else if (coord.Equals("GA"))
        {
            up += "K";
            down += "B";
            left += "T";
            right += "O";
            center += "J";
        }
        else if (coord.Equals("G1"))
        {
            up += "E";
            down += "Z";
            left += "J";
            right += "N";
            center += "O";
        }
        else if (coord.Equals("GB"))
        {
            up += "I";
            down += "Q";
            left += "O";
            right += "D";
            center += "N";
        }
        else if (coord.Equals("G8"))
        {
            up += "T";
            down += "K";
            left += "N";
            right += "X";
            center += "D";
        }
        else if (coord.Equals("GF"))
        {
            up += "S";
            down += "A";
            left += "D";
            right += "W";
            center += "X";
        }
        else if (coord.Equals("G4"))
        {
            up += "R";
            down += "U";
            left += "X";
            right += "I";
            center += "W";
        }
        else if (coord.Equals("GE"))
        {
            up += "P";
            down += "L";
            left += "W";
            right += "T";
            center += "I";
        }
        else if (coord.Equals("G6"))
        {
            up += "P";
            down += "N";
            left += "I";
            right += "J";
            center += "T";
        }
        else if (coord.Equals("7A"))
        {
            up += "J";
            down += "V";
            left += "N";
            right += "Z";
            center += "B";
        }
        else if (coord.Equals("71"))
        {
            up += "O";
            down += "S";
            left += "B";
            right += "Q";
            center += "Z";
        }
        else if (coord.Equals("7B"))
        {
            up += "N";
            down += "G";
            left += "Z";
            right += "K";
            center += "Q";
        }
        else if (coord.Equals("78"))
        {
            up += "D";
            down += "C";
            left += "Q";
            right += "A";
            center += "K";
        }
        else if (coord.Equals("7F"))
        {
            up += "X";
            down += "O";
            left += "K";
            right += "U";
            center += "A";
        }
        else if (coord.Equals("74"))
        {
            up += "W";
            down += "H";
            left += "A";
            right += "L";
            center += "U";
        }
        else if (coord.Equals("7E"))
        {
            up += "I";
            down += "H";
            left += "U";
            right += "N";
            center += "L";
        }
        else if (coord.Equals("76"))
        {
            up += "T";
            down += "Y";
            left += "L";
            right += "B";
            center += "N";
        }
        else if (coord.Equals("DA"))
        {
            up += "B";
            down += "F";
            left += "Y";
            right += "S";
            center += "B";
        }
        else if (coord.Equals("D1"))
        {
            up += "Z";
            down += "N";
            left += "V";
            right += "G";
            center += "S";
        }
        else if (coord.Equals("DB"))
        {
            up += "Q";
            down += "M";
            left += "S";
            right += "C";
            center += "G";
        }
        else if (coord.Equals("D8"))
        {
            up += "K";
            down += "P";
            left += "G";
            right += "O";
            center += "C";
        }
        else if (coord.Equals("DF"))
        {
            up += "A";
            down += "L";
            left += "C";
            right += "H";
            center += "O";
        }
        else if (coord.Equals("D4"))
        {
            up += "U";
            down += "R";
            left += "O";
            right += "H";
            center += "H";
        }
        else if (coord.Equals("DE"))
        {
            up += "L";
            down += "T";
            left += "H";
            right += "Y";
            center += "H";
        }
        else if (coord.Equals("D6"))
        {
            up += "N";
            down += "B";
            left += "H";
            right += "V";
            center += "Y";
        }
        else if (coord.Equals("5A"))
        {
            up += "V";
            down += "W";
            left += "B";
            right += "N";
            center += "V";
        }
        else if (coord.Equals("51"))
        {
            up += "S";
            down += "R";
            left += "F";
            right += "M";
            center += "N";
        }
        else if (coord.Equals("5B"))
        {
            up += "G";
            down += "E";
            left += "N";
            right += "P";
            center += "M";
        }
        else if (coord.Equals("58"))
        {
            up += "C";
            down += "U";
            left += "M";
            right += "L";
            center += "P";
        }
        else if (coord.Equals("5F"))
        {
            up += "O";
            down += "F";
            left += "P";
            right += "R";
            center += "L";
        }
        else if (coord.Equals("54"))
        {
            up += "H";
            down += "Z";
            left += "L";
            right += "T";
            center += "R";
        }
        else if (coord.Equals("5E"))
        {
            up += "H";
            down += "V";
            left += "R";
            right += "B";
            center += "T";
        }
        else if (coord.Equals("56"))
        {
            up += "Y";
            down += "O";
            left += "T";
            right += "F";
            center += "B";
        }
        else if (coord.Equals("HA"))
        {
            up += "F";
            down += "D";
            left += "O";
            right += "R";
            center += "F";
        }
        else if (coord.Equals("H1"))
        {
            up += "N";
            down += "Z";
            left += "W";
            right += "E";
            center += "R";
        }
        else if (coord.Equals("HB"))
        {
            up += "M";
            down += "X";
            left += "R";
            right += "U";
            center += "E";
        }
        else if (coord.Equals("H8"))
        {
            up += "P";
            down += "I";
            left += "E";
            right += "F";
            center += "U";
        }
        else if (coord.Equals("HF"))
        {
            up += "L";
            down += "M";
            left += "U";
            right += "Z";
            center += "F";
        }
        else if (coord.Equals("H4"))
        {
            up += "R";
            down += "C";
            left += "F";
            right += "V";
            center += "Z";
        }
        else if (coord.Equals("HE"))
        {
            up += "T";
            down += "S";
            left += "Z";
            right += "O";
            center += "V";
        }
        else if (coord.Equals("H6"))
        {
            up += "B";
            down += "Q";
            left += "V";
            right += "W";
            center += "O";
        }
        else if (coord.Equals("2A"))
        {
            up += "W";
            down += "A";
            left += "Q";
            right += "Z";
            center += "W";
        }
        else if (coord.Equals("21"))
        {
            up += "R";
            down += "G";
            left += "D";
            right += "X";
            center += "Z";
        }
        else if (coord.Equals("2B"))
        {
            up += "E";
            down += "Y";
            left += "Z";
            right += "I";
            center += "X";
        }
        else if (coord.Equals("28"))
        {
            up += "U";
            down += "F";
            left += "X";
            right += "M";
            center += "I";
        }
        else if (coord.Equals("2F"))
        {
            up += "F";
            down += "J";
            left += "I";
            right += "C";
            center += "M";
        }
        else if (coord.Equals("24"))
        {
            up += "Z";
            down += "D";
            left += "M";
            right += "S";
            center += "C";
        }
        else if (coord.Equals("2E"))
        {
            up += "V";
            down += "K";
            left += "C";
            right += "Q";
            center += "S";
        }
        else if (coord.Equals("26"))
        {
            up += "O";
            down += "B";
            left += "S";
            right += "D";
            center += "Q";
        }
        Debug.LogFormat("[Blue Arrows #{0}] Up Arrow is assigned to '{1}'! Down Arrow is assigned to '{2}'! Left Arrow is assigned to '{3}'! Right Arrow is assigned to '{4}'!", moduleId, up.ElementAt(2), down.ElementAt(2), left.ElementAt(2), right.ElementAt(2));
        makePriorityString();
    }

    private IEnumerator showText()
    {
        animate = true;
        yield return new WaitForSeconds(0.5f);
        numDisplay.GetComponent<TextMesh>().text = "" + coord[0];
        yield return new WaitForSeconds(0.5f);
        numDisplay.GetComponent<TextMesh>().text += "" + coord[1];
        animate = false;
    }

    private void generateNewCoord()
    {
        int rando = UnityEngine.Random.Range(0, 8);
        int rando2 = UnityEngine.Random.Range(0, 8);
        coord = coord1[rando] + "" + coord2[rando2];
        Debug.LogFormat("[Blue Arrows #{0}] The initial Coordinate is '{1}'!", moduleId, coord);
        getArrowLetters();
    }

    private void makePriorityString()
    {
        priority = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        int temp;
        int.TryParse(""+bomb.GetSerialNumber().ElementAt(5), out temp);
        priority = caesarShift(temp);
        Debug.LogFormat("[Blue Arrows #{0}] String Caesar Shift: "+priority, moduleId);
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
                    string tem = "" + priority.ElementAt(te);
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
                preformOperation(0);
                Debug.LogFormat("[Blue Arrows #{0}] Condition 2: " + priority, moduleId);
            }
            if ((bomb.GetBatteryCount() % 2) == 0)
            {
                preformOperation(1);
                Debug.LogFormat("[Blue Arrows #{0}] Condition 3: " + priority, moduleId);
            }
            if (!bomb.IsPortPresent("DVI"))
            {
                preformOperation(2);
                Debug.LogFormat("[Blue Arrows #{0}] Condition 4: " + priority, moduleId);
            }
            if (bomb.IsPortPresent("StereoRCA"))
            {
                preformOperation(3);
                Debug.LogFormat("[Blue Arrows #{0}] Condition 5: " + priority, moduleId);
            }
            if (isDisplayNumbers())
            {
                preformOperation(4);
                Debug.LogFormat("[Blue Arrows #{0}] Condition 6: " + priority, moduleId);
            }
            if (!((bomb.GetBatteryHolderCount() % 2) == 0))
            {
                preformOperation(5);
                Debug.LogFormat("[Blue Arrows #{0}] Condition 7: " + priority, moduleId);
            }
            if (coordLandsOnVowel())
            {
                preformOperation(6);
                Debug.LogFormat("[Blue Arrows #{0}] Condition 8: " + priority, moduleId);
            }
            if (charIsVowel(up.ElementAt(2)))
            {
                preformOperation(7);
                Debug.LogFormat("[Blue Arrows #{0}] Condition 9: " + priority, moduleId);
            }
            if (charIsVowel(down.ElementAt(2)))
            {
                preformOperation(8);
                Debug.LogFormat("[Blue Arrows #{0}] Condition 10: " + priority, moduleId);
            }
            if (isAllLetters(coord))
            {
                preformOperation(9);
                Debug.LogFormat("[Blue Arrows #{0}] Condition 11: " + priority, moduleId);
            }
        }
        getMoves();
    }

    private string caesarShift(int key)
    {
        string shifted = "";
        char[] alpha = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        for (int i = 0; i < priority.Length; i++)
        {
            int ind = Array.IndexOf(alpha, priority[i]);
            ind -= key;
            if (ind < 0)
                ind += 26;
            shifted += alpha[ind];
        }
        return shifted;
    }

    private void preformOperation(int type)
    {
        if (type == 0)
        {
            prevpriority = priority;
            string tempprior = priority;
            priority = "";
            for (int i = 25; i >= 0; i--)
            {
                priority += ("" + tempprior.ElementAt(i));
            }
        }
        else if (type == 1)
        {
            prevpriority = priority;
            string vowels = "";
            int counter = 26;
            for (int i = 0; i < counter; i++)
            {
                if (charIsVowel(priority.ElementAt(i)))
                {
                    vowels += ("" + priority.ElementAt(i));
                    priority = priority.Remove(i, 1);
                    counter--;
                    i--;
                }
            }
            priority += vowels;
        }
        else if (type == 2)
        {
            prevpriority = priority;
            string tempprior = priority;
            priority = priority.Substring(0, 13);
            for (int i = 25; i >= 13; i--)
            {
                priority += ("" + tempprior.ElementAt(i));
            }
        }
        else if (type == 3)
        {
            prevpriority = priority;
            int rind = priority.IndexOf('R');
            priority = priority.Remove(rind, 1);
            int cind = priority.IndexOf('C');
            priority = priority.Remove(cind, 1);
            int aind = priority.IndexOf('A');
            priority = priority.Remove(aind, 1);
            priority = "RCA" + priority;
        }
        else if (type == 4 || type == 9)
        {
            if (usedOperations.Count >= 1)
            {
                string temp = priority;
                priority = prevpriority;
                prevpriority = temp;
            }
        }
        else if (type == 5)
        {
            prevpriority = priority;
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
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < priority.Length; j++)
                {
                    if (priority.ElementAt(j).Equals(primes.ElementAt(i)))
                    {
                        priority = priority.Remove(j, 1);
                    }
                }
            }
            string reverse = "";
            for (int i = primes.Length - 1; i >= 0; i--)
            {
                reverse = reverse + primes.ElementAt(i);
            }
            priority = reverse + "" + priority;
        }
        else if (type == 6)
        {
            prevpriority = priority;
            int index1 = priority.IndexOf('A');
            int index2 = priority.IndexOf('E');
            string front = "";
            if (index1 < index2)
            {
                front = priority.Substring((index1 + 1), (index2 - index1 - 1));
                priority = priority.Remove((index1 + 1), (index2 - index1 - 1));
            }
            else
            {
                front = priority.Substring((index2 + 1), (index1 - index2 - 1));
                priority = priority.Remove((index2 + 1), (index1 - index2 - 1));
            }
            priority = front + "" + priority;
        }
        else if (type == 7)
        {
            prevpriority = priority;
            int temp2 = ((int)left.ElementAt(2));
            int temp_integer = 64;
            priority = caesarShift(temp2 - temp_integer);
        }
        else if (type == 8)
        {
            if (usedOperations.Count >= 1)
            {
                prevpriority = priority;
                preformOperation(usedOperations.Last());
            }
        }
        usedOperations.Add(type);
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

    private bool coordLandsOnVowel()
    {
        if (center.Contains("A") || center.Contains("E") || center.Contains("O") || center.Contains("U") || center.Contains("I"))
        {
            return true;
        }
        return false;
    }

    private bool charIsVowel(char c)
    {
        string temp = "" + c;
        if (temp.Equals("A") || temp.Equals("E") || temp.Equals("O") || temp.Equals("U") || temp.Equals("I"))
        {
            return true;
        }
        return false;
    }

    private bool isDisplayNumbers()
    {
        string num = coord;
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
        solveAnim = true;
        for (int i = 0; i < 100; i++)
        {
            int rand1 = UnityEngine.Random.Range(0, 10);
            int rand2 = UnityEngine.Random.Range(0, 10);
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
        GetComponent<KMBombModule>().HandlePass();
        solveAnim = false;
    }

    private void getMoves()
    {
        int counter = 0;
        for(int i = 0; i < 26; i++)
        {
            if (priority.ElementAt(i).Equals(up.ElementAt(2))){
                moves[counter] = "UP";
                counter++;
            }
            else if (priority.ElementAt(i).Equals(down.ElementAt(2)))
            {
                moves[counter] = "DOWN";
                counter++;
            }
            else if (priority.ElementAt(i).Equals(left.ElementAt(2)))
            {
                moves[counter] = "LEFT";
                counter++;
            }
            else if (priority.ElementAt(i).Equals(right.ElementAt(2)))
            {
                moves[counter] = "RIGHT";
                counter++;
            }
        }
        Debug.LogFormat("[Blue Arrows #{0}] The correct order of presses is: '{1}', '{2}', '{3}', and '{4}'.", moduleId, moves[0], moves[1], moves[2], moves[3]);
    }

    //twitch plays
    #pragma warning disable 414
    private readonly string TwitchHelpMessage = @"!{0} up/down/left/right [Presses the specified arrow button] | !{0} left right down up [Chain button presses] | !{0} reset [Resets the module back to the start] | Direction words can be substituted as one letter (Ex. right as r)";
    #pragma warning restore 414

    IEnumerator ProcessTwitchCommand(string command)
    {
        if (Regex.IsMatch(command, @"^\s*reset\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
        {
            yield return null;
            numDisplay.GetComponent<TextMesh>().text = " ";
            yield return new WaitForSeconds(0.5f);
            current = 0;
            numDisplay.GetComponent<TextMesh>().text = "" + coord;
            Debug.LogFormat("[Blue Arrows #{0}] Module Reset back to initial state (no inputs)!", moduleId);
            yield break;
        }

        string[] parameters = command.Split(' ');
        var buttonsToPress = new List<KMSelectable>();
        foreach (string param in parameters)
        {
            if (param.EqualsIgnoreCase("up") || param.EqualsIgnoreCase("u"))
                buttonsToPress.Add(buttons[0]);
            else if (param.EqualsIgnoreCase("down") || param.EqualsIgnoreCase("d"))
                buttonsToPress.Add(buttons[1]);
            else if (param.EqualsIgnoreCase("left") || param.EqualsIgnoreCase("l"))
                buttonsToPress.Add(buttons[2]);
            else if (param.EqualsIgnoreCase("right") || param.EqualsIgnoreCase("r"))
                buttonsToPress.Add(buttons[3]);
            else
                yield break;
        }

        yield return null;
        yield return buttonsToPress;
        if (moduleSolved) { yield return "solve"; }
    }

    IEnumerator TwitchHandleForcedSolve()
    {
        while (animate) { yield return true; yield return new WaitForSeconds(0.1f); }
        int start = current;
        for (int i = start; i < 4; i++)
        {
            if (moves[i].Equals("UP"))
                buttons[0].OnInteract();
            else if (moves[i].Equals("DOWN"))
                buttons[1].OnInteract();
            else if (moves[i].Equals("LEFT"))
                buttons[2].OnInteract();
            else if (moves[i].Equals("RIGHT"))
                buttons[3].OnInteract();
            yield return new WaitForSeconds(0.1f);
        }
        while (solveAnim) { yield return true; yield return new WaitForSeconds(0.1f); }
    }
}
