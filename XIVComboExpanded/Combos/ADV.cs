﻿using Lumina.Excel.GeneratedSheets2;

namespace XIVComboExpandedPlugin.Combos;

internal static class ADV
{
    public const byte ClassID = 0;
    public const byte JobID = 0;

    public const uint
        Provoke = 7533,
        Shirk = 7537,
        HeadGraze = 7551,
        Peloton = 7557,
        Swiftcast = 7561,
        LucidDreaming = 7562,
        AngelWhisper = 18317,
        VariantRaise2 = 29734;

    public static class Buffs
    {
        public const ushort
            Peloton = 1199,
            Medicated = 49;
    }

    public static class Debuffs
    {
        public const ushort
            Placeholder = 0;
    }

    public static class Levels
    {
        public const byte
            Swiftcast = 18,
            VariantRaise2 = 90;
    }
}

internal class SwiftRaiseFeature : CustomCombo
{
    protected internal override CustomComboPreset Preset => CustomComboPreset.AdvSwiftcastFeature;

    protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
    {
        if ((actionID == AST.Ascend && level >= AST.Levels.Ascend) ||
            (actionID == SCH.Resurrection && level >= SCH.Levels.Resurrection) ||
            (actionID == SGE.Egeiro && level >= SGE.Levels.Egeiro) ||
            (actionID == WHM.Raise && level >= WHM.Levels.Raise))
        {
            if (level >= ADV.Levels.Swiftcast && IsCooldownUsable(ADV.Swiftcast))
                return ADV.Swiftcast;
        }

        if (actionID == RDM.Verraise && level >= RDM.Levels.Verraise && !HasEffect(RDM.Buffs.Dualcast))
        {
            if (IsEnabled(CustomComboPreset.AdvVerRaiseToVerCureFeature))
            {
                if (level >= RDM.Levels.Vercure)
                    return RDM.Vercure;
            }
            else if (!IsEnabled(CustomComboPreset.AdvDisableVerRaiseFeature))
            {
                if (level >= ADV.Levels.Swiftcast && IsCooldownUsable(ADV.Swiftcast))
                    return ADV.Swiftcast;
            }
        }

        return actionID;
    }
}

internal class VariantRaiseFeature : CustomCombo
{
    protected internal override CustomComboPreset Preset => CustomComboPreset.AdvVariantRaiseFeature;

    protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
    {
        if ((actionID == AST.Ascend && level >= AST.Levels.Ascend) ||
            (actionID == SCH.Resurrection && level >= SCH.Levels.Resurrection) ||
            (actionID == SGE.Egeiro && level >= SGE.Levels.Egeiro) ||
            (actionID == WHM.Raise && level >= WHM.Levels.Raise) ||
            (actionID == RDM.Verraise && level >= RDM.Levels.Verraise && !HasEffect(RDM.Buffs.Dualcast)) ||
            (actionID == BLU.AngelWhisper && level >= BLU.Levels.AngelWhisper))
        {
            // Per Splatoon:
            // 1069: solo
            // 1075: group
            // 1076: savage
            if (level >= ADV.Levels.VariantRaise2 && CurrentTerritory == 1075u)
                return ADV.VariantRaise2;
        }

        return actionID;
    }
}

internal class StanceProvokeFeature : CustomCombo
{
    protected internal override CustomComboPreset Preset => CustomComboPreset.AdvStanceProvokeFeature;

    protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
    {
        if (actionID == ADV.Provoke)
        {
            var job = LocalPlayer?.ClassJob.Id;

            if (!HasEffect(PLD.Buffs.IronWill)
                && !HasEffect(WAR.Buffs.Defiance)
                && !HasEffect(DRK.Buffs.Grit)
                && !HasEffect(GNB.Buffs.RoyalGuard))
            {
                if (job == PLD.JobID && level >= PLD.Levels.IronWill)
                    return PLD.IronWill;
                if (job == WAR.JobID && level >= WAR.Levels.Defiance)
                    return WAR.Defiance;
                if (job == DRK.JobID && level >= DRK.Levels.Grit)
                    return DRK.Grit;
                if (job == GNB.JobID && level >= GNB.Levels.RoyalGuard)
                    return GNB.RoyalGuard;
            }

            if (IsEnabled(CustomComboPreset.AdvStanceBackProvokeFeature) && !IsCooldownUsable(ADV.Provoke))
            {
                if (job == PLD.JobID && level >= PLD.Levels.IronWill)
                    return PLD.IronWillRemoval;
                if (job == WAR.JobID && level >= WAR.Levels.Defiance)
                    return WAR.DefianceRemoval;
                if (job == DRK.JobID && level >= DRK.Levels.Grit)
                    return DRK.GritRemoval;
                if (job == GNB.JobID && level >= GNB.Levels.RoyalGuard)
                    return GNB.RoyalGuardRemoval;
            }
        }

        return actionID;
    }
}

internal class ShirkStanceFeature : CustomCombo
{
    protected internal override CustomComboPreset Preset => CustomComboPreset.AdvShirkStanceFeature;

    protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
    {
        if (actionID == ADV.Shirk && !IsCooldownUsable(ADV.Shirk))
        {
            var job = LocalPlayer?.ClassJob.Id;

            if (HasEffect(PLD.Buffs.IronWill)
                || HasEffect(WAR.Buffs.Defiance)
                || HasEffect(DRK.Buffs.Grit)
                || HasEffect(GNB.Buffs.RoyalGuard))
            {
                if (job == PLD.JobID && level >= PLD.Levels.IronWill)
                    return PLD.IronWillRemoval;
                if (job == WAR.JobID && level >= WAR.Levels.Defiance)
                    return WAR.DefianceRemoval;
                if (job == DRK.JobID && level >= DRK.Levels.Grit)
                    return DRK.GritRemoval;
                if (job == GNB.JobID && level >= GNB.Levels.RoyalGuard)
                    return GNB.RoyalGuardRemoval;
            }
        }

        return actionID;
    }
}

internal class SprintPelotonFeature : CustomCombo
{
    protected internal override CustomComboPreset Preset => CustomComboPreset.AdvPelotonSTFeature;

    protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
    {
        if (actionID == ADV.HeadGraze && (LocalPlayer?.ClassJob.Id == BRD.ClassID || LocalPlayer?.ClassJob.Id == BRD.JobID || LocalPlayer?.ClassJob.Id == MCH.JobID || LocalPlayer?.ClassJob.Id == DNC.JobID))
        {
            if (!HasEffect(ADV.Buffs.Peloton) && OutOfCombat())
            {
                return ADV.Peloton;
            }
        }

        return actionID;
    }
}


internal class AdvAutoLucidDreamingFeature : CustomCombo
{
    protected internal override CustomComboPreset Preset => CustomComboPreset.AdvAutoLucidDreamingFeature;

    protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
    {
        if (LocalPlayer?.CurrentMp < 5000)
        {
            if(LocalPlayer?.ClassJob.Id == WHM.ClassID
            || LocalPlayer?.ClassJob.Id == BLM.ClassID
            || LocalPlayer?.ClassJob.Id == WHM.JobID
            || LocalPlayer?.ClassJob.Id == BLM.JobID
            || LocalPlayer?.ClassJob.Id == SMN.ClassID
            || LocalPlayer?.ClassJob.Id == SMN.JobID
            || LocalPlayer?.ClassJob.Id == SCH.JobID
            || LocalPlayer?.ClassJob.Id == AST.JobID
            || LocalPlayer?.ClassJob.Id == RDM.JobID
            || LocalPlayer?.ClassJob.Id == BLU.JobID
            || LocalPlayer?.ClassJob.Id == SGE.JobID
            || LocalPlayer?.ClassJob.Id == PCT.JobID)
            {
                return ADV.LucidDreaming;
            }
        }

        return actionID;
    }
}
