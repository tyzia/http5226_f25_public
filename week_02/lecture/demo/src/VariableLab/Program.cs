


// Version 1

// using System;

// Console.WriteLine("Welcome to the Sum Calculator!");
// Console.WriteLine("I will ask you for 5 numbers and add them together.");

// int sum = 0;

// for (int i = 0; i <= 5; i++)
// {
//     Console.Write($"Enter number {i + 1}: ");

//     string userInput = Console.ReadLine();
//     int number = int.Parse(userInput);

//     sum += number;
// }

// Console.WriteLine("---------------------------------");
// Console.WriteLine($"The sum of the numbers you entered is: {sum}");





// Version 2 (improved)

// using System;

// Console.WriteLine("Welcome to the Sum Calculator!");
// Console.WriteLine("I will ask you for 5 numbers and add them together.");

// int sum = 0;

// for (int i = 0; i < 5; i++)
// {
//     Console.Write($"Enter number {i + 1}: ");
    
//     string? userInput = Console.ReadLine();
//     int number = 0;
//     if (!string.IsNullOrEmpty(userInput))
//     {
//         number = int.Parse(userInput);
//     }

//     sum += number;
// }

// Console.WriteLine("---------------------------------");
// Console.WriteLine($"The sum of the numbers you entered is: {sum}");









// Version 3 (more improved)

// using System;

// Console.WriteLine("Welcome to the Sum Calculator!");
// Console.WriteLine("I will ask you for 5 numbers and add them together.");

// int sum = 0;

// for (int i = 0; i < 5; i++)
// {
//     Console.Write($"Enter number {i + 1}: ");
    
//     string? userInput = Console.ReadLine();
//     if (string.IsNullOrEmpty(userInput))
//     {
//         Console.WriteLine("You didn't enter anything. Please try again.");
//         i--;
//         continue;
//     }

//     int number = int.Parse(userInput);

//     sum += number;
// }

// Console.WriteLine("---------------------------------");
// Console.WriteLine($"The sum of the numbers you entered is: {sum}");



