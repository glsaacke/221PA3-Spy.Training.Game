//C:\Users\gavin\Documents\Schoolwork\CodingMaterials\Projects\221repo\PA3\mis221-pa3-glsaacke

// next: cause pass game to quit when hours reach 3 & make sure hours update in main console
//***Main
string[] names = new string[100];
int[] hours = new int[100];

LoginLogic(names, hours);

int totalHours = 0;
int wheelHours = 0;
int passHours = 0;

string menuInput = RunMenu();
while(menuInput != "3" && totalHours != 6){
    totalHours = MenuLogic(menuInput, ref wheelHours, ref passHours);
    menuInput = RunMenu();
}

if(totalHours == 6){
    System.Console.WriteLine("Your credit hours are completed. Thanks for playing!");
}

//***End Main

static void LoginLogic(string[] names, int[] hours){

    System.Console.WriteLine("Login: Please enter your first name");
    string userName = Console.ReadLine().ToUpper();

    int count = GetNamesFromFile(names, hours);
    int check = 0;

    for(int i = 0; i < count; i++){
        if(CompareNames(names[i], userName)){
            System.Console.WriteLine("Welcome back " + userName);
            int userNum = i;
            check++;
        }
    }

    if(check == 0){
        System.Console.WriteLine("New user: " + userName + "\nUser added to system!");
    }
}

static int GetNamesFromFile(string[] names, int[] hours){

    StreamReader inFile = new StreamReader("Login.txt");
    string line = inFile.ReadLine();

    int count = 0;

    while(line != null){
        string[] temp = line.Split('#');
        names[count] = temp[0];
        hours[count] = int.Parse(temp[1]);

        count++;
        line = inFile.ReadLine();
    }

    return count;
}

static bool CompareNames(string name1, string name2){
    int result = name1.CompareTo(name2);

      if(result < 0){
            return false;
        }
        else{
            return true;
        }
}

static void FindName(){

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
static int MenuLogic(string x, ref int wheelHours, ref int passHours){

    if(x == "1"){
        passHours = PasswordGame(ref passHours);
    }
    else if (x == "2"){
        wheelHours = WheelGame(ref wheelHours);
    }
    else{
       Error();
    }
    int totalHours = passHours + wheelHours;

    return totalHours;
}

//Method responsible for password game
static int PasswordGame(ref int passHours){
    string check = "1";
    string check2 = "1";
    char[] blanks = new char[7];

    while(check == "1" && passHours < 3){
        Console.Clear();
        string pass = RandomPass();
        check2 = "1";

        int charCount = pass.Length;

        for(int j = 0; j < charCount; j++){
            blanks[j] = '_';
        }

        while(check2 == "1"){

            PrintPass(blanks, charCount);

            try{
                System.Console.WriteLine("\nEnter a letter to guess the password");
                char userInput = char.Parse(Console.ReadLine());//try catch

                CheckInput(pass, userInput, blanks);

                CheckWin(pass, blanks, ref check2);
            }
            catch{
                Error();
            }
        }
        passHours++;

        if(passHours < 3){
             System.Console.WriteLine("Enter 1 to play again, enter to quit");
            string playAgain = Console.ReadLine();
        

            if(playAgain != "1"){
                check = "0";
            }
        }
    }
    
    //variable.Length = number of characters in the string
    //char goes in single quotes

    if(passHours >= 3){
        System.Console.WriteLine("3 hours reached. Password cracker completed.");
    }
    return passHours;
}

static void PrintPass(char[] blanks, int count){
    System.Console.WriteLine("Password: " );

    for(int i = 0; i < count; i++){
        System.Console.Write(blanks[i] + " ");
    }
    
}

static void CheckInput(string pass, char userInput, char[] blanks){
    // char[] chars = new char[pass.Length];

    
    // for(int j = 0; j <= count; j++){
    //     chars[j] = pass[j];
    // }
    int count2 = 0;

    while(count2 < pass.Length){
        if(userInput == pass[count2]){
            blanks[count2] = userInput;
        }
        count2++;
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
static int WheelGame(ref int wheelHours){
    string check = "1";

    while(check == "1" && wheelHours < 3){
        Console.Clear();
        System.Console.WriteLine("Welcome to the wheel game!\n\nHere are the possible outcomes:\n1. Earn 1 credit hour\n2. Earn 2 credit hours\n3. Lose 2 credit hours\n4. Lose 1 credit hour\n5. Lose all credit hours\n6. Nothing!\n\nPress any key to spin the wheel");
        Console.ReadKey();

        Random rnd = new Random();
        int number = rnd.Next(5);

        System.Console.WriteLine("\nThe wheel landed on...\n");
        wheelHours = WheelLogic(number, ref wheelHours);

        System.Console.WriteLine("Wheel game credit hours: " + wheelHours + "\n");

        System.Console.WriteLine("Enter 1 to spin again, enter to quit");
        check = Console.ReadLine();
    }
    if (wheelHours >= 3){
        wheelHours = 3;
        System.Console.WriteLine("Your Wheel credit hours are complete. You will be returned to the main menu.");
    }
    return wheelHours;
}

//Method for coding wheel outcomes
static int WheelLogic(int number, ref int wheelHours){
    Random rnd = new Random();
    int number2 = rnd.Next(2);

    switch (number){
        case 0:
            Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine("Earn 1 credit hour!");
            wheelHours += 1;
            break;
        case 1:
            if(number2 == 0){
                Console.ForegroundColor = ConsoleColor.Green;
                System.Console.WriteLine("Earn 2 credit hours!");
                wheelHours += 2;
            }
            else{
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("Lose 2 credit hours");
                wheelHours -= 2;
            }
            break;
        case 2:
            Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine("Lose 1 credit hour!");
            wheelHours -= 1;
            break;
        case 3:
            Console.ForegroundColor = ConsoleColor.DarkRed;
            System.Console.WriteLine("Lose all credit hours!");
            if(wheelHours > 0){
                wheelHours = 0;
            }
            else{
                wheelHours -= 2;
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

    return wheelHours;
}

//Method for error message
static void Error(){
    Console.ForegroundColor = ConsoleColor.Red;
    System.Console.WriteLine("Error: please enter a valid response");
    Console.ResetColor();
}