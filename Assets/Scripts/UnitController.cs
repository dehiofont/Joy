using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace FomeCharacters
{
    public class UnitController : MonoBehaviour
    {
        [SerializeField] string unitName;
        [SerializeField] UnitController parent;

        [SerializeField] float characterMoveSpeed = 0.5f;

        public Material matCharacterSelected;
        public Material matCharacterInCombatSphere;
        public Material matCharacterBase;

        public RendererManager rendereManager;
        [SerializeField] GameObject visBody;
        public enum CharacterType
        {
            Player,
            Part,
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
        public Renderer characterRenderer;


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


        public List<Armament> playerArmaments = new List<Armament>();
        public List<UnitController> parts = new List<UnitController>();
        [SerializeField] GameObject targetPoint;

        private PartManager partManager;

        private int movementToggle = 1;

        private int thrustToggle = -1;

        private void Awake()
        {
            if (characterType == CharacterType.Part)
            {
                parent.GetComponent<PartManager>().AddPart(gameObject.GetComponent<UnitController>());
            }         
        }

        private void Start()
        {
            if (characterType == CharacterType.Player)
            {
                Debug.Log("Player Starting");
                playerArmaments = gameObject.GetComponent<ArmamentManager>().getArmaments();
                Event.OnPlayerSpawn?.Invoke(gameObject.GetComponent<UnitController>());
            }
            if (unitName == null)
            {
                switch (characterType)
                {
                    case CharacterType.Player:
                        unitName = "Player";
                        break;
                    case CharacterType.Part:
                        unitName = "Part";
                        break;
                    case CharacterType.Monster:
                        unitName = "Monster";
                        break;
                    case CharacterType.Boss:
                        unitName = "Boss";
                        break;
                    default:
                        unitName = "I don't Know";
                        break;
                }
            }

            if ((characterType != CharacterType.Player &&
                !GameManager.Instance.unitControllersInStage.Contains(gameObject.GetComponent<UnitController>())))
            {
                GameManager.Instance.unitControllersInStage.Add(gameObject.GetComponent<UnitController>());
            }

            if (characterType != CharacterType.Part && characterType != CharacterType.Player)
            {
                partManager = gameObject.GetComponent<PartManager>();
                parts = partManager.GetParts();
            }

            if (characterType == CharacterType.Player)
            {
            }
        }
        private void Update()
        {

            if(characterType == CharacterType.Player)
            {
                if(characterMovementActive == true)
                {
                    AddCharMoveThrust();
                    if(thrustToggle == 1)
                    {
                        RotateChar();
                    }
                }

                if (Input.GetKeyDown("space"))
                {
                    gameObject.GetComponent<CombatSphereController>().CombatSphereEnablerToggle();
                    if(movementToggle == 1)
                    {
                        characterMovementActive = false;
                    }
                    else
                    {
                        characterMovementActive = true;
                    }
                    movementToggle *= -1;
                }
            }
        }
        public Vector3 GetTargetPoint()
        {
            return targetPoint.transform.position;
        }
        public Vector3 GetUnitPos()
        {
            return this.transform.position;
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
            if (Input.GetKeyDown("w"))
            {
                thrustToggle *= -1;
            }

            if(thrustToggle == 1)
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

        public CharacterType getCharacterType()
        {
            return characterType;
        }
        public CharacterTeam getCharacterTeam()
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
