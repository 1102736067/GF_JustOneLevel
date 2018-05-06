using GameFramework;
using GameFramework.Fsm;
using UnityEngine;

public class HeroAtkState : HeroListenAttackState {
    /// <summary>
    /// 有限状态机状态初始化时调用。
    /// </summary>
    /// <param name="fsm">有限状态机引用。</param>
    protected override void OnInit (IFsm<HeroLogic> fsm) { 
        base.OnInit(fsm);
    }

    /// <summary>
    /// 有限状态机状态进入时调用。
    /// </summary>
    /// <param name="fsm">有限状态机引用。</param>
    protected override void OnEnter (IFsm<HeroLogic> fsm) {
        base.OnEnter(fsm);

        Log.Info("HeroAtkState OnEnter");

        fsm.Owner.ChangeAnimation (HeroAnimationState.atk);

        /* 判断是否有怪物进入攻击范围 */
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
        foreach(GameObject obj in monsters) {
            TargetableObject monster = obj.GetComponent<TargetableObject>();

            if (monster.IsDead == false) {
                float distance = AIUtility.GetDistance(fsm.Owner, monster);

                if (fsm.Owner.CheckInAtkRange(distance)) {
                    fsm.Owner.PerformAttack(monster);
                }
            }
        }
    }

    /// <summary>
    /// 有限状态机状态轮询时调用。
    /// </summary>
    /// <param name="fsm">有限状态机引用。</param>
    /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
    /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
    protected override void OnUpdate (IFsm<HeroLogic> fsm, float elapseSeconds, float realElapseSeconds) {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);

        ChangeState<HeroAtkCDState>(fsm);
    }

    /// <summary>
    /// 有限状态机状态离开时调用。
    /// </summary>
    /// <param name="fsm">有限状态机引用。</param>
    /// <param name="isShutdown">是否是关闭有限状态机时触发。</param>
    protected override void OnLeave (IFsm<HeroLogic> fsm, bool isShutdown) {
        base.OnLeave(fsm, isShutdown);
    }

    /// <summary>
    /// 有限状态机状态销毁时调用。
    /// </summary>
    /// <param name="fsm">有限状态机引用。</param>
    protected override void OnDestroy (IFsm<HeroLogic> fsm) {
        base.OnDestroy (fsm);
    }
}