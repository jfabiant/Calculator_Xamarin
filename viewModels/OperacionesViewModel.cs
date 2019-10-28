using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Calculator.viewModels
{
    public class OperacionesViewModel : ViewModelBase
    {
        int currentState = 1;
        String mathOperator;

        double firstNumber;
        public double FirstNumber
        {
            get { return firstNumber; }
            set
            {
                if (firstNumber != value)
                {
                    firstNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        double secondNumber;
        public double SecondNumber
        {
            get { return secondNumber; }
            set
            {
                if (secondNumber != value)
                {
                    secondNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        String result;
        public String Result
        {
            get { return result; }
            set
            {
                if (result != value)
                {
                    result = value;
                    OnPropertyChanged();
                }
            }
        }

        String operation;
        public String Operation
        {
            get { return operation; }
            set
            {
                if (operation != value)
                {
                    operation = value;
                    OnPropertyChanged();
                }
            }
        }


        public ICommand OnClear { protected set; get; }
        public ICommand OnCalculate { protected set; get; }
        public ICommand OnSelectOperator { protected set; get; }
        public ICommand NumericCommand { protected set; get; }

        public OperacionesViewModel()
        {

            Result = "0";
            NumericCommand = new Command<String>(
                 execute: (String parameter) =>
                 {
                     if (Result == "0" || currentState < 0)
                     {
                         Result = "";
                         if (currentState < 0)
                             currentState *= -1;
                     }
                     Result += parameter;

                     double number;
                     if (double.TryParse(Result, out number))
                     {
                         Result = number.ToString("N0");
                         if (currentState == 1)
                         {
                             FirstNumber = number;
                         }
                         else
                         {
                             SecondNumber = number;
                         }
                     }

                 });

            OnSelectOperator = new Command<String>(
                 execute: (String parameter) =>
                 {
                     if (parameter == "+")
                     {
                         FirstNumber = Int32.Parse(Result);
                         Operation = "s";
                     }
                     if (parameter == "-")
                     {
                         FirstNumber = Int32.Parse(Result);
                         Operation = "r";
                     }
                     if (parameter == "*")
                     {
                         FirstNumber = Int32.Parse(Result);
                         Operation = "m";
                     }
                     if (parameter == "/")
                     {
                         FirstNumber = Int32.Parse(Result);
                         Operation = "d";
                     }

                     currentState = -2;
                     mathOperator = parameter;

                 });

            OnCalculate = new Command(() =>
            {
                SecondNumber = Int32.Parse(Result);
                double resultOper = 0;

                switch (Operation)
                {
                    case "s":
                        resultOper = FirstNumber + SecondNumber;
                        break;
                    case "r":
                        resultOper = FirstNumber - SecondNumber;
                        break;
                    case "m":
                        resultOper = FirstNumber * SecondNumber;
                        break;
                    case "d":
                        resultOper = FirstNumber / SecondNumber;
                        break;
                }
                Result = resultOper + "";

            });

            OnClear = new Command(() =>
            {
                Result = "0";
                FirstNumber = 0;
                SecondNumber = 0;
            });

            
        }


    }
}
