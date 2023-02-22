namespace Calculator;

public partial class MainPage : ContentPage
{
    string firstNumber = string.Empty;
    string secondNumber = string.Empty;
    string tempResult = string.Empty;
    string mathOperator = string.Empty;
    bool operatorLock = true;
    private double ConvertToDouble(string x)
    {
        if (string.IsNullOrEmpty(x)) return 0;
        return double.Parse(x);
    }
    private string ConvertToString(double x)
    {
        return string.Format("{0:N0}", x);
    }
    private double Calc(double x, double y, string operand)
    {
        switch (operand)
        {
            case "+":
                return x + y;

            case "-":
                return x - y;
            case "*":
                return x * y;
            case "/":
                return x / y;
            default:
                return 0;
        }
    }
    private double Calc(double x,string operand)
    {
        if (operand == "+-")
            return -x;
        return 0;
    }
    private void CE_Clicked(object sender, EventArgs e)
    {
        Clear();
    }

    private void Clear()
    {
        result.Text = String.Empty;
        secondNumber = string.Empty;
        tempResult = String.Empty;
        mathOperator = string.Empty;
        operatorLock = true;
    }
    private void C_Clicked(object sender, EventArgs e)
    {
        result.Text = String.Empty;
        secondNumber = string.Empty;
        tempResult = String.Empty;
        mathOperator = string.Empty;
        operatorLock = true;
    }
    private void Dot_Clicked(object sender, EventArgs e)
    {

    }
    private void BackSpaceClicked(object sender, EventArgs e)
    {
        //יש תווים
        if (!string.IsNullOrEmpty(result.Text))
        {
            //האם התו הוא פעולה חשבונית
            char last = result.Text[result.Text.Length - 1];
            switch (last)
            {
                case '+':
                case '-':
                case '*':
                case '/':
                    mathOperator = string.Empty;
                    operatorLock = false;
                    break;
                default:
                    break;

            }
            //נקצץ את התו
            result.Text = result.Text.Substring(0, result.Text.Length - 1);
        }
    }
    private void NumberClicked(object sender, EventArgs e)
    {
        Button button = sender as Button;
        if (string.IsNullOrEmpty(this.mathOperator))
        {
            firstNumber += button.Text;
            if (result.Text != "0")
                result.Text += button.Text;
            else
                result.Text = firstNumber;
        }
        else
        {
            secondNumber += button.Text;
            result.Text += button.Text;
        }
        operatorLock = false;
    }
    private void EqualsClicked(object sender, EventArgs e)
    {
        //if(!string.IsNullOrEmpty(firstNumber)&&(!string.IsNullOrEmpty(this.secondNumber)))
        {
            //המרת שני הביטויים
            double x = ConvertToDouble(this.firstNumber);
            double y = ConvertToDouble(this.secondNumber);
        	//יש סיכוי שהפעולה החשבונית תכשל בחלוקה ב-0
            try
            {
                if (!string.IsNullOrEmpty(firstNumber) && (!string.IsNullOrEmpty(this.secondNumber)))
                    tempResult = ConvertToString(Calc(x, y, mathOperator));
                else if (!string.IsNullOrEmpty(firstNumber))
                    tempResult = ConvertToString(Calc(x, mathOperator));
                //divide 0 exception...
                if (tempResult == "∞")
                    firstNumber = string.Empty;
                else
                    firstNumber = tempResult;
                result.Text = tempResult;
            }
            catch
            {
                result.Text = "NaN";
            }
            //פעולות שנעשה ללא קשר אם קרה חריג או לא.
            finally
            {
                secondNumber = string.Empty;
                tempResult = String.Empty;
                mathOperator = string.Empty;
                operatorLock = false;
            }
        }
    }
    private void MathOperationClicked(object sender, EventArgs e)
    {
        if (!operatorLock)
        {
            Button button = sender as Button;
            if (string.IsNullOrEmpty(this.mathOperator))
            {
                mathOperator = button.Text;
                result.Text += mathOperator;
            }
            else
            {
                string op = mathOperator;
                EqualsClicked(this, null);
                mathOperator = op;
                result.Text += mathOperator;
                operatorLock = true;
            }
        }
    }

    public MainPage()
	{
		InitializeComponent();
        InitializeDigits();

    }
    private void InitializeDigits()
	{
        Button btn = null;

        int column = 0;//מספר עמודה בגריד
        int row = 2;//מספר שורה בגריד
        for (int j = 0; j < 3; j++)
        {
            column = 0;
            for (int i = 7; i <= 9; i++)
            {
                btn = CreateButton(i-3*j);//סדר עולה של שלוש מספרים עוקבים בכל שורה
                calcGrid.Add(btn, column, row);//שיוך כפתור לשורה ועמודה
                column++;
            }
            row++;
        }
        //יצירת כפתור 0
        btn = CreateButton(0);
        calcGrid.Add(btn, 1, 5);
    }
    //זהה להגדרות ב XAML
    private Button CreateButton(int i)
    {
        Button button = new Button
        {
            Text = i.ToString(),
            Background =
            new SolidColorBrush { Color = Colors.WhiteSmoke },
            TextColor = Colors.Black
        };
        button.Clicked += NumberClicked;
        return button;
    }


}

