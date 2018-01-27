using System;
using System.Collections.Generic;
using System.Text;

namespace Archspace2.Battle
{
    public abstract class Unit : NamedEntity, IUnit
    {
        public double X { get; set; }
        public double Y { get; set; }

        private double mDirection;
        public double Direction
        {
            get
            {
                return mDirection;
            }
            set
            {
                mDirection = value;

                while (mDirection < 0)
                {
                    mDirection += 360;
                }
                while (mDirection > 360)
                {
                    mDirection -= 360;
                }
            }
        }

        public void SetVector(int aX, int aY, double aDirection)
        {
            X = aX;
            Y = aY;
            Direction = aDirection;
        }

        public void Turn(double aDirection)
        {
            Direction += aDirection;
        }

        public void TurnTo(Unit aUnit, double aMaxDirection)
        {
            double deltaDirection = DeltaDirection(aUnit);

            if (deltaDirection > 180)
            {
                deltaDirection -= 360;
            }

            if (deltaDirection > aMaxDirection)
            {
                deltaDirection = aMaxDirection;
            }
            if (deltaDirection < -aMaxDirection)
            {
                deltaDirection = -aMaxDirection;
            }

            Turn(deltaDirection);
        }

        public bool IsHeadingTo(Unit aUnit, double aTolerance = 2)
        {
            double delta = DeltaDirection(aUnit);
            if (delta < aTolerance || delta > (360 - aTolerance))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsInRange(Unit aUnit, int aDistance, int aDirection)
        {
            if (Distance(aUnit) > aDistance)
            {
                return false;
            }
            else
            {
                double deltaDirection = DeltaDirection(aUnit);

                if (deltaDirection < aDirection || deltaDirection > (360 - aDirection))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public double Distance(Unit aUnit)
        {
            double dX = X - aUnit.X;
            double dY = Y - aUnit.Y;

            return Math.Sqrt((dX * dX) + (dY * dY));
        }

        public double DeltaDirection(Unit aUnit)
        {
            double dX = aUnit.X - X;
            double dY = aUnit.Y - Y;

            if (dX == 0 && dY == 0)
            {
                return 0;
            }
            else
            {
                double direction;

                if (dX == 0)
                {
                    if (dY > 0)
                    {
                        direction = 90;
                    }
                    else
                    {
                        direction = 270;
                    }
                }
                else if (dY == 0)
                {
                    if (dX > 0)
                    {
                        direction = 0;
                    }
                    else
                    {
                        direction = 180;
                    }
                }
                else
                {
                    double distance = Math.Sqrt((dX * dX) + (dY * dY));

                    direction = Math.Acos(dX / distance);
                    if (dY < 0)
                    {
                        direction = 360 - direction;
                    }
                }

                double delta = direction - Direction;

                while (delta < 0)
                {
                    delta += 360;
                }
                while (delta > 360)
                {
                    delta -= 360;
                }

                return delta;
            }
        }

        public void Rotate(double aDirection)
        {
            double newX;
            double newY;

            newX = (Math.Cos(aDirection) * X) - (Math.Sin(aDirection) * Y);
            newY = (Math.Sin(aDirection) * X) + (Math.Cos(aDirection) * Y);
        }

        public void Rotate(double aDirection, Unit aFixedPoint)
        {
            X -= aFixedPoint.X;
            Y -= aFixedPoint.Y;

            Rotate(aDirection);

            X += aFixedPoint.X;
            Y += aFixedPoint.Y;
        }

        public void Move(int aDeltaX, int aDeltaY)
        {
            X += aDeltaX;
            Y += aDeltaY;

            if (X < 0)
            {
                X = 0;
            }
            if (X > 10000)
            {
                X = 10000;
            }
            if (Y < 0)
            {
                Y = 0;
            }
            if (Y > 10000)
            {
                Y = 10000;
            }
        }

        public void Move(int aLength)
        {
            double dX = aLength * Math.Cos(Direction);
            double dY = aLength * Math.Sin(Direction);

            Move((int)dX, (int)dY);
        }

        public bool AtBorder()
        {
            if (X <= 0 || X >= 10000 ||
                Y <= 0 || Y >= 10000)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool OnPath(Unit aPoint, double aLeftX, double aRightX, double aTopY, double aBottomY)
        {
            double dX = aPoint.X - X;
            double dY = aPoint.Y - Y;

            double newX = dX * Math.Cos(-Direction) - dY * Math.Sin(-Direction);
            double newY = dX * Math.Sin(-Direction) + dY * Math.Cos(-Direction);

            if (newX > aLeftX && newX < aRightX && newY > aBottomY && newY < aTopY)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
