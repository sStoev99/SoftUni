string input = Console.ReadLine();

string command = Console.ReadLine();

while (command != "Easter")
{
    string[] splittedInput = command.Split();

    string action = splittedInput[0];

    switch (action)
    {
        case "Replace":
            {
                char charToReplace = char.Parse(splittedInput[1]);
                char newChar = char.Parse(splittedInput[2]);
                input = input.Replace(charToReplace, newChar);

                Console.WriteLine(input);
                break;


            }

        case "Remove":
            {
                string subStringToRemove = splittedInput[1];

                input = input.Replace(subStringToRemove, "");

                Console.WriteLine(input);
                break;


            }

        case "Includes":
            {
                bool includes = false;
                string subString = splittedInput[1];

                if (input.Contains(subString))
                {
                    includes = true;
                }

                Console.WriteLine(includes);
                break;



            }

        case "Change":
            {
                string LowerOrUpper = splittedInput[1];

                if (LowerOrUpper == "Lower")
                   input = input.ToLower();

                else 
                  input = input.ToUpper();

                Console.WriteLine(input);
                break;

            }

        case "Reverse":
            {
                int startIndex = int.Parse(splittedInput[1]);
                int endIndex = int.Parse(splittedInput[2]);

                if (startIndex < 0 || startIndex >= input.Length || endIndex < 0 || endIndex >= input.Length)
                    break;

                string substring = input.Substring(startIndex, endIndex - startIndex + 1);
                char[] charArray = substring.ToCharArray();
                Array.Reverse(charArray);
                string reversed = new string(charArray);

                Console.WriteLine(reversed);
                break;


            }
    }

    command = Console.ReadLine();



}