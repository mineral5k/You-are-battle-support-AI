# You Are Battle Support AI

> 전투가 이미 진행된 뒤 기동한 **전투 지원 AI**가 과거 턴에 개입해 전투 결과를 바꾸는 Unity 기반 턴제 전투 프로젝트입니다.

당신은 뛰어난 성능의 전투 지원 AI프로그램 입니다.

하지만 그런 당신에겐, 기동에 5턴이 필요하다는 치명적인 단점이 있습니다.

적이 강하다면, 당신이 눈을 뜬 6턴에 이미 전투가 불리해져 있을 수도 있겠죠.

그렇지만 걱정하지 마세요! 당신은 과거에 개입해서 현재의 불리한 상황을 타파할 수 있습니다.

당신이 눈을 뜬 6턴의 상황을 통해 과거에 어떤 일이 있어났는지를 추측하세요. 스킬의 쿨타임, 상태이상의 지속 시간, 남은 체력.. 모든게 단서가 될 수 있습니다.

그 추측을 통해서 과거에 적절한 개입을 해 전투를 승리로 이끄세요!

---

## 시연 영상
https://youtu.be/6zPypDp69dU

## Core Features

### 1. Blind Turn 기록 및 과거 개변

- Blind Turn 동안 사용 가능한 스킬 중 행동을 선택하고, 아군/적군이 사용한 스킬을 기록합니다.
- 과거 개변 시 HP, 마나, 상태효과, 쿨다운을 초기 상태로 되돌린 뒤 기록된 스킬을 사용해 1~5턴을 다시 진행합니다.
- 재생 시 `SelectedAction`을 현재 `UnitState` 기준으로 다시 생성하여 마나 소비량과 최종 위력을 다시 계산합니다.
- 현재 구현에서는 선택한 턴에 **공격 위력 증가** 또는 **방어도 증가** 개입을 배치할 수 있습니다.

### 2. 턴제 전투 처리

- 스킬을 `Attack`, `Defense`, `Charge` 세 카테고리로 구분합니다.
- 비공격 행동을 먼저 처리한 뒤 공격 행동을 처리합니다.
- 공격 vs 공격에서는 양측의 최종 위력을 비교하고, 더 높은 쪽의 스킬만 실제 효과를 실행합니다.
- 동일 위력일 경우 양쪽 공격 모두 실행하지 않습니다.
- 고정 마나 / 가변 마나 스킬과 쿨다운을 지원합니다.

### 3. Skill / Status Effect 시스템

- `SkillData`를 기반으로 개별 스킬 클래스를 구성하고, 마나 비용과 위력 계산을 공통화했습니다.
- `SelectedAction`에서 현재 마나와 공격력 증가 효과를 반영해 실제 소비 마나와 최종 위력을 계산합니다.
- `StatusEffect` 기반으로 버프/디버프를 관리하며 동일 ID의 효과를 병합할 수 있도록 구현했습니다.
- 공격력 증가, 마나 충전, 화상, 독, 조건부 독 부여 등의 효과를 구현했습니다.

### 4. 전투 계산과 연출 흐름

- `CombatCommand`를 통해 전투 진행을 `RevealSkill`, `RevealClash`, `ExecuteSkill` 단계로 구성합니다.
- `BattlePresenter`가 Command를 순서대로 재생하며 스킬 공개, 위력 비교, 공격/방어 애니메이션과 실제 스킬 실행 시점을 제어합니다.
- DOTween과 Animator를 사용해 스킬 패널 등장, 충돌 연출, 패널 파괴, 결과 화면 등의 UI/전투 연출을 구현했습니다.

### 5. 상태 및 스킬 UI

- `UnitState.OnStatusChanged` 이벤트를 이용해 HP, 마나, 방어도, 스킬 쿨다운, 상태효과 UI를 갱신합니다.
- 최대 10개의 마나 UI를 현재 마나량에 따라 활성/비활성 색상으로 구분합니다.
- 스킬 Hover 시 툴팁을 표시하고, 스킬 사용에 필요한 마나를 UI에서 강조합니다.
- 스킬과 연관된 상태효과 정보를 별도 패널로 표시합니다.
- 현재 적용 중인 상태효과를 아이콘과 스택 수치로 표시하고 Hover 시 상세 설명을 확인할 수 있습니다.

### 6. Damage Popup Pooling

- 피해 숫자 UI를 매번 생성/삭제하지 않고 `Queue` 기반 오브젝트 풀에서 재사용합니다.
- 실제 HP 피해와 방어 성공(`Blocked`)을 구분해 표시합니다.


---

## Tech Stack

| Category | Technology |
| --- | --- |
| Engine | Unity |
| Language | C# |
| Tween | DOTween |


---

## Main Structure

```text
Scripts/
├─ Combat/
│  ├─ BattleManager.cs
│  ├─ TurnProcesser.cs
│  ├─ CombatResolver.cs
│  ├─ UnitState.cs
│  ├─ Skill/
│  │  ├─ SkillData.cs
│  │  ├─ SelectedAction.cs
│  │  └─ PlayerSkill/
│  ├─ Buff_Debuff/
│  │  ├─ StatusEffect.cs
│  │  ├─ Buff/
│  │  └─ Debuff/
│  └─ openBattle/
│     ├─ CombatCommand.cs
│     └─ BattlePresenter.cs
├─ UI/
│  ├─ HPBarUI.cs
│  ├─ SkillButtonUI.cs
│  ├─ TooltipPanel.cs
│  ├─ AlterPastPanel.cs
│  ├─ SkillRevealPanel.cs
│  └─ DamagePopUpPool.cs
└─ Sound/
   ├─ AudioManager.cs
   └─ CharacterSoundPlayer.cs
```

---

## Core Flow

```text
Blind Turn 1~5 자동 진행
        ↓
양측이 사용한 SkillData 기록
        ↓
과거 개변 대상 효과 / 턴 선택
        ↓
전투 상태 초기화
        ↓
기록된 SkillData로 SelectedAction 재생성
        ↓
선택한 턴에 개변 효과 적용
        ↓
CombatCommand 생성
        ↓
BattlePresenter가 전투 연출과 효과 실행
        ↓
개변 이후 전투 계속 진행
```

Blind Turn의 행동 기록은 유지하면서도, **과거 개입으로 달라진 상태를 기준으로 이후 전투 결과를 다시 계산**하도록 구현했습니다.
