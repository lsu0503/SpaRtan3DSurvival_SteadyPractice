# SpaRtan3DSurvival_SteadyPractice
 SpaRtan3DSurvival_SteadyPractice

# 문제 풀이
**※ 분석 문제의 경우. Question Analysis 폴더 안의 .docx 파일으로도 정리되어 있습니다.**

## Q1
* 분석 문제 [Commit id: 5d7b7e2ebb9bd6df20233cdbf345b7e67245784f]
  * **입문 주차와 비교해서 입력 받는 방식의 차이와 공통점을 비교해보세요.**
    * 내용 정리
      * 입문 주차: SendMassage를 이용한 입력 처리.
      * 숙련 주차: Invoke Unity Events를 이용한 입력 처리.
    * 공통점
      *	스크립트에서 OnIput과 같은 함수를 이용해서 입력을 처리한다.
      *	입력 방식에 대한 반응을 개발자가 원하는 대로 구성할 수 있다.
    *	SendMassage에 비해서 Invoke Unity Events가 가지는 이점
      *	다른 코드 없이 입력 시 여러 함수를 동시에 진행하도록 만들 수 있다.
      *	스크립트가 PlayerInput 컴포넌트와 다른 객체에 있어도 사용 가능하다.
      *	적용 타이밍을 정확하게 지정할 수 있다.
        *	Started: 버튼을 누를 때.
        *	Performed: 버튼 지속
        *	Canceled: 버튼을 땔 때.
    *	Invoke Unity Events에 비해서 SendMassage가 가지는 이점
      *	OnInput(InputValue value)로 선언되기만 하면 되서 코드가 간결하다.
      *	함수를 인스펙터를 통해서 이벤트로 지정할 필요가 없다.

  *	**CharacterManager와 Player의 역할에 대해서 고민해보세요.**
    * CharacterManager는 Player에 대한 정보를 다른 클래스에서 사용할 수 있도록 하는 연결체 역할으로, 플레이어에 대한 정보를 싱글톤으로 들고 있음으로써 다른 클래스에서 Player 객체 정보를 가지고 있을 필요 없이 CharacterManager.Instance.Player 라는 코드 하나로 Player에 대해서 접근할 수 있도록 하는 역할입니다.
    * Player는 CharacterManager와 유사하게 다른 객체에서 Player에 대한 정보를 확인하기 쉽도록 모아놓는 역할이지만, CharacterManager와는 달리 플레이어 객체에 직접 포함되어 플레이어의 상황에 따라서 달라지는 요소를 모두 적용할 수 있는 클래스입니다.
    * 정리하면, CharacterManager는 Player라는 정보에 접근할 수 있도록 만들어주는 연결 매체라면, Player는 실질적인 요소(객체)를 담고 있는 DataContainer라고 할 수 있습니다.
 
  *	**핵심 로직을 분석해보세요(Move, CameraLook, IsGrounded)**
    * Move
      * 입력값이 들어오면 그것을 CurMovementInput에 반영한 뒤, FixedUpdate에서CurMovementInput 변수를 이용해서 이동을 구현합니다.
      * 이동 방식은 Rigidbody.velocity를 입력값에 맞춰서 고정하는 방식으로, 이를 통해서 일정한 속도를 유지할 수 있도록 구성되어 있습니다.
      * 다만, 이 경우 넉백이나 물체 상호작용에 의해서 발생하는 큰 충격을 처리하기 위해서는 AddForce 이외에도 별도의 다른 알고리즘을 같이 사용해야 한다는 난점이 발생하기도 하는 방식입니다.
      * 만약 상승 혹은 하강 속도가 있다면 dir.y = rigidbody.velocity.y 라인을 통해서 그 속도를 유지하도록 만든 구성입니다.
    * CameraLook
      * Move와 마찬가지로, 입력값이 들어오면 그것을 mouseDelta에 반영한 뒤, Update에서 mouseDelta 값을 이용해서 카메라 회전을 구현합니다.
      * X축 각도(가로를 기점으로 하므로 위아래로 회전한다.)는 최대 및 최소 한계가 존재하여 ‘정면’은 계속해서 유지되도록 구성이 되어 있고, Y축 각도(세로를 기점으로 하므로 좌우로 화전한다.)는 자유롭게 움직일 수 있는 구조입니다.
    * IsGrounded
      * 일정 주기로 바닥의 0.1 위에서 바닥을 향해서 길이 0.1의 Raycast를 작동, 이 결과에 따라서 지면에 서 있는지, 공중에 떠 있는지를 판단합니다.
      * 만약에 Raycast 지점 사이에 첨단처럼 받치는 지형이 있다면 그에 대한 처리가 난감해진다는 단점이 있어, 캐릭터의 크기에 따라서 Raycast 정점의 배치와 양을 어떻게 구성할 지 조정해야할 수도 있습니다.
      * 여담으로, 같은 방식을 정면을 향해서 작동하면 ‘벽’을 감지할 수 있습니다. 이에 대한 조작을 추가하여 등반을 구현할 수도 있습니다.
 
  *	**Move와 CameraLook 함수를 각각 FixedUpate, LateUpdate에서 호출하는 이유에 대해 생각해보세요.**
    * Move는 캐릭터 객체가 이동하는 것이기 때문에 고정된 시간이 아니라면 넘실대듯이 움직이는 문제점이 발생합니다. 특히 Rigidbody의 이동 판정은 FixedUpdate와 동일한 타이밍에 발생하기 때문에 이에 대한 타이밍 문제 상으로도 FixedUpdate에서 처리하는 것이 적합합니다.
    * CameraLook은 화면을 돌리는 것으로, 화면 갱신은 Update와 동일한 타이밍에 발생하기 때문에 플레이어 조작과 맞춘 적절한 회전 각도를 나타내기 위해서는 Update에서 처리하는 것이 더욱 적합합니다.

* 확장 문제 [Commit id: 0e9a296e1f6fe19bc1a98dca7780ce66386ff45d]
  * Esc키로 열고 닫을 수 있는 게임 종료 창을 추가하였습니다.
* 개선 문제 [Commit id: e15393a0e3eb001b5b6461808b94e106445f0787]
  * transform.position을 움직여서 작동하는 전방 돌진 기능을 추가하였습니다. [버튼 할당: Shift]

 ## Q2
* 분석 문제 [Commit id: 34180c7f9e008c6f88403ead8701f94974b1de53]
  *	**별도의 UI 스크립트를 만드는 이유에 대해 객체지향적 관점에서 생각해보세요.**
    *	UI스크립트는 ‘보이는’ 부분을 담당하는 특성 상, 변화나 제거가 자주 발생합니다. 그렇기 때문에 UI 스크립트에서 실 처리를 담당하게 된다면 추후 간단한 UI 수정이나 기능 수정에도 대처해야 하는 코드의 양이 많아질 가능성이 생깁니다.
    *	특히 해당 UI는 제거하지만 그 UI에서 담당하던 기능은 필요한 경우, 잘못하여 해당 UI를 지워버린 경우에는 해당 기능을 다시 작성해야 합니다.
    *	때문에, UI는 기능 영역에 혼선이 발생하지 않는 범위 안에서 가능한 한 많은 종류로 나눠서 작성하는 것이 좋습니다.

  *	**인터페이스의 특징에 대해 정리해보고 구현된 로직을 분석해보세요.**
    * 인터페이스는 ‘공통된 특징’을 명시하기 위해서 사용하는 요소입니다.
      * 동시에 같은 인터페이스를 상속받은 클래스라면 인터페이스에서 명시된 함수에 한하여 같은 코드로 호출할 수 있게 됩니다.
      * 이러한 방식을 통해서 코드의 유연성을 크게 증진시킬 수 있습니다.
    * 본 프로그램에 작성된 interface는 IDamageable과 IInteractable의 2종류가 있습니다.
      * IDamageable: ‘피격 시 판정’을 담당하는 인터페이스입니다.
        * 피격 시 처리에 대한 함수인 TakePhysicalDamage(int) 함수 하나 만 가지고 있습니다.
      * IInteratable: 상호작용 가능한 물체에 추가되는 인터페이스입니다.
        * GetInteractPrompt 함수는 해당 객체의 내부에 존재하는 이름 및 설명을 외부 클래스에서 받아올 수 있도록 만들어주는 함수입니다.
        * OnInteract 함수는 상호작용 시 발생하는 효과에 대한 함수입니다.
 
  *	**핵심 로직을 분석해보세요. (UI 스크립트 구조, Campfire, DamageIndicator)**
    * UI스크립트 구조
      * UI를 관리하는 스크립트가 해당 기능의 스크립트에 포함되어 있거나, 해당 기능의 UI 스크립트에 기능 구현이 포함되어 있는 경향이 있습니다.
      * 대표적으로 인벤토리는 UIInventory 스크립트에서 아이템 습득 및 사용, 버리기 등의 기능이 구현되어 있고, ItemSlot 스크립트에서 해당 슬롯의 아이템 정보를 지니고 있으면서도 아이템 슬롯에 따른 UI 변동 함수를 지니고 있는 구성입니다.
      * 반면에, 각 기능 별로 확실히 나뉘어져 있어, 기능 상의 의존관계 이외에는 별다른 의존관계가 발생하지는 않고 있습니다.
    * Campfire
      * Collider의 trigger 범위 안에 들어온 객체를 리스트에 저장하고 있다가 일정 주기로 저장된 객체들에게 피해를 주는 방식입니다.
      * Trigger 범위에서 나갈 경우 해당 정보를 리스트에서 빼서 더 이상 campfire에 의한 피해를 입지 않도록 조치합니다.
      * 데미지를 주는 방식은 InvokeRepeating을 이용한 반복 유발으로 구성되어 있습니다.
    * DamageIndicator
      * 플레이어 캐릭터가 공격을 받으면 화면이 빨개졌다가 되돌아오는 구조입니다.
      * Coroutine을 이용해서 시간 경과에 따른 변화를 구현하였습니다.
      * Condition의 onTakeDamage 이벤트를 통해서 피격 관련 기능들을 한 번에 불러오는 구조입니다.

* 확장 문제 [Commit id: 325b618d108092f13d7802eaa7045137f1625bf8]
  * 새로운 스텟을 만들고, 이를 UI를 통해 관리 해 보세요.
    * Dash 스탯을 만든 뒤, Q1-개선 문제에서 제작한 대쉬 기능에서 해당 자원을 소비하도록 구성하였습니다.
  * 새로운 장애물을 만들어서 캐릭터에게 n초 마다 데미지를 입혀 보세요.
    * 기존 CampFire 컴포넌트를 활용하여 1.5초 마다 큰 피해를 입히는 빨간 발판을 추가하였습니다.
* 개선 문제 [Commit id: 68549bad8e39768c162581e962a812c633c1a37d]
  * 새로운 아이템을 통해 체력을 회복시켜주는 Heal 효과를 만들어보세요.
    * 해당 문제의 해답이 이미 본 프로젝트에 존재했으나, Q2가 숙련 4~6강을 기준으로 하기 때문에, IInteractable 인터페이스와 추가로 IHeal이라는 인터페이스를 정의하여 상속받은 HealObj 클래스를 추가하였습니다.
    * 이 HealObj 컴포넌트를 지니고 있는 객체와 상호작용할 경우 체력이 대량으로 회복되도록 구성하였습니다.
  * CampFire 클래스의 DealDamage 호출방식을 InvokeRepeating에서 Coroutine으로 바꿔보세요.
    * DealDamage() 함수를 지정된 시간 텀으로 반복하는 코루틴을 제작한 뒤, Start에서 StartCoroutine()으로 작동 시키는 방식으로 수정하였습니다.
