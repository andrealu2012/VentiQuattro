using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VentiQuattro
{
    class ValueandOP
    {
        public ValueandOP(List<int> vI,List<int> opI)
        {
            valueIndex = vI;
            operatorIndex = opI;
        }
      public  static List<int> TargetRawValue
        {
            get;set;
        }
       public static List<string> TargetRawOperator
        {
            get;set;
        }

        private static double GetValue(double a,string op,double b)
        {
            double value=0;
            if(op=="+")
            {
                value = a + b;
            }
            else if(op=="-")
            {
                value = a - b;
            }
            else if(op=="*")
            {
                value = a * b;
            }
            else if(op=="/")
            {
                if(b!=0)
                { value = a / b; }
                else
                {
                    value = 10000000;
                }
            }
            return value;
        }
        List<int> valueIndex;
        List<int> operatorIndex;
        bool isUseSecondMethod = false;
       public string ShowMethod
        {
            get
            {
                string message;
                if (isUseSecondMethod==false)
                {
                     message = $"(({this.RealValue[0]}{this.RealOperator[0]}{this.RealValue[1]}){this.RealOperator[1]}{this.RealValue[2]}){this.RealOperator[2]}{this.RealValue[3]}";
                }
                else
                {
                    message = $"({this.RealValue[0]}{this.RealOperator[0]}{this.RealValue[1]}){this.RealOperator[1]}({this.RealValue[2]}{this.RealOperator[2]}{this.RealValue[3]})";

                }
                return message;
            }
        }
      public  List<int> RealValue
        {
            get
            {
                int v1 = TargetRawValue[valueIndex[0] - 1];
                int v2 = TargetRawValue[valueIndex[1] - 1];
                int v3 = TargetRawValue[valueIndex[2] - 1];
                int v4 = TargetRawValue[valueIndex[3] - 1];
                List<int> V = new List<int>() { v1, v2, v3, v4 };
                return V;
            }
        }
      public  List<string> RealOperator
        {
            get
            {
                string s1 = TargetRawOperator[operatorIndex[0] - 1];
                string s2 = TargetRawOperator[operatorIndex[1] - 1];
                string s3 = TargetRawOperator[operatorIndex[2] - 1];
                List<string> Op = new List<string> { s1, s2, s3 };
                return Op;
            }
        }
      public  double Value
        {
            get
            {
                double value = 0;
                int opI = 0;
                value = RealValue[0];
                for (int vI = 1; opI < 3; opI++)
                {
                    value = GetValue(value, RealOperator[opI], RealValue[vI]);
                    vI++;
                }
                if (value == 24)
                {
                    isUseSecondMethod = false;
                    return value;
                }
                else
                {
                    value = GetValue(RealValue[0], RealOperator[0], RealValue[1]);
                    double temp = GetValue(RealValue[2], RealOperator[2], RealValue[3]);
                    value = GetValue(value, RealOperator[1], temp);
                    if(value==24)
                    {
                        isUseSecondMethod = true;
                        return value;
                    }
                }

                return value;
            }
        }

    }


    class Program
    {
        static void Main(string[] args)
        {
           // Console.WriteLine($"{args[0]},{args[1]},{args[2]},{args[3]},number is {args.Length}");

            int v1 = Convert.ToInt32(args[0]);
            int v2 = Convert.ToInt32(args[1]);
            int v3 = Convert.ToInt32(args[2]);
            int v4 = Convert.ToInt32(args[3]);

            ValueandOP.TargetRawValue = new List<int>() { v1, v2, v3, v4 };
            ValueandOP.TargetRawOperator = new List<string>() { "+", "-", "*", "/" };

            List<List<int>> valueList = GetValueList();
            List<List<int>> operationList= GetOperationList();
            ValueandOP fv;
            for (int i = 0;i<24;i++)
            {
                for (int j = 0; j < 64; j++)
                {
                    fv = new ValueandOP(valueList[i], operationList[j]);
                    double v = fv.Value;
                    if (v==24)
                    {
                        Console.WriteLine(fv.ShowMethod);
                        
                        return;
                    }
                }
            }
            Console.WriteLine("These numbers have no solution.");
        }
      
        static List<List<int>> GetOperationList()
        {
            List<List<int>> totalOpList = new List<List<int>>();

            for (int i = 1; i <=4; i++)
            {
                for (int j = 1; j <= 4; j++)
                {
                    for (int k = 1; k <=4; k++)
                    {
                        List<int> tempOp = new List<int>() { i,j,k};
                        totalOpList.Add(tempOp);
                    }
                }
            }

            return totalOpList;
        }

        static List<List<int>> GetValueList()
        {
            //1 2 3 4，1 2 4 3，1 3 2 4，1 3 4 2，1 4 2 3，1 4 3 2 

            List<List<int>> totalValueList = new List<List<int>>();
            totalValueList.Add(new List<int>() { 1, 2, 3, 4 });
            totalValueList.Add(new List<int>() { 1, 2, 4, 3 });
            totalValueList.Add(new List<int>() { 1, 3, 2, 4 });
            totalValueList.Add(new List<int>() { 1, 3, 4, 2 });
            totalValueList.Add(new List<int>() { 1, 4, 2, 3 });
            totalValueList.Add(new List<int>() { 1, 4, 3, 2 });
            //2 1 3 4，2 1 4 3，2 3 1 4，2 3 4 1，2 4 1 3，2 4 3 1
            totalValueList.Add(new List<int>() { 2, 1, 3, 4 });
            totalValueList.Add(new List<int>() { 2, 1, 4, 3 });
            totalValueList.Add(new List<int>() { 2, 3, 1, 4 });
            totalValueList.Add(new List<int>() { 2, 3, 4, 1 });
            totalValueList.Add(new List<int>() { 2, 4, 1, 3 });
            totalValueList.Add(new List<int>() { 2, 4, 3, 1 });
            //3 1 2 4，3 1 4 2，3 2 1 4，3 2 4 1，3 4 1 2，3 4 2 1

            totalValueList.Add(new List<int>() { 3, 1, 2, 4 });
            totalValueList.Add(new List<int>() { 3, 1, 4, 2 });
            totalValueList.Add(new List<int>() { 3, 2, 1, 4 });
            totalValueList.Add(new List<int>() { 3, 2, 4, 1 });
            totalValueList.Add(new List<int>() { 3, 4, 1, 2 });
            totalValueList.Add(new List<int>() { 3, 4, 2, 1 });
            //4 1 2 3，4 1 3 2，4 2 1 3，4 2 3 1，4 3 1 2，4 3 2 1

            totalValueList.Add(new List<int>() { 4, 1, 2, 3 });
            totalValueList.Add(new List<int>() { 4, 1, 3, 2 });
            totalValueList.Add(new List<int>() { 4, 2, 1, 3 });
            totalValueList.Add(new List<int>() { 4, 2, 3, 1 });
            totalValueList.Add(new List<int>() { 4, 3, 1, 2 });
            totalValueList.Add(new List<int>() { 4, 3, 2, 1 });
            return totalValueList;
        }
    }
}
