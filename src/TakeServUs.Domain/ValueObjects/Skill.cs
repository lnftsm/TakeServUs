namespace TakeServUs.Domain.ValueObjects;

using TakeServUs.Domain.Enums;
public class Skill
{
  public string Name { get; set; }
  public SkillLevel Level { get; set; }

  public Skill(string name, SkillLevel level)
  {
    Name = name;
    Level = level;
  }
}
