
//***Main

string menuInput = RunMenu();
while(menuInput != "3"){
    MenuLogic(menuInput);
    menuInput = RunMenu();
}

//***End Main

//Method with menu priming read
static string RunMenu(){
    System.Console.WriteLine("Hello! Please make a menu selection below:\n1. Password game\n2. Wheel game\n3. Exit");
    string menuInput = Console.ReadLine();
    return menuInput;
}

//Method making menu selection
static void MenuLogic(string x){
    if(x == "1"){
        PasswordGame();
    }
    else if (x == "2"){
        WheelGame();
    }
    else{
       Error();
    }
}

//Method responsible for password game
static void PasswordGame(){

}

//Method responsible for wheel game
static void WheelGame(){
    string check = "1";
    int wheelHours = 0;

    while(check == "1"){
        Console.Clear();
        System.Console.WriteLine("Welcome to the wheel game!\n\nHere are the possible outcomes:\n1. Earn 1 credit hour\n2. Earn 2 credit hours\n3. Lose 2 credit hours\n4. Lose 1 credit hour\n5. Lose all credit hours\n6. Nothing!\n\nPress any key to spin the wheel");
        Console.ReadKey();

        Random rnd = new Random();
        int number = rnd.Next(5);

        System.Console.WriteLine("\nThe wheel landed on...\n");
        wheelHours = WheelLogic(number, wheelHours);

        System.Console.WriteLine("Wheel game credit hours: " + wheelHours + "\n");

        System.Console.WriteLine("Enter 1 to spin again, enter to quit");
        check = Console.ReadLine();
    }
}

//Method for coding wheel outcomes
static int WheelLogic(int number, int hours){
    Random rnd = new Random();
    int number2 = rnd.Next(2);

    switch (number){
        case 0:
            Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine("Earn 1 credit hour!");
            hours += 1;
            break;
        case 1:
            if(number2 == 0){
                Console.ForegroundColor = ConsoleColor.Green;
                System.Console.WriteLine("Earn 2 credit hours!");
                hours += 2;
            }
            else{
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("Lose 2 credit hours");
                hours -= 2;
            }
            break;
        case 2:
            Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine("Lose 1 credit hour!");
            hours -= 1;
            break;
        case 3:
            Console.ForegroundColor = ConsoleColor.DarkRed;
            System.Console.WriteLine("Lose all credit hours!");
            if(hours > 0){
                hours = 0;
            }
            else{
                hours -= 2;
            }
            break;
        case 4:
            Console.ForegroundColor = ConsoleColor.Blue;
            System.Console.WriteLine("Nothing happened...");
            break;
    }
    Console.ResetColor();
    System.Console.WriteLine("");

    return hours;
}

//Method for error message
static void Error(){
    Console.ForegroundColor = ConsoleColor.Red;
    System.Console.WriteLine("Error: please enter a valid response");
    Console.ResetColor();
}