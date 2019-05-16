using System.Collections.Generic;
using System.Linq;
using System.Text;

public class RuleTransformer
{
    public static string TransfromString(string input, List<Rule> setOfRules)
    {
        StringBuilder builder = new StringBuilder(input);
        StringBuilder output = new StringBuilder();
        for (int i = 0; i < builder.Length; i++)
        {
            var rule = getRequiredRule(builder[i].ToString(), setOfRules);
            if (rule != null)
            {
                output.Append(rule.output);
            }
            else
            {
                output.Append(builder[i].ToString());
            }
        }
        return output.ToString();
    }

    private static Rule getRequiredRule(string input, List<Rule> rules)
    {   if (rules.Where(e => e.input.Equals(input)).Count() > 0)
        {
            return rules.Where(e => e.input.Equals(input)).First();
        }
        else { return null; }
    }
}
