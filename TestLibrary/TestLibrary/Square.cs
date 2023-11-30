using System.Diagnostics;
using System.Reflection.Metadata;
using System.Threading.Channels;

namespace TestLibrary
{
    public abstract class Shape
    {
        public virtual string GetInfo()
        {
            return base.ToString();
        }
    }

    public class Square : Shape
    {
        #region Объяснения
        /// <summary>
        /// можно написать через 3 класса. Один базовый фигура доопустим, остальные круг, треугольник
        /// дальше у ник вызывать конструктор с ответами
        /// 
        /// РАСШИРЯЕМОСТЬ/ДОБАВЛЕНИЕ НОВОЙ ФИГУРЫ
        /// 
        /// Если идти по этому решению, достаточно просто добавить метод и перегрузить конструктор
        /// Если идти через добавление новых классов фигур, тогда можно обозначить base формулу и добавить в родительский класс вывод
        /// сообщения и тогда не нудно свойство Message. Добавление фигуры - создание класса фигуры
        /// 
        /// "Вычисление площади фигуры без знания типа фигуры в compile-time"
        /// 
        /// Можно написать конструктор/класс (основываюсь на двух выборах написания программы выше) с вычислением стандартной площади, по другому не знаю как можно узнать площадь без формулы
        /// 
        /// "Юнит-тесты"
        /// 
        /// Как правильно помню, в идеале пишутся тесты, потом прога, в репозиториях у меня остались старые юниты, работают они одинаково
        /// Пишем какой ответ должен получится и проверям
        /// 
        /// 
        /// Вопрос №3: 
        /// 
        /// Названия абстрактны
        /// 
        /// Select product from product union select category from category
        /// order by product
        /// 
        /// если же не получается и выходит ошибка, то тогда так
        /// 
        /// select product,category from Product inner join Category on Product.id=category.id
        /// 
        /// если не получается, используем Full вместо inner
        /// 
        /// 
        /// P.S. на гитхабе в файле это объяснение будет написано, но на всякий случай продублировал здесь
        /// </summary>
#endregion

        private double _a;
        private double _b;
        private double _c;
        private double _radius;
        private double _pow = 2;

        public string Message { get; private set; }

        private List<double> _array = new List<double>(); //можно использовать обычный массив и самому написать сортировку. для своего удобства использовал лист
        public Square(double radius)
        {
            try
            {
                _radius = radius;
                if (_radius <= 0)
                {
                    throw new ArgumentException("Не может быть нулевой или отрицательный радиус");
                }
                else
                {
                    Message = GetSquareCircle(_radius).ToString();
                }

            }
            catch (Exception e)
            {

                Message = e.Message.ToString();
            }
        }
        public override string GetInfo()
        {
            return Message.ToString();
        }

        public Square(double a, double b, double c)
        {
            try
            {
                _a = a;
                _b = b;
                _c = c;
                _array.Add(_a);
                _array.Add(_b);
                _array.Add(_c);
                _array.Sort();
                if (_a <= 0 || _b <= 0 || _c <= 0)
                {
                    throw new ArgumentException("Не может быть нулевая или отрицательная сторона");
                }
                else
                {
                    Message = GetSquareTriangle(_a, _b, _c).ToString();
                }
                if (Math.Pow(_array[2], 2) == (Math.Pow(_array[0], 2) + Math.Pow(_array[1], 2))) //проверка на прямоугольность (math функции больше доверяю, хлть она и возможно занимает время)
                {
                    Message = "Треугольник прямоугольный. Площадь " + GetSquareTriangle(_a, _b, _c).ToString();
                }


            }
            catch (Exception e)
            {
                Message = e.Message.ToString();
            }

        }

        private double GetSquareTriangle(double a, double b, double c)
        {
            double S, p;

            p = (a + b + c) / 2;

            S = Math.Sqrt((((p * (p - a)) * (p - b)) * (p - c)));
            return S;
        }

        private double GetSquareCircle(double radius)
        {
            double _s;
            _s = Math.PI * Math.Pow(radius, _pow);
            return _s;
        }
    }
}