﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Archspace2
{
    public class Vector
    {
        public int X { get; set; }
        public int Y { get; set; }

        private double mAngle;
        public double Angle
        {
            get
            {
                return mAngle;
            }
            set
            {
                mAngle = value;

                while (mAngle < 0)
                {
                    mAngle += 360;
                }
                while (mAngle > 360)
                {
                    mAngle -= 360;
                }
            }
        }

        public void SetVector(int aX, int aY, double aAngle)
        {
            X = aX;
            Y = aY;
            Angle = aAngle;
        }

        public void Turn(double aAngle)
        {
            Angle += aAngle;
        }

        public void TurnTo(Vector aFleet, double aMaxAngle)
        {
            double deltaDirection = DeltaDirection(aFleet);

            if (deltaDirection > 180)
            {
                deltaDirection -= 360;
            }

            if (deltaDirection > aMaxAngle)
            {
                deltaDirection = aMaxAngle;
            }
            if (deltaDirection < -aMaxAngle)
            {
                deltaDirection = -aMaxAngle;
            }

            Turn(deltaDirection);
        }

        public bool IsHeadingTo(Vector aVector, double aTolerance = 2)
        {
            double delta = DeltaDirection(aVector);
            if (delta < aTolerance || delta > (360 - aTolerance))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsInRange(Vector aVector, int aDistance, int aDirection)
        {
            if (Distance(aVector) > aDistance)
            {
                return false;
            }
            else
            {
                double deltaDirection = DeltaDirection(aVector);

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

        public int Distance(Vector aVector)
        {
            int dX = X - aVector.X;
            int dY = Y - aVector.Y;

            return (int)Math.Sqrt((dX * dX) + (dY * dY));
        }

        public double DeltaDirection(Vector aVector)
        {
            int dX = aVector.X - X;
            int dY = aVector.Y - Y;

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

                double delta = direction - Angle;

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

        public void Rotate(double aAngle)
        {
            double newX;
            double newY;

            newX = (Math.Cos(aAngle) * X) - (Math.Sin(aAngle) * Y);
            newY = (Math.Sin(aAngle) * X) + (Math.Cos(aAngle) * Y);
        }

        public void Rotate(double aAngle, Vector aFixedPoint)
        {
            X -= aFixedPoint.X;
            Y -= aFixedPoint.Y;

            Rotate(aAngle);

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
            if (X > Game.Configuration.Battle.MaxX)
            {
                X = Game.Configuration.Battle.MaxX;
            }
            if (Y < 0)
            {
                Y = 0;
            }
            if (Y > Game.Configuration.Battle.MaxY)
            {
                Y = Game.Configuration.Battle.MaxY;
            }
        }

        public void Move(int aLength)
        {
            double dX = aLength * Math.Cos(Angle);
            double dY = aLength * Math.Sin(Angle);

            Move((int)dX, (int)dY);
        }

        public bool AtBorder()
        {
            if (X <= 0 || X >= Game.Configuration.Battle.MaxX ||
                Y <= 0 || Y >= Game.Configuration.Battle.MaxY)
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