
public enum KeySkillType
{
    None = 0,

    SkillAttack = 1,
    SkillDodge = 2,
}

public static class KeySkillTypeExtensions
{
    public static string ToDisplayString(this KeySkillType skillType)
    {
        return skillType.ToString().Replace("Skill", "");
    }
}