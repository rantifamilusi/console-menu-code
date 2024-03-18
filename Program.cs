// See https://aka.ms/new-console-template for more information
//Console.ForegroundColor = ConsoleColor.Cyan;

using System.Text;

internal class Program
{
      //string black = "\u001b[30m";
      static  string red = "\u001b[31m";
      static   string green = "\u001b[32m";
      static string yellow = "\u001b[33m";
      //string blue = "\u001b[34m";
      //string magenta = "\u001b[35m";
      static  string cyan = "\u001b[36m";
      //string white = "\u001b[37m";
      static   string reset = "\u001b[0m";
      static  string arrow ="✅ ";
    private static void Main(string[] args)
    {

        Console.CancelKeyPress += new ConsoleCancelEventHandler(myHandler);
        Console.Clear();
        Console.OutputEncoding = Encoding.UTF8;
        Console.CursorVisible = false;
        var menu = BuildMenu();
   

        Console.WriteLine($"{cyan}Welcome to Illustration Automated test{reset}");

       // Console.WriteLine($"\nUse ⬆️ and ⬇️ to navigate and press {red}⮐  Enter/Return{reset} to select");

        var parent= DisplayMenu(menu);
        DisplayMenu(menu,parent);
    }

    static List<Menu> BuildMenu()
    {
        return [
          new Menu {
            Name = "Illustrations Validations",
            Functions = [
                new Function{ Name = "TC", Action="validation"},
                new Function{ Name = "CO", Action="validation"},
                new Function{ Name = "CT", Action="validation"},
                new Function{ Name = "TI", Action="validation"},
                new Function{ Name = "TT", Action="validation"},
            ]
          },
          new Menu {
            Name = "PDF",
            Functions = [
               new Function{ Name =  "Available Coverages", Action=""},
              new Function{ Name =   "Coverage Details", Action=""},
               new Function{ Name =  "Coverages Details (Multiple)", Action=""},
               new Function{ Name =  "Business Rules", Action=""},
               new Function{ Name =  "Additional Descriptions" , Action=""}
               ]
          },
          new Menu {
             Name = "Illustrations Projections",
            Functions = [
                 new Function{ Name = "Premium Only",Action=""},
                 new Function{ Name =  "Full Projection",Action=""},
                 new Function{ Name =  "FaceAmountRequest",Action=""},
                 new Function{ Name =  "Coverage Time Values",Action=""}
                 ]
          }
        ];
    }

    static int DisplayMenu(List<Menu> menu,  int ParentIndex = -1)
    {
        Console.Clear();
        Console.WriteLine($"\nUse ⬆️ and ⬇️ to navigate and press {red}⮐  Enter/Return{reset} to select. ESC to cancel");

        if (ParentIndex >= 0)
        {
            if (menu[ParentIndex].Functions == null)
            return -1;
        }

        bool isSelected = false;
        (int left, int top) = Console.GetCursorPosition();
        ConsoleKeyInfo key;
  
        int option = 1;
        var maxIndex = ParentIndex == -1 ? menu.Count : menu[ParentIndex].Functions.Count;
        
        var functions = ParentIndex == -1?null: menu[ParentIndex].Functions;

        while (!isSelected)
        {
            Console.SetCursorPosition(left,top);

            if (ParentIndex == -1)
            {
                for (int i = 0;i <maxIndex;i++)
                {
                    Console.WriteLine($"{(option == i+1 ? arrow + green: "    ")}{menu[i].Name} {reset}");
                }
            }
            else
            {
                for (int i = 0;i < maxIndex;i++)
                {
                    Console.WriteLine($"{(option == i+1 ? arrow + green: "    ")}{functions[i].Name} {reset}");
                }
            }
            
            key = Console.ReadKey(true);

            switch(key.Key)
            {
                case ConsoleKey.UpArrow:
                option = option == 1? maxIndex: option - 1;
                break;
                case ConsoleKey.DownArrow:
                option = option == maxIndex? 1: option + 1;
                break;
                case ConsoleKey.Enter:
                isSelected = true;
                break;
                case ConsoleKey.Escape:
                  Environment.Exit(0);
                break;
            }
          
        }
        if (ParentIndex != -1)
        Console.WriteLine($"You are about to execute {menu[ParentIndex].Name}/{menu[ParentIndex].Functions[option].Name} automated test in folder {menu[ParentIndex].Functions[option].Action}");
          return option-1;
    }

    protected static void myHandler(object sender, ConsoleCancelEventArgs args)
    {
        args.Cancel = true;
        Console.WriteLine($"{yellow}CANCEL command received! [Please implement clean up here]{reset}");
        Environment.Exit(0);

    }
}
