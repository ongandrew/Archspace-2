using System;

namespace Archspace2.Battle
{
    public class Unit : NamedEntity, IUnit
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

        public Unit()
        {
        }

        public Unit(double aX, double aY)
        {
            SetVector(aX, aY, 0);
        }

        public Unit(double aX, double aY, double aDirection)
        {
            SetVector(aX, aY, aDirection);
        }

        public Unit(Unit aUnit) : this(aUnit.X, aUnit.Y, aUnit.Direction)
        {
        }

        public void SetVector(double aX, double aY, double aDirection)
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

        public bool IsInRange(Unit aUnit, double aDistance, double aDirection)
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

                    direction = Math.Acos(dX / distance) * 180.0 / Math.PI;
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

            newX = (Math.Cos(aDirection * Math.PI / 180) * X) - (Math.Sin(aDirection * Math.PI / 180) * Y);
            newY = (Math.Sin(aDirection * Math.PI / 180) * X) + (Math.Cos(aDirection * Math.PI / 180) * Y);
        }

        public void Rotate(double aDirection, Unit aFixedPoint)
        {
            X -= aFixedPoint.X;
            Y -= aFixedPoint.Y;

            Rotate(aDirection);

            X += aFixedPoint.X;
            Y += aFixedPoint.Y;
        }

        public void Move(double aDeltaX, double aDeltaY)
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

        public void Move(double aLength)
        {
            double dX = aLength * Math.Cos(Direction * Math.PI / 180);
            double dY = aLength * Math.Sin(Direction * Math.PI / 180);

            Move(dX, dY);
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

        public bool OnPath(Unit aPoint, BoundingBox aBoundingBox)
        {
            double dX = aPoint.X - X;
            double dY = aPoint.Y - Y;

            double newX = dX * Math.Cos(-Direction * Math.PI / 180) - dY * Math.Sin(-Direction * Math.PI / 180);
            double newY = dX * Math.Sin(-Direction * Math.PI / 180) + dY * Math.Cos(-Direction * Math.PI / 180);

            if (newX > aBoundingBox.LeftX && newX < aBoundingBox.RightX && newY > aBoundingBox.BottomY && newY < aBoundingBox.TopY)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool PathMeetsBorder(BoundingBox aBoundingBox)
        {
            Unit topLeft = new Unit(X + aBoundingBox.LeftX, Y + aBoundingBox.TopY);
            Unit topRight = new Unit(X + aBoundingBox.RightX, Y + aBoundingBox.TopY);
            Unit bottomLeft = new Unit(X + aBoundingBox.LeftX, Y + aBoundingBox.BottomY);
            Unit bottomRight = new Unit(X + aBoundingBox.RightX, Y + aBoundingBox.BottomY);

            topLeft.Rotate(Direction, this);
            topRight.Rotate(Direction, this);
            bottomLeft.Rotate(Direction, this);
            bottomRight.Rotate(Direction, this);

            if (topLeft.AtBorder() || topRight.AtBorder() || bottomLeft.AtBorder() || bottomRight.AtBorder())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool PathMeetsVerticalBorder(BoundingBox aBoundingBox)
        {
            Unit topLeft = new Unit(X + aBoundingBox.LeftX, Y + aBoundingBox.TopY);
            Unit topRight = new Unit(X + aBoundingBox.RightX, Y + aBoundingBox.TopY);
            Unit bottomLeft = new Unit(X + aBoundingBox.LeftX, Y + aBoundingBox.BottomY);
            Unit bottomRight = new Unit(X + aBoundingBox.RightX, Y + aBoundingBox.BottomY);

            topLeft.Rotate(Direction, this);
            topRight.Rotate(Direction, this);
            bottomLeft.Rotate(Direction, this);
            bottomRight.Rotate(Direction, this);

            if (topLeft.X <= 0 || topLeft.X >= 10000 || topRight.X <= 0 || topRight.X >= 10000 || bottomLeft.X <= 0 || bottomLeft.X >= 10000 || bottomRight.X <= 0 || bottomRight.X >= 0)
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
