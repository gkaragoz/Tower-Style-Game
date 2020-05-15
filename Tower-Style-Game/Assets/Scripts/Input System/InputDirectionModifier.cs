using UnityEngine;

namespace GK {

    /// <summary>
    /// /// Quaternion Vector Rotation Check
    /// </summary>/
    public class InputDirectionModifier {

        private static float _tanValue = .2f;

        private static bool IsInFirstArea(Vector2 direction) {
            return direction.x >= 0 && direction.y > 0;
        }

        private static bool IsInSecondArea(Vector2 direction) {
            return direction.x < 0 && direction.y > 0;
        }

        private static bool IsInThirdArea(Vector2 direction) {
            return direction.x <= 0 && direction.y < 0;
        }

        private static bool IsInFourthArea(Vector2 direction) {
            return direction.x > 0 && direction.y < 0;
        }

        public static Vector2 UserDirectionVector(Vector2 direction) {
            return CalculateVector(direction);
        }

        public static Vector2 InputDirectionVector(Vector2 direction) {
            return -1 * CalculateVector(direction);
        }

        private static Vector2 CalculateVector(Vector2 direction) {
            Vector2 modifiedVector = Vector2.zero;
            float dividedValue = direction.y / direction.x;

            ///if the touch is dragging to positive side
            if (IsInFourthArea(direction) || IsInFirstArea(direction)) {
                if (dividedValue >= _tanValue) {
                    modifiedVector = direction;
                } else {
                    modifiedVector = new Vector2(direction.x, direction.y / dividedValue * _tanValue);
                }

                return modifiedVector;
            }
            
            ///if the touch is dragging to negative side
            if (IsInThirdArea(direction) || IsInSecondArea(direction)) {
                if (dividedValue <= -_tanValue) {
                    modifiedVector = direction;
                } else {
                    modifiedVector = new Vector2(direction.x, direction.y / dividedValue * -_tanValue);
                }

                return modifiedVector;
            }

            return modifiedVector;
        }
    }
}