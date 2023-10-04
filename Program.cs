
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

static void PasswordGame(){

}

static void WheelGame(){
    Console.Clear();
    System.Console.WriteLine("Welcome to the wheel game!\nPress any key to spin the wheel");
    Console.ReadKey();

    Random rnd = new Random();
    int number = rnd.Next(6);

    WheelLogic();
}

static void WheelLogic(int number){
    switch number{
        case 0:
            break;
        case 1:
            System.Console.WriteLine();
            break;
        case 2:
            System.Console.WriteLine();
            break;
        case 3:
            System.Console.WriteLine();
            break;
        case 4:
            System.Console.WriteLine();
            break;
    }

}

static void Error(){
        Console.ForegroundColor = ConsoleColor.Red;
        System.Console.WriteLine("Error: please enter a valid response");
        Console.ResetColor();
}