namespace Calculator;

public partial class MainPage : ContentPage
{
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

