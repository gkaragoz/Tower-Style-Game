using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GK
{
    /// <summary>
    /// /// Quaternion Vector Rotation Check
    /// </summary>/
    public class InputDirectionModifier : MonoBehaviour
    {

        static private float _tanValue = .5f;

        public static Vector2 UserDirectionVector(Vector2 direction)
        {
            return CalculateVector(direction);

        }

        public static Vector2 InputDirectionVector(Vector2 direction)
        {
            return -1*CalculateVector(direction);

        }



        private static Vector2 CalculateVector(Vector2 direction)
        {
            Vector2 trueVector = -direction;
            Vector2 modifiedVector1 = Vector2.zero;
            float dividedValue = trueVector.y / trueVector.x;


            if (trueVector.x > 0 && trueVector.y > 0)
            {
                if (dividedValue < -_tanValue)
                {
                    modifiedVector1 = trueVector;
                }
                else
                {

                    modifiedVector1 = new Vector2(trueVector.x, trueVector.y / dividedValue * -_tanValue);

                }

                return modifiedVector1;


            }

            if (trueVector.x > 0 && trueVector.y < 0)
            {
                if (dividedValue < -_tanValue)
                {
                    modifiedVector1 = trueVector;
                }
                else
                {
                    modifiedVector1 = new Vector2(trueVector.x, trueVector.y / dividedValue * -_tanValue);

                }

                return modifiedVector1;
            }

            if (trueVector.x < 0 && trueVector.y < 0)
            {
                if (dividedValue > _tanValue)
                {
                    modifiedVector1 = trueVector;
                }
                else
                {
                    modifiedVector1 = new Vector2(trueVector.x, trueVector.y / dividedValue * _tanValue);

                }

                return modifiedVector1;
            }



            if (trueVector.x < 0 && trueVector.y > 0)
            {
                if (dividedValue > _tanValue)
                {
                    modifiedVector1 = trueVector;
                }
                else
                {
                    modifiedVector1 = new Vector2(trueVector.x, trueVector.y / dividedValue * _tanValue);

                }

                return modifiedVector1;
            }


            return modifiedVector1;
        }




    }
}