using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice
{

    //Проблема при параллельных прямых. Точка все еще внутри, но почему-то пересечение оно не ловит
    public struct LineEquation
    {
        public int A, B, C;
        public Point P1, P2;
    }

    public enum Figures
    {
        Circle, Triangle, Rectangle
    }
    class CrossChecking
    {
        public static LineEquation GetLineEquation(Point a, Point b)
        {
            LineEquation le = new LineEquation();
            le.A = b.Y - a.Y;
            le.B = a.X - b.X;
            le.C = a.X * (a.Y - b.Y) + a.Y * (b.X - a.X);
            le.P1 = a;
            le.P2 = b;
            return le;
        }

        public static int DistanceToPoint(LineEquation le, Point a)
        {
            int res = (int)(Math.Abs(le.A * a.X + le.B * a.Y + le.C) / Math.Sqrt(Math.Pow(le.A, 2) + Math.Pow(le.B, 2)));
            return res;
        }

        public static bool IsCrossLineCircle(int d, int r)
        {
            if (d-2 <= r) return true;
            else return false;
        }

        public static int GetPointsDistance(Point a, Point b)
        {
            int res = (int)(Math.Sqrt(Math.Pow((b.X - a.X), 2) + Math.Pow((b.Y - a.Y), 2)));
            return res;
        }

        public static void CrossFigures(List<Figure> figures, int xMAx, int yMax)
        {
            if (figures.Count > 1)
            {
                foreach (Figure f1 in figures)
                {
                    foreach (Figure f2 in figures)
                    {
                        if (f1.Equals(f2)) continue;
                        CrossByType(f1, f2, xMAx, yMax);
                    }
                }
            }
        }


        public static bool CheckCrossing(Circle circle, Triangle triangle, int xMAx, int yMax)
        {
            LineEquation[] le = GetLineEqualtions(triangle);
            bool res = false;
            foreach (LineEquation l in le)
            {
                if (IsPointInCircle(l.P1, circle) || IsPointInCircle(l.P2, circle))
                    res = true;
                int d = DistanceToPoint(l, new Point(circle.X, circle.Y));
                Point p = GetCrossPoitBetwenLineAndPoint(l, new Point(circle.X, circle.Y));
                bool isOnSegment = IsPointOnSegment(l.P1, l.P2, p);
                if (IsCrossLineCircle(d, circle.Radius) && isOnSegment)
                    res = true;
            }
            if (res)
            {
                Console.WriteLine("crossin between figures {0} and {1}  at time: {2}", circle.Id, triangle.Id, DateTime.Now);
                
                circle.ChangeDirection();
                triangle.ChangeDirection();
                circle.Move(xMAx, yMax);
                triangle.Move(xMAx, yMax);
                circle.Move(xMAx, yMax);
                triangle.Move(xMAx, yMax);
            }
            return res;
        }


        public static bool CheckCrossing(Circle circle, Rectangle rectangle, int xMAx, int yMax)
        {
            bool res = false;
            LineEquation[] le = new LineEquation[4];
            le[0] = GetLineEquation(rectangle.Points[0], rectangle.Points[1]);
            le[1] = GetLineEquation(rectangle.Points[1], rectangle.Points[2]);
            le[2] = GetLineEquation(rectangle.Points[2], rectangle.Points[3]);
            le[3] = GetLineEquation(rectangle.Points[3], rectangle.Points[0]);
            foreach (LineEquation l in le)
            {
                if (IsPointInCircle(l.P1, circle) || IsPointInCircle(l.P2, circle))
                {
                    res = true;
                    break;
                }
                int d = DistanceToPoint(l, new Point(circle.X, circle.Y));
                Point p = GetCrossPoitBetwenLineAndPoint(l, new Point(circle.X, circle.Y));
                bool isOnSegment = IsPointOnSegment(l.P1, l.P2, p);
                if (IsCrossLineCircle(d, circle.Radius) && isOnSegment)
                    res = true;
            }
            if (res)
            {
                Console.WriteLine("crossin between figures {0} and {1}  at time: {2}", circle.Id, rectangle.Id, DateTime.Now);
                circle.ChangeDirection();
               rectangle.ChangeDirection();
                circle.Move(xMAx, yMax);
                rectangle.Move(xMAx, yMax);
                circle.Move(xMAx, yMax);
                rectangle.Move(xMAx, yMax);
            }
            return res;
        }
        public static bool CheckCrossing(Circle circle1, Circle circle2, int xMAx, int yMax)
        {
            bool res = false;
            int d = GetPointsDistance(new Point(circle1.X, circle1.Y), new Point(circle2.X, circle2.Y));
            int sum = circle1.Radius + circle2.Radius;
            if (d <= sum)
                res = true;
            if (res)
            {
                Console.WriteLine("crossin between figures {0} and {1} at time: {2}", circle1.Id, circle2.Id, DateTime.Now);
                circle1.ChangeDirection();
                circle2.ChangeDirection();
                circle1.Move(xMAx, yMax);
                circle2.Move(xMAx, yMax);
            }
            return res;
        }
        public static bool CheckCrossing(Triangle triangle, Rectangle rectangle, int xMAx, int yMax)
        {
            bool res = false;
            if (!IsPointInRectangle(rectangle, triangle.A) && !(IsPointInRectangle(rectangle, triangle.B)) && !(IsPointInRectangle(rectangle, triangle.C)))
            {
                LineEquation[] le1 = GetLineEqualtions(triangle);
                //LineEquation[] le2 = new LineEquation[4];
                //le2[0] = GetLineEquation(rectangle.Points[0], rectangle.Points[1]);
                //le2[1] = GetLineEquation(rectangle.Points[1], rectangle.Points[2]);
                //le2[2] = GetLineEquation(rectangle.Points[2], rectangle.Points[3]);
                //le2[3] = GetLineEquation(rectangle.Points[3], rectangle.Points[0]);
                foreach (LineEquation l1 in le1)
                {
                    foreach (Point p in rectangle.Points)
                    {
                        //Point p = GetLinesCrossPoint(l1, l2); //return false when rectangle in triangle and triangle isnt in rectangle
                        //if (IsPointOnSegment(l1.P1, l1.P2, p) && IsPointOnSegment(l2.P1, l2.P2, p))
                        //    res = true;
                        if (IsPointOnLine(l1, p))
                        {
                            triangle.ChangeDirection();
                            rectangle.ChangeDirection();
                            triangle.Move(xMAx, yMax);
                            rectangle.Move(xMAx, yMax);
                            triangle.Move(xMAx, yMax);
                            rectangle.Move(xMAx, yMax);
                            res = true;
                            Console.Beep();
                            Console.WriteLine("!If reclangle in triangle!! crossin between figures {0} and {1}  at time: {2}", triangle.Id, rectangle.Id, DateTime.Now);
                            return res;
                        }
                    }
                }
            }
            else
            {
                triangle.ChangeDirection();
                rectangle.ChangeDirection();
                triangle.Move(xMAx, yMax);
                rectangle.Move(xMAx, yMax);
                triangle.Move(xMAx, yMax);
                rectangle.Move(xMAx, yMax);
                Console.WriteLine("!if triangle in rectangle!! crossin between figures {0} and {1}  at time: {2}", triangle.Id, rectangle.Id, DateTime.Now);
                return true;
            }
            
            if (res)
            {
                triangle.ChangeDirection();
                rectangle.ChangeDirection();
                triangle.Move(xMAx, yMax);
                rectangle.Move(xMAx, yMax);
                triangle.Move(xMAx, yMax);
                rectangle.Move(xMAx, yMax);
                Console.WriteLine("between figures {0} and {1}  at time: {2}", triangle.Id, rectangle.Id, DateTime.Now);
            }
            return res;
        }
        public static bool CheckCrossing(Triangle triangle1, Triangle triangle2, int xMAx, int yMax)
        {
            bool res = false;
            LineEquation[] le1 = GetLineEqualtions(triangle1);
            LineEquation[] le2 = GetLineEqualtions(triangle2);
            foreach (LineEquation l1 in le1)
            {
                foreach (LineEquation l2 in le2)
                {
                    Point p = GetLinesCrossPoint(l1, l2);
                    if (IsPointOnSegment(l1.P1, l1.P2, p) && IsPointOnSegment(l2.P1, l2.P2, p))
                    {
                        res = true;
                        break;
                    }
                }
            }
            if (res)
            {
                triangle1.ChangeDirection();
                triangle2.ChangeDirection();
                triangle1.Move(xMAx, yMax);
                triangle2.Move(xMAx, yMax);
                triangle1.Move(xMAx, yMax);
                triangle2.Move(xMAx, yMax);
                Console.WriteLine("crossin between figures {0} and {1}  at time: {2}", triangle1.Id, triangle2.Id, DateTime.Now);
            }
            return res;
        }

        public static LineEquation[] GetLineEqualtions(Triangle triangle)
        {
            LineEquation[] le = new LineEquation[3];
            le[0] = GetLineEquation(triangle.A, triangle.B);
            le[1] = GetLineEquation(triangle.B, triangle.C);
            le[2] = GetLineEquation(triangle.C, triangle.A);
            return le;
            
        }
        public static bool CheckCrossing(Rectangle rectangle1, Rectangle rectangle2, int xMAx, int yMax)
        {
            var res = IsRectanglesCrossed(rectangle1, rectangle2);
            if (res)
            {
                rectangle1.ChangeDirection();
                rectangle2.ChangeDirection();
                rectangle1.Move(xMAx, yMax);
                rectangle2.Move(xMAx, yMax);
                rectangle1.Move(xMAx, yMax);
                rectangle2.Move(xMAx, yMax);
                Console.WriteLine("crossin between figures {0} and {1}  at time: {2}", rectangle1.Id, rectangle2.Id, DateTime.Now);
            }
            return res;
        }

        public static bool CheckCrossing(Rectangle rectangle, Triangle triangle, int xMAx, int yMax)
        {
            return CheckCrossing(triangle, rectangle, xMAx, yMax);
        }

        public static bool CheckCrossing(Rectangle rectangle, Circle circle, int xMAx, int yMax)
        {
            return CheckCrossing(circle, rectangle, xMAx, yMax);
        }

        public static bool CheckCrossing(Triangle triangle, Circle circle, int xMAx, int yMax)
        {
            return CheckCrossing(circle, triangle, xMAx, yMax);
        }

        public static bool CrossByType(Figure f1, Figure f2, int xMAx, int yMax)
        {
            string type1 = f1.GetType().Name;
            string type2 = f2.GetType().Name;

            if (type1 == Figures.Circle.ToString() && type2 == Figures.Circle.ToString())
            {
                return CheckCrossing((Circle)f1, (Circle)f2, xMAx, yMax);
            }

            if (type1 == Figures.Circle.ToString() && type2 == Figures.Triangle.ToString())
            {
                return CheckCrossing((Circle)f1, (Triangle)f2, xMAx, yMax);
            }

            if (type1 == Figures.Circle.ToString() && type2 == Figures.Rectangle.ToString())
            {
                return CheckCrossing((Circle)f1, (Rectangle)f2, xMAx, yMax);
            }

            if (type1 == Figures.Triangle.ToString() && type2 == Figures.Triangle.ToString())
            {
                return CheckCrossing((Triangle)f1, (Triangle)f2, xMAx, yMax);
            }

            if (type1 == Figures.Triangle.ToString() && type2 == Figures.Rectangle.ToString())
            {
                return CheckCrossing((Triangle)f1, (Rectangle)f2, xMAx, yMax);
            }

            if (type1 == Figures.Rectangle.ToString() && type2 == Figures.Rectangle.ToString())
            {
                return CheckCrossing((Rectangle)f1, (Rectangle)f2, xMAx, yMax);
            }

            if (type1 == Figures.Rectangle.ToString() && type2 == Figures.Triangle.ToString())
            {
                return CheckCrossing((Rectangle)f1, (Triangle)f2, xMAx, yMax);
            }

            if (type1 == Figures.Rectangle.ToString() && type2 == Figures.Circle.ToString())
            {
                return CheckCrossing((Rectangle)f1, (Circle)f2, xMAx, yMax);
            }

            if (type1 == Figures.Triangle.ToString() && type2 == Figures.Circle.ToString())
            {
                return CheckCrossing((Triangle)f1, (Circle)f2, xMAx, yMax);
            }

            else return false;

        }

        public static Point GetCrossPoitBetwenLineAndPoint(LineEquation le, Point p)
        {
            if (le.A == 0) return new Point(p.X, le.P1.Y);
            if (le.B == 0) return new Point(le.P1.X, p.Y);
                int c2 = le.A * p.Y - le.B * p.X;
            int x = (int)(((le.A * le.C) / le.B - c2) / (Math.Pow(le.A, 2) / le.B + le.B));
            int y = (int)(-1 * (le.A * x + le.C) / le.B);
            return new Point(x, y);
        }

        public static bool IsPointOnSegment(Point a, Point b, Point p)
        {
            if ((p.X >= a.X && p.X <= b.X && p.Y >= a.Y && p.Y <= b.Y) 
                || (p.X >= b.X && p.X <= a.X && p.Y >= b.Y && p.Y <= a.Y))
                return true;
            else
                return false;
        }

        public static bool IsPointInCircle(Point a, Circle circle)
        {
            bool res = false;
            if (GetPointsDistance(a, new Point(circle.X, circle.Y)) < circle.Radius) res = true;
            return res;
        }

        public static bool IsRectanglesCrossed(Rectangle rec1, Rectangle rec2)
        {
            bool res = false;
            foreach (Point p in rec1.Points)
            {
                if (IsPointInRectangle(rec2, p))
                    return true;
            }
            foreach (Point p in rec2.Points)
            {
                if (IsPointInRectangle(rec1, p))
                    return true;
            }
            return res;
        }

        public static bool IsPointInRectangle(Rectangle rec, Point p)
        {
            bool res = false;
            if (p.X >= rec.X && p.X <= rec.X + rec.Width && p.Y >= rec.Y && p.Y <= rec.Y + rec.Height)
                res = true;
            return res;
        }

        public static Point GetLinesCrossPoint(LineEquation le1, LineEquation le2)
        {
            int x = 0;
            int y = 0;
            if (le1.B == 0 && le2.B == 0)
            {
                if (le1.A == le2.A)
                    return le1.P1;
                else
                    return new Point(-1000, -1000);
                    
            }
            if (le1.A == 0 && le2.A == 0)
            {
                if (le1.B == le2.B)
                    return le1.P1;
                else
                    return new Point(-1000, -1000);

            }
            else
            {
                int temp = ((le2.A + ((le1.A * le2.B) / le1.B)));
                if (temp != 0)
                {
                    x = (int)(((le1.C * le2.B) / le1.B - le2.C) / temp);
                    y = (int)(-1 * (le1.A * x + le1.C) / le1.B);
                }
                
            }
            return new Point(x, y);
        }

        public static bool IsPointOnLine(LineEquation le, Point p)
        {
            if (IsPointOnSegment(le.P1, le.P2, p))
            {
                int result = le.A * p.X + le.B * p.Y + le.C;
                if (result == 0)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
