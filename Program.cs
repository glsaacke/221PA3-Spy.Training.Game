//C:\Users\gavin\Documents\Schoolwork\CodingMaterials\Projects\221repo\PA3\mis221-pa3-glsaacke


//***Main
using System.Reflection;

string[] names = new string[100];
int[] hours = new int[100];
int[] wheelHours = new int[100];
int[] passHours = new int[100];
int userCount = 0;

int userVal = LoginLogic(names, hours, wheelHours, passHours, ref userCount);

string menuInput = RunMenu();
while(menuInput != "3" && hours[userVal] != 6){
    MenuLogic(menuInput, hours, wheelHours, passHours, ref userVal);
    menuInput = RunMenu();
}

if(hours[userVal] == 6){
    System.Console.WriteLine("Your credit hours are completed. Thanks for playing!");
}

UpdateFile(names, hours, ref userCount, hours, wheelHours, passHours, ref userVal);

//***End Main

static int LoginLogic(string[] names, int[] hours, int[] wheelHours, int[] passHours, ref int userCount){

    System.Console.WriteLine("Login: Please enter your first name");
    string userName = Console.ReadLine().ToUpper();

    GetNamesFromFile(names, hours, wheelHours, passHours, ref userCount);
    int check = 0;

    int userVal = SearchUser(names, userName, ref userCount);

    for(int i = 0; i < userCount; i++){
        if(CompareNames(names[i], userName)){
            Console.ForegroundColor = ConsoleColor.Blue;
            System.Console.WriteLine("Welcome back " + userName);
            System.Console.WriteLine($"Total hours: {hours[userVal]}");
            System.Console.WriteLine($"Wheel game hours: {wheelHours[userVal]} Password cracker hours: {passHours[userVal]}");
            Console.ResetColor();
            int userNum = i;
            check++;
        }
    }

    if(check == 0){
        AddUser(names, hours,userName, ref userCount);
        Console.ForegroundColor = ConsoleColor.Blue;
        System.Console.WriteLine("New user: " + userName + "\nUser added to system!");
        Console.ResetColor();
    }

    return userVal;
}

static void GetNamesFromFile(string[] names, int[] hours, int[] wheelHours, int[] passHours, ref int userCount){

    StreamReader inFile = new StreamReader("Login.txt");
    string line = inFile.ReadLine();


    while(line != null){
        string[] temp = line.Split('#');
        names[userCount] = temp[0];
        hours[userCount] = int.Parse(temp[1]);
        wheelHours[userCount] = int.Parse(temp[2]);
        passHours[userCount] = int.Parse(temp[3]);

        userCount++;
        line = inFile.ReadLine();
    }
    inFile.Close();
}

static bool CompareNames(string name1, string name2){

      if(name1 == name2){
            return true;
        }
        else{
            return false;
        }
}

static void AddUser(string[] names, int[] hours, string userName, ref int userCount){
    userCount ++;
    names[userCount - 1] = userName;
    hours[userCount - 1] = 0;
}

static int SearchUser(string[] names, string userName, ref int userCount){
    int searchVal = 0;

    for(int i = 0; i < userCount; i ++){
        if(names[i] == userName){
            searchVal = i;
        }
    }
    return searchVal;
}
// static void AddChangeUser(string[] names, string userName, int count){
//     var result = SearchUser(names, userName, count);
//     int searchVal = result.Item1;
//     bool search = result.Item2;

//     if(search){

//     }
// }

static void UpdateFile(string[]names, int[] hours, ref int userCount, int[] totalHours, int[] wheelHours, int[] passHours, ref int userVal){
    StreamWriter outFile = new StreamWriter("Login.txt", false);
    for(int i = 0; i < userCount; i ++){
        outFile.WriteLine($"{names[i]}#{hours[i]}#{wheelHours[i]}#{passHours[i]}");
    }
    outFile.Close();
}

static void UpdateLogin(){

    

    //make array of lines, then split, change third index 
}

//Method with menu priming & update reads
static string RunMenu(){
    System.Console.WriteLine("Hello! Please make a menu selection below:\n1. Password game\n2. Wheel game\n3. Exit");
    string menuInput = Console.ReadLine();
    return menuInput;
}

//Method making menu selection
static void MenuLogic(string x, int[] hours, int[] wheelHours, int[] passHours, ref int userVal){

    if(x == "1"){
        PasswordGame(passHours, ref userVal);
    }
    else if (x == "2"){
        WheelGame(wheelHours, ref userVal);
    }
    else{
       Error();
    }
    hours[userVal] = passHours[userVal] + wheelHours[userVal];
}

//Method responsible for password game
static void PasswordGame(int[] passHours, ref int userVal){
    string check = "1";
    string check2 = "1";
    char[] blanks = new char[7];

    while(check == "1" && passHours[userVal] < 3){
        Console.Clear();
        string pass = RandomPass();
        check2 = "1";
        int check3 = 1;
        string userInput = "";

        int charCount = pass.Length;

        for(int j = 0; j < charCount; j++){
            blanks[j] = '_';
        }

        while(check2 == "1"){

            PrintPass(blanks, charCount);
            try{
                System.Console.WriteLine("\nEnter a word to guess the password");
                userInput = Console.ReadLine();//try catch
            }
            catch{
                Error();
            }
            

            CheckInput(pass, userInput, blanks);

            CheckWin(pass, blanks, ref check2);
    
        }
        passHours[userVal]++;

        if(passHours[userVal] < 3){
             System.Console.WriteLine("Enter 1 to play again, enter to quit");
            string playAgain = Console.ReadLine();
        

            if(playAgain != "1"){
                check = "0";
            }
        }
    }
    
    //variable.Length = number of characters in the string
    //char goes in single quotes

    if(passHours[userVal] >= 3){
        Console.ForegroundColor = ConsoleColor.Green;
        System.Console.WriteLine("3 hours reached. Password cracker completed!");
        Console.ResetColor();
    }
}

static void PrintPass(char[] blanks, int count){
    System.Console.WriteLine("Password: " );

    for(int i = 0; i < count; i++){
        System.Console.Write(blanks[i] + " ");
    }
    
}

static void CheckInput(string pass, string userInput, char[] blanks){
    // char[] chars = new char[pass.Length];

    
    // for(int j = 0; j <= count; j++){
    //     chars[j] = pass[j];
    // }
    int minLength = Math.Min(pass.Length, userInput.Length);

    for(int i = 0; i < minLength; i++){
        if(userInput[i] == pass[i]){
            blanks[i] = userInput[i];
        }
    }
}

static void CheckWin(string pass, char[] blanks, ref string check2){
    int count = 0;

    foreach(char x in blanks){
        if(x == '_'){
            count++;
        }
    }

    if(count == 0){
        System.Console.WriteLine("You Win!");
        check2 = "0";
    }
}

static string RandomPass(){

    string[] passwords = new string[5];
    passwords[0] = "secret";
    passwords[1] = "spy";
    passwords[2] = "hacker";
    passwords[3] = "penguin";
    passwords[4] = "herbert";

    Random rnd = new Random();
    int num = rnd.Next(5);

    return passwords[num];
}

//Method responsible for wheel game
static void WheelGame(int[] wheelHours, ref int userVal){
    string check = "1";

    while(check == "1" && wheelHours[userVal] < 3){
        Console.Clear();
        System.Console.WriteLine("Welcome to the wheel game!\n\nHere are the possible outcomes:\n1. Earn 1 credit hour\n2. Earn 2 credit hours\n3. Lose 2 credit hours\n4. Lose 1 credit hour\n5. Lose all credit hours\n6. Nothing!\n\nPress any key to spin the wheel");
        Console.ReadKey();

        Random rnd = new Random();
        int number = rnd.Next(5);

        System.Console.WriteLine("\nThe wheel landed on...\n");
        WheelLogic(number, wheelHours, ref userVal);

        System.Console.WriteLine("Wheel game credit hours: " + wheelHours[userVal] + "\n");

        System.Console.WriteLine("Enter 1 to spin again, enter to quit");
        check = Console.ReadLine();
    }
    if (wheelHours[userVal] >= 3){
        wheelHours[userVal] = 3;
        System.Console.WriteLine("Your Wheel credit hours are complete. You will be returned to the main menu.");
    }
}

//Method for coding wheel outcomes
static void WheelLogic(int number, int[] wheelHours, ref int userVal){
    Random rnd = new Random();
    int number2 = rnd.Next(2);

    switch (number){
        case 0:
            Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine("Earn 1 credit hour!");
            wheelHours[userVal] += 1;
            break;
        case 1:
            if(number2 == 0){
                Console.ForegroundColor = ConsoleColor.Green;
                System.Console.WriteLine("Earn 2 credit hours!");
                wheelHours[userVal] += 2;
            }
            else{
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("Lose 2 credit hours");
                wheelHours[userVal] -= 2;
            }
            break;
        case 2:
            Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine("Lose 1 credit hour!");
            wheelHours[userVal] -= 1;
            break;
        case 3:
            Console.ForegroundColor = ConsoleColor.DarkRed;
            System.Console.WriteLine("Lose all credit hours!");
            if(wheelHours[userVal] > 0){
                wheelHours[userVal] = 0;
            }
            else{
                wheelHours[userVal] -= 2;
                System.Console.WriteLine("(hours -2 since you don't have any to lose)");
            }
            break;
        case 4:
            Console.ForegroundColor = ConsoleColor.Blue;
            System.Console.WriteLine("Nothing happened...");
            break;
    }
    Console.ResetColor();
    System.Console.WriteLine("");
}

//Method for error message
static void Error(){
    Console.ForegroundColor = ConsoleColor.Red;
    System.Console.WriteLine("Error: please enter a valid response");
    Console.ResetColor();
}