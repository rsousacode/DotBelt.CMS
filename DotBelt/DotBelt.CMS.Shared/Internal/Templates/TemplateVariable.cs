namespace DotBelt.CMS.Shared.Internal.Templates;

public class TemplateVariable 
{
    public virtual string? Name { get; set; }
    public virtual string? Description { get; set; }
    protected virtual object? ValueRaw { get; set; }

}

public class TemplateVariable<T> : TemplateVariable
{
    public T? Value
    {
        get => (T?)ValueRaw is null ? default : (T)ValueRaw;
        set => ValueRaw = value;
    }
}