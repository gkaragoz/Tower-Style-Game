using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GK
{
    /// <summary>
    /// /// Quaternion Vector Rotation Check
    /// </summary>/
    public  class InputDirectionModifier:MonoBehaviour
    {

        static private float _tanValue = .5f;

        public static Vector2 UserDirectionVector(Vector2 direction)
        {
            return -1 * CalculateVector(direction);

        }

        public static Vector2 InputDirectionVector(Vector2 direction)
        {
            return CalculateVector(direction);

        }



        private static Vector2 CalculateVector(Vector2 direction)
        {            
            Vector2 modifiedVector = Vector2.zero;
            float dividedValue = direction.y / direction.x;
            

            ///if the touch is dragging to positive side
            if (direction.x > 0 && direction.y < 0|| direction.x > 0 && direction.y > 0)
            {
                if (dividedValue > _tanValue)
                {
                    modifiedVector = direction;
                }
                else
                {
                    modifiedVector = new Vector2(direction.x, direction.y / dividedValue * _tanValue);

                }

                return modifiedVector;
            }
            ///if the touch is dragging to negative side

            if (direction.x < 0 && direction.y < 0|| direction.x < 0 && direction.y > 0)
            {
                if (dividedValue < -_tanValue)
                {
                    modifiedVector = direction;
                }
                else
                {
                    modifiedVector = new Vector2(direction.x, direction.y / dividedValue * -_tanValue);

                }

                return modifiedVector;
            }



            return modifiedVector;
        }




    }
}