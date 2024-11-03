public class TTriangle
{
    protected double a; //дозволяє викорстовувати тільки в класі і нащадку
    protected double b;
    protected double c;

    //для отримання значень сторін в перевант.
    //public double A => a;
    //public double B => b;
    //public double C => c;
    public double A
    {
        get { return a; }
    }

    public double B
    {
        get { return b; }
    }

    public double C
    {
        get { return c; }
    }

    // конструктор за замовчуванням
    public TTriangle() //початкові значення трикутника/призми доки не будуть присвоєні нові
    {
        a = 1;
        b = 1;
        c = 1;
    }

    // конструктор з параметрами+if перевірка можливості трикутника
    public TTriangle(double a, double b, double c) //присвоєння значень
    {
        if (IsValidTriangle(a, b, c)) //+перевірка
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }
        else
        {
            throw new ArgumentException("Неможливо створити трикутник з такими сторонами.");
        }
    }

    // конструктор копіювання
    public TTriangle(TTriangle other) //перший трикутник залиш. в other
    {
        this.a = other.a;
        this.b = other.b;
        this.c = other.c;
    }

    // перевірка можливості трикутника
    private bool IsValidTriangle(double a, double b, double c)
    {
        return (a + b > c) && (a + c > b) && (b + c > a);
    }

    // рядкове представлення трикутника
    public override string ToString()
    {
        return $"Трикутник зі сторонами: a = {a}, b = {b}, c = {c}";
    }

    // ввід сторін з консолі
    public void InputSides()
    {
        Console.WriteLine("Введіть сторони трикутника:");
        a = Convert.ToDouble(Console.ReadLine());
        b = Convert.ToDouble(Console.ReadLine());
        c = Convert.ToDouble(Console.ReadLine());

        //перевірка
        if (!IsValidTriangle(a, b, c))
        {
            throw new ArgumentException("Неможливо створити трикутник з такими сторонами.");
        }
    }

    // виведення сторін + периметра + площі
    public void DisplaySides()
    {
        Console.WriteLine(ToString());
        Console.WriteLine($"Периметр трикутника: {GetPerimeter()}");
        Console.WriteLine($"Площа трикутника: {GetArea()}");
    }

    // обчислення площі трикутника за ф. Герона
    public double GetArea()
    {
        double p = GetPerimeter() / 2;
        return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
    }

    // обчислення периметра трикутника
    public double GetPerimeter()
    {
        return a + b + c;
    }

    // порівняння трикутників за сторонами
    public virtual bool Equals(TTriangle other) //в параметрі об'єкт з яким буде порівнюватись поточний
    {
        if (ReferenceEquals(other, null)) //чи є взагалі об'єкт
        {
            return false;
        }
        return (this.a == other.a && this.b == other.b && this.c == other.c);
    }
    //з оверрайдом щоб однакові екз. мали однаковий хешкод
    public override int GetHashCode()
    {
        return HashCode.Combine(a, b, c);
    }

    //
    public override bool Equals(object obj)
    {
        if (obj is TTriangle other) //якщо є екземпляром
        {
            return this.a == other.a && this.b == other.b && this.c == other.c;
        }
        return false;
    }
    // перевірка нерівності трикутників
    public virtual bool NotEquals(TTriangle other)
    {
        return !Equals(other);
    }

    // перевантаження оператора множення (трикутник * число)
    public static TTriangle operator *(TTriangle triangle, double factor)   //фактор це просто змінна під множник...
    {
        return new TTriangle(triangle.a * factor, triangle.b * factor, triangle.c * factor);
    }

    // перевантаження оператора множення (число * трикутник)
    public static TTriangle operator *(double factor, TTriangle triangle)
    {
        return new TTriangle(triangle.a * factor, triangle.b * factor, triangle.c * factor);
    }
}
// // // // // // // //
public class TTrianglePrism : TTriangle
{
    private double height;

    // конструктор за замовчуванням, ініціалізує трикутник-призму
    public TTrianglePrism() : base()
    {
        height = 1;
    }

    // конструктор з параметрами для трикутника + h призми
    public TTrianglePrism(double a, double b, double c, double height) : base(a, b, c)
    {
        if (height <= 0)
        {
            throw new ArgumentException("Висота має бути більшою за нуль.");
        }
        this.height = height;
    }

    // конструктор копіювання для трикутної призми
    public TTrianglePrism(TTrianglePrism other) : base(other)
    {
        this.height = other.height;
    }

    // перевизначення ToString() для відображення трикутника-призми
    public override string ToString()
    {
        return base.ToString() + $", висота призми = {height}";
    }

    // введення сторін трикутника та висоти призми
    public new void InputSides()
    {
        base.InputSides(); // викликаємо метод батьківського класу для введення сторін трикутника
        Console.WriteLine("Введіть висоту призми:");
        height = Convert.ToDouble(Console.ReadLine());
        if (height <= 0)
        {
            throw new ArgumentException("Висота має бути більшою за нуль.");
        }
        DisplaySides();
    }

    // відображення властивостей трикутника-призми
    public new void DisplaySides()
    {
        base.DisplaySides();
        Console.WriteLine($"Об'єм призми: {GetVolume()}");
        Console.WriteLine($"Периметр призми (всі грані): {GetPerimeter()}");
    }

    // обчислення об'єму призми
    public double GetVolume()
    {
        return base.GetArea() * height;
    }

    // обчислення периметра призми 
    public new double GetPerimeter()
    {
        return 2 * base.GetPerimeter() + 3 * height;
    }

    // обчислення площі поверхні призми
    public double GetSurfaceArea()
    {
        // площа двох трикутних основ
        double baseArea = base.GetArea();
        // площа трьох прямокутних бокових граней
        double sideArea = (A + B + C) * height;
        return 2 * baseArea + sideArea;
    }

    //порівняння для призми
    public override bool Equals(TTriangle other)
    {
        if (other is TTrianglePrism otherPrism) //перевырка об'єкта
        {
            return base.Equals(other) && height == otherPrism.height;
        }
        return false;
    }

    // перевизначений метод для перевірки нерівності для призми
    public override bool NotEquals(TTriangle other)
    {
        return !Equals(other);
    }

    // перевантажений оператор множення для призми
    public static TTrianglePrism operator *(TTrianglePrism prism, double factor)
    {
        // властивості A, B, C замість прямого доступу до полів a, b, c
        TTriangle baseTriangle = new TTriangle(prism.A * factor, prism.B * factor, prism.C * factor);

        return new TTrianglePrism(baseTriangle.A, baseTriangle.B, baseTriangle.C, prism.height * factor);
    }

    public static TTrianglePrism operator *(double factor, TTrianglePrism prism)
    {
        return prism * factor; // викликає попередньо перевантажений оператор
    }
}
//Створити клас-нащадок TTriangleF на основі класу TTriangle, в якому додатково перевизначити методи Equals та GetHashCode.
public class TTriangleF : TTriangle
{
    //новий конструктор викликає старий
    public TTriangleF(double a, double b, double c) : base(a, b, c) { }
    //оверрайд Equals для порівняння obj TTriangleF
    public override bool Equals(object obj)
    {
        if (obj is TTriangleF other) //чи об'єкт є екз. класу
        {
            return base.Equals(other); //виклик базовго
        }
        return false;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode(); //просто виклик базового
    }
}
//Створити клас-нащадок TTrianglePrizm на основі класу TTriangleF. 
public class TTrianglePrismF : TTriangleF
{
    //властив. Color 
    public string Color { get; set; }
    //
    private double height;

    //конструктор класу TTrianglePrismF. приймає сторони трикутника+ висота + колір
    public TTrianglePrismF(double a, double b, double c, double height, string color) : base(a, b, c)
    {
        //присвоєння значень
        this.height = height;
        this.Color = color;
    }

    //обчислення об'єма призми 
    public double GetVolume()
    {
        //площа трик.*висота
        return GetArea() * height;
    }

    //перевизн для рядкового представлення призми
    public override string ToString()
    {
        //трикутник, висота та колір
        return base.ToString() + $", Height = {height}, Color = {Color}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        //створюємо хешсет
        HashSet<TTriangle> triangleSet = new HashSet<TTriangle>();

        //свторюємо перший трикутник і додаємо до хешсету
        TTriangle triangle1 = new TTriangle(3, 4, 5);
        triangleSet.Add(triangle1);

        //створюємо ідентичний трикутник але інш об'єкт)
        TTriangle identicalTriangle = new TTriangle(3, 4, 5);
        //перевірямо чи порівняння і створення хешкоду правильне
        bool found = triangleSet.Contains(identicalTriangle);

        Console.WriteLine($"Чи знайдено ідентичний трикутник у HashSet: {found}");
        //хешсет для<TTriangleF>
        HashSet<TTriangleF> triangleFSet = new HashSet<TTriangleF>();

        TTriangleF triangleF1 = new TTriangleF(3, 4, 5);
        triangleFSet.Add(triangleF1);

        TTriangleF identicalTriangleF = new TTriangleF(3, 4, 5);
        bool foundF = triangleFSet.Contains(identicalTriangleF);

        //результ. теста
        Console.WriteLine($"Чи знайдено ідентичний TTriangleF у HashSet: {foundF}");


        // тест призма
        TTrianglePrism prism1 = new TTrianglePrism(3, 4, 5, 10);

        // введення даних для нової призми
        TTrianglePrism inputPrism = new TTrianglePrism();
        inputPrism.InputSides();

        // порівняння введеної призми з початковою
        Console.WriteLine($"Призми рівні? {inputPrism.Equals(prism1)}");
        Console.WriteLine($"Призми не рівні? {inputPrism.NotEquals(prism1)}");

        // множення трикутної призми на число
        TTrianglePrism scaledPrism = inputPrism * 2;
        Console.WriteLine($"Масштабована призма (призма * 2): {scaledPrism}");

        // множення числа на трикутну призму
        TTrianglePrism scaledPrism2 = 2 * inputPrism;
        Console.WriteLine($"Масштабована призма (2 * призма): {scaledPrism2}");

        Console.WriteLine($"Площа поверхні введеної призми: {inputPrism.GetSurfaceArea()}");
    }
}
