using System;
using System.Linq;
using System.Collections.Generic;

namespace Calculator
{
    public class Program
    {
        public static void Main(string[] args)
        {

            Console.WriteLine("Please enter only numbers and operators:");
            string input = Console.ReadLine(); // Console input 

            int input_length = input.Length;
            double answer = 0;

            //Validation to make sure user input 
            if (input_length > 0)
            {

                answer = Calculate(input); //parsing input value to method for Calculation
                Console.WriteLine("The answer is:");
                Console.WriteLine(answer);


            }
            else
            {
                Console.WriteLine("No value entered.");
            }

            answer = 0;
        }

        /*Calculation formula*/
        public static double Calculate(string s)
        {
            double result = 0;
            string[] split = s.Split(' '); //Remove spaces from the inputs and store in array

            try
            {

                //Stack is Last In First Out (LIFO)
                Stack<double> stack = new Stack<double>();

                char op = '+'; // operator without bracket/ outside of bracket
                double num = 0;
                int length = s.Length;

                double prior1 = 0; //first value in bracket
                double prior2 = 0; //second value in bracket
                char opera = '+'; // operator in bracket
                int ind = 0; // index of array
                foreach (var a in split.Select((value, i) => (value, i)).Where(p => p.i == ind))
                {
                    if (a.value == "(" && a.i == 0) //To priority calculate value input by user start with bracket
                    {
                        ind++;
                        prior1 = Convert.ToDouble(split[ind]);
                        ind++;
                        opera = split[ind].ToCharArray()[0];
                        ind++;
                        prior2 = Convert.ToDouble(split[ind]);

                        if (opera == '+') stack.Push(prior1 + prior2);
                        if (opera == '-') stack.Push(prior1 - prior2);
                        if (opera == '*') stack.Push(prior1 * prior2);
                        if (opera == '/') stack.Push(prior1 / prior2);
                        ind++;


                    }
                    else if (a.value == "(" && a.i > 0) //To priority calculate value input by user not start with bracket
                    {
                        ind++;
                        prior1 = Convert.ToDouble(split[ind]);
                        ind++;
                        opera = split[ind].ToCharArray()[0];
                        ind++;
                        prior2 = Convert.ToDouble(split[ind]);

                        if (op == '+' && opera == '+') stack.Push(stack.Pop() + (prior1 + prior2));
                        if (op == '+' && opera == '-') stack.Push(stack.Pop() + (prior1 - prior2));
                        if (op == '+' && opera == '*') stack.Push(stack.Pop() + (prior1 * prior2));
                        if (op == '+' && opera == '/') stack.Push(stack.Pop() + (prior1 / prior2));
                        if (op == '-' && opera == '+') stack.Push(stack.Pop() - (prior1 + prior2));
                        if (op == '-' && opera == '-') stack.Push(stack.Pop() - (prior1 - prior2));
                        if (op == '-' && opera == '*') stack.Push(stack.Pop() - (prior1 * prior2));
                        if (op == '-' && opera == '/') stack.Push(stack.Pop() - (prior1 / prior2));
                        if (op == '*' && opera == '+') stack.Push(stack.Pop() * (prior1 + prior2));
                        if (op == '*' && opera == '-') stack.Push(stack.Pop() * (prior1 - prior2));
                        if (op == '*' && opera == '*') stack.Push(stack.Pop() * (prior1 * prior2));
                        if (op == '*' && opera == '/') stack.Push(stack.Pop() * (prior1 / prior2));
                        if (op == '/' && opera == '+') stack.Push(stack.Pop() / (prior1 + prior2));
                        if (op == '/' && opera == '-') stack.Push(stack.Pop() / (prior1 - prior2));
                        if (op == '/' && opera == '*') stack.Push(stack.Pop() / (prior1 * prior2));
                        if (op == '/' && opera == '/') stack.Push(stack.Pop() / (prior1 / prior2));

                        ind++;
                    }
                    if (!char.IsNumber(a.value.ToCharArray()[0]) && a.value != "(" && a.value != ")")
                    {
                        op = a.value.ToCharArray()[0];
                    }
                    if (char.IsNumber(a.value.ToCharArray()[0]) && a.value != "(" && a.value != ")")
                    {

                        num = num * 10 + (Convert.ToDouble(a.value) - 0);

                        if (op == '+') stack.Push(num);
                        if (op == '-') stack.Push(-num);
                        if (op == '*') stack.Push(stack.Pop() * num);
                        if (op == '/') stack.Push(stack.Pop() / num);


                        num = 0;
                    }


                    ind++;
                }

                ind = 0;
                num = 0;
                prior1 = 0;
                prior2 = 0;

                var final_result = stack.Pop();
                while (stack.Count > 0)
                {
                    final_result += stack.Pop();
                }

                result = final_result;

                stack.Clear();
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
            }




            return result;

        }
    }
}
