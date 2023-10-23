//C:\Users\gavin\Documents\Schoolwork\CodingMaterials\Projects\221repo\PA3\mis221-pa3-glsaacke


//***Main

using System.Data.SqlTypes;
using System.Reflection;
using System.Runtime.ExceptionServices;
using mis221_pa3_glsaacke;

const int MAX_USERS = 100;
User[] users = new User[MAX_USERS];
int userCount = 0;

int userVal = LoginLogic(users, ref userCount);

User currentUser = users[userVal];

string menuInput = RunMenu();
while(menuInput != "3" && currentUser.GetHours() != 6){
    MenuLogic(menuInput, currentUser);
    menuInput = RunMenu();
}

if(currentUser.GetHours() == 6){
    System.Console.WriteLine("Your credit hours are completed. Thanks for playing!");
}

users[userVal] = currentUser;
UpdateFile(users);

//***End Main


//EXTRA: Manages and runs the appropriate login methodology 
static int LoginLogic(User[] users, ref int userCount){

    System.Console.WriteLine("Login: Please enter your first name");
    string userName = Console.ReadLine().ToUpper();

    int count = GetUsersFromFile(users);
    int check = 0;
    int userVal = 0;
    userCount += count;

    int check2 = SearchUser(users, userName, ref userCount);
    if(check2 != 0){
        for(int i = 0; i < userCount; i++){
            if(CompareNames(users[i].GetFirstName(), userName)){
                Console.ForegroundColor = ConsoleColor.Blue;
                System.Console.WriteLine("Welcome back " + userName);
                System.Console.WriteLine($"Total hours: {users[i].GetHours()}");
                System.Console.WriteLine($"Wheel game hours: {users[i].GetWheelHours()} Password cracker hours: {users[i].GetPassHours()}");
                Console.ResetColor();
                userVal = i;
                check++;
            }
        }
    }
    else{
        userVal = AddUser(users, userName, ref userCount);
        Console.ForegroundColor = ConsoleColor.Blue;
        System.Console.WriteLine("New user: " + userName + "\nUser added to system!");
        Console.ResetColor();
    }

    return userVal;
}

//EXTRA: Imports user data from .txt file
static int GetUsersFromFile(User[] users){
    int userCount = 0;

    StreamReader inFile = new StreamReader("Login.txt");
        
        string line; //Priming read
        while ((line = inFile.ReadLine()) != null && userCount < MAX_USERS){
            
            string[] temp = line.Split('#');

            if (temp.Length >= 4)
            {
                string userName = temp[0];
                int totalHours = int.Parse(temp[1]);
                int wheelHours = int.Parse(temp[2]);
                int passHours = int.Parse(temp[3]);

                User user = new User(userName, totalHours, wheelHours, passHours);
                users[userCount] = user;
                userCount++;
            }
            //Update read in while condition
        }
    inFile.Close();

    return userCount;
}


//EXTRA: Compares user input against user data
static bool CompareNames(string name1, string name2){

      if(name1 == name2){
            return true;
        }
        else{
            return false;
        }
}

//EXTRA: Adds a new user to the system
static int AddUser(User[] users, string userName, ref int userCount){
    userCount ++;
    int userVal = userCount - 1;
    User user = new User(userName, 0, 0, 0);
    users[userVal] = user;

    return userVal;
}

//EXTRA: Searches through array to determine if a user exists
static int SearchUser(User[] users, string userName, ref int userCount){
    int check = 0;

    for (int i = 0; i < userCount; i++){
        User user = users[i];
        if (user != null && user.GetFirstName() == userName){
            check ++;
        }
    }
    return check; 
}

//EXTRA: Overwrites the .txt file with current user information
static void UpdateFile(User[] users){
    StreamWriter outFile = new StreamWriter("Login.txt", false);
    for(int i = 0; i < users.Length; i ++){
        User user = users[i];
        if(user != null){
            outFile.WriteLine($"{user.GetFirstName()}#{user.GetHours()}#{user.GetWheelHours()}#{user.GetPassHours()}");
        }
    }
    outFile.Close();
}

//Method with menu priming & update reads
static string RunMenu(){
    System.Console.WriteLine("Hello! Please make a menu selection below:\n1. Password cracker\n2. Wheel game\n3. Exit");
    string menuInput = Console.ReadLine();
    return menuInput;
}

//Method making menu selection
static void MenuLogic(string x, User currentUser){

    if(x == "1"){
        PasswordGame(currentUser);
    }
    else if (x == "2"){
        WheelGame(currentUser);
    }
    else{
       Error();
    }
    int totalHours = currentUser.GetPassHours() + currentUser.GetWheelHours();
    currentUser.SetHours(totalHours);
}

//Method responsible for password game
static void PasswordGame(User currentUser){
    string check = "1";
    string check2 = "1";
    char[] blanks = new char[7];
    string[] words = new string[100];
    int incorrectGuesses = 0;

    RulesMenu();

    while(check == "1" && currentUser.GetPassHours() < 3){
        Console.Clear();
        string pass = RandomPass();
        check2 = "1";
        string userInput = "";
        int wordsGuessed = 0;

        int charCount = pass.Length;

        for(int j = 0; j < charCount; j++){
            blanks[j] = '_';
        }

        while(check2 == "1"){
            Console.Clear();
            words[wordsGuessed] = userInput;
            PrintPass(blanks, charCount);
            PrintGuessedWords(words, wordsGuessed);

            System.Console.WriteLine("\nEnter a word to guess the password");
            userInput = Console.ReadLine();

            CheckInput(pass, userInput, blanks);

            if(CheckWin(pass, blanks)){
                Console.ForegroundColor = ConsoleColor.Green;
                System.Console.WriteLine("You Win!");
                Console.ResetColor();
                currentUser.IncrementPassHours();
                check2 = "0";
            }

            if(userInput != pass){
                incorrectGuesses++;
                if (incorrectGuesses >= 10) {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    System.Console.WriteLine("You lose!");
                    Console.ResetColor();
                    check2 = "0";
                }
            }

            wordsGuessed++;
        }
        incorrectGuesses = 0;

        if(currentUser.GetPassHours() < 3){
            System.Console.WriteLine("Enter 1 to play again, enter to quit");
            string playAgain = Console.ReadLine();
            
            if(playAgain != "1"){
                check = "0";
            }
        }
    }
    if(currentUser.GetPassHours() >= 3){
        Console.ForegroundColor = ConsoleColor.Green;
        System.Console.WriteLine("3 hours reached. Password cracker completed!");
        Console.ResetColor();
    }
}


    //variable.Length = number of characters in the string
    //char goes in single quotes

//Gives user option to read rules
static void RulesMenu(){
    System.Console.WriteLine("Welcome to Password Cracker! enter 1 to read rules or 2 to continue");
    int check = 0;
    while (check == 0){
        try{
            int userInput = int.Parse(Console.ReadLine());
            if(userInput == 1){
                System.Console.WriteLine("A series of blanks will appear on the screen representing the secret password.\nYou have 10 tries to guess the word. Letters that match the letters\nin the password will be revealed.");
                System.Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                
                check ++;
            }
            else if(userInput == 2){
                check ++;
            }
            else{
                Error();
            }

        }
        catch{
            Error();
        }
    }
}

//Prints blanks/characters matching user guesses
static void PrintPass(char[] blanks, int count){
    System.Console.WriteLine("Password: " );

    for(int i = 0; i < count; i++){
        System.Console.Write(blanks[i] + " ");
    }
    
}

//Prints guessed words
static void PrintGuessedWords(string[] words, int wordsGuessed){
    System.Console.WriteLine("\nWords guessed:");
    for(int i = 0; i <= wordsGuessed; i++){
        Console.Write(words[i] + " ");
    }
}

//Determines if user input maches characters in the password
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

//Determines if the user has sucessfully guessed the password
// Update the CheckWin method to return a boolean
static bool CheckWin(string pass, char[] blanks){
    int count = 0;

    foreach (char x in blanks){
        if (x == '_') {
            count++;
        }
    }

    return count == 0;
}

//Sets the password
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
static void WheelGame(User currentUser){
    string check = "1";

    while(check == "1" && currentUser.GetWheelHours() < 3){
        Console.Clear();
        System.Console.WriteLine("Welcome to the wheel game!\n\nHere are the possible outcomes:\n1. Earn 1 credit hour\n2. Earn 2 credit hours\n3. Lose 2 credit hours\n4. Lose 1 credit hour\n5. Lose all credit hours\n6. Nothing!\n\nPress any key to spin the wheel");
        Console.ReadKey();

        Random rnd = new Random();
        int number = rnd.Next(5);

        System.Console.WriteLine("\nThe wheel landed on...\n");
        WheelLogic(number, currentUser);

        System.Console.WriteLine("Wheel game credit hours: " + currentUser.GetWheelHours() + "\n");

        System.Console.WriteLine("Enter 1 to spin again, enter to quit");
        check = Console.ReadLine();
    }
    if (currentUser.GetWheelHours() >= 3){
        currentUser.SetWheelHours(3);
        System.Console.WriteLine("Your Wheel credit hours are complete. You will be returned to the main menu.");
    }
}

//Method for coding wheel outcomes
static void WheelLogic(int number, User currentUser){
    Random rnd = new Random();
    int number2 = rnd.Next(2);

    switch (number){
        case 0:
            Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine("Earn 1 credit hour!");
            currentUser.MathWheelHours(1);
            break;
        case 1:
            if(number2 == 0){
                Console.ForegroundColor = ConsoleColor.Green;
                System.Console.WriteLine("Earn 2 credit hours!");
                currentUser.MathWheelHours(2);
            }
            else{
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("Lose 2 credit hours");
                currentUser.MathWheelHours(-2);
            }
            break;
        case 2:
            Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine("Lose 1 credit hour!");
            currentUser.MathWheelHours(-1);
            break;
        case 3:
            Console.ForegroundColor = ConsoleColor.DarkRed;
            System.Console.WriteLine("Lose all credit hours!");
            if(currentUser.GetWheelHours() > 0){
                currentUser.SetWheelHours(0);
            }
            else{
                currentUser.MathWheelHours(-2);
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