using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FomeCharacters
{
    public class UnitController : MonoBehaviour
    {
        //private void Start()
        //{

        //}

        [SerializeField] string unitName;

        [SerializeField] float characterMoveSpeed = 0.5f;

        [SerializeField] Material matCharacterSelected;
        [SerializeField] Material matCharacterInCombatSphere;
        [SerializeField] Material matCharacterBase;
        
        [SerializeField] GameObject visBody;
        public enum CharacterType
        {
            Player,
            Monster,
            Boss,
        }
        public enum CharacterTeam
        {
            Ally,
            Enemy,
            Neutral,
        }

        [SerializeField] CharacterType characterType;
        [SerializeField] CharacterTeam characterTeam;

        private bool allowCharacterInput = true;
        private bool characterMovementActive = true;
        private Renderer characterRenderer;


        [SerializeField] float charMoveAcceleration;
        [SerializeField] float charMoveDeceleration;
        [SerializeField] float maxCharMoveSpeed;

        private float charMoveThrust;
        public float charMoveSpeed;

        [SerializeField] float charRotationAcceleration;
        [SerializeField] float charRotationDeceleration;
        [SerializeField] float maxCharRotationSpeed;
        [SerializeField] float directionChangeHelpMultiplier = 1;

        private int charRotationDirection;
        private float charRotationThrust;
        public float charRotationSpeed;

        private void Start()
        {
            if(unitName == null)
            {
                if(characterType == CharacterType.Player)
                {
                    unitName = "Player";
                }
                else if(characterType == CharacterType.Monster)
                {
                    unitName = "Monster";
                }
                else
                {
                    unitName = "Boss";
                }
            }
            characterRenderer = visBody.GetComponent<Renderer>();

            if(characterType != CharacterType.Player && !GameManager.Instance.listOfAllPotentialTargets.Contains(gameObject.GetComponent<UnitController>()))
            {
                GameManager.Instance.listOfAllPotentialTargets.Add(gameObject.GetComponent<UnitController>());
            }
        }
        private void Update()
        {

            if(characterType == CharacterType.Player)
            {
                if(characterMovementActive == true)
                {
                    AddCharMoveThrust();
                    RotateChar();
                }

                if (Input.GetKeyDown("space"))
                {
                    GameManager.Instance.CombatSphereController.CombatSphereEnablerToggle();
                }
            }
        }
        public Vector3 GetUnitPos()
        {
            return gameObject.transform.position;
        }
        public string GetName()
        {
            return unitName;
        }
        private void RotateChar()
        {
            if (Input.GetKeyDown("a"))
            {
                charRotationDirection = -1;
            }

            if (Input.GetKeyDown("d"))
            {
                charRotationDirection = 1;
            }

            if (Input.GetKey("a") || Input.GetKey("d"))
            {
                ChangeRotationThrustDirection(charRotationDirection);
            }            
            else
            {
                charRotationThrust = 0;
                DecelerateRotation(charRotationDirection);
            }

            if (charRotationSpeed > maxCharRotationSpeed || charRotationSpeed < (maxCharRotationSpeed * -1))
            {
                charRotationSpeed = maxCharRotationSpeed * charRotationDirection;
            }

            if(charRotationDirection == 1)
            {
                charRotationSpeed += Time.deltaTime * charRotationThrust;
            }
            else
            {
                charRotationSpeed -= Time.deltaTime * charRotationThrust;
            }
            
            transform.Rotate(Vector3.up * charRotationSpeed);
        }

        private void ChangeRotationThrustDirection(int _direction)
        {
            charRotationThrust = charRotationAcceleration;
        }
        private void DecelerateRotation(float _direction, float decelerationMultiplier = 1)
        {
            if (_direction == 1)
            {
                if (charRotationSpeed > 0)
                {
                    charRotationSpeed -= Time.deltaTime * charRotationDeceleration * decelerationMultiplier;
                }
                else if (charRotationSpeed < 0)
                {
                    charRotationSpeed = 0;
                }
            }
            else
            {
                if (charRotationSpeed < 0)
                {
                    charRotationSpeed += Time.deltaTime * charRotationDeceleration * decelerationMultiplier;
                }
                else if (charRotationSpeed > 0)
                {
                    charRotationSpeed = 0;
                }
            }
        }
        private void AddCharMoveThrust()
        {
            if (Input.GetKey("w"))
            {
                charMoveThrust = charMoveAcceleration;
            }
            else
            {
                charMoveThrust = 0;
                if(charMoveSpeed > 0)
                {
                    charMoveSpeed -= Time.deltaTime * charMoveDeceleration;
                }
                else if(charMoveSpeed < 0)
                {
                    charMoveSpeed = 0;
                }
            }

            if( charMoveSpeed > maxCharMoveSpeed)
            {
                charMoveSpeed = maxCharMoveSpeed;
            }

            charMoveSpeed += Time.deltaTime * charMoveThrust;
            transform.Translate(Vector3.forward * charMoveSpeed);

        }
        public void SetCharacterMovement(bool _characterMovementActive)
        {
            characterMovementActive = _characterMovementActive;
        }

        public void AllowInput(bool _allowInput)
        {
            allowCharacterInput = _allowInput;
        }

        public CharacterType GetCharacterType()
        {
            return characterType;
        }
        public CharacterTeam GetCharacterTeam()
        {
            return characterTeam;
        }
        public void SetCharacterSelectedMat()
        {
            characterRenderer.material = matCharacterSelected;
        }
        public void SetCharacterInCombatSphereMat()
        {
            characterRenderer.material = matCharacterInCombatSphere;
        }
        public void SetCharacterBaseMat()
        {
            characterRenderer.material = matCharacterBase;
        }
    }
}
