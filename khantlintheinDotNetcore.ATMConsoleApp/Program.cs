using System;
using System.Data;
using System.Xml.Serialization;

public class cardHolder
{
    String cardNumber;
    int pinNumber;
    String firstName;
    String lastName;
    double balance;


    public cardHolder(string cardNumber, int pinNumber, string firstName, string lastName, double balance)
    {
        this.cardNumber = cardNumber;
        this.pinNumber = pinNumber;
        this.firstName = firstName;
        this.lastName = lastName;
        this.balance = balance;
    }


    public String getNum()
    {
        return cardNumber;
    }

    public int getPinNumber()
    {
        return pinNumber;
    }

    public String getFirstName()
    {
        return firstName;
    }

    public String getLastName()
    {
        return lastName;
    }

    public double getBalance()
    {
        return balance;
    }

    public void setNum(String newCardNumber)
    {
        cardNumber = newCardNumber;
    }

    public void setPin(int newPinNumber)
    {
        pinNumber = newPinNumber;
    }

    public void setFirstName(String newFirstName)
    {
        firstName = newFirstName;
    }

    public void setLastName(String newLastName)
    {
        lastName = newLastName;
    }

    public void setBalance(double newBalance)
    {
        balance = newBalance;
    }

    public static void Main(String[] args)
    {
        void printOptions()
        {
            Console.WriteLine("Please choose one options from these....");
            Console.WriteLine("1,Deposit");
            Console.WriteLine("2,Withdraw");
            Console.WriteLine("3,Show Balance");
            Console.WriteLine("4,Exit");
        }

        void depositOption(cardHolder currentUser)
        {
            Console.WriteLine("How much $ would you like to deposit?");
            double deposit = Double.Parse(Console.ReadLine()!);
            currentUser.setBalance(deposit);
            Console.WriteLine("Thank you for your $$. Your new balance is: " + currentUser.getBalance());
        }

        void withdrawOption(cardHolder currentUser)
        {
            Console.WriteLine("How much $$ would you like to withdraw?");
            double withdraw = Double.Parse(Console.ReadLine()!);

            // Check if the user has enough money.
            if(currentUser.getBalance() > withdraw)
            {
                Console.WriteLine("Insufficient balance :( ");
            }else
            {
                currentUser.setBalance(currentUser.getBalance() - withdraw);
                Console.WriteLine("Thank you so much :) ");
            }
        }

        void balanceOption(cardHolder currentUser)
        {
            Console.WriteLine("Current balance " + currentUser.getBalance());
        }

        List<cardHolder> cardHolders = new List<cardHolder>();
        cardHolders.Add(new cardHolder("33324078050345", 3345, "James","Thomas", 150.33));
        cardHolders.Add(new cardHolder("89789745953433", 4456, 
            "Min", "Khant", 33.34));
        cardHolders.Add(new cardHolder("98789343846973", 9878, "Lemon", "Thee", 563.45));
        cardHolders.Add(new cardHolder("986863459654343", 9863, "Sandi", "Soe", 44.45));
        cardHolders.Add(new cardHolder("988734875823889", 8763, "Lin", "Khant", 450.32));

        // Prompt User 
        Console.WriteLine("Welcome Love ATM");
        Console.WriteLine("Take out your card in your jeans.");
        Console.WriteLine("Please insert your debit/credit card:");
        String debitCardNumber = "";
        cardHolder currentUser;
        
        while (true)
        {
            try
            {
                debitCardNumber = Console.ReadLine()!;
                // Check against our db
                currentUser = cardHolders.FirstOrDefault(a => a.cardNumber == debitCardNumber)!;
                if(currentUser != null)
                {
                    break;
                } 
                else
                {
                    Console.WriteLine("Card not recognized.Please try again.");
                }
            } 
            catch
            {
                Console.WriteLine("Card not recognized. Please try again.");
            }
        }

        // Pin Number 
        Console.WriteLine("Please enter your pin:");
        int usesrPin = 0;
        int maxAttempts = 3;
        int incorrectAttempts = 0;

        while (true)
        {
            try
            {
                usesrPin = int.Parse(Console.ReadLine()!);
                if (currentUser.getPinNumber() == usesrPin) { break; }
                incorrectAttempts++;
                Console.WriteLine($"Wrong pin. {maxAttempts - incorrectAttempts} attempts remaining. Please try again!");
            }
            catch
            {
                Console.WriteLine("Invalid input. Please enter a numeric pin.");
            }
            if (incorrectAttempts == maxAttempts)
            {
                Console.WriteLine("Too many incorrect attempts. Your account is locked.");
            }
        }


        // UserName

        Console.WriteLine("Welcome" + currentUser.getFirstName());
        int option = 0;

        do
        {
            printOptions();
            try
            {
                option = int.Parse(Console.ReadLine()!);
            }
            catch
            {

            }

            if(option == 1)
            {
                depositOption(currentUser);
            }
            else if(option == 2)
            {
                withdrawOption(currentUser);
            }
            else if(option == 3)
            {
                balanceOption(currentUser);
            }else if(option == 4)
            {
                break;
            }else
            {
                option = 0;
            }
        }
        while (option != 4);
        Console.WriteLine("Thank You! Have a nice day.");

    }
}
