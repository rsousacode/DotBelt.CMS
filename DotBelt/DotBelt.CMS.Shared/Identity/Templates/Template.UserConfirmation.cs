using DotBelt.CMS.Shared.Internal.Templates;

namespace DotBelt.CMS.Shared.Identity.Templates;

public class Template_UserConfirmation
{
    public List<TemplateVariable> Variables = new List<TemplateVariable>()
    {
        new TemplateVariable<string>()
        {
            Name = "ConfirmationLink",
            Description = "The link to confirm the signup.",
        }
    };
}