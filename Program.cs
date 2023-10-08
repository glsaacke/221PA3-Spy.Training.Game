//C:\Users\gavin\Documents\Schoolwork\CodingMaterials\Projects\221repo\PA3\mis221-pa3-glsaacke

// returned hours to main to quit the games. untested
//***Main
int totalHours = 0;
int wheelHours = 0;

string menuInput = RunMenu();
while(menuInput != "3" && totalHours != 6){
    totalHours = MenuLogic(menuInput, ref wheelHours);
    menuInput = RunMenu();
}

if(totalHours == 6){
    System.Console.WriteLine("Your credit hours are completed. Thanks for playing!");
}

//***End Main

//Method with menu priming & update reads
static string RunMenu(){
    System.Console.WriteLine("Hello! Please make a menu selection below:\n1. Password game\n2. Wheel game\n3. Exit");
    string menuInput = Console.ReadLine();
    return menuInput;
}

//Method making menu selection
static int MenuLogic(string x, ref int wheelHours){
    int passHours = 0;

    if(x == "1"){
        passHours = PasswordGame();
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
static int PasswordGame(){
    string check = "1";
    int passHours = 0;

    while(check == "1"){

    }
    return passHours;
    //variable.Length = number of characters in the string
    //char goes in single quotes
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